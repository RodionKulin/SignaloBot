﻿using Sanatana.Notifications.DAL;
using Sanatana.Notifications.Composing;
using Sanatana.Notifications.Monitoring;
using Sanatana.Notifications.Queues;
using Sanatana.Notifications.Dispatching;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanatana.Notifications.Composing.Templates;
using Sanatana.Notifications.DAL.Interfaces;
using Sanatana.Notifications.DAL.Entities;
using Microsoft.Extensions.Logging;
using Sanatana.Timers.Switchables;
using Sanatana.Notifications.Sender;

namespace Sanatana.Notifications.Processing
{
    public class CompositionProcessor<TKey> : ProcessorBase<TKey>, IRegularJob
        where TKey : struct
    {
        //fields
        protected SenderState<TKey> _hubState;
        protected IMonitor<TKey> _eventSink;
        protected IEventQueue<TKey> _eventQueue;
        protected IDispatchQueue<TKey> _dispatchQueue;
        protected ICompositionHandlerRegistry<TKey> _handlerRegistry;
        protected IComposerSettingsQueries<TKey> _composerSettingsQueries;


        //init
        public CompositionProcessor(SenderState<TKey> hubState, IMonitor<TKey> eventSink
            , ILogger<CompositionProcessor<TKey>> logger, SenderSettings senderSettings
            , IEventQueue<TKey> eventQueue, IDispatchQueue<TKey> dispatchQueue
            , ICompositionHandlerRegistry<TKey> handlerRegistry, IComposerSettingsQueries<TKey> composerSettingsQueries)
            : base(logger)
        {
            _hubState = hubState;
            _eventSink = eventSink;
            _eventQueue = eventQueue;
            _dispatchQueue = dispatchQueue;
            _handlerRegistry = handlerRegistry;
            _composerSettingsQueries = composerSettingsQueries;

            MaxParallelItems = senderSettings.MaxParallelEventsProcessed;
        }


        //IRegularJob methods
        public virtual void Tick()
        {
            if (CanContinue())
            {
                DequeueAll();
            }

            return;
        }
        public virtual void Flush()
        {
        }




        //processing methods
        protected virtual void DequeueAll()
        {
            while (CanContinue())
            {
                SignalWrapper<SignalEvent<TKey>> item = _eventQueue.DequeueNext();
                if (item == null)
                {
                    break;
                }

                StartNextTask(() => ProcessSignal(item, _eventQueue));
            }

            WaitForCompletion();
        }

        protected virtual bool CanContinue()
        {
            bool isQueueEmpty = _eventQueue.CountQueueItems() == 0;

            int actualDispatches = _dispatchQueue.CountQueueItems();
            int maxDispatches = _dispatchQueue.PersistBeginOnItemsCount;
            bool isDispatchQueueFull = actualDispatches >= maxDispatches;

            return !isQueueEmpty && !isDispatchQueueFull && _hubState.State == SwitchState.Started;
        }

        protected void ProcessSignal(SignalWrapper<SignalEvent<TKey>> item, IEventQueue<TKey> eventQueue)
        {
            try
            {
                if (item.Signal.ComposerSettingsId == null)
                {
                    SplitEvent(item, eventQueue);
                }
                else
                {
                    ComposeAndApplyResult(item, eventQueue);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, null);
                //increment fail counter and don't let same event to repeat exceptions multiple times 
                eventQueue.ApplyResult(item, ProcessingResult.Fail);
            }
        }

        protected virtual void SplitEvent(SignalWrapper<SignalEvent<TKey>> item, IEventQueue<TKey> eventQueue)
        {
            List<ComposerSettings<TKey>> composerSettings = _composerSettingsQueries
                .Select(item.Signal.CategoryId).Result;

            if (composerSettings.Count == 0)
            {
                eventQueue.ApplyResult(item, ProcessingResult.NoHandlerFound);
            }
            else if (composerSettings.Count == 1)
            {
                item.Signal.ComposerSettingsId = composerSettings.First().ComposerSettingsId;
                item.IsUpdated = true;

                ComposeAndApplyResult(item, eventQueue);
            }
            else if (composerSettings.Count > 1)
            {
                var splitedEvents = new List<SignalEvent<TKey>>();
                foreach (ComposerSettings<TKey> settings in composerSettings)
                {
                    SignalEvent<TKey> clone = item.Signal.CreateClone();
                    clone.SignalEventId = default(TKey);
                    clone.ComposerSettingsId = settings.ComposerSettingsId;
                    splitedEvents.Add(clone);
                }

                eventQueue.ApplyResult(item, ProcessingResult.Success);
                eventQueue.Append(splitedEvents, false);
            }
        }

        protected virtual void ComposeAndApplyResult(SignalWrapper<SignalEvent<TKey>> item, IEventQueue<TKey> eventQueue)
        {
            Stopwatch composeTimer = Stopwatch.StartNew();

            ComposeResult<SignalDispatch<TKey>> composeResult = ComposeDispatches(item);
            eventQueue.ApplyResult(item, composeResult.Result);

            TimeSpan composeDuration = composeTimer.Elapsed;
            _eventSink.DispatchesComposed(
                item.Signal, composeDuration, composeResult.Result, composeResult.Items);
        }

        protected virtual ComposeResult<SignalDispatch<TKey>> ComposeDispatches(SignalWrapper<SignalEvent<TKey>> item)
        {
            ComposerSettings<TKey> composerSettings = _composerSettingsQueries
                .Select(item.Signal.ComposerSettingsId.Value).Result;
            if (composerSettings == null)
            {
                return ComposeResult<SignalDispatch<TKey>>.FromResult(ProcessingResult.NoHandlerFound);
            }

            ICompositionHandler<TKey> compositionHandler =
                _handlerRegistry.MatchHandler(composerSettings.CompositionHandlerId);
            if(compositionHandler == null)
            {
                return ComposeResult<SignalDispatch<TKey>>.FromResult(ProcessingResult.NoHandlerFound);
            }

            ComposeResult<SignalDispatch<TKey>> composeResult = 
                compositionHandler.ProcessEvent(composerSettings, item.Signal);
            if (composeResult.Result == ProcessingResult.Success)
            {
                item.IsUpdated = true;

                _dispatchQueue.Append(composeResult.Items, false);

                if (composeResult.IsFinished == false)
                {
                    composeResult.Result = ProcessingResult.Repeat;
                }
            }

            return composeResult;
        }

    }
}