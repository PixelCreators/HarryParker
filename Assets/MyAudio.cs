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
        Audio = GetComponent<AudioSource>();
    }

    public static void PlayBackgroundMusic(AudioClip audioClip)
    {
        Debug.Log("Play");
        Instance.Audio.Stop();
        Debug.Log(audioClip.name);
        Instance.Audio.clip = audioClip;
        Instance.Audio.Play();
    }
	
    
}
