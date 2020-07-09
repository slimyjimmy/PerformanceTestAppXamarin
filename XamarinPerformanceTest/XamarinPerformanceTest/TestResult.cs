using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinPerformanceTest
{
    class TestResult
    {
        public double Duration { get; set; }
        public String Message { get; set; }

        public TestResult(double duration, string message)
        {
            Duration = duration;
            Message = message;
        }

        public override string ToString()
        {
            return Duration + "ns" + Message;
        }
    }
}
