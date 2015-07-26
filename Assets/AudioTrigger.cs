using UnityEngine;
using System.Collections;

public class AudioTrigger : MonoBehaviour
{

    public AudioClip Clip;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            MyAudio.PlayBackgroundMusic(Clip);
    }
}
