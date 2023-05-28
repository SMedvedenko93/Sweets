using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    GameController GameController;

    public List<GameObject> characterButtonList = new List<GameObject>();
    public string name;
    public int count;
    private int currentCount;

    public static event Action ActionCharacterBtnPressed;

    void Start()
    {
        GameController = GameManager.instance.GetComponent<GameManager>().GameController;

        GetComponent<Button>().onClick.AddListener(() => CharacterOnClick(name));
        characterButtonList = transform.parent.transform.GetComponent<CharacterList>().characterButtonList;
        currentCount = count;
    }

    private void Update()
    {
        //if (Input.GetMouseButtonUp(0))
        //{
        //    if (!RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition))
        //    {
        //        Debug.Log(9999);
        //        GameController.editMode = false;
        //        GameController.currentCharacter = null;
        //        GameController.selectedCharacter = "";
        //        GameController.deactivateButton();
        //    }
        //}
    }

    void CharacterOnClick(string name)
    {
        if (currentCount > 0)
        {
            GameManager.instance.GetComponent<GameManager>().GameController.ClearHints();

            foreach (var character in characterButtonList)
            {
                character.GetComponent<ButtonComponent>().deactivateButton();
            }

            GameManager.instance.GetComponent<GameManager>().GameController.currentCharacterButton = gameObject;
            GameManager.instance.GetComponent<GameManager>().GameController.editMode = true;
            GameManager.instance.GetComponent<GameManager>().GameController.selectedCharacter = name;

            ActionCharacterBtnPressed?.Invoke();
        }
    }

    public void ChangeCountMinus()
    { 
        currentCount = currentCount - 1;

        if (currentCount == 0)
        {
            GetComponent<Button>().interactable = false;
        }

        transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentCount.ToString();
    }

    public void ChangeCountPlus()
    {
        currentCount = currentCount + 1;

        if (currentCount > 0)
        {
            GetComponent<Button>().interactable = true;
        }

        transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentCount.ToString();
    }
}
