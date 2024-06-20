using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Common
{
    internal class LogLevelFilter : ITelemetryProcessor
    {
        private ITelemetryProcessor Next { get; set; }
        private SeverityLevel SeverityLevel {  get; set; }

        // next will point to the next TelemetryProcessor in the chain.
        public LogLevelFilter(ITelemetryProcessor next, SeverityLevel severityLevel)
        {
            this.Next = next;   
            this.SeverityLevel = severityLevel;
        }

        public void Process(ITelemetry item)
        {
            // To filter out an item, return without calling the next processor.
            if (!OKtoSend(item)) { return; }

            this.Next.Process(item);
        }

        // Example: replace with your own criteria.
        private bool OKtoSend(ITelemetry item)
        {
            if (item is TraceTelemetry traceTelemetry)
            {
                bool isOK = ((int)traceTelemetry.SeverityLevel >= (int)this.SeverityLevel);
                return isOK;
            }

            return true;
        }
    }
}
