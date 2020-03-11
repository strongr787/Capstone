using Capstone.Common;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
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
            this.PopulateScreenWithAlarms();
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
            List<Alarm> alarms = new List<Alarm> { new Alarm(-1, "Test Title", System.DateTime.Now, false)};
            // TODO database stuff
            return alarms;
        }

        private void PopulateScreenWithAlarms()
        {
            this.Alarms.ForEach(this.AddAlarmToScreen);
        }

        private void AddAlarmToScreen(Alarm AlarmToAdd)
        {
            // each alarm is wrapped in a relative panel
            RelativePanel alarmPanel = new RelativePanel();
            alarmPanel.Margin = new Thickness(5, 0, 5, 5);
            var borderBrush = new SolidColorBrush(Windows.UI.Colors.Gray);
            alarmPanel.BorderBrush = borderBrush;
            alarmPanel.BorderThickness = new Thickness(1);
            // create the text blocks for the title and date
            var alarmTitleBlock = CreateAlarmTitleBlock(AlarmToAdd);
            var alarmDateBlock = CreateAlarmDateBlock(AlarmToAdd);
            // add the blocks and buttons
            alarmPanel.Children.Add(alarmTitleBlock);
            alarmPanel.Children.Add(alarmDateBlock);
            RelativePanel.SetBelow(alarmDateBlock, alarmTitleBlock);
            this.VariableGrid.Children.Add(alarmPanel);
        }

        private static TextBlock CreateAlarmTitleBlock(Alarm AlarmToAdd)
        {
            var alarmTitleBlock = new TextBlock();
            alarmTitleBlock.Text = AlarmToAdd.Title;
            alarmTitleBlock.Margin = new Thickness(10);
            alarmTitleBlock.FontSize = 32;
            alarmTitleBlock.TextWrapping = TextWrapping.Wrap;
            alarmTitleBlock.MaxLines = 2;
            return alarmTitleBlock;
        }

        private static TextBlock CreateAlarmDateBlock(Alarm AlarmToAdd)
        {
            var alarmDateBlock = new TextBlock();
            alarmDateBlock.Text = AlarmToAdd.ActivateDateAndTime.ToString("g");
            alarmDateBlock.FontSize = 24;
            alarmDateBlock.Margin = new Thickness(10);
            return alarmDateBlock;
        }
    }
}
