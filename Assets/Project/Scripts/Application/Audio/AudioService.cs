using UnityEngine;

public class AudioService : IAudioService
{
    private readonly AudioDatabase database;
    private readonly AudioSource musicSource;
    private readonly AudioSource sFXSource;
    public AudioService(AudioDatabase database, AudioSource musicSource, AudioSource sFXSource)
    {
        this.database = database;
        this.musicSource = musicSource;
        this.sFXSource = sFXSource;
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