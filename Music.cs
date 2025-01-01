using NAudio.Wave;

public class Music
{
    private WaveOutEvent waveOutEvent;
    private AudioFileReader audioFileReader;
    
    public void PlayMusic(string filepath)
    {
        waveOutEvent = new WaveOutEvent();  // Crée un lecteur audio
        audioFileReader = new AudioFileReader(filepath); // Charge le fichier audio

        waveOutEvent.Init(audioFileReader);  // Initialise le lecteur avec le fichier audio
        waveOutEvent.Play(); // Joue le fichier audio en boucle

        // Pour jouer en boucle, réinitialisez le lecteur chaque fois qu'il termine
        waveOutEvent.PlaybackStopped += (sender, args) =>
        {
            audioFileReader.Position = 0;  // Réinitialise la position du fichier
            waveOutEvent.Play();  // Rejoue la musique en boucle
        };
    }
}
