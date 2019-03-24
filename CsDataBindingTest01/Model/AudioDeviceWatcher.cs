using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Media.Devices;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace CsDataBindingTest01
{
    public class AudioDeviceWatcher //: INotifyCollectionChanged
    {
        public enum AudioDeviceType {Input, Output};

        //public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ObservableCollection<DeviceInformation> DeviceInformationList { get; }

        //public DeviceInformationCollection DeviceInformationCollection { get; set; }

        private DeviceInformationCollection m_deviceInformationCollection;
        private DeviceWatcher m_deviceWatcher;
        private string m_deviceSelectorString;
        //private ListBox deviceListBox;
        private CoreDispatcher m_coreDispatcher;
        private AudioDeviceType m_deviceType;

        public AudioDeviceWatcher(AudioDeviceType ioType, CoreDispatcher dispatcher)
        {
            this.DeviceInformationList = new ObservableCollection<DeviceInformation>();

            m_coreDispatcher = dispatcher;

            switch (ioType)
            {
                case AudioDeviceType.Input:
                    {
                        m_deviceSelectorString = MediaDevice.GetAudioCaptureSelector();
                        break;
                    }
                    
                case AudioDeviceType.Output:
                    {
                        m_deviceSelectorString = MediaDevice.GetAudioRenderSelector();
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

        ~AudioDeviceWatcher()
        {
            m_deviceWatcher.Added -= DeviceWatcher_Added;
            m_deviceWatcher.Removed -= DeviceWatcher_Removed;
            m_deviceWatcher.Updated -= DeviceWatcher_Updated;
            m_deviceWatcher.EnumerationCompleted -= DeviceWatcher_EnumerationCompleted;
            m_deviceWatcher = null;
        }

        //protected void OnCollectionChanged(string name)
        //{
        //    NotifyCollectionChangedEventHandler handler = CollectionChanged;
        //    if(handler != null)
        //    {
        //        handler(this, new NotifyCollectionChangedEventArgs() );
        //    }
        //}

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
            // Get a list of all Audio Input or Output devices
            m_deviceInformationCollection = await DeviceInformation.FindAllAsync(m_deviceSelectorString);

            //deviceListBox.Items.Clear();
            this.DeviceInformationList.Clear();

            if (!m_deviceInformationCollection.Any())
            {
                //deviceListBox.Items.Add("No audio devices found!");
            }

            if (AudioDeviceType.Input == m_deviceType)
            {
                //deviceListBox.Items.Add("Select an audio input device:");
            }
            else
            {
                //deviceListBox.Items.Add("Select an audio output device:");
            }

            foreach (var deviceInformation in m_deviceInformationCollection)
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