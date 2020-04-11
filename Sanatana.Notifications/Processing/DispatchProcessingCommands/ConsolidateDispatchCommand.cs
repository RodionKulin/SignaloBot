﻿using Sanatana.Notifications.DAL.Entities;
using Sanatana.Notifications.DAL.Interfaces;
using Sanatana.Notifications.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sanatana.Notifications.Processing.DispatchProcessingCommands
{
    public class ConsolidateDispatchCommand<TKey> : IDispatchProcessingCommand<TKey>
        where TKey: struct
    {
        //fields
        protected ISignalDispatchQueries<TKey> _signalDispatchQueries;


        //properties
        public int Order { get; set; }


        //ctor
        public ConsolidateDispatchCommand(ISignalDispatchQueries<TKey> signalDispatchQueries)
        {
            _signalDispatchQueries = signalDispatchQueries;
        }


        //methods
        public virtual async Task<bool> Execute(SignalWrapper<SignalDispatch<TKey>> item)
        {
            if(item.Signal.Consolidate == false ||
                item.Signal.CategoryId == null ||
                item.Signal.ReceiverSubscriberId == null)
            {
                return true;
            }

            var categories = new List<(int deliveryType, int category)>();
            categories.Add((item.Signal.DeliveryType, item.Signal.CategoryId.Value));

            List<SignalDispatch<TKey>> sameCategoryScheduledDispatches = await _signalDispatchQueries
                .SelectScheduled(item.Signal.ReceiverSubscriberId.Value, categories)
                .ConfigureAwait(false);

            return true;
        }
    }
}