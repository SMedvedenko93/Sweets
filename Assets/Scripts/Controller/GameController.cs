using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GameManager GameManager;
    private GameScreen GameScreen;

    private GameObject pathActiveCellObject, pathCellItem;
    private int pathStep = 0;

    [Header("Game")]
    public GameObject LoadingLevelScreen;
    public Level levelGlobal;
    public GameObject Path;
    public GameObject Hints;
    public bool editMode = false;
    public bool gamePause = false;
    public bool playMode = false;

    [Header("Enemy")]
    public GameObject Enemy;
    public GameObject pathCell;
    public GameObject pathCellAngle;
    public GameObject pathCellEnd;
    public GameObject pathActiveCell;
    public float EnemyPosX;
    public float EnemyPosZ;

    [Header("Character")]
    public GameObject characterGroup;
    public GameObject characterList;
    public GameObject CharacterControls;
    public int[,] CharacterCellArrayTemp = new int[6, 10];
    public int[,] CharacterCellArray = new int[6, 10];
    public List<CharacterCell> characterCells = new List<CharacterCell>();
    public string selectedCharacter;
    public GameObject currentCharacter;
    public float currentCharacterX;
    public float currentCharacterZ;
    public GameObject currentCharacterButton;

    [Header("Trap")]
    public GameObject Trap;
    public float TrapxPos;
    public float TrapzPos;

    public TutorialController Tutorial;
    public GameObject Location;

    public void Awake()
    {
        GameScreen = GetComponent<GameScreen>();
    }

    public void deactivateButton()
    {
        foreach (Transform character in characterList.transform)
        {
            character.GetComponent<ButtonComponent>().deactivateButton();
        }
    }

    public void createEnemyPath()
    {
        characterCells = new List<CharacterCell>();

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                CharacterCellArray[i, j] = 0;
                CharacterCellArrayTemp[i, j] = 0;
            }
        }


        foreach (Transform child in Path.transform)
        {
            Destroy(child.gameObject);
        }

        var pathCellPosX = EnemyPosX;
        var pathCellPosZ = EnemyPosZ;
        var pathCellRotate = 0;
        var pathCellAngleRotate = 0;
        var currentStep = Enemy.GetComponent<EnemyController>().moveList[0];
        var nextStep = Enemy.GetComponent<EnemyController>().moveList[0];


        switch (nextStep)
        {
            case "xm":
                pathCellRotate = 90;
                break;
            case "xp":
                pathCellRotate = -90;
                break;
            case "ym":
                pathCellRotate = 0;
                break;
            case "yp":
                pathCellRotate = 180;
                break;
        }

        pathCellItem = Instantiate(pathCellEnd, new Vector3(pathCellPosX, 0.01f, pathCellPosZ), Quaternion.Euler(0f, pathCellRotate, 0f));
        pathCellItem.transform.SetParent(Path.transform);

        for (var i = 0; i < Enemy.GetComponent<EnemyController>().moveList.Count; i++)
        {
            currentStep = Enemy.GetComponent<EnemyController>().moveList[i];

            if ((i + 1) < Enemy.GetComponent<EnemyController>().moveList.Count)
            {
                nextStep = Enemy.GetComponent<EnemyController>().moveList[i + 1];
            }
            

            switch (currentStep)
            {
                case "xm":
                    pathCellPosX = pathCellPosX - 1;
                    pathCellRotate = -90;
                    break;
                case "xp":
                    pathCellPosX = pathCellPosX + 1;
                    pathCellRotate = 90;
                    break;
                case "ym":
                    pathCellPosZ = pathCellPosZ - 1;
                    pathCellRotate = 180;
                    break;
                case "yp":
                    pathCellPosZ = pathCellPosZ + 1;
                    pathCellRotate = 0;
                    break;
            }

            switch (currentStep + "" + nextStep)
            {
                case "ypxm":
                    pathCellAngleRotate = -90;
                    break;
                case "xpym":
                    pathCellAngleRotate = -90;
                    break;
                case "ypxp":
                    pathCellAngleRotate = 180;
                    break;
                case "xmyp":
                    pathCellAngleRotate = 90;
                    break;
                case "ymxp":
                    pathCellAngleRotate = 90;
                    break;
                case "ymxm":
                    pathCellAngleRotate = 0;
                    break;
                case "xpyp":
                    pathCellAngleRotate = 0;
                    break;
                case "xmym":
                    pathCellAngleRotate = 180;
                    break;
            }

            if (nextStep != currentStep)
            {
                pathCellItem = Instantiate(pathCellAngle, new Vector3(pathCellPosX, 0.01f, pathCellPosZ), Quaternion.Euler(0f, pathCellAngleRotate, 0f));
                pathCellItem.transform.SetParent(Path.transform);
            }
            else
            {
                if (i == (Enemy.GetComponent<EnemyController>().moveList.Count - 1))
                {
                    pathCellItem = Instantiate(pathCellEnd, new Vector3(pathCellPosX, 0.01f, pathCellPosZ), Quaternion.Euler(0f, pathCellRotate, 0f));
                    pathCellItem.transform.SetParent(Path.transform);
                }
                else
                {
                    pathCellItem = Instantiate(pathCell, new Vector3(pathCellPosX, 0.01f, pathCellPosZ), Quaternion.Euler(0f, pathCellRotate, 0f));
                    pathCellItem.transform.SetParent(Path.transform);
                }


            }

        }
    }

    public void ClearHints()
    {
        foreach (Transform child in Hints.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
