using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int showAdsCount = 5;
    public static GameManager instance = null;

    public GameObject SoundManager;
    public GameObject GameWindowsManager;
    public GameObject AdsManager;
    public GameObject PurchaseManager;
    public GameController GameController;
    public GameObject Level;
    public LevelParser LevelParser;
    public WelcomeController WelcomeController;

    private string CurrentLevel, CurrentLocation, CountHint, Advert, GameCount, Welcome;

    private const string KEYSALT = "gameSaltCTSweet";
    private const string CURRENTLOCATION = "CurrentLocation";
    private const string CURRENTLEVEL = "CurrentLevel";
    private const string ADS = "Ads";
    private const string COUNTHINT = "CountHint";
    private const string GAMECOUNT = "GameCount";
    private const string WELCOME = "Welcome";

    void Awake()
    {
        //If this script does not exit already, use this current instance
        if (instance == null)
            instance = this;

        //If this script already exit, DESTROY this current instance
        else if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelParser.Parse();


        //CurrentLocation = Crypto.Encrypt("2", KEYSALT);
        //PlayerPrefs.SetString(CURRENTLOCATION, CurrentLocation);
        //PlayerPrefs.Save();

        //CurrentLevel = Crypto.Encrypt("1", KEYSALT);
        //PlayerPrefs.SetString(CURRENTLEVEL, CurrentLevel);
        //PlayerPrefs.Save();


        //PlayerPrefs.DeleteAll();

        // welcome screen
        if (!PlayerPrefs.HasKey(WELCOME))
        {
            Welcome = Crypto.Encrypt("0", KEYSALT);
            PlayerPrefs.SetString(WELCOME, Welcome);
            PlayerPrefs.Save();
        }
        else
        {
            Welcome = PlayerPrefs.GetString(WELCOME);
        }

        // ads
        if (!PlayerPrefs.HasKey(ADS))
        {
            var ads = Crypto.Encrypt("show", KEYSALT);
            PlayerPrefs.SetString(ADS, ads);
            PlayerPrefs.Save();
        }


        // Current Level
        if (!PlayerPrefs.HasKey(CURRENTLOCATION))
        {
            CurrentLocation = Crypto.Encrypt("1", KEYSALT);
            PlayerPrefs.SetString(CURRENTLOCATION, CurrentLocation);
            PlayerPrefs.Save();
        }
        else
        {
            CurrentLocation = PlayerPrefs.GetString(CURRENTLOCATION);
        }


        // Current Level
        if (!PlayerPrefs.HasKey(CURRENTLEVEL))
        {
            CurrentLevel = Crypto.Encrypt("1", KEYSALT);
            PlayerPrefs.SetString(CURRENTLEVEL, CurrentLevel);
            PlayerPrefs.Save();
        } else
        {
            CurrentLevel = PlayerPrefs.GetString(CURRENTLEVEL);
        }

        //count Hint
        if (!PlayerPrefs.HasKey(COUNTHINT))
        {
            CountHint = Crypto.Encrypt("3", KEYSALT);
            PlayerPrefs.SetString(COUNTHINT, CountHint);
            PlayerPrefs.Save();
        }
        else
        {
            CountHint = PlayerPrefs.GetString(COUNTHINT);
        }

        //music setting
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetInt("Music", 1);
            PlayerPrefs.Save();
        }

        //sfx setting
        if (!PlayerPrefs.HasKey("SFX"))
        {
            PlayerPrefs.SetInt("SFX", 1);
            PlayerPrefs.Save();
        }

        //game counter
        if (!PlayerPrefs.HasKey(GAMECOUNT))
        {
            GameCount = Crypto.Encrypt("1", KEYSALT);
            PlayerPrefs.SetString(GAMECOUNT, GameCount);
            PlayerPrefs.Save();
        }
    }

    public int getCurrentLocation()
    {
        return int.Parse(Crypto.Decrypt(PlayerPrefs.GetString(CURRENTLOCATION), KEYSALT));
    }

    public int getCurrentLevel()
    {
        return int.Parse(Crypto.Decrypt(PlayerPrefs.GetString(CURRENTLEVEL), KEYSALT));
    }

    public void removeAds()
    {
        var ads = Crypto.Encrypt("hide", KEYSALT);
        PlayerPrefs.SetString(ADS, ads);
        PlayerPrefs.Save();
    }

    public bool checkShowAds()
    {
        var ads = Crypto.Decrypt(PlayerPrefs.GetString(ADS), KEYSALT);
        if (ads == "hide")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public string getCountHint()
    {
        return Crypto.Decrypt(CountHint, KEYSALT);
    }


    public void UseHint()
    {
        CountHint = PlayerPrefs.GetString(COUNTHINT);
        CountHint = Crypto.Decrypt(CountHint, KEYSALT);

        var newCountHint = int.Parse(CountHint) - 1;
        CountHint = Crypto.Encrypt(newCountHint.ToString(), KEYSALT);

        PlayerPrefs.SetString(COUNTHINT, CountHint);
        PlayerPrefs.Save();
    }

    public void ChangeHint(int count)
    {
        CountHint = PlayerPrefs.GetString(COUNTHINT);
        CountHint = Crypto.Decrypt(CountHint, KEYSALT);

        var newCountHint = int.Parse(CountHint) + count;
        CountHint = Crypto.Encrypt(newCountHint.ToString(), KEYSALT);

        PlayerPrefs.SetString(COUNTHINT, CountHint);
        PlayerPrefs.Save();

        GameController.GetComponent<GameScreen>().HintValue.GetComponent<Text>().text = newCountHint.ToString();
    }

    public void changeGameCounter()
    {
        int newGameCount = int.Parse(Crypto.Decrypt(PlayerPrefs.GetString(GAMECOUNT), KEYSALT)) + 1;
        if (newGameCount > showAdsCount)
        {
            newGameCount = 1;
        }
        var GameCount = Crypto.Encrypt(newGameCount.ToString(), KEYSALT);
        PlayerPrefs.SetString(GAMECOUNT, GameCount);
        PlayerPrefs.Save();
    }

    public int getGameCount()
    {
        return int.Parse(Crypto.Decrypt(PlayerPrefs.GetString(GAMECOUNT), KEYSALT));
    }

    public void UpdateLevelLoadingGame() {

        int location = GameController.LoadingLevelScreen.GetComponent<LoadingLevelScreen>().location;
        int level = GameController.LoadingLevelScreen.GetComponent<LoadingLevelScreen>().level;

        if (level + 1 > LevelParser.getLevelList()[location - 1].levelCount)
        {
            location = location + 1;
            level = 1;
            GameController.LoadingLevelScreen.GetComponent<LoadingLevelScreen>().location = location;
            GameController.LoadingLevelScreen.GetComponent<LoadingLevelScreen>().level = level;
        }
        else
        {
            level = level + 1;
            GameController.LoadingLevelScreen.GetComponent<LoadingLevelScreen>().level = level;
        }
    }

    public void SaveLevel(string type) // record to Prefs
    {
        if (type == "next")
        {
            changeGameCounter();
        }

        int location = GameController.LoadingLevelScreen.GetComponent<LoadingLevelScreen>().location;
        int level = GameController.LoadingLevelScreen.GetComponent<LoadingLevelScreen>().level;

        if (level + 1 > LevelParser.getLevelList()[location - 1].levelCount)
        {
            location = location + 1;
            level = 1;
        }
        else
        {
            level = level + 1;
        }

        if (PlayerPrefs.HasKey(CURRENTLOCATION))
        {
            int CurrentLocation = int.Parse(Crypto.Decrypt(PlayerPrefs.GetString(CURRENTLOCATION), KEYSALT));
            if (CurrentLocation < location)
            {
                var NewLocation = Crypto.Encrypt(location.ToString(), KEYSALT);
                PlayerPrefs.SetString(CURRENTLOCATION, NewLocation);
                PlayerPrefs.Save();

                var NewLevel = Crypto.Encrypt("1", KEYSALT);
                PlayerPrefs.SetString(CURRENTLEVEL, NewLevel);
                PlayerPrefs.Save();
            }
            else
            {
                int CurrentLevel = int.Parse(Crypto.Decrypt(PlayerPrefs.GetString(CURRENTLEVEL), KEYSALT));
                if (CurrentLevel < level)
                {
                    var NewLevel = Crypto.Encrypt(level.ToString(), KEYSALT);
                    PlayerPrefs.SetString(CURRENTLEVEL, NewLevel);
                    PlayerPrefs.Save();
                }
            }
        }
    }

    public void LoadingLevel()
    {
        GameWindowsManager.GetComponent<GameWindowsManager>().Overlay.SetActive(false);
        foreach (Transform child in GameWindowsManager.GetComponent<GameWindowsManager>().Overlay.transform)
        {
            child.gameObject.SetActive(false);
        }
        AdsManager.GetComponent<AdsInitializer>().LoadInterstitialAds();
        ScreenManager.instance.ShowLoadingLevelScreen();
    }

    public void ReplayLevel()
    {
        changeGameCounter();
        if ((getGameCount() >= showAdsCount) && checkShowAds())
        {
            AdsManager.GetComponent<AdsInitializer>().ShowInterstitialAds();
        }
        else
        {
            LoadingLevel();
        }
    }


    public void NextLevel()
    {
        UpdateLevelLoadingGame();
        changeGameCounter();
        if ((getGameCount() >= showAdsCount) && checkShowAds())
        {
            AdsManager.GetComponent<AdsInitializer>().ShowInterstitialAds();
        }
        else
        {
            LoadingLevel();
        }
    }

    public void SkipLevel()
    {
        SaveLevel("skip");
        UpdateLevelLoadingGame();

        LoadingLevel();
    }

    public int checkWelcome()
    {
        return int.Parse(Crypto.Decrypt(PlayerPrefs.GetString(WELCOME), KEYSALT));
    }


    public void ShowWelcome()
    {
        WelcomeController.gameObject.SetActive(true);
    }

    public void ChangeWelcome()
    {
        Welcome = Crypto.Encrypt("1", KEYSALT);
        PlayerPrefs.SetString(WELCOME, Welcome);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
