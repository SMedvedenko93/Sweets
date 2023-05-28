using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;

    public GameObject LoadingGameScreen;
    public GameObject MainScreen;
    public GameObject LocationListScreen;
    public GameObject LoadingLevelScreen;
    public GameObject GameScreen;

    private GameManager GameManager;
    private GameObject currentScreen;

    void Awake()
    {
            instance = this;
    }

    private void Start()
    {
        GameManager = GameManager.instance.GetComponent<GameManager>();

        if (GameManager.checkWelcome() != 1)
        {
            GameManager.ShowWelcome();
            GameManager.GameController.LoadingLevelScreen.GetComponent<LoadingLevelScreen>().location = 1;
            GameManager.GameController.LoadingLevelScreen.GetComponent<LoadingLevelScreen>().level = 1;
            currentScreen = LoadingLevelScreen;
        }
        else
        {
            currentScreen = LoadingGameScreen;
        }

        ShowScreen(currentScreen);
    }

    public void ShowScreen(GameObject screen)
    {
        currentScreen.SetActive(false);
        currentScreen = screen;
        currentScreen.SetActive(true);
    }

    public void ShowLoadingGameScreen()
    {
        ShowScreen(LoadingGameScreen);
    }

    public void ShowMainScreen()
    {
        ShowScreen(MainScreen);
    }

    public void ShowLoadingLevelScreen()
    {
        ShowScreen(LoadingLevelScreen);
    }

    public void ShowGameScreen()
    {
        ShowScreen(GameScreen);
    }

    public void ShowLocationListScreen()
    {
        ShowScreen(LocationListScreen);
    }

    
}
