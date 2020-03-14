using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Capstone.Common;
using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;
using Capstone.Models;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Text;
using System;
using Windows.UI.ViewManagement;

namespace Capstone
{
    /// <summary>
    /// A page to view and create voice Memos from as well as listen to them and delete them
    /// </summary>
    public sealed partial class VoiceMemosPage : Page
    {

        public List<VoiceMemo> VoiceMemos { get; set; }
        // UI settings to detect when we should change accent colors for certain parts
        private UISettings uiSettings;

        public VoiceMemosPage()
        {
            this.InitializeComponent();
            this.VoiceMemos = ReadVoiceMemosFromDatabase();
            this.PopulateListOfVoiceMemos();
            this.uiSettings = new UISettings();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            UIUtils.GoToMainPage(this);
        }

        private List<VoiceMemo> ReadVoiceMemosFromDatabase()
        {
            List<VoiceMemo> voiceMemos = new List<VoiceMemo>();
            // TODO database stuff
            return voiceMemos;
        }

        private void PopulateListOfVoiceMemos()
        {
            this.VoiceMemos.ForEach(BuildMemoPanel);
        }

        private void BuildMemoPanel(VoiceMemo VoiceMemoToAdd)
        {
            var panel = new RelativePanel();
            panel.Margin = new Thickness(0, 10, 0, 10);
            var ellipse = BuildMemoEllipse();
            var titleBlock = BuildTitleBlock(VoiceMemoToAdd);
            var durationBlock = BuildDurationBlock(VoiceMemoToAdd);
            var dateRecordedBlock = BuildDateRecordedBlock(VoiceMemoToAdd);
            var deleteButton = BuildDeleteButton(VoiceMemoToAdd);
            var playbackButton = BuildPlayBackButton(VoiceMemoToAdd);
            panel.Children.Add(ellipse);
            panel.Children.Add(titleBlock);
            panel.Children.Add(durationBlock);
            panel.Children.Add(dateRecordedBlock);
            panel.Children.Add(deleteButton);
            panel.Children.Add(playbackButton);
            // position the elements within the panel
            RelativePanel.SetRightOf(titleBlock, ellipse);
            RelativePanel.SetAlignVerticalCenterWith(titleBlock, ellipse);
            RelativePanel.SetBelow(durationBlock, titleBlock);
            RelativePanel.SetAlignLeftWith(durationBlock, titleBlock);
            RelativePanel.SetBelow(dateRecordedBlock, durationBlock);
            RelativePanel.SetAlignLeftWith(dateRecordedBlock, durationBlock);
            RelativePanel.SetBelow(deleteButton, dateRecordedBlock);
            RelativePanel.SetAlignBottomWithPanel(deleteButton, true);
            RelativePanel.SetAlignLeftWithPanel(deleteButton, true);
            RelativePanel.SetBelow(playbackButton, dateRecordedBlock);
            RelativePanel.SetAlignBottomWithPanel(playbackButton, true);
            RelativePanel.SetAlignRightWithPanel(playbackButton, true);
            this.VoiceNoteList.Children.Add(panel);
        }

        private Ellipse BuildMemoEllipse()
        {
            var ellipse = new Ellipse();
            ellipse.Margin = new Thickness(10);
            ellipse.Width = 25;
            ellipse.Height = 25;
            var brush = new SolidColorBrush();
            ellipse.Fill = this.CreateUIColorBrush();
            // change the color of this ellipse when the system colors change
            return ellipse;
        }

        private Brush CreateUIColorBrush()
        {
            var brush = new SolidColorBrush();
            brush.Color = (Color)Application.Current.Resources["SystemAccentColor"];
            return brush;
        }

        private TextBlock BuildTitleBlock(VoiceMemo VoiceMemoToAdd)
        {
            var titleBlock = new TextBlock();
            titleBlock.FontWeight = FontWeights.Bold;
            titleBlock.Text = VoiceMemoToAdd.DisplayName;
            return titleBlock;
        }

        private TextBlock BuildDurationBlock(VoiceMemo VoiceMemoToAdd)
        {
            var durationBlock = new TextBlock();
            durationBlock.Text = $"Duration: {TimeSpan.FromSeconds(VoiceMemoToAdd.RecordingDuration).ToString(@"mm\:ss")}";
            return durationBlock;
        }

        private TextBlock BuildDateRecordedBlock(VoiceMemo VoiceMemoToAdd)
        {
            var dateRecordedBlock = new TextBlock();
            dateRecordedBlock.Margin = new Thickness(0, 0, 0, 20);
            dateRecordedBlock.Text = VoiceMemoToAdd.DateRecorded.ToShortDateString();
            return dateRecordedBlock;
        }

        private Button BuildDeleteButton(VoiceMemo VoiceMemoToAdd)
        {
            var deleteButton = new Button();
            deleteButton.Content = "Delete";
            deleteButton.Click += (sender, arguments) => DeleteVoiceMemo(VoiceMemoToAdd);
            return deleteButton;
        }

        private Button BuildPlayBackButton(VoiceMemo VoiceMemoToAdd)
        {
            var playbackButton = new Button();
            playbackButton.Content = "Playback";
            playbackButton.Click += (sender, arguments) => PlayVoiceMemo(VoiceMemoToAdd);
            return playbackButton;
        }

        private void PlayVoiceMemo(VoiceMemo VoiceMemoToPlay)
        {
            // TODO
        }

        private void DeleteVoiceMemo(VoiceMemo VoiceMemoToDelete)
        {
            // TODO delete from file system and database, and reload the list of voice memos
        }
    }
}
