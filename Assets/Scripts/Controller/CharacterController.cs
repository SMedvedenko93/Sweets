using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public bool rotateble;
    public string orientation = "right";
    public string name;

    public bool editMode = true;

    private Collider collider;
    private GameScreen GameScreen;
    private GameController GameController;
    private bool temporary;

    // Start is called before the first frame update
    void Start()
    {
        temporary = true;
        collider = GetComponent<Collider>();
        GameController = GameManager.instance.GetComponent<GameManager>().GameController;

        EnemyController.ActionMove += activeState;
    }

    private void OnDestroy()
    {
        EnemyController.ActionMove -= activeState;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void activeState()
    {
        transform.GetChild(0).GetComponent<Animator>().SetBool("Active", true);
    }

    void deactivateCharacter()
    {

    }

    public void rotateRight()
    {
        if (transform.childCount > 3)
        {
            transform.GetChild(0).transform.Rotate(0, 90, 0);
            transform.GetChild(1).transform.Rotate(0, 90, 0);
            transform.GetChild(3).transform.Rotate(0, 90, 0);

            if (!isTemp())
            {
                var index = GameController.characterCells.FindIndex(item => item.x == GameController.currentCharacterX && item.z == GameController.currentCharacterZ);
                var name = GameController.currentCharacter.GetComponent<CharacterController>().name;
                var orientation = GameController.currentCharacter.GetComponent<CharacterController>().orientation;
            }

        }  
    }

    /*
    public void rotateLeft()
    {
        if (transform.childCount > 3)
        {
            transform.GetChild(1).transform.Rotate(0, -90, 0);
            transform.GetChild(3).transform.Rotate(0, -90, 0);

            if (!isTemp())
            {
                var index = Game.characterCells.FindIndex(item => item.x == Game.currentCharacterX && item.z == Game.currentCharacterZ);
                var name = Game.currentCharacter.GetComponent<CharacterController>().name;
                var orientation = Game.currentCharacter.GetComponent<CharacterController>().orientation;
            }
        }
    }
    */

    public void applyCharacter()
    {
        temporary = false;
        gameObject.transform.GetChild(1).gameObject.SetActive(false);

        GameController.characterCells.Add(
            new CharacterCell()
                {
                name = name,
                x = transform.position.x,
                z = transform.position.z,
                orientation = orientation,
                pair = 1
            }
        );
        updateToCharacterCellArray(GameController.characterCells.Count);
    }

    public void cancelCharacter()
    {
        if (!isTemp())
        {
            var index = GameController.characterCells.FindIndex(item => item.x == GameController.currentCharacterX && item.z == GameController.currentCharacterZ);
            var name = GameController.currentCharacter.GetComponent<CharacterController>().name;
            var orientation = GameController.currentCharacter.GetComponent<CharacterController>().orientation;
            deleteEffectedCells(name, orientation, index);
        }
    }

    private void updateEffectedCells(string name, string orientation, int index)
    {
        updateToCharacterCellArray(index);
    }

    private void deleteEffectedCells(string name, string orientation, int index)
    {
        GameController.characterCells.RemoveAt(index);
        updateToCharacterCellArray(0);
    }


    public bool isTemp()
    {
        if (temporary == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void hideNovisible() // hide cells out field
    {
        foreach (Transform child in transform.GetChild(1).transform)
        {
            child.gameObject.SetActive(true);
        }

        foreach (Transform child in transform.GetChild(1).transform)
        {
            if (child.transform.position.x > 5 || child.transform.position.x < 0)
            {
                child.gameObject.SetActive(false);
            }
            if (child.transform.position.z > 9 || child.transform.position.z < 0)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void updateToCharacterCellArray(int indexCharacter)
    {
        var index = indexCharacter;

        Debug.Log(name);

        switch (name)
        {
            case "Skipcake":
                GameController.CharacterCellArray[(int)transform.position.x, (int)transform.position.z] = index;
                break;
            case "Hypnopie":
                GameController.CharacterCellArray[(int)transform.position.x, (int)transform.position.z] = index;
                switch (orientation)
                {
                    case "right":
                        for (var i = transform.position.x + 1; i <= transform.position.x + 3; i++)
                        {
                            if (checkValueX((int)i) && checkValueZ((int)transform.position.z))
                            {
                                GameController.CharacterCellArray[(int)i, (int)transform.position.z] = index;
                            }
                        }
                        break;
                    case "left":
                        for (var i = transform.position.x - 1; i >= transform.position.x - 3; i--)
                        {
                            if (checkValueX((int)i) && checkValueZ((int)transform.position.z))
                            {
                                GameController.CharacterCellArray[(int)i, (int)transform.position.z] = index;
                            }
                        }
                        break;
                    case "up":
                        for (var i = transform.position.z + 1; i <= transform.position.z + 3; i++)
                        {
                            if (checkValueX((int)transform.position.x) && checkValueZ((int)i))
                            {
                                GameController.CharacterCellArray[(int)transform.position.x, (int)i] = index;
                            }
                        }
                        break;
                    case "down":
                        for (var i = transform.position.z - 1; i >= transform.position.z - 3; i--)
                        {
                            if (checkValueX((int)transform.position.x) && checkValueZ((int)i))
                            {
                                GameController.CharacterCellArray[(int)transform.position.x, (int)i] = index;
                            }
                        }
                        break;
                }
                break;
            case "Swapple":
                GameController.CharacterCellArray[(int)transform.position.x, (int)transform.position.z] = index;
                break;
            case "Jellyjump":
                GameController.CharacterCellArray[(int)transform.position.x, (int)transform.position.z] = index;
                break;
            case "Magneticecream":
                GameController.CharacterCellArray[(int)transform.position.x, (int)transform.position.z] = index;
                for (var i = transform.position.x + 1; i <= transform.position.x + 2; i++)
                {
                    if (checkValueX((int)i) && checkValueZ((int)transform.position.z))
                    {
                        GameController.CharacterCellArray[(int)i, (int)transform.position.z] = index;
                    }
                }
                for (var i = transform.position.x - 1; i >= transform.position.x - 2; i--)
                {
                    if (checkValueX((int)i) && checkValueZ((int)transform.position.z))
                    {
                        GameController.CharacterCellArray[(int)i, (int)transform.position.z] = index;
                    }
                }
                for (var i = transform.position.z + 1; i <= transform.position.z + 2; i++)
                {
                    if (checkValueX((int)transform.position.x) && checkValueZ((int)i))
                    {
                        GameController.CharacterCellArray[(int)transform.position.x, (int)i] = index;
                    }
                }
                for (var i = transform.position.z - 1; i >= transform.position.z - 2; i--)
                {
                    if (checkValueX((int)transform.position.x) && checkValueZ((int)i))
                    {
                        GameController.CharacterCellArray[(int)transform.position.x, (int)i] = index;
                    }
                }
                break;
        }

        for (var i = 0; i < 6; i++)
        {
            for (var j = 0; j < 10; j++)
            {
                //Debug.Log(i + " : " + j + " = " + GameController.CharacterCellArray[i, j]);
            }
        }

    }

    private bool checkValueZ(int z)
    {
        var strings = GameController.CharacterCellArray.GetLength(1);
        if (z < strings && z >= 0)
        {
            return true;
        }

        return false;
    }

    private bool checkValueX(int x)
    {
        var rows = GameController.CharacterCellArray.GetLength(0);
        if (x < rows && x >= 0)
        {
            return true;
        }

        return false;
    }
}
