using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource music;

    public AudioClip backgroundMenu;
    public AudioClip backgroundGame;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartMusicGame()
    {
        if (music.clip != backgroundGame)
        {
            music.clip = backgroundGame;
            if (music.mute == false)
            {
                music.Play();
            }
        }

    }

    public void StartMusicMenu()
    {
        if (music.clip != backgroundMenu)
        {
            music.clip = backgroundMenu;
            if (music.mute == false)
            {
                music.Play();
            }
        }
    }

    public void Mute()
    {
        Debug.Log("Mute misuc");
        music.mute = true;
    }

    public void UnMute()
    {
        Debug.Log("UnMute misuc");
        music.mute = false;
        music.Play();
    }


}
