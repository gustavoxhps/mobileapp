﻿using CoreGraphics;
using Toggl.Daneel.Extensions;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS;
using MvvmCross.iOS.Views;
using MvvmCross.Plugins.Visibility;
using Toggl.Daneel.Presentation.Attributes;
using Toggl.Foundation.MvvmCross.Converters;
using Toggl.Foundation.MvvmCross.ViewModels;
using MvvmCross.Plugins.Color;
using Toggl.Daneel.Converters;

namespace Toggl.Daneel.ViewControllers
{
    [ModalCardPresentation]
    public partial class EditTimeEntryViewController : MvxViewController
    {
        private const int switchHeight = 24;

        public EditTimeEntryViewController() : base(nameof(EditTimeEntryViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            DurationLabel.Font = DurationLabel.Font.GetMonospacedDigitFont();

            PreferredContentSize = View.Frame.Size;

            resizeSwitch();

            var durationConverter = new TimeSpanToDurationWithUnitValueConverter();
            var dateConverter = new DateToTitleStringValueConverter();
            var timeConverter = new DateTimeToTimeConverter();
            var visibilityConverter = new MvxVisibilityValueConverter();
            var inverterVisibilityConverter = new MvxInvertedVisibilityValueConverter();
            var projectTaskClientConverter = new ProjectTaskClientToAttributedStringValueConverter(ProjectTaskClientLabel.Font);

            var bindingSet = this.CreateBindingSet<EditTimeEntryViewController, EditTimeEntryViewModel>();
            
            //Text
            bindingSet.Bind(DescriptionLabel).To(vm => vm.Description);
            bindingSet.Bind(BillableSwitch).To(vm => vm.Billable);
            bindingSet.Bind(DurationLabel)
                      .To(vm => vm.Duration)
                      .WithConversion(durationConverter);

            bindingSet.Bind(ProjectTaskClientLabel)
                      .For(v => v.AttributedText)
                      .To(vm => vm.ProjectTaskClient)
                      .WithConversion(projectTaskClientConverter);
            
            bindingSet.Bind(StartDateLabel)
                      .To(vm => vm.StartTime)
                      .WithConversion(dateConverter);
            
            bindingSet.Bind(StartTimeLabel)
                      .To(vm => vm.StartTime)
                      .WithConversion(timeConverter);
            
            //Commands
            bindingSet.Bind(CloseButton).To(vm => vm.CloseCommand);
            bindingSet.Bind(DeleteButton).To(vm => vm.DeleteCommand);

            //Description visibility
            bindingSet.Bind(AddDescriptionView)
                      .For(v => v.BindVisible())
                      .To(vm => vm.Description)
                      .WithConversion(visibilityConverter);
            
            bindingSet.Bind(DescriptionLabel)
                      .For(v => v.BindVisible())
                      .To(vm => vm.Description)
                      .WithConversion(inverterVisibilityConverter);

            //Project visibility
            bindingSet.Bind(AddProjectAndTaskView)
                      .For(v => v.BindVisible())
                      .To(vm => vm.Project)
                      .WithConversion(visibilityConverter);
            
            bindingSet.Bind(ProjectTaskClientLabel)
                      .For(v => v.BindVisible())
                      .To(vm => vm.Project)
                      .WithConversion(inverterVisibilityConverter);
            
            //Tags visibility
            bindingSet.Bind(AddTagsView)
                      .For(v => v.BindVisible())
                      .To(vm => vm.Tags)
                      .WithConversion(visibilityConverter);
            
            bindingSet.Bind(TagsLabel)
                      .For(v => v.BindVisible())
                      .To(vm => vm.Tags)
                      .WithConversion(inverterVisibilityConverter);
            
            bindingSet.Apply();
        }

        private void resizeSwitch()
        {
            var scale = switchHeight / BillableSwitch.Frame.Height;
            BillableSwitch.Transform = CGAffineTransform.MakeScale(scale, scale);
        }
    }
}
