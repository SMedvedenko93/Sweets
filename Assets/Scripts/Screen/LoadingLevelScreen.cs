using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingLevelScreen : MonoBehaviour
{

    public GameObject boxPrefab;
    public GameObject Trap;
    public Transform Level;

    public int location;
    public int level;

    public GameObject CharacterList;

    private GameManager GameManager;

    private string tileArraySource = "2,4,1,3,2,3;3,1,2,1,4,2;4,2,3,2,3,4;1,3,1,4,1,3;3,4,2,3,2,4;2,1,3,4,3,1;1,3,2,3,1,4;3,2,4,2,4,2;4,1,3,1,3,1;2,3,2,4,2,3";
    private int[,] tileArray = new int[10, 6];

    void OnEnable()
    {
        StartLevel();
    }


    void StartLevel()
    {

        string[] rows = tileArraySource.Split(';');
        for (int i = 0; i < rows.Length; i++)
        {
            string[] cols = rows[i].Split(',');
            for (int j = 0; j < cols.Length; j++)
            {
                tileArray[i, j] = int.Parse(cols[j]);
            }
        }

        GameManager = GameManager.instance.GetComponent<GameManager>();
        GameManager.SoundManager.GetComponent<SoundManager>().StartMusicGame();

        GameManager.GameController.gamePause = false;

        GameManager.GameController.CharacterControls.SetActive(false);
        GameManager.GameController.characterList.SetActive(true);

        GameManager.GameController.GetComponent<GameScreen>().PlayButton.GetComponent<Button>().interactable = true;
        GameManager.GameController.GetComponent<GameScreen>().HintButton.GetComponent<Button>().interactable = true;

        if (GameManager.GameController.GetComponent<GameScreen>().PlayButton.transform.childCount > 1)
        {
            Destroy(GameManager.GameController.GetComponent<GameScreen>().PlayButton.transform.GetChild(1).gameObject);
        }

        GameManager.GameController.CharacterControls.GetComponent<CharacterControls>().cancel.GetComponent<Button>().interactable = true;
        GameManager.GameController.CharacterControls.GetComponent<CharacterControls>().rotate.GetComponent<Button>().interactable = true;

        foreach (Transform child in Level)
        {
            Destroy(child.gameObject);
        }

        var characterGroupPrefab = new GameObject();
        var characterGroup = Instantiate(characterGroupPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        characterGroup.name = "CharacterGroup";
        characterGroup.transform.SetParent(Level);
        GameManager.GameController.characterGroup = characterGroup;


        GameManager.GameWindowsManager.GetComponent<GameWindowsManager>().UnBlockScreen();

        var materialBox = "";
        var locationGlobal = GameManager.LevelParser.getLevelList()[location - 1];
        var levelGlobal = GameManager.LevelParser.getLevelList()[location - 1].levelList[level - 1];
        
        GameManager.GameController.levelGlobal = levelGlobal;

        var enemyInstantiate = Instantiate(Resources.Load("Enemy/Enemy0" + levelGlobal.enemy) as GameObject, new Vector3(levelGlobal.enemyPositionX, 0.0f, levelGlobal.enemyPositionZ), Quaternion.identity);
        GameManager.GameController.Enemy = enemyInstantiate;

        var enemy = GameManager.GameController.Enemy;
        enemy.transform.SetParent(Level);

        var locationGame = Instantiate(Resources.Load("Locations/" + locationGlobal.location) as GameObject, new Vector3(2.5f, 0.0f, 5.0f), Quaternion.identity);
        locationGame.transform.SetParent(Level);
        enemy.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);

        var trapInstantiate = Instantiate(Trap, new Vector3(levelGlobal.trapPositionX, 0, levelGlobal.trapPositionZ), Quaternion.identity);
        trapInstantiate.transform.SetParent(Level);

        GameManager.GameController.Trap = trapInstantiate;
        GameManager.GameController.Location = locationGame;

        GameManager.GameController.TrapxPos = levelGlobal.trapPositionX;
        GameManager.GameController.TrapzPos  = levelGlobal.trapPositionZ;

        GameManager.GameController.EnemyPosX = enemy.transform.position.x;
        GameManager.GameController.EnemyPosZ = enemy.transform.position.z;

        for (var i = 0; i <= 5; i++)
        {
            for (var j = 0; j <= 9; j++)
            {
                if (levelGlobal.trapPositionX == i && levelGlobal.trapPositionZ == j)
                {

                }
                else
                {
                    var box = Instantiate(boxPrefab, new Vector3(i, 0, j), Quaternion.identity);
                    box.transform.SetParent(Level.transform);

                    var blockCount = tileArray[j, i];

                    switch (blockCount)
                    {
                        case 1:
                            materialBox = "blockColor01";
                            break;
                        case 2:
                            materialBox = "blockColor02";
                            break;
                        case 3:
                            materialBox = "blockColor03";
                            break;
                        case 4:
                            materialBox = "blockColor04";
                            break;
                    }

                    Material material = Resources.Load("Materials/" + materialBox, typeof(Material)) as Material;
                    box.GetComponent<Renderer>().material = material;
                    
                }
            }
        }

        enemy.transform.position = new Vector3(levelGlobal.enemyPositionX, 0, levelGlobal.enemyPositionZ);
        enemy.GetComponent<EnemyController>().startPositionX = levelGlobal.enemyPositionX;
        enemy.GetComponent<EnemyController>().startPositionZ = levelGlobal.enemyPositionZ;

        enemy.GetComponent<EnemyController>().moveList = null;
        enemy.GetComponent<EnemyController>().moveList = new List<string>();
        
        foreach (string move in levelGlobal.enemyPath)
        {
            enemy.GetComponent<EnemyController>().moveList.Add(move);
        }

        GameManager.GameController.createEnemyPath();

        ScreenManager.instance.ShowGameScreen();

        foreach (Transform child in CharacterList.transform)
        {
            Destroy(child.gameObject);
        }

        CharacterList.GetComponent<CharacterList>().characterButtonList = new List<GameObject>();
        foreach (Character character in levelGlobal.characterList)
        {
            var characterButton = Instantiate(CharacterList.GetComponent<CharacterList>().characterButton, new Vector3(0, 0, 0), Quaternion.identity);
            characterButton.transform.SetParent(CharacterList.transform);
            characterButton.GetComponent<CharacterButton>().name = character.name;
            characterButton.GetComponent<CharacterButton>().count = character.count;
            characterButton.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = character.count.ToString();
            characterButton.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/icons/" + character.name);
            CharacterList.GetComponent<CharacterList>().characterButtonList.Add(characterButton);
        }


        GameManager.GameController.Tutorial.tutorialExist = false;
        GameManager.GameController.Tutorial.gameObject.SetActive(false);

        if (levelGlobal.tutorial == 1)
        {
            GameManager.GameController.Tutorial.CreateTutorial(Level, levelGlobal.hints[0].positionX, levelGlobal.hints[0].positionZ - 0.3f, locationGlobal.tutorial);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
