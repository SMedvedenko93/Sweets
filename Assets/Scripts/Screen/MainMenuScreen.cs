using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{

    public GameObject PlayButton;
    public GameObject ShopButton;
    public GameObject SettingsButton;

    private GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameManager.instance.GetComponent<GameManager>();
        GameManager.SoundManager.GetComponent<SoundManager>().StartMusicMenu();

        PlayButton.GetComponent<Button>().onClick.AddListener(PlayButtonOnClick);
        ShopButton.GetComponent<Button>().onClick.AddListener(ShopButtonOnClick);
        SettingsButton.GetComponent<Button>().onClick.AddListener(SettingsButtonOnClick);
    }

    void PlayButtonOnClick()
    {
        //GameManager.AdsManager.GetComponent<AdsInitializer>().ShowInterstitialAds();
        ScreenManager.instance.ShowLocationListScreen();
    }

    void ShopButtonOnClick()
    {
        //GameManager.AdsManager.GetComponent<AdsInitializer>().ShowRewardedAds();
        GameManager.GameWindowsManager.GetComponent<GameWindowsManager>().windowShop();
    }

    void SettingsButtonOnClick()
    {
        GameManager.GameWindowsManager.GetComponent<GameWindowsManager>().windowSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
