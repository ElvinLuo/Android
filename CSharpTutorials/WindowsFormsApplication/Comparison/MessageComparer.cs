using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LIPS.Common.Comparison
{
    public class MessageComparer<T>
    {
        private readonly T expected;
        private readonly T actual;

        public MessageComparer(T expected, T actual)
        {
            this.expected = expected;
            this.actual = actual;
        }

        public bool Compare()
        {
            bool isEqual = true;


            return isEqual;
        }
    }
}
