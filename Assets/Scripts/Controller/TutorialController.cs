using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    private GameManager GameManager;
    private GameScreen GameScreen;

    [Header("Character objects")]
    public GameObject characterList;
    public GameObject characterControls;
    [Header("Tutorial objects")]
    public GameObject cellArrowGroup;
    public GameObject tutorialArrowGroup;
    private GameObject cellArrowGroupInstantiate;
    private GameObject tutorialArrowGroupInstantiate01;
    private GameObject tutorialArrowGroupInstantiate02;
    private GameObject tutorialArrowGroupInstantiate03;
    private GameObject tutorialArrowGroupInstantiate04;
    public bool tutorialExist;

    [SerializeField]
    private List<string> tutorialSteps = new List<string>();
    private int step;
    private string stepType;

    private void OnEnable()
    {
        CharacterButton.ActionCharacterBtnPressed += showTutorialStep;
        TileController.ActionTilePressed += showTutorialStep;
        CharacterControls.ActionCharacterApplyPressed += showTutorialStep;
        GameScreen.ActionPlayPressed += showTutorialStep;
        CharacterControls.ActionCharacterRotatePressed += showTutorialStep;
    }

    private void OnDisable()
    {
        CharacterButton.ActionCharacterBtnPressed -= showTutorialStep;
        TileController.ActionTilePressed -= showTutorialStep;
        CharacterControls.ActionCharacterApplyPressed -= showTutorialStep;
        GameScreen.ActionPlayPressed -= showTutorialStep;
        CharacterControls.ActionCharacterRotatePressed -= showTutorialStep;
    }

    public void Awake()
    {
        GameManager = GameManager.instance.GetComponent<GameManager>();
        GameScreen = GameManager.GameController.GetComponent<GameScreen>();
    }

    public void showTutorialStep()
    {
        tutorialStep(step);
        step++;
    }

    public void CreateTutorial(Transform Level, float cellArrowGroupPozX, float cellArrowGroupPozZ, List<string> tutorial)
    {
        step = 0;
        tutorialSteps = tutorial;
        gameObject.SetActive(true);
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // block other buttons
        GameScreen.PlayButton.GetComponent<Button>().interactable = false;
        GameScreen.HintButton.GetComponent<Button>().interactable = false;
        characterControls.GetComponent<CharacterControls>().cancel.GetComponent<Button>().interactable = false;
        characterControls.GetComponent<CharacterControls>().rotate.GetComponent<Button>().interactable = false;

        // arrows for cell
        cellArrowGroupInstantiate = Instantiate(cellArrowGroup, new Vector3(cellArrowGroupPozX, 0.02f, cellArrowGroupPozZ), Quaternion.identity);
        cellArrowGroupInstantiate.transform.SetParent(Level);
        cellArrowGroupInstantiate.SetActive(false);

        // arrow for character button
        tutorialArrowGroupInstantiate01 = Instantiate(tutorialArrowGroup, new Vector3(0, 0, 0), Quaternion.identity);
        tutorialArrowGroupInstantiate01.transform.SetParent(transform);
        tutorialArrowGroupInstantiate01.SetActive(false);
        tutorialArrowGroupInstantiate01.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 420);
        tutorialArrowGroupInstantiate01.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // arrow for apply button
        tutorialArrowGroupInstantiate02 = Instantiate(tutorialArrowGroup, new Vector3(0, 0, 0), Quaternion.identity);
        tutorialArrowGroupInstantiate02.transform.SetParent(transform);
        tutorialArrowGroupInstantiate02.SetActive(false);
        tutorialArrowGroupInstantiate02.GetComponent<RectTransform>().anchoredPosition = new Vector2(-215, 450);
        tutorialArrowGroupInstantiate02.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // arrow for play button
        tutorialArrowGroupInstantiate03 = Instantiate(tutorialArrowGroup, new Vector3(0, 0, 0), Quaternion.identity);
        tutorialArrowGroupInstantiate03.transform.SetParent(GameScreen.PlayButton.transform);
        tutorialArrowGroupInstantiate03.SetActive(false);
        tutorialArrowGroupInstantiate03.GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, 180);
        tutorialArrowGroupInstantiate03.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 50);
        tutorialArrowGroupInstantiate03.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        
        // arrow for rotate button
        tutorialArrowGroupInstantiate04 = Instantiate(tutorialArrowGroup, new Vector3(0, 0, 0), Quaternion.identity);
        tutorialArrowGroupInstantiate04.transform.SetParent(transform);
        tutorialArrowGroupInstantiate04.SetActive(false);
        tutorialArrowGroupInstantiate04.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 450);
        tutorialArrowGroupInstantiate04.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);

        tutorialExist = true;
        tutorialStep01();
    }

    private void tutorialStep(int step)
    {
        stepType = tutorialSteps[step];

        switch (stepType)
        {
            case "ActionCharacterBtnPressed":
                ActionCharacterBtnPressed();
                break;
            case "ActionTilePressed01":
                ActionTilePressed01();
                break;
            case "ActionTilePressed02":
                ActionTilePressed02();
                break;
            case "ActionTilePressed03":
                ActionTilePressed03();
                break;
            case "ActionTilePressed04":
                ActionTilePressed04();
                break;
            case "ActionCharacterApplyPressed":
                ActionCharacterApplyPressed();
                break;
            case "ActionCharacterRotatePressed":
                ActionCharacterRotatePressed();
                break;
            case "ActionPlayPressed":
                ActionPlayPressed();
                break;
        }
    }

    private void ActionCharacterBtnPressed()
    {
        tutorialArrowGroupInstantiate01.SetActive(false);
        cellArrowGroupInstantiate.SetActive(true);
        foreach (Transform child in characterList.transform)
        {
            child.GetComponent<Button>().interactable = false;
        }
    }

    private void ActionTilePressed01() // to apply button
    {
        cellArrowGroupInstantiate.SetActive(false);
        tutorialArrowGroupInstantiate02.SetActive(true);

        foreach (Transform child in characterList.transform)
        {
            child.GetComponent<Button>().interactable = false;
        }
    }

    private void ActionTilePressed02() // to rotate button
    {
        cellArrowGroupInstantiate.SetActive(false);
        tutorialArrowGroupInstantiate04.SetActive(true);
        characterControls.GetComponent<CharacterControls>().rotate.GetComponent<Button>().interactable = true;
        characterControls.GetComponent<CharacterControls>().cancel.GetComponent<Button>().interactable = false;
        foreach (Transform child in characterList.transform)
        {
            child.GetComponent<Button>().interactable = false;
        }

    }

    private void ActionTilePressed03() // to tile 
    {
        tutorialArrowGroupInstantiate01.SetActive(false);
        cellArrowGroupInstantiate.SetActive(true);
    }

    private void ActionTilePressed04() // to tile 
    {
        cellArrowGroupInstantiate.SetActive(false);
        tutorialArrowGroupInstantiate02.SetActive(true);
        tutorialArrowGroupInstantiate02.GetComponent<RectTransform>().anchoredPosition = new Vector2(-275, 450);

        foreach (Transform child in characterList.transform)
        {
            child.GetComponent<Button>().interactable = false;
        }
    }

    private void ActionCharacterApplyPressed()
    {
        tutorialArrowGroupInstantiate02.SetActive(false);
        tutorialArrowGroupInstantiate03.SetActive(true);
        GameScreen.PlayButton.GetComponent<Button>().interactable = true;
        GameScreen.HintButton.GetComponent<Button>().interactable = false;
    }

    private void ActionCharacterRotatePressed()
    {
        tutorialArrowGroupInstantiate02.GetComponent<RectTransform>().anchoredPosition = new Vector2(-295, 450);
        tutorialArrowGroupInstantiate02.SetActive(true);
        tutorialArrowGroupInstantiate04.SetActive(false);

        characterControls.GetComponent<CharacterControls>().rotate.GetComponent<Button>().interactable = false;
        characterControls.GetComponent<CharacterControls>().cancel.GetComponent<Button>().interactable = false;

        foreach (Transform child in characterList.transform)
        {
            child.GetComponent<Button>().interactable = false;
        }
    }
        

    private void ActionPlayPressed()
    {
        tutorialArrowGroupInstantiate03.SetActive(false);
    }


    public void tutorialStep01()
    {
        tutorialArrowGroupInstantiate02.SetActive(false);
        tutorialArrowGroupInstantiate03.SetActive(false);
        tutorialArrowGroupInstantiate01.SetActive(true);
    }

    private void tutorialStep02()
    {
        tutorialArrowGroupInstantiate01.SetActive(false);
        cellArrowGroupInstantiate.SetActive(true);
    }

    public void tutorialStep03()
    {
        cellArrowGroupInstantiate.SetActive(false);
        tutorialArrowGroupInstantiate02.SetActive(true);

        foreach (Transform child in characterList.transform)
        {
            child.GetComponent<Button>().interactable = false;
        }
    }

    public void tutorialStep04()
    {
        tutorialArrowGroupInstantiate02.SetActive(false);
        tutorialArrowGroupInstantiate03.SetActive(true);
        GameScreen.PlayButton.GetComponent<Button>().interactable = true;
    }

    public void tutorialStep05()
    {
        tutorialArrowGroupInstantiate03.SetActive(false);
    }


}

