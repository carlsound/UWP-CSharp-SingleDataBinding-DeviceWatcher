using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Midi;
using Windows.Media.Devices;
using Windows.UI.Core;

namespace CsDataBindingTest01
{
    public class MainPageViewModel
    {
        public Audio Audio;

        //public DeviceInformationCollection AudioInputDevices;
        //public DeviceInformationCollection AudioOutputDevices;

        //public DeviceInformationCollection MidiInputDevices;
        //public DeviceInformationCollection MidiOutputDevices;

        public AudioDeviceWatcher AudioInputWatcher;
        public AudioDeviceWatcher AudioOutputWatcher;

        public MidiDeviceWatcher MidiInputWatcher;
        public MidiDeviceWatcher MidiOutputWatcher;

        public MainPageViewModel(CoreDispatcher dispatcher)
        {
            this.Audio = new Audio(70.0);

            //this.DataContext = this.audio;

            this.AudioInputWatcher = new AudioDeviceWatcher(AudioDeviceWatcher.AudioDeviceType.Input, dispatcher);
            AudioInputWatcher.StartWatcher();

            this.AudioOutputWatcher = new AudioDeviceWatcher(AudioDeviceWatcher.AudioDeviceType.Output, dispatcher);
            AudioOutputWatcher.StartWatcher();

            this.MidiInputWatcher = new MidiDeviceWatcher(MidiDeviceWatcher.MidiDeviceType.Input, dispatcher);
            MidiInputWatcher.StartWatcher();

            this.MidiOutputWatcher = new MidiDeviceWatcher(MidiDeviceWatcher.MidiDeviceType.Output, dispatcher);
            MidiOutputWatcher.StartWatcher();
        }

        //public async void OnNaviagtedTo()
        //{
        //    await PopulateAudioInputDeviceList();
        //    await PopulateAudioOutputDeviceList();
        //}

        //private async Task PopulateAudioInputDeviceList()
        //{
        //    audioInputDevicesStaticListBox.Items.Clear();
        //    audioInputDevices = await DeviceInformation.FindAllAsync(MediaDevice.GetAudioCaptureSelector());
        //    audioInputDevicesStaticListBox.Items.Add("Select an audio input device:");
        //    foreach (var device in audioInputDevices)
        //    {
        //        audioInputDevicesStaticListBox.Items.Add(device.Name);
        //    }
        //}

        //private async Task PopulateAudioOutputDeviceList()
        //{
        //    audioOutputDevicesStaticListBox.Items.Clear();
        //    audioOutputDevices = await DeviceInformation.FindAllAsync(MediaDevice.GetAudioRenderSelector());
        //    audioOutputDevicesStaticListBox.Items.Add("Select an audio output device:");
        //    foreach (var device in audioOutputDevices)
        //    {
        //        audioOutputDevicesStaticListBox.Items.Add(device.Name);
        //    }
        //}
    }
}
