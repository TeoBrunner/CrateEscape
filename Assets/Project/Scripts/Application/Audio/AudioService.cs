using UnityEngine;

public class AudioService : IAudioService
{
    private readonly AudioDatabase database;
    private readonly AudioSource musicSource;
    private readonly AudioSource sFXSource;
    public AudioService(AudioDatabase database, AudioView AudioViewPrefab)
    {
        this.database = database;
        this.musicSource = AudioViewPrefab.MusicSource;
        this.sFXSource = AudioViewPrefab.SFXSource;
    }
    public void PlayMusic(string id)
    {
        AudioClip clip = database.GetAudioClip(id);
        musicSource.loop = true;
        musicSource.PlayOneShot(clip);
    }

    public void PlaySound(string id)
    {
        AudioClip clip = database.GetAudioClip(id);
        sFXSource.loop = false;
        sFXSource.PlayOneShot(clip);
    }
}