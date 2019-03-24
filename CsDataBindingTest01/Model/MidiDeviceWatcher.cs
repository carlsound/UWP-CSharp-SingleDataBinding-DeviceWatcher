using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Midi;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace CsDataBindingTest01
{
    public class MidiDeviceWatcher //: INotifyCollectionChanged
    {
        public enum MidiDeviceType { Input, Output };

        //public event NotifyCollectionChangedEventHandler CollectionChanged;

        //public DeviceInformationCollection DeviceInformationCollection { get; set; }

        public ObservableCollection<DeviceInformation> DeviceInformationList { get; }

        private DeviceInformationCollection m_deviceInformationCollection;
        private DeviceWatcher m_deviceWatcher;
        private string m_deviceSelectorString;
        //private ListBox deviceListBox;
        private CoreDispatcher m_coreDispatcher;
        private MidiDeviceType m_deviceType;

        public MidiDeviceWatcher(MidiDeviceType ioType, CoreDispatcher dispatcher)
        {
            this.DeviceInformationList = new ObservableCollection<DeviceInformation>();

            m_coreDispatcher = dispatcher;

            switch (ioType)
            {
                case MidiDeviceType.Input:
                    {
                        m_deviceSelectorString = MidiInPort.GetDeviceSelector();
                        break;
                    }
                case MidiDeviceType.Output:
                    {
                        m_deviceSelectorString = MidiOutPort.GetDeviceSelector();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            m_deviceWatcher = DeviceInformation.CreateWatcher(m_deviceSelectorString);
            m_deviceWatcher.Added += DeviceWatcher_Added;
            m_deviceWatcher.Removed += DeviceWatcher_Removed;
            m_deviceWatcher.Updated += DeviceWatcher_Updated;
            m_deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;

            m_deviceType = ioType;
        }

        ~MidiDeviceWatcher()
        {
            m_deviceWatcher.Added -= DeviceWatcher_Added;
            m_deviceWatcher.Removed -= DeviceWatcher_Removed;
            m_deviceWatcher.Updated -= DeviceWatcher_Updated;
            m_deviceWatcher.EnumerationCompleted -= DeviceWatcher_EnumerationCompleted;
            m_deviceWatcher = null;
        }

        public void StartWatcher()
        {
            m_deviceWatcher.Start();
        }
        public void StopWatcher()
        {
            m_deviceWatcher.Stop();
        }

        private async void UpdateDevices()
        {
            // Get a list of all MIDI devices
            this.m_deviceInformationCollection = await DeviceInformation.FindAllAsync(m_deviceSelectorString);

            //deviceListBox.Items.Clear();
            this.DeviceInformationList.Clear();

            if (!this.m_deviceInformationCollection.Any())
            {
                //deviceListBox.Items.Add("No audio devices found!");
            }

            if (MidiDeviceType.Input == m_deviceType)
            {
                //deviceListBox.Items.Add("Select a MIDI input device:");
            }
            else
            {
                //deviceListBox.Items.Add("Select a MIDI output device:");
            }

            foreach (var deviceInformation in this.m_deviceInformationCollection)
            {
                //deviceListBox.Items.Add(deviceInformation.Name);
                this.DeviceInformationList.Add(deviceInformation);
            }

        }

        private async void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            await m_coreDispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                // Update the device list
                UpdateDevices();
            });
        }

        private async void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            await m_coreDispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                // Update the device list
                UpdateDevices();
            });
        }

        private async void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object args)
        {
            await m_coreDispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                // Update the device list
                UpdateDevices();
            });
        }

        private async void DeviceWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            await m_coreDispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                // Update the device list
                UpdateDevices();
            });
        }
    }
}