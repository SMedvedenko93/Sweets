using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocationListScreen : MonoBehaviour
{
    public GameObject BackButton;

    public GameObject LocationListWrapper;
    public GameObject LocationListContent;

    public GameObject[] Locations;
    public GameObject SnapCenter;

    private float[] distance;
    private bool dragging = false;
    private int locationDistance;
    private int minLocationNum;

    public GameObject LevelButtonPrefab;
    public GameObject LocationPrefab;

    private float width;
    private float height;

    float posX;

    float posXButton, posYButton;

    private GameManager GameManager;
    private GameObject[] LineList;

    private int currentLocation, currentLevel;
    private int count, countButton, countLevel, countLocation;


    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("OnEnable");

       foreach (Transform child in LocationListContent.transform)
       {
            Destroy(child.gameObject);
       }

        GameManager = GameManager.instance.GetComponent<GameManager>();

        currentLocation = GameManager.instance.GetComponent<GameManager>().getCurrentLocation();
        currentLevel = GameManager.instance.GetComponent<GameManager>().getCurrentLevel();

        countLocation = 1;
        countLevel = 1;
        count = 0;
        countButton = 0;

        BackButton.GetComponent<Button>().onClick.AddListener(BackButtonOnClick);
        width = Screen.width;
        width = LocationListWrapper.GetComponent<RectTransform>().rect.width;

        height = Screen.height;
        height = LocationListWrapper.GetComponent<RectTransform>().rect.height;

        Locations = new GameObject[GameManager.LevelParser.getLevelList().Count];

        foreach (Location Location in GameManager.LevelParser.getLevelList())
        {
            var index = -1;
            //countLevel = 1;

            if (count == 0)
            {
                posX = 0;
            }
            else
            {
                posX = (count * width);
            }

            GameObject LocationItem = Instantiate(LocationPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            if (countLocation <= currentLocation)
            {
                LocationItem.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/levels/" + Location.name + "Level");
            }
            else
            {
                LocationItem.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/levels/" + Location.name + "LevelClose");
            }
                
            LineList = new GameObject[5];
            LineList[0] = LocationItem.transform.GetChild(1).gameObject;
            LineList[1] = LocationItem.transform.GetChild(2).gameObject;
            LineList[2] = LocationItem.transform.GetChild(3).gameObject;
            LineList[3] = LocationItem.transform.GetChild(4).gameObject;
            LineList[4] = LocationItem.transform.GetChild(5).gameObject;


            LocationItem.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            LocationItem.transform.SetParent(LocationListContent.transform);

            LocationItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, -100);
            LocationItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            /*
            if (countLocation <= int.Parse(currentLocation))
            {
                LocationItem.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = Location.name;
            } 
            else
            {
                LocationItem.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Locked";
            }
            */

            posXButton = 30;
            posYButton = -500;

            foreach (Level Level in Location.levelList)
            {
                if (countButton % 5 == 0)
                {
                    index++;
                    //posYButton = posYButton - 210;
                    countButton = 0;
                }

                if (countButton == 0)
                {
                    posXButton = 30;
                }
                else
                {
                    posXButton = (countButton * 180) + 30 * (countButton + 1);
                }

                GameObject LevelItem;
                LevelItem = Instantiate(LevelButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);

                //LevelItem.transform.SetParent(LocationItem.transform);
                LevelItem.transform.SetParent(LineList[index].transform);

                LevelItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(posXButton, posYButton);
                LevelItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                LevelItem.GetComponent<LevelButton>().location = count + 1;
                LevelItem.GetComponent<LevelButton>().level = countLevel;

                if (countLocation < currentLocation)
                {
                    var countLevelVal = "";

                    if (countLevel < 10)
                    {
                        countLevelVal = "0" + countLevel;
                    }
                    else
                    {
                        countLevelVal = countLevel.ToString();
                    }

                    LevelItem.transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().text = countLevelVal;
                    LevelItem.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = countLevelVal;
                    LevelItem.transform.GetChild(4).gameObject.SetActive(false);
                }
                else if (countLocation == currentLocation)
                {
                    if (countLevel <= currentLevel)
                    {
                        var countLevelVal = "";

                        if (countLevel < 10)
                        {
                            countLevelVal = "0" + countLevel;
                        }
                        else
                        {
                            countLevelVal = countLevel.ToString();
                        }

                        LevelItem.transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().text = countLevelVal;
                        LevelItem.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = countLevelVal;
                        LevelItem.transform.GetChild(4).gameObject.SetActive(false);
                    }
                    else
                    {
                        LevelItem.GetComponent<Button>().interactable = false;
                        LevelItem.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "";
                        LevelItem.transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().text = "";
                        LevelItem.transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = "";
                        LevelItem.transform.GetChild(3).transform.GetComponent<TextMeshProUGUI>().text = "";
                        LevelItem.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/blueButton");
                    }
                }
                else
                {
                    LevelItem.GetComponent<Button>().interactable = false;
                    LevelItem.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "";
                    LevelItem.transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().text = "";
                    LevelItem.transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = "";
                    LevelItem.transform.GetChild(3).transform.GetComponent<TextMeshProUGUI>().text = "";
                    LevelItem.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/blueButton");
                }

                countButton++;
                countLevel++;
            }

            Locations[countLocation - 1] = LocationItem;

            countLocation++;
            countButton = 0;
            countLevel = 1;
            count++;
        }

        LocationListContent.GetComponent<RectTransform>().sizeDelta = new Vector2(width * count, height);

        int locationLength = Locations.Length;
        distance = new float[locationLength];

        if (Locations.Length > 1)
        {
            locationDistance = (int)Mathf.Abs(Locations[1].GetComponent<RectTransform>().anchoredPosition.x - Locations[0].GetComponent<RectTransform>().anchoredPosition.x);
        }

    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < Locations.Length; i++)
        {
            distance[i] = Mathf.Abs(SnapCenter.transform.position.x - Locations[i].transform.position.x);
        }

        float minDistance = Mathf.Min(distance);

        for (int a = 0; a < Locations.Length; a++)
        {
            if (minDistance == distance[a])
            {
                minLocationNum = a;
            }
        }

        if (!dragging)
        {
            LerpToLocation(minLocationNum * - locationDistance);
        }
    }

    void LerpToLocation(int position)
    {
        float newX = Mathf.Lerp(LocationListContent.GetComponent<RectTransform>().anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, LocationListContent.GetComponent<RectTransform>().anchoredPosition.y);

        LocationListContent.GetComponent<RectTransform>().anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }

    void BackButtonOnClick()
    {
        ScreenManager.instance.ShowMainScreen();
    }

}
