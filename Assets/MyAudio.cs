using UnityEngine;
using System.Collections;

public class MyAudio : MonoBehaviour
{
    public static MyAudio Instance;
    private AudioSource Audio;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public static void PlayBackgroundMusic(AudioClip audioClip)
    {
        Instance.Audio.Stop();
        Instance.Audio.clip = audioClip;
        Instance.Audio.Play();
    }
	
    
}
