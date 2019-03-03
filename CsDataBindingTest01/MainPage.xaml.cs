using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Midi;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Devices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CsDataBindingTest01
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //public float volume;

        public Audio audio;

        private DeviceInformationCollection audioInputDevices;
        private DeviceInformationCollection audioOutputDevices;

        private AudioDeviceWatcher audioInputWatcher;
        private AudioDeviceWatcher audioOutputWatcher;

        private DeviceInformationCollection midiInputDevices;
        private DeviceInformationCollection midiOutputDevices;

        private MidiDeviceWatcher midiInputWatcher;
        private MidiDeviceWatcher midiOutputWatcher;

        /*
    public static readonly DependencyProperty volumeProperty = DependencyProperty.Register(
        "volume",
        typeof(float),
        typeof(MainPage),
        new PropertyMetadata(75.0f)
        );
    */

        public MainPage()
        {
            this.InitializeComponent();

            this.audio = new Audio(70.0);

            this.DataContext = this.audio;
            //this.mainPageGrid.DataContext = audio;

            //DataContext.Equals(this);

            //volume = 50.0f;

            this.audioInputWatcher = new AudioDeviceWatcher(MediaDevice.GetAudioCaptureSelector(), audioInputDevicesDynamicListBox, Dispatcher, AudioDeviceWatcher.AudioDeviceType.Input);
            audioInputWatcher.StartWatcher();

            this.audioOutputWatcher = new AudioDeviceWatcher(MediaDevice.GetAudioRenderSelector(), audioOutputDevicesDynamicListBox, Dispatcher, AudioDeviceWatcher.AudioDeviceType.Output);
            audioOutputWatcher.StartWatcher();

            this.midiInputWatcher = new MidiDeviceWatcher(MidiInPort.GetDeviceSelector(), midiInputDevicesDynamicListBox, Dispatcher, MidiDeviceWatcher.MidiDeviceType.Input);
            midiInputWatcher.StartWatcher();

            this.midiOutputWatcher = new MidiDeviceWatcher(MidiOutPort.GetDeviceSelector(), midiOutputDevicesDynamicListBox, Dispatcher, MidiDeviceWatcher.MidiDeviceType.Output);
            midiOutputWatcher.StartWatcher();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await PopulateAudioInputDeviceList();
            await PopulateAudioOutputDeviceList();
        }

        private async Task PopulateAudioInputDeviceList()
        {
            audioInputDevicesStaticListBox.Items.Clear();
            audioInputDevices = await DeviceInformation.FindAllAsync(MediaDevice.GetAudioCaptureSelector());
            audioInputDevicesStaticListBox.Items.Add("Select an audio input device:");
            foreach (var device in audioInputDevices)
            {
                audioInputDevicesStaticListBox.Items.Add(device.Name);
            }
        }

        private async Task PopulateAudioOutputDeviceList()
        {
            audioOutputDevicesStaticListBox.Items.Clear();
            audioOutputDevices = await DeviceInformation.FindAllAsync(MediaDevice.GetAudioRenderSelector());
            audioOutputDevicesStaticListBox.Items.Add("Select an audio output device:");
            foreach (var device in audioOutputDevices)
            {
                audioOutputDevicesStaticListBox.Items.Add(device.Name);
            }
        }

        private void audioInputDevicesStaticListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void audioOutputDevicesStaticListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void audioInputDevicesDynamicListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void audioOutputDevicesDynamicListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void midiInputDevicesDynamicListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void midiOutputDevicesDynamicListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
