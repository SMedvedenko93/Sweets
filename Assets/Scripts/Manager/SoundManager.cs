using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject MusicManagerSource;
    public GameObject SFXManagerSource;

    private SFXManager SFXManager;
    private MusicManager MusicManager;


    // Start is called before the first frame update
    void Awake()
    {
        MusicManager = MusicManagerSource.GetComponent<MusicManager>();
        SFXManager = SFXManagerSource.GetComponent<SFXManager>();

        if (getMusic() == 0)
        {
            MusicManager.Mute();
        }

    }

    public void SwitchMusic()
    {
        if (getMusic() == 1)
        {
            setMusic(0);
        }
        else
        {
            setMusic(1);
        }
    }

    public void SwitchSFX()
    {
        if (getSFX() == 1)
        {
            setSFX(0);
        }
        else
        {
            setSFX(1);
        }
    }

    public int getMusic()
    {
        return PlayerPrefs.GetInt("Music");
    }

    public int getSFX()
    {
        return PlayerPrefs.GetInt("SFX");
    }

    public void setMusic(int flag)
    {
        if (flag == 1)
        {
            UnMuteMusic();
            PlayerPrefs.SetInt("Music", 1);
            PlayerPrefs.Save();
        }
        else
        {
            MuteMusic();
            PlayerPrefs.SetInt("Music", 0);
            PlayerPrefs.Save();
        }
    }

    public void setSFX(int flag)
    {
        if (flag == 1)
        {
            UnMuteSFX();
            PlayerPrefs.SetInt("SFX", 1);
            PlayerPrefs.Save();
        }
        else
        {
            MuteSFX();
            PlayerPrefs.SetInt("SFX", 0);
            PlayerPrefs.Save();
        }
    }

    public void StartMusicMenu()
    {
        MusicManager.StartMusicMenu();
    }

    public void StartMusicGame()
    {
        MusicManager.StartMusicGame();
    }

    public void ButtonClickAudio()
    {
        SFXManager.ButtonClickAudio();
    }

    public void UnMuteMusic()
    {
        MusicManager.UnMute();
    }

    public void MuteMusic()
    {
        MusicManager.Mute();
    }

    public void UnMuteSFX()
    {
        SFXManager.UnMute();
    }

    public void MuteSFX()
    {
        SFXManager.Mute();
    }


}
