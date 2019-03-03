using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace CsDataBindingTest01
{
    class MidiDeviceWatcher
    {
        public enum MidiDeviceType { Input, Output };

        public DeviceInformationCollection DeviceInformationCollection { get; set; }

        private DeviceWatcher deviceWatcher;
        private string deviceSelectorString;
        private ListBox deviceListBox;
        private CoreDispatcher coreDispatcher;
        private MidiDeviceType deviceType;

        public MidiDeviceWatcher(string audioDeviceSelectorString, ListBox audioDeviceListBox, CoreDispatcher dispatcher, MidiDeviceType ioType)
        {
            deviceListBox = audioDeviceListBox;
            coreDispatcher = dispatcher;

            deviceSelectorString = audioDeviceSelectorString;

            deviceWatcher = DeviceInformation.CreateWatcher(deviceSelectorString);
            deviceWatcher.Added += DeviceWatcher_Added;
            deviceWatcher.Removed += DeviceWatcher_Removed;
            deviceWatcher.Updated += DeviceWatcher_Updated;
            deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;

            deviceType = ioType;
        }

        ~MidiDeviceWatcher()
        {
            deviceWatcher.Added -= DeviceWatcher_Added;
            deviceWatcher.Removed -= DeviceWatcher_Removed;
            deviceWatcher.Updated -= DeviceWatcher_Updated;
            deviceWatcher.EnumerationCompleted -= DeviceWatcher_EnumerationCompleted;
            deviceWatcher = null;
        }

        public void StartWatcher()
        {
            deviceWatcher.Start();
        }
        public void StopWatcher()
        {
            deviceWatcher.Stop();
        }

        private async void UpdateDevices()
        {
            // Get a list of all MIDI devices
            this.DeviceInformationCollection = await DeviceInformation.FindAllAsync(deviceSelectorString);

            deviceListBox.Items.Clear();

            if (!this.DeviceInformationCollection.Any())
            {
                deviceListBox.Items.Add("No audio devices found!");
            }

            if (MidiDeviceType.Input == deviceType)
            {
                deviceListBox.Items.Add("Select a MIDI input device:");
            }
            else
            {
                deviceListBox.Items.Add("Select a MIDI output device:");
            }

            foreach (var deviceInformation in this.DeviceInformationCollection)
            {
                deviceListBox.Items.Add(deviceInformation.Name);
            }

        }

        private async void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            await coreDispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                // Update the device list
                UpdateDevices();
            });
        }

        private async void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            await coreDispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                // Update the device list
                UpdateDevices();
            });
        }

        private async void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object args)
        {
            await coreDispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                // Update the device list
                UpdateDevices();
            });
        }

        private async void DeviceWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            await coreDispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                // Update the device list
                UpdateDevices();
            });
        }
    }
}
