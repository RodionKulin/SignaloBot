﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sanatana.Notifications.DAL.Entities;

namespace Sanatana.Notifications.DAL.Interfaces
{
    public interface ISignalDispatchQueries<TKey> : ISignalQueries<SignalDispatch<TKey>>
        where TKey : struct
    {
        Task<List<SignalDispatch<TKey>>> SelectConsolidated(int pageSize, List<TKey> subscriberIds, List<(int deliveryType, int category)> categories, DateTime createdBefore, DateTime? createdAfter = null);
        Task<List<SignalDispatch<TKey>>> Select(int count, List<int> deliveryTypes, int maxFailedAttempts);
        Task DeleteConsolidated(List<SignalDispatch<TKey>> item);
    }
}