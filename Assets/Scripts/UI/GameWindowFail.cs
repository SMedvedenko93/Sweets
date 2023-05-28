using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWindowFail : MonoBehaviour
{
    public GameObject Overlay;

    public GameObject SkipButton;
    public GameObject ReplayButton;

    private GameManager GameManager;
    private GameController GameController;
    private AdsInitializer AdsManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameManager.instance.GetComponent<GameManager>();
        GameController = GameManager.instance.GetComponent<GameManager>().GameController;
        AdsManager = GameManager.instance.GetComponent<GameManager>().AdsManager.GetComponent<AdsInitializer>();

        SkipButton.GetComponent<Button>().onClick.AddListener(SkipButtonOnClick);
        ReplayButton.GetComponent<Button>().onClick.AddListener(ReplayButtonOnClick);
    }

    private void ReplayButtonOnClick()
    {
        GameManager.GameWindowsManager.GetComponent<GameWindowsManager>().BlockScreen();
        GameManager.ReplayLevel();
    }

    private void SkipButtonOnClick()
    {
        GameManager.GameWindowsManager.GetComponent<GameWindowsManager>().BlockScreen();
        AdsManager.ShowRewardedAds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
