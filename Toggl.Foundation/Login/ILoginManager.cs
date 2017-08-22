﻿using System;
using Toggl.Foundation.DataSources;
using Toggl.Multivac;

namespace Toggl.Foundation.Login
{
    public interface ILoginManager
    {
        ITogglDataSource GetDataSourceIfLoggedIn();
        IObservable<ITogglDataSource> Login(Email email, string password);
    }
}
