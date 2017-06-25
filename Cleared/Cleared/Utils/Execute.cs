using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cleared.Utils
{
    public static class Execute
    {
        public static SynchronizationContext synchronizationContext;

        public static Task OnUIThread(Action action)
        {
            TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();

            if (synchronizationContext == SynchronizationContext.Current)
            {
                try
                {
                    action();
                    taskCompletionSource.SetResult(null);
                }
                catch (Exception exception)
                {
                    taskCompletionSource.SetException(exception);
                }
            }
            else
            {
                // Run the action asyncronously. The Send method can be used to run syncronously.
                synchronizationContext.Post( obj =>
                    {
                        try
                        {
                            action();
                            taskCompletionSource.SetResult(null);
                        }
                        catch (Exception exception)
                        {
                            taskCompletionSource.SetException(exception);
                        }
                    }, null);
            }

            return taskCompletionSource.Task;
        }

        public static void DelayAction(TimeSpan delay, Action action)
        {
            new Task(
                async () =>
                {
                    await Task.Delay(delay);
                    await OnUIThread(action);
                }).Start();

        }

    }
}
