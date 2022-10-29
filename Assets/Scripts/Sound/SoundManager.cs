using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] soundEffects;
    public AudioSource audioSrc;

    private GameObject RestartScreen;

    public void playSoundEffect(int soundFileNum, bool IsDynamic=false)
    {
        RestartScreen = GameObject.Find("Restart Background");
        if(RestartScreen!=null){
            if(soundFileNum<=1 && IsDynamic==false){
                audioSrc.PlayOneShot (soundEffects[soundFileNum]);
            }
        }else{
            audioSrc.PlayOneShot (soundEffects[soundFileNum]);
        }
    }
}
 