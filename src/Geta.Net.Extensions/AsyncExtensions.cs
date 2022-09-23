// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Geta.Net.Extensions
{
    public static class AsyncExtensions
    {
        /// <summary>
        /// Allows a cancellation token to be awaited.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static CancellationTokenAwaiter GetAwaiter(this CancellationToken ct)
        {
            return new CancellationTokenAwaiter(ct);
        }

        /// <summary>
        /// The awaiter for cancellation tokens.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public struct CancellationTokenAwaiter : ICriticalNotifyCompletion
        {
            public CancellationTokenAwaiter(CancellationToken cancellationToken)
            {
                _cancellationToken = cancellationToken;
            }

            private CancellationToken _cancellationToken;

            public object GetResult()
            {
                // this is called by compiler generated methods when the
                // task has completed. Instead of returning a result, we
                // just throw an exception.
                if (IsCompleted)
                {
                    throw new OperationCanceledException();
                }

                throw new InvalidOperationException("The cancellation token has not yet been cancelled.");
            }

            // called by compiler generated/.net internals to check
            // if the task has completed.
            public bool IsCompleted => _cancellationToken.IsCancellationRequested;

            public void OnCompleted(Action continuation)
            {
                _cancellationToken.Register(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                _cancellationToken.Register(continuation);
            }
        }
    }
}
