using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWindowsManager : MonoBehaviour
{

    public GameObject Overlay;
    public GameObject GameWindowPause;
    public GameObject GameWindowSuccess;
    public GameObject GameWindowFail;
    public GameObject WindowShop;
    public GameObject WindowSettings;

    public GameObject BlockPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BlockScreen()
    {
        BlockPanel.SetActive(true);
    }

    public void UnBlockScreen()
    {
        BlockPanel.SetActive(false);
    }

    public void pauseLevel()
    {
        Overlay.SetActive(true);
        GameWindowPause.SetActive(true);
        //Time.timeScale = 0;
    }

    public void failLevel()
    {
        StartCoroutine(failLevelCoroutine());
    }

    public void successLevel()
    {
        StartCoroutine(successLevelCoroutine());
    }

    public void windowShop()
    {
        Overlay.SetActive(true);
        WindowShop.SetActive(true);
    }

    public void windowSettings()
    {
        Overlay.SetActive(true);
        WindowSettings.SetActive(true);
    }


    IEnumerator failLevelCoroutine()
    {
        yield return new WaitForSeconds(2);
        Overlay.SetActive(true);
        GameWindowFail.SetActive(true);
    }

    IEnumerator successLevelCoroutine()
    {
        yield return new WaitForSeconds(4);
        GameManager.instance.GetComponent<GameManager>().SoundManager.GetComponent<SoundManager>().SFXManagerSource.GetComponent<SFXManager>().CompleteAudio();
        Overlay.SetActive(true);
        GameWindowSuccess.SetActive(true);
    }
}