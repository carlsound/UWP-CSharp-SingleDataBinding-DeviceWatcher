using System;
using System.ComponentModel;
using System.Diagnostics;

public class Audio: INotifyPropertyChanged
{
    private double m_volume;
    public double Volume
    {
        get { return m_volume; }
        set
        {
            m_volume = value;
            // Call OnPropertyChanged whenever the property is updated
            OnPropertyChanged("Volume");
            //
            //Debug.WriteLine("Volume = ");
            //Debug.Write(volume);
            //Debug.WriteLine("\n");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public Audio()
	{
        this.Volume = 50.0;
    }

    public Audio(double volumeValue)
    {
        this.Volume = volumeValue;
    }

    // Create the OnPropertyChanged method to raise the event
    protected void OnPropertyChanged(string name)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
