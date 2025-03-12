using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {  get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;
    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        musicSource =transform.GetChild(0).GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        ChangeSoundVolume(0);
        ChangeMusicVolume(0);

    }
    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }
    public void ChangeSoundVolume(float _volume)
    {
        ChangeSourceVolume(1f,"SoundVolume", _volume, soundSource);
    }

    public void ChangeMusicVolume(float _volume)
    {
        ChangeSourceVolume(0.6f,"MusicVolume",_volume,musicSource);
    }
    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource audioSource)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }
        float finalVolume = currentVolume * baseVolume;
        audioSource.volume = finalVolume;
        PlayerPrefs.SetFloat (volumeName, currentVolume);
    }
}
