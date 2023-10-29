using UnityEngine.Audio;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] footstepSounds;
    public Sound[] jumpingSounds;
    public Sound[] landingSounds;

    // Singleton design pattern
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound s in footstepSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }

        foreach (Sound s in jumpingSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }

        foreach (Sound s in landingSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }
    }

    private void Start()
    {
        Play("Background Noise");
    }

    public void Play(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("No sound with name \"" + name + "\"found.");
            return;
        }
        s.source.Play();
    }

    public void PlayFootsteps()
    {
        Sound s = footstepSounds[Random.Range(0, footstepSounds.Count())];
        s.source.volume = Random.Range(0.3f, 0.5f);
        s.source.pitch = Random.Range(0.8f, 1.2f);
        s.source.Play();
    }

    public void PlayJumping()
    {
        Sound s = jumpingSounds[Random.Range(0, jumpingSounds.Count())];
        s.source.volume = Random.Range(0.3f, 0.5f);
        s.source.pitch = Random.Range(0.8f, 1.2f);
        s.source.Play();
    }

    public void PlayLanding()
    {
        Sound s = landingSounds[Random.Range(0, landingSounds.Count())];
        s.source.volume = Random.Range(0.3f, 0.5f);
        s.source.pitch = Random.Range(0.8f, 1.2f);
        s.source.Play();
    }
}
