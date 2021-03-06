﻿using MongoDB.Bson;
using Sanatana.DataGenerator;
using Sanatana.DataGenerator.Entities;
using Sanatana.DataGenerator.Generators;
using Sanatana.DataGenerator.Internals;
using Sanatana.Notifications.DAL.Entities;
using Sanatana.Notifications.DAL.MongoDbSpecs.SpecObjects;
using Sanatana.Notifications.DAL.MongoDbSpecs.TestTools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sanatana.Notifications.DAL.MongoDbSpecs.TestTools.DataGeneration
{
    public class SubscribersLoadTestData : SubscribersData
    {
        //generators
        protected override List<SubscriberTopicSettings<ObjectId>> GenerateTopics(GeneratorContext genContext,
            SubscriberCategorySettings<ObjectId> category, SubscriberWithMissingData subscriber)
        {
            EntityContext subscriberContext = genContext.EntityContexts[typeof(SubscriberWithMissingData)];
            long subscriberNumber = subscriberContext.EntityProgress.CurrentCount;
            int subscribersPerTopic = 1000;
            long subscribersTopicGroup = subscriberNumber / subscribersPerTopic;

            string[] topics = new[]
            {
                "3011" + subscribersTopicGroup,
                "3012" + subscribersTopicGroup
            };

            var categoryTopics = topics
                .Select((topic, i) => new SubscriberTopicSettings<ObjectId>
                {
                    SubscriberTopicSettingsId = ObjectId.GenerateNewId(),
                    SubscriberId = category.SubscriberId,
                    DeliveryType = category.DeliveryType,
                    CategoryId = category.CategoryId,
                    TopicId = topic,
                    IsEnabled = subscriber.HasTopicsSettingsEnabled,
                    AddDateUtc = DateTime.UtcNow,
                    LastSendDateUtc = subscriber.HasTopicLastSendDate
                        ? (DateTime?)DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(5))
                        : null
                })
                .ToList();

            subscriber.Topics = subscriber.Topics ?? new List<SubscriberTopicSettings<ObjectId>>();
            subscriber.Topics.AddRange(categoryTopics);

            return categoryTopics;
        }
    }
}
