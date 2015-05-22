﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestAutomation.Shared.Extensions
{
    public static class Wait
    {
        /// <summary>
        /// Waits for condition to occur
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="maxWaitSeconds"></param>
        /// <param name="sleepPerFailSeconds"></param>
        /// <returns>Ture if condition occured within time waited</returns>
        public static bool For(Func<bool> condition, int maxWaitSeconds, int sleepPerFailSeconds)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            bool success = condition();
            while (!success && watch.ElapsedMilliseconds < (maxWaitSeconds * 1000))
            {
                Thread.Sleep(sleepPerFailSeconds * 1000);
                success = condition();
            }
            watch.Stop();

            return success;
        }
    }
}
