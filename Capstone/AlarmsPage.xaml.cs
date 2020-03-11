using Capstone.Common;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Capstone.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Capstone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlarmsPage : Page
    {
        private readonly List<Alarm> Alarms;
        public AlarmsPage()
        {
            this.InitializeComponent();
            this.Alarms = this.GetAlarmsFromDatabase();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            UIUtils.GoToMainPage(this);
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            var NewAlarm = new Alarm();
            this.Frame.Navigate(typeof(AlarmsFormPage), NewAlarm);
        }

        private List<Alarm> GetAlarmsFromDatabase()
        {
            List<Alarm> alarms = new List<Alarm>();
            // TODO database stuff
            return alarms;
        }

        private void PopulateScreenWithAlarms()
        {
            // TODO
        }
    }
}
