using System;
using DotNetStandard.Tests.Models;
using DotNetStandard.Vent;
using NUnit.Framework;

namespace DotNetStandard.Tests
{
    public class EventAggregatorTests
    {
        private EventAggregator _vent;
        private Consumer _consumer;
        private Producer _producer1;
        private Producer _producer2;

        [SetUp]
        public void Initialize()
        {
            _vent = EventAggregator.Instance;
            _consumer = new Consumer();
            _producer1 = new Producer("consume");
            _producer2 = new Producer("reconsume");
        }

        [Test]
        public void TestCanSubscribeEvent()
        {
            Assert.DoesNotThrow(() => _vent.Subscribe(new EventTest("eventname"), _consumer.React));
        }

        [Test]
        public void TestCanUnsubscribeEvent()
        {
            Assert.DoesNotThrow(() => _vent.Subscribe(new EventTest("eventname"), _consumer.React));
            Assert.DoesNotThrow(() => _vent.Unsubscribe(new EventTest("eventname"), _consumer.React));
        }

        [Test]
        public void TestCanUnsubscribeEventDoesNotExist()
        {
            Assert.DoesNotThrow(() => _vent.Subscribe(new EventTest("eventname"), _consumer.React));
            Assert.DoesNotThrow(() => _vent.Unsubscribe(new EventTest("doesnotexist"), _consumer.React));
        }

        [Test]
        public void TestCanTriggerNoopEvent()
        {
            Assert.DoesNotThrow(() => _vent.Trigger(new EventTest("doesnotexist"), new dynamic[]{0}));
        }

        [Test]
        public void TestSubscribedConsumerCanReactToProducerTrigger()
        {
            _producer1.TriggerEvent();
            Assert.AreEqual(1, _consumer.Counter);
            Assert.AreEqual("stringParam", _consumer.Param[0]);
        }

        [Test]
        public void TestSubscribedConsumerCanReactToMulipleProducerTriggers()
        {
            _producer1.TriggerEvent();
            Assert.AreEqual(1, _consumer.Counter);
            Assert.AreEqual("stringParam", _consumer.Param[0]);
            _producer1.TriggerEvent();
            Assert.AreEqual(2, _consumer.Counter);
            Assert.AreEqual("stringParam", _consumer.Param[0]);
            _producer1.TriggerEvent();
            Assert.AreEqual(3, _consumer.Counter);
            Assert.AreEqual("stringParam", _consumer.Param[0]);
        }

        [Test]
        public void TestSubscribedConsumerCanReactToMulipleProducersTrigger()
        {
            _producer1.TriggerEvent();
            Assert.AreEqual(1, _consumer.Counter);
            Assert.AreEqual("stringParam", _consumer.Param[0]);
            _producer2.TriggerEvent();
            Assert.AreEqual(2, _consumer.Counter);
            Assert.AreEqual("stringParam", _consumer.Param[0]);
        }

        [Test]
        public void TestSubscribedConsumerCanReactToProducerAfterUnsubscribe()
        {
            _producer1.TriggerEvent();
            Assert.AreEqual(1, _consumer.Counter);
            Assert.AreEqual("stringParam", _consumer.Param[0]);
            _producer2.TriggerEvent();
            Assert.AreEqual(2, _consumer.Counter);
            Assert.AreEqual("stringParam", _consumer.Param[0]);

            _vent.Unsubscribe(new EventTest("consume"), _consumer.React);
            _producer1.TriggerEvent();
            Assert.AreEqual(2, _consumer.Counter);
            Assert.AreEqual("stringParam", _consumer.Param[0]);
        }

        [Test]
        public void TestSubscribedConsumerCanReactToMultipleActions()
        {
            Producer producer = new Producer("multipleactions");
            Assert.AreEqual(0, _consumer.Counter);
            producer.TriggerEvent();
            Assert.AreEqual(3, _consumer.Counter);
            Assert.AreEqual("stringParam", _consumer.Param[0]);
        }

        [Test]
        public void TestUnsubscribedMultipleActions()
        {
            Producer producer = new Producer("multipleactions");
            Assert.AreEqual(0, _consumer.Counter);
            producer.TriggerEvent();
            Assert.AreEqual(3, _consumer.Counter);
            Assert.AreEqual("stringParam", _consumer.Param[0]);
            _vent.Unsubscribe(new EventTest("multipleactions"), new Action<dynamic>[] {_consumer.React, _consumer.ReactTwo});
            producer.TriggerEvent();
            Assert.AreEqual(3, _consumer.Counter);
            Assert.AreEqual("stringParam", _consumer.Param[0]);
        }

        [Test]
        public void TestSubscribedConsumerCanReactToProducerTriggerWithMultipleParams()
        {
            _producer1.TriggerEventWithMultipleParams();
            Assert.AreEqual(1, _consumer.Counter);
            Assert.AreEqual("stringParam", _consumer.Param[0]);
            Assert.AreEqual(1, _consumer.Param[1]);
            Assert.AreEqual(true, _consumer.Param[2]);
        }
    }
}
