using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowSettings : MonoBehaviour
{
    public GameObject Overlay;

    public GameObject MusicRadio;
    public GameObject MusicRadioButton;

    public GameObject SFXRadio;
    public GameObject SFXRadioButton;

    public GameObject RemoveAdsButton;
    public GameObject TermsButton;

    public GameObject CloseButton;

    public Texture noActiveRadio, activeRadio;

    private GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameManager.instance.GetComponent<GameManager>();

        if (GameManager.SoundManager.GetComponent<SoundManager>().getMusic() == 1)
        {
            MusicRadioButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(60, MusicRadioButton.transform.localPosition.y, MusicRadioButton.transform.localPosition.z);
            MusicRadioButton.GetComponent<RawImage>().texture = activeRadio;
        }
        else
        {
            MusicRadioButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(10, MusicRadioButton.transform.localPosition.y, MusicRadioButton.transform.localPosition.z);
            MusicRadioButton.GetComponent<RawImage>().texture = noActiveRadio;
        }

        if (GameManager.SoundManager.GetComponent<SoundManager>().getSFX() == 1)
        {
            SFXRadioButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(60, SFXRadioButton.transform.localPosition.y, SFXRadioButton.transform.localPosition.z);
            SFXRadioButton.GetComponent<RawImage>().texture = activeRadio;

        }
        else
        {
            SFXRadioButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(10, SFXRadioButton.transform.localPosition.y, SFXRadioButton.transform.localPosition.z);
            SFXRadioButton.GetComponent<RawImage>().texture = noActiveRadio;
        }

        MusicRadio.GetComponent<Button>().onClick.AddListener(MusicRadioOnClick);
        SFXRadio.GetComponent<Button>().onClick.AddListener(SFXRadioOnClick);

        RemoveAdsButton.GetComponent<Button>().onClick.AddListener(RemoveAdsButtonOnClick);
        TermsButton.GetComponent<Button>().onClick.AddListener(TermsButtonOnClick);

        CloseButton.GetComponent<Button>().onClick.AddListener(CloseButtonOnClick);


    }

    private void CloseButtonOnClick()
    {
        Overlay.SetActive(false);
        gameObject.SetActive(false);
    }

    private void TermsButtonOnClick()
    {
        Debug.Log(111111);
        Application.OpenURL("http://ya.ru");
    }

    private void RemoveAdsButtonOnClick()
    {

    }

    private void MusicRadioOnClick()
    {
        GameManager.SoundManager.GetComponent<SoundManager>().ButtonClickAudio();
        if (GameManager.SoundManager.GetComponent<SoundManager>().getMusic() == 1)
        {
            MusicRadioButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(10, MusicRadioButton.transform.localPosition.y, MusicRadioButton.transform.localPosition.z);
            MusicRadioButton.GetComponent<RawImage>().texture = noActiveRadio;
        }
        else
        {
            MusicRadioButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(60, MusicRadioButton.transform.localPosition.y, MusicRadioButton.transform.localPosition.z);
            MusicRadioButton.GetComponent<RawImage>().texture = activeRadio;
        }
        GameManager.SoundManager.GetComponent<SoundManager>().SwitchMusic();
    }

    private void SFXRadioOnClick()
    {
        GameManager.SoundManager.GetComponent<SoundManager>().ButtonClickAudio();

        if (GameManager.SoundManager.GetComponent<SoundManager>().getSFX() == 1)
        {

            SFXRadioButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(10, SFXRadioButton.transform.localPosition.y, SFXRadioButton.transform.localPosition.z);
            SFXRadioButton.GetComponent<RawImage>().texture = noActiveRadio;
        }
        else
        {

            SFXRadioButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(60, SFXRadioButton.transform.localPosition.y, SFXRadioButton.transform.localPosition.z);
            SFXRadioButton.GetComponent<RawImage>().texture = activeRadio;
        }
        GameManager.SoundManager.GetComponent<SoundManager>().SwitchSFX();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
