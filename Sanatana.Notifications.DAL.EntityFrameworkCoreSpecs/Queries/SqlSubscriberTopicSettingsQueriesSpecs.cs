﻿using NUnit.Framework;
using Sanatana.Notifications.DAL.Entities;
using Sanatana.Notifications.DAL.EntityFrameworkCore;
using Sanatana.Notifications.DAL.EntityFrameworkCore.Context;
using Sanatana.Notifications.DAL.EntityFrameworkCoreSpecs.TestTools.Interfaces;
using SpecsFor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Should;
using SpecsFor.ShouldExtensions;

namespace Sanatana.Notifications.DAL.EntityFrameworkCoreSpecs.Queries
{
    public class SqlSubscriberTopicSettingsQueriesSpecs
    {
        [TestFixture]
        public class when_topic_settings_insert_using_ef
           : SpecsFor<SqlSubscriberTopicSettingsQueries>, INeedDbContext
        {
            private List<SubscriberTopicSettings<long>> _insertedData;
            private long _subscriberId;
            public SenderDbContext DbContext { get; set; }
            

            protected override void When()
            {
                _subscriberId = 12;
                _insertedData = new List<SubscriberTopicSettings<long>>
                {
                    new SubscriberTopicSettings<long>()
                    {
                        TopicId = DateTime.UtcNow.ToString(),
                        CategoryId = 1,
                        DeliveryType = 1,
                        IsEnabled = true,
                        AddDateUtc = DateTime.UtcNow,
                        SubscriberId = _subscriberId
                    }
                };

                SUT.Insert(_insertedData).Wait();
            }
            
            [Test]
            public void then_topic_settings_inserted_are_found_using_ef()
            {
                List<SubscriberTopicSettingsLong> actual = DbContext.SubscriberTopicSettings
                   .Where(x => x.SubscriberId == _subscriberId)
                   .OrderBy(x => x.SubscriberTopicSettingsId)
                   .ToList();

                actual.ShouldNotBeEmpty();
                actual.Count.ShouldEqual(_insertedData.Count);

                for (int i = 0; i < _insertedData.Count; i++)
                {
                    SubscriberTopicSettingsLong actualItem = actual[i];
                    SubscriberTopicSettings<long> expectedItem = _insertedData[i];

                    expectedItem.SubscriberTopicSettingsId = actualItem.SubscriberTopicSettingsId;
                    actualItem.ShouldLookLike(expectedItem);
                }
            }

        }


        [TestFixture]
        public class when_topic_settings_upnsert_isenabled_using_ef
           : SpecsFor<SqlSubscriberTopicSettingsQueries>, INeedDbContext
        {
            private List<SubscriberTopicSettings<long>> _insertedData;
            private long _subscriberId;
            public SenderDbContext DbContext { get; set; }


            protected override void When()
            {
                _subscriberId = 13;
                _insertedData = new List<SubscriberTopicSettings<long>>
                {
                    new SubscriberTopicSettings<long>()
                    {
                        TopicId = DateTime.UtcNow.ToString(),
                        CategoryId = 1,
                        DeliveryType = 4,
                        IsEnabled = true,
                        AddDateUtc = DateTime.UtcNow,
                        SubscriberId = _subscriberId
                    },
                    new SubscriberTopicSettings<long>()
                    {
                        TopicId = DateTime.UtcNow.ToString(),
                        CategoryId = 1,
                        DeliveryType = 2,
                        IsEnabled = true,
                        AddDateUtc = DateTime.UtcNow,
                        SubscriberId = _subscriberId
                    }
                };

                SUT.UpsertIsEnabled(_insertedData).Wait();
            }

            [Test]
            public void then_topic_settings_upserted_by_isenabled_property_are_found_using_ef()
            {
                List<SubscriberTopicSettingsLong> actual = DbContext.SubscriberTopicSettings
                   .Where(x => x.SubscriberId == _subscriberId)
                   .OrderBy(x => x.SubscriberTopicSettingsId)
                   .ToList();

                actual.ShouldNotBeEmpty();
                actual.Count.ShouldEqual(_insertedData.Count);

                for (int i = 0; i < _insertedData.Count; i++)
                {
                    SubscriberTopicSettingsLong actualItem = actual[i];
                    SubscriberTopicSettings<long> expectedItem = _insertedData[i];

                    expectedItem.SubscriberTopicSettingsId = actualItem.SubscriberTopicSettingsId;
                    actualItem.ShouldLookLike(expectedItem);
                }
            }

        }
    }
}
