using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControls : MonoBehaviour
{

    public GameObject apply;
    public GameObject cancel;
    public GameObject rotate;

    private GameController GameController;
    private GameScreen GameScreen;

    public static event Action ActionCharacterApplyPressed;
    public static event Action ActionCharacterRotatePressed;

    // Start is called before the first frame update
    void Start()
    {
        GameController = GameManager.instance.GetComponent<GameManager>().GameController;
        GameScreen = GameManager.instance.GetComponent<GameManager>().GameController.GetComponent<GameScreen>();

        apply.GetComponent<Button>().onClick.AddListener(applyButtonOnClick);
        cancel.GetComponent<Button>().onClick.AddListener(cancelButtonOnClick);
        rotate.GetComponent<Button>().onClick.AddListener(rotateButtonOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void applyButtonOnClick()
    {
        GameController.characterList.SetActive(true);
        GameController.CharacterControls.SetActive(false);
        if (GameController.currentCharacter.GetComponent<CharacterController>().isTemp())
        {
            GameController.currentCharacterButton.GetComponent<CharacterButton>().ChangeCountMinus();
        }
        GameController.currentCharacter.GetComponent<CharacterController>().applyCharacter();
        GameScreen.PlayButton.GetComponent<Button>().interactable = true;
        GameScreen.HintButton.GetComponent<Button>().interactable = true;

        ActionCharacterApplyPressed?.Invoke();
    }

    void cancelButtonOnClick()
    {
        Destroy(GameController.currentCharacter.gameObject);

        GameController.characterList.SetActive(true);
        GameController.CharacterControls.SetActive(false);
        if (!GameController.currentCharacter.GetComponent<CharacterController>().isTemp())
        {
            GameController.currentCharacterButton.GetComponent<CharacterButton>().ChangeCountPlus();
        }
        GameController.currentCharacterButton = null;
        GameController.currentCharacter.GetComponent<CharacterController>().cancelCharacter();
        GameController.CharacterCellArrayTemp[(int)GameController.currentCharacter.transform.position.x, (int)GameController.currentCharacter.transform.position.z] = 0;

        GameScreen.PlayButton.GetComponent<Button>().interactable = true;
        GameScreen.HintButton.GetComponent<Button>().interactable = true;
    }

    void rotateButtonOnClick()
    {
        var orientation = GameController.currentCharacter.GetComponent<CharacterController>().orientation;
        GameController.currentCharacter.GetComponent<CharacterController>().rotateRight();
        switch (orientation)
        {
            case "right":
                GameController.currentCharacter.GetComponent<CharacterController>().orientation = "down";
                break;
            case "left":
                GameController.currentCharacter.GetComponent<CharacterController>().orientation = "up";
                break;
            case "up":
                GameController.currentCharacter.GetComponent<CharacterController>().orientation = "right";
                break;
            case "down":
                GameController.currentCharacter.GetComponent<CharacterController>().orientation = "left";
                break;
        }

        GameController.currentCharacter.GetComponent<CharacterController>().hideNovisible();
        ActionCharacterRotatePressed?.Invoke();

    }

}
