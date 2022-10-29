using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] soundEffects;
    public AudioSource audioSrc;

    public void playSoundEffect(int soundFileNum)
    {
        audioSrc.PlayOneShot (soundEffects[soundFileNum]);
    }
}
 