using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Capstone.Models;

namespace Capstone
{

    /// <summary>
    /// The form for editing and creating an alarm. Technically this page is only used to edit an alarm, as "creating" an alarm involves passing in a blank alarm to this page
    /// </summary>
    public sealed partial class AlarmsFormPage : Page
    {
        private Alarm AlarmToEdit;
        public AlarmsFormPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // get the alarm being passed to this screen
            this.AlarmToEdit = (Alarm)e.Parameter;
            this.PopulateFormFromAlarm();
        }

        private void PopulateFormFromAlarm()
        {
            // set the title field
            this.AlarmTitleInput.Text = this.AlarmToEdit.Title;
            // set the date and time fields
            this.AlarmDatePicker.Date = this.AlarmToEdit.ActivateDateAndTime.Date;
            this.AlarmTimePicker.Time = this.AlarmToEdit.ActivateDateAndTime.TimeOfDay;

            // set the minimum date on our date picker field (minimum time can't be set on a timepicker field)
            this.AlarmDatePicker.MinDate = this.AlarmToEdit.ActivateDateAndTime.Date;
        }

        private void PopulateAlarmFromForm()
        {
            var date = this.AlarmDatePicker.Date;
            var time = this.AlarmTimePicker.Time;
            this.AlarmToEdit.ActivateDateAndTime = date.Value.DateTime + time;
            this.AlarmToEdit.Title = this.AlarmTitleInput.Text.Trim();
        }

        private void SubmitAlarmButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (this.ValidateForm())
            {
                this.PopulateAlarmFromForm();
                // TODO save alarm to database once that's all set up
                this.Frame.Navigate(typeof(AlarmsPage));
            }
        }

        private bool ValidateForm()
        {
            // make sure that the title is not empty or blank
            bool isValid = true;
            if (Common.StringUtils.IsBlank(this.AlarmTitleInput.Text))
            {
                isValid = false;
            }
            if (!this.ValidateTime())
            {
                isValid = false;
            }
            // don't need to validate date since the earliest it can go is today
            return isValid;
        }

        private bool ValidateTime()
        {
            // get the hours and minutes of our time and compare them against date.now
            var now = System.DateTime.Now;
            var timeHours = this.AlarmTimePicker.Time.Hours;
            var timeMinutes = this.AlarmTimePicker.Time.Minutes;
            return timeHours > now.Hour || (timeHours >= now.Hour && timeMinutes >= now.Minute);
        }
    }
}
