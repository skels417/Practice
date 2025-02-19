using System.Collections.Generic;
using System;

namespace Task5
{
    public class InterpretedData
    {
        public DateTime Time { get; set; }
        public Dictionary<string, object> Values { get; set; }

        public InterpretedData()
        {
            Values = new Dictionary<string, object>();
        }
    }
}