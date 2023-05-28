using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    public AudioSource sfx;
    public AudioClip buttonClick, tileClick, spring, complete;

    public void ButtonClickAudio()
    {
        sfx.PlayOneShot(buttonClick);
    }

    public void TileClickAudio()
    {
        sfx.PlayOneShot(tileClick);
    }

    public void SpringAudio()
    {
        sfx.PlayOneShot(spring);
    }

    public void CompleteAudio()
    {
        sfx.PlayOneShot(complete);
    }


    public void Mute()
    {
        sfx.mute = true;
    }

    public void UnMute()
    {
        sfx.mute = false;
    }
}
