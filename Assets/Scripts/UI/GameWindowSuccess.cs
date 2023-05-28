using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWindowSuccess : MonoBehaviour
{

    public GameObject Overlay;

    public GameObject NextButton;
    public GameObject ReplayButton;

    private int location;
    private int level;

    private GameManager GameManager;
    private GameController GameController;
    private AdsInitializer AdsManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameManager.instance.GetComponent<GameManager>();
        GameController = GameManager.instance.GetComponent<GameManager>().GameController;
        AdsManager = GameManager.instance.GetComponent<GameManager>().AdsManager.GetComponent<AdsInitializer>();

        NextButton.GetComponent<Button>().onClick.AddListener(NextButtonOnClick);
        ReplayButton.GetComponent<Button>().onClick.AddListener(ReplayButtonOnClick);
    }

    void ReplayButtonOnClick()
    {
        GameManager.GameWindowsManager.GetComponent<GameWindowsManager>().BlockScreen();
        GameManager.ReplayLevel();
    }

    void NextButtonOnClick()
    {
        Debug.Log("NEXTNEXT");
        GameManager.GameWindowsManager.GetComponent<GameWindowsManager>().BlockScreen();
        GameManager.NextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
