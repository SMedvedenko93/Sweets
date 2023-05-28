using System;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{
    public GameObject characterGroup;
    private Collider collider;

    private GameController GameController;
    private GameScreen GameScreen;

    public static event Action ActionTilePressed;

    // Start is called before the first frame update
    void Start()
    {
        GameController = GameManager.instance.GetComponent<GameManager>().GameController;
        GameScreen = GameManager.instance.GetComponent<GameManager>().GameController.GetComponent<GameScreen>();

        collider = GetComponent<Collider>();
        characterGroup = GameController.characterGroup;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (collider.Raycast(ray, out hit, 100))
            {
                if (hit.collider.gameObject.tag == "Cell")
                {
                    var x = transform.position.x;
                    var z = transform.position.z;
                    var name = GameController.selectedCharacter;

                    if (GameController.gamePause == false && GameController.editMode == true && GameController.CharacterCellArrayTemp[(int)x, (int)z] == 0)
                    {
                        GameManager.instance.GetComponent<GameManager>().SoundManager.GetComponent<SoundManager>().SFXManagerSource.GetComponent<SFXManager>().TileClickAudio();

                        GameController.characterList.SetActive(false);
                        GameController.CharacterControls.SetActive(true);

                        GameScreen.PlayButton.GetComponent<Button>().interactable = false;
                        GameScreen.HintButton.GetComponent<Button>().interactable = false;

                        GameController.CharacterCellArrayTemp[(int)x, (int)z] = 1;

                        var character = Instantiate(Resources.Load("Characters/" + name) as GameObject, new Vector3(x, 0f, z), Quaternion.identity);
                        GameController.currentCharacter = character;

                        if (GameController.currentCharacter.GetComponent<CharacterController>().rotateble)
                        {
                            GameController.CharacterControls.GetComponent<CharacterControls>().rotate.SetActive(true);
                        }
                        else
                        {
                            GameController.CharacterControls.GetComponent<CharacterControls>().rotate.SetActive(false);
                        }

                        GameController.currentCharacter.GetComponent<CharacterController>().hideNovisible();
                        GameController.currentCharacter.GetComponent<CharacterController>().editMode = false;
                        GameController.editMode = false;

                        character.transform.SetParent(characterGroup.transform);

                        ActionTilePressed?.Invoke();
                    }
                }
            }
        }
    }
}
