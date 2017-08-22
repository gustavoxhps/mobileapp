﻿using System;

namespace Toggl.Foundation.Sync
{
    public interface IStateMachineOrchestrator
    {
        SyncState State { get; }
        IObservable<SyncState> StateObservable { get; }
        IObservable<SyncState> SyncCompleteObservable { get; }
        
        void StartPushSync();
        void StartPullSync();
        void GoToSleep();
    }
}
