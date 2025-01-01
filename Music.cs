using NAudio.Wave;
using System;

public class Music
{
    private WaveOutEvent waveOutEvent;
    private AudioFileReader audioFileReader;

    public void PlayMusic(string filepath)
    {    
        if (waveOutEvent == null)
        {
            waveOutEvent = new WaveOutEvent();
            audioFileReader = new AudioFileReader(filepath);
            waveOutEvent.Init(audioFileReader);
            waveOutEvent.Play();
        }
    }
    public void StopMusic()
    {
        if (waveOutEvent != null)
        {
            waveOutEvent.Stop();
            waveOutEvent.Dispose();
            waveOutEvent = null;
            audioFileReader = null;
            Console.WriteLine("Musique arrêtée.");
        }
    }
}
