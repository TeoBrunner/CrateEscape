using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioDatabase", menuName = "Configs/AudioDatabase")]

public class AudioDatabase : ScriptableObject
{
    [SerializeField] List<AudioClip> clips = new();
    public AudioClip GetAudioClip(string id)
    {
        if (clips.Count == 0)
        {
            Debug.Log("Audio database is empty!");
            return null;
        }

        AudioClip clip = clips.FirstOrDefault(a => a.name == id);
        if (clip == null)
        {
            Debug.Log("Audio database does not contain" + id + "!");
        }

        return clip;
    }
}