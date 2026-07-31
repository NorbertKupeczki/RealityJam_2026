using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    private AudioSource m_AudioSource;

    protected override void Awake()
    {
        base.Awake();
        m_AudioSource = GetComponent<AudioSource>();
    }

    public static void PlayOneShotAudio(AudioClip clip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(clip, position);
    }
}
