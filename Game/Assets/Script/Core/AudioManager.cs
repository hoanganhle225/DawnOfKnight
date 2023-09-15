using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;



	void Awake()
	{
		
			instance = this;
			
		

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}
    public static void PlaySoundStatic(string name)
    {
        if (instance == null)
        {
            Debug.Log("No audio manager ");
            return;
        }
        instance.Play(name);
    }
    public static void stopLoop(string name)
    {
        if (instance == null)
        {
            Debug.Log("No audio manager ");
            return;
        }
        instance.Stoploop(name);
    }
    public void Stoploop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

		s.source.Stop();
        
    }
    public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

    
}
