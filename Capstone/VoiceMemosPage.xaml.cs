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
using System.Diagnostics;
using Windows.Storage.FileProperties;
using System.Threading.Tasks;


namespace Capstone
{
    /// <summary>
    /// A page to view and create voice Memos from as well as listen to them and delete them
    /// </summary>
    public sealed partial class VoiceMemosPage : Page
    {
       
        AudioRecorder _audioRecorder;
        
        public List<VoiceMemo> VoiceMemos { get; set; }
        // UI settings to detect when we should change accent colors for certain parts
        private UISettings uiSettings;

        private VoiceMemo CreateVoiceMemo = new VoiceMemo();
        public VoiceMemosPage()
        {
            this.InitializeComponent();
            this.HideInitialControls();
            this.VoiceMemos = ReadVoiceMemosFromDatabase();
            this.PopulateListOfVoiceMemos();
            this.uiSettings = new UISettings();

            //used to record, stop, and play voice note
            this._audioRecorder = new AudioRecorder();
        }

        private void HideInitialControls()
        {
            this.saveRecording.Visibility = Visibility.Collapsed;
            this.stopRecording.Visibility = Visibility.Collapsed;
            this.deleteRecording.Visibility = Visibility.Collapsed;
            this.displayName.Visibility = Visibility.Collapsed;
            this.lblFileName.Visibility = Visibility.Collapsed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private async void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            //make sure user wants to leave page in case of work in progress
            bool goToMainPage = await DisplayGoBackToMainPageDialog();

            if(goToMainPage)
            {
                _audioRecorder.StopPlaybackMedia();
                _audioRecorder.DisposeStream();
                _audioRecorder.DisposeMedia();
                _audioRecorder.DisposeMemoryBuffer();
                UIUtils.GoToMainPage(this);
            }
            
        }

        private List<VoiceMemo> ReadVoiceMemosFromDatabase()
        {
            List<VoiceMemo> voiceMemos = StoredProcedures.QueryAllVoiceMemos();
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
            deleteButton.Click += (sender, arguments) => DeleteVoiceMemoAsync(VoiceMemoToAdd);
            return deleteButton;
        }

        private Button BuildPlayBackButton(VoiceMemo VoiceMemoToAdd)
        {
            var playbackButton = new Button();
            playbackButton.Content = "Playback";
            playbackButton.Click += (sender, arguments) => PlayVoiceMemo(VoiceMemoToAdd);
            return playbackButton;
        }

        private async void PlayVoiceMemo(VoiceMemo VoiceMemoToPlay)
        {
            //don't let playback if in recording session 
            if (!_audioRecorder.IsRecording )
            {
                await this._audioRecorder.PlayFromDisk(VoiceMemoToPlay.FileName );
            }
        }

        private async Task DeleteVoiceMemoAsync(VoiceMemo VoiceMemoToDelete)
        {
            bool deleteVoiceMemo = false;

            deleteVoiceMemo = await DisplayDeleteFileDialog();

            if (deleteVoiceMemo)
            {
                //stop playing an dispose stream
                _audioRecorder.StopPlaybackMedia();
                _audioRecorder.DisposeStream();

                //delete file and from database
                this._audioRecorder.DeleteFile(VoiceMemoToDelete.FileName);
                StoredProcedures.DeleteVoiceNote(VoiceMemoToDelete.VoiceMemoID);

                this.VoiceMemos.Clear();
                this.VoiceMemos = ReadVoiceMemosFromDatabase();
                this.PopulateListOfVoiceMemos();

                await Task.Delay(100);
                Frame.Navigate(this.GetType());
            }
        }

        private async void Button_ClickDelete(object sender, RoutedEventArgs e)
        {

            //we need to make sure user understands nothing will be saved
            bool discardFile = false;
            discardFile = await DisplayDeleteFileDialog();

            if (discardFile)
            {
                //stop the recording and go back to main voice notes screen
                _audioRecorder.DisposeMedia();
                _audioRecorder.DisposeMemoryBuffer();
                _audioRecorder.DisposeStream();
                Frame.Navigate(this.GetType());
            }
        }

        private void Button_ClickStart(object sender, RoutedEventArgs e)
        {
            //toggle pause/play buttons
            OnStartRecordingToggle();
            this._audioRecorder.Record(); 
        }
        private void Button_ClickStop(object sender, RoutedEventArgs e)
        {
            //toggle buttons
            OnStopRecordingToggle();
            this._audioRecorder.StopRecording();      
        }

        private async void Button_ClickSave(object sender, RoutedEventArgs e)
        {
            //make sure user enters a file name. does not need to be unique, as saveaudiotofile will create a unique file if the file name already exists
            bool validateName = ValidateFileName();
            if(validateName)
            {
                //set values
                CreateVoiceMemo.DisplayName = displayName.Text;
                //unfortunately, we don't know the file name for sure until this is ran
                CreateVoiceMemo.FileName = await this._audioRecorder.SaveAudioToFile(CreateVoiceMemo.DisplayName);
                CreateVoiceMemo.FullFilePath = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
                CreateVoiceMemo.RecordingDuration = await _audioRecorder.AudioDuration(CreateVoiceMemo.FileName);
                CreateVoiceMemo.DateRecorded = _audioRecorder.DateRecorded();
                DateTime timeRecorded = _audioRecorder.RecordTime();
                
                StoredProcedures.CreateVoiceNote(CreateVoiceMemo.FileName, CreateVoiceMemo.DisplayName, CreateVoiceMemo.RecordingDuration, CreateVoiceMemo.FullFilePath, CreateVoiceMemo.DateRecorded, timeRecorded);
                

                await Task.Delay(100);
                Frame.Navigate(this.GetType());
            }else
            {
                DisplayEnterNameDialog();
            }
        }

        private void OnStartRecordingToggle()
        {
            this.startRecording.Visibility = Visibility.Collapsed;
            this.stopRecording.Visibility = Visibility.Visible;
        }

        private void OnStopRecordingToggle()
        {
            this.stopRecording.Visibility = Visibility.Collapsed;
            this.saveRecording.Visibility = Visibility.Visible;
            this.deleteRecording.Visibility = Visibility.Visible;
            this.displayName.Visibility = Visibility.Visible;
            this.lblFileName.Visibility = Visibility.Visible;

        }

        private async Task<bool> DisplayDeleteFileDialog()
        {
            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = "Delete file permanently?",
                Content = "If you delete this file, you won't be able to recover it. Do you want to delete it?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await deleteFileDialog.ShowAsync();
            return result == ContentDialogResult.Primary;
        }

        private async Task<bool> DisplayGoBackToMainPageDialog()
        {
            ContentDialog goToMainPageDialog = new ContentDialog
            {
                Title = "Exit Voice Notes",
                Content = "Are you sure you want to go back to Main Page. All works in progress will be lost.",
                PrimaryButtonText = "Yes",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await goToMainPageDialog.ShowAsync();
            return result == ContentDialogResult.Primary;
        }

        private async void DisplayEnterNameDialog()
        {
            ContentDialog noNameDialog = new ContentDialog
            {
                Title = "No Name Entered",
                Content = "Please Enter a Display Name to save the File.",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noNameDialog.ShowAsync();
        }

        private bool ValidateFileName()
        {
            return StringUtils.IsNotBlank(displayName.Text);
        }

    }
}
