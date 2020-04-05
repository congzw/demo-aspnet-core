using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DemoDI.Demos.InitTasks
{
    /// <summary>
    /// Recurrent Cancellable Task
    /// </summary>
    public static class LoopTaskHelper
    {
        public static void StartLoop(string taskName, Action action, TimeSpan pollInterval, TaskCreationOptions taskCreationOptions = TaskCreationOptions.None)
        {
            var cts = TryGetCancellationTokenSource(taskName);
            if (cts != null)
            {
                throw new InvalidOperationException("已经存在任务:" + taskName);
            }

            var taskTokenSource = new CancellationTokenSource();
            _cancelTokenSources[taskName] = taskTokenSource;
            StartNew(action, pollInterval, taskTokenSource.Token, taskCreationOptions);
        }

        public static void StopLoop(string taskName)
        {
            var cts = TryGetCancellationTokenSource(taskName);
            if (cts == null)
            {
                return;
            }
            cts.Cancel();
        }

        /// <summary>
        /// Starts a new task in a recurrent manner repeating it according to the polling interval.
        /// Whoever use this method should protect himself by surrounding critical code in the task 
        /// in a Try-Catch block.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="pollInterval">The poll interval.</param>
        /// <param name="token">The token.</param>
        /// <param name="taskCreationOptions">The task creation options</param>
        private static void StartNew(Action action, TimeSpan pollInterval, CancellationToken token, TaskCreationOptions taskCreationOptions = TaskCreationOptions.None)
        {
            var maxTryCount = 3;
            var tryTime = 0;
            Task.Factory.StartNew(
                () =>
                {
                    do
                    {
                        try
                        {
                            action();
                            tryTime = 0;
                            if (token.WaitHandle.WaitOne(pollInterval)) break;
                        }
                        catch(Exception ex)
                        {
                            tryTime++;
                            LogHelper.Instance.Error(ex, ex.Message);
                            if (tryTime >= maxTryCount)
                            {
                                return;
                            }
                            if (token.WaitHandle.WaitOne(pollInterval)) break;
                        }
                    }
                    while (true);
                },
                token,
                taskCreationOptions,
                TaskScheduler.Default);
        }

        private static readonly IDictionary<string, CancellationTokenSource> _cancelTokenSources = new ConcurrentDictionary<string, CancellationTokenSource>(StringComparer.OrdinalIgnoreCase);
        private static CancellationTokenSource TryGetCancellationTokenSource(string taskName)
        {
            if (!_cancelTokenSources.ContainsKey(taskName))
            {
                return null;
            }
            return _cancelTokenSources[taskName];
        }
    }

    //internal static class Repeat
    //{
    //    public static Task Interval(TimeSpan pollInterval, Action action, CancellationToken token)
    //    {
    //        // We don't use Observable.Interval:
    //        // If we block, the values start bunching up behind each other.
    //        return Task.Factory.StartNew(
    //            () =>
    //            {
    //                for (; ; )
    //                {
    //                    if (token.WaitCancellationRequested(pollInterval))
    //                        break;

    //                    action();
    //                }
    //            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    //    }
    //    private static bool WaitCancellationRequested(this CancellationToken token, TimeSpan timeout)
    //    {
    //        return token.WaitHandle.WaitOne(timeout);
    //    }
    //}
}
