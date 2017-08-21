using System;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Toggl.Foundation.MvvmCross.Parameters;
using Toggl.Multivac;
using Toggl.PrimeRadiant.Models;

namespace Toggl.Foundation.MvvmCross.ViewModels
{
    public class TimeEntryViewModel : MvxNotifyPropertyChanged
    {
        private readonly IMvxNavigationService navigationService;

        public long Id { get; }

        public string Description { get; } = "";

        public string ProjectName { get; } = "";

        public string TaskName { get; } = "";

        public DateTimeOffset Start { get; }

        public TimeSpan Duration { get; }

        public string ProjectColor { get; } = "#00000000";

        public bool HasProject { get; }

        public IMvxAsyncCommand EditCommand { get; }

        public TimeEntryViewModel(IDatabaseTimeEntry timeEntry, IMvxNavigationService navigationService)
        {
            Ensure.Argument.IsNotNull(timeEntry, nameof(timeEntry));
            Ensure.Argument.IsNotNull(navigationService, nameof(navigationService));

            Id = timeEntry.Id;
            Start = timeEntry.Start;
            Description = timeEntry.Description;
            HasProject = timeEntry.Project != null;
            Duration = timeEntry.Stop.Value - Start;

            if (HasProject)
            {
                ProjectName = timeEntry.Project.Name;
                ProjectColor = timeEntry.Project.Color;
            }

            this.navigationService = navigationService;

            EditCommand = new MvxAsyncCommand(edit);
        }

        private System.Threading.Tasks.Task edit() => navigationService.Navigate<EditTimeEntryViewModel, IdParameter>(IdParameter.WithId(Id));
    }
}
