using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using FluentAssertions;
using NSubstitute;
using Toggl.Foundation.MvvmCross.ViewModels;
using Toggl.Foundation.Tests.Generators;
using Toggl.PrimeRadiant.Models;
using Xunit;

namespace Toggl.Foundation.Tests.MvvmCross.ViewModels
{
    public class TimeEntryViewModelTests
    {
        public class TimeEntryViewModelTest : BaseMvvmCrossTests
        {
            protected IDatabaseProject Project = Substitute.For<IDatabaseProject>();
            protected ITimeService TimeService { get; } = Substitute.For<ITimeService>();
            protected IDatabaseTimeEntry MockTimeEntry = Substitute.For<IDatabaseTimeEntry>();

            protected Subject<DateTimeOffset> TickSubject = new Subject<DateTimeOffset>();

            protected TimeEntryViewModelTest()
            {
                var observable = TickSubject.AsObservable().Publish();
                observable.Connect();
                TimeService.CurrentDateTimeObservable.Returns(observable);
            }
        }

        public class TheConstructor : TimeEntryViewModelTest
        {
            [Theory]
            [ClassData(typeof(TwoParameterConstructorTestData))]
            public void ThrowsIfAnyOfTheTheArgumentsIsNull(bool useTimeEntry, bool useNavigationService)
            {
                var timeEntry = useTimeEntry ? MockTimeEntry : null;
                var navigationService = useNavigationService ? NavigationService : null;

                Action tryingToConstructWithEmptyParameters =
                    () => new TimeEntryViewModel(timeEntry, navigationService);

                tryingToConstructWithEmptyParameters
                    .ShouldThrow<ArgumentNullException>();
            }
        }

        public class TheHasProjectProperty : TimeEntryViewModelTest
        {
            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void ChecksIfTheTimeEntryProvidedHasANonNullProject(bool hasProject)
            {
                MockTimeEntry.Stop.Returns(DateTimeOffset.UtcNow);
                MockTimeEntry.Project.Returns(hasProject ? Project : null);

                var viewModel = new TimeEntryViewModel(MockTimeEntry, NavigationService);

                viewModel.HasProject.Should().Be(hasProject);
            }
        }
    }
}
