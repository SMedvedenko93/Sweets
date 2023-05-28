using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWindowPause : MonoBehaviour
{
    public GameObject Overlay;

    public GameObject BackToGame;
    public GameObject MenuButton;
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

        BackToGame.GetComponent<Button>().onClick.AddListener(BackToGameOnClick);
        MenuButton.GetComponent<Button>().onClick.AddListener(MenuButtonOnClick);
        ReplayButton.GetComponent<Button>().onClick.AddListener(ReplayButtonOnClick);
    }

    void BackToGameOnClick()
    {
        Overlay.SetActive(false);
        gameObject.SetActive(false);
        GameController.Enemy.GetComponent<Animator>().enabled = true;
        GameController.gamePause = false;
    }

    void MenuButtonOnClick()
    {
        GameManager.SoundManager.GetComponent<SoundManager>().StartMusicMenu();
        Overlay.SetActive(false);
        gameObject.SetActive(false);
        ScreenManager.instance.ShowLocationListScreen();
    }

    void ReplayButtonOnClick()
    {
        GameManager.GameWindowsManager.GetComponent<GameWindowsManager>().BlockScreen();
        GameController.gamePause = false;
        GameManager.ReplayLevel();
    }
}
