﻿using System;

namespace Toggl.Multivac.Models
{
    public interface IProject : IBaseModel
    {
        long WorkspaceId { get; }

        long? ClientId { get; }

        string Name { get; }

        bool IsPrivate { get; }

        bool Active { get; }

        DateTimeOffset At { get; }

        DateTimeOffset? ServerDeletedAt { get; }

        string Color { get; }

        bool? Billable { get; }

        bool? Template { get; }

        bool? AutoEstimates { get; }

        int? EstimatedHours { get; }

        double? Rate { get; }

        string Currency { get; }

        int? ActualHours { get; }
    }
}
