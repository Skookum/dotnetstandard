﻿using System;
using DotNetStandard.Vent;

namespace DotNetStandard.Tests.Models
{
    public class Consumer
    {
        private EventAggregator _vent = EventAggregator.Instance;
        public int Counter { get; set; }
        public dynamic Param { get; set; }
        public Consumer()
        {
            Counter = 0;
            _vent.Subscribe(new EventTest("consume"), React);
            _vent.Subscribe(new EventTest("produce"), React);
            _vent.Subscribe(new EventTest("reconsume"), React);
            _vent.Subscribe(new EventTest("multipleactions"), 
                            new Action<dynamic>[] {React, ReactTwo});
        }

        public void React(dynamic param)
        {
            Counter++;
            Param = param;
        }

        public void ReactTwo(dynamic param)
        {
            Counter += 2;
            Param = param;
        }
    }
}