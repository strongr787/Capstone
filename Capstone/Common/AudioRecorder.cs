using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace Capstone.Common
{
    class AudioRecorder
    {

        public MediaElement playbackMediaElement = new MediaElement();
        private MediaCapture _mediaCapture;
        private InMemoryRandomAccessStream _memoryBuffer;
        private string _fileName;
        
        public bool IsRecording { get; set; }
        
        

        public async void Record()
        {
            if (IsRecording)
            {
                throw new InvalidOperationException("Recording already in progress!");
            }

            if (_memoryBuffer != null)
            {
                _memoryBuffer.Dispose();
            }
            _memoryBuffer = new InMemoryRandomAccessStream();
            if (_mediaCapture != null)
            {
                _mediaCapture.Dispose();
            }

            MediaCaptureInitializationSettings settings =
            new MediaCaptureInitializationSettings
            {
                StreamingCaptureMode = StreamingCaptureMode.Audio
            };
            _mediaCapture = new MediaCapture();
            await _mediaCapture.InitializeAsync(settings);
            await _mediaCapture.StartRecordToStreamAsync(
            MediaEncodingProfile.CreateMp3(AudioEncodingQuality.Auto), _memoryBuffer);
            IsRecording = true;
        }

        public async void StopRecording()
        {
            await _mediaCapture.StopRecordAsync();
            IsRecording = false;
      
        }


        public async Task<string> SaveAudioToFile(string fileName)
        {
            IRandomAccessStream audioStream = _memoryBuffer.CloneStream();
            StorageFolder storageFolder = Package.Current.InstalledLocation;
            StorageFile storageFile = await storageFolder.CreateFileAsync(
              fileName + ".mp3", CreationCollisionOption.GenerateUniqueName);
            this._fileName = storageFile.Name;
            using (IRandomAccessStream fileStream =
              await storageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                await RandomAccessStream.CopyAndCloseAsync(
                  audioStream.GetInputStreamAt(0), fileStream.GetOutputStreamAt(0));
                await audioStream.FlushAsync();
                audioStream.Dispose();
            }
            return this._fileName;
        }

        public async Task PlayFromDisk(CoreDispatcher dispatcher, string fileName)
        {
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {

                
                StorageFolder storageFolder = Package.Current.InstalledLocation;
                StorageFile storageFile = await storageFolder.GetFileAsync(fileName);
                IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.Read);
                playbackMediaElement.SetSource(stream, storageFile.FileType);
                playbackMediaElement.Play();
 
            });
        }


        public async void DeleteFile(string file)
        {
            StorageFile fileToDelete = await Package.Current.InstalledLocation.GetFileAsync(file);
            if (fileToDelete != null)
            {
                await fileToDelete.DeleteAsync();
            }
        }

        public void DisposeMedia()
        {
            _mediaCapture.Dispose();
        }

        public async Task<int> AudioDuration(string fileName)
        {
            int duration = 0;
            StorageFile audioFile = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(fileName);
            MusicProperties properties = await audioFile.Properties.GetMusicPropertiesAsync();
            TimeSpan myTrackDuration = properties.Duration;
            duration = myTrackDuration.Seconds;


            return duration;
        }

        public DateTime DateRecorded()
        {
            DateTime today = DateTime.Now;
           
            return Convert.ToDateTime(today.ToShortDateString());
        }

        public DateTime RecordTime ()
        {
            DateTime today = DateTime.Now;
            return Convert.ToDateTime(today.ToShortTimeString());

        }
    }
}
