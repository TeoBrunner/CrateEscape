using UnityEngine;

public class AudioView : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sFXSource;

    public AudioSource MusicSource => musicSource;
    public AudioSource SFXSource => sFXSource;
}
