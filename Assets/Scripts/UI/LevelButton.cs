using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int location;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LocationItemButtonOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LocationItemButtonOnClick()
    {
        var location = GetComponent<LevelButton>().location;
        var level = GetComponent<LevelButton>().level;

        GameManager.instance.GetComponent<GameManager>().GameController.LoadingLevelScreen.GetComponent<LoadingLevelScreen>().location = location;
        GameManager.instance.GetComponent<GameManager>().GameController.LoadingLevelScreen.GetComponent<LoadingLevelScreen>().level = level;

        ScreenManager.instance.ShowLoadingLevelScreen();
    }

}
