using System;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    private GameManager GameManager;
    GameWindowsManager GameWindowsManager;
    AdsInitializer AdsManager;
    GameController GameController;

    [Header("Buttons")]
    public GameObject MenuButton;
    public GameObject PlayButton;
    public GameObject HintButton;
    public GameObject BlockPanel;

    public GameObject HintValue;
    public static event Action ActionPlayPressed;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameManager.instance.GetComponent<GameManager>();
        GameController = GameManager.GameController;

        HintValue.GetComponent<Text>().text = GameManager.getCountHint();

        GameWindowsManager = GameManager.GameWindowsManager.GetComponent<GameWindowsManager>();
        AdsManager = GameManager.AdsManager.GetComponent<AdsInitializer>();
        GameController = GetComponent<GameController>();

        MenuButton.GetComponent<Button>().onClick.AddListener(MenuButtonOnClick);
        PlayButton.GetComponent<Button>().onClick.AddListener(PlayButtonOnClick);
        HintButton.GetComponent<Button>().onClick.AddListener(HintButtonOnClick);

        //GetComponent<Button>().onClick.AddListener(GameOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (GameController.gamePause == false && hit.collider.gameObject.tag == "Character")
                {
                    var character = hit.collider.gameObject;
                    var characterController = hit.collider.gameObject.GetComponent<CharacterController>();

                    GameManager.instance.GetComponent<GameManager>().SoundManager.GetComponent<SoundManager>().SFXManagerSource.GetComponent<SFXManager>().ButtonClickAudio();
                    character.transform.GetChild(0).GetComponent<Animator>().SetTrigger("React");

                    characterController.editMode = true;
                    GameController.characterList.SetActive(false);
                    GameController.CharacterControls.SetActive(true);

                    if (characterController.rotateble)
                    {
                        GameController.CharacterControls.GetComponent<CharacterControls>().rotate.SetActive(true);
                    }
                    else
                    {
                        GameController.CharacterControls.GetComponent<CharacterControls>().rotate.SetActive(false);
                    }

                    GameController.editMode = false;
                    character.transform.GetChild(1).gameObject.SetActive(true);

                    GameController.currentCharacter = character.gameObject;
                    GameController.currentCharacterX = character.gameObject.transform.position.x;
                    GameController.currentCharacterZ = character.gameObject.transform.position.z;

                    foreach (Transform child in GameController.characterList.transform)
                    {
                        if (child.GetComponent<CharacterButton>().name == characterController.name)
                        {
                            GameController.currentCharacterButton = child.gameObject;
                        }
                    }
                }
            }
        }
    }

    void MenuButtonOnClick()
    {
        GameController.editMode = false;
        GameController.deactivateButton();
        GameController.ClearHints();
        GameController.gamePause = true;
        GameWindowsManager.pauseLevel();
        GameController.Enemy.GetComponent<Animator>().enabled = false;
    }

    void PlayButtonOnClick()
    {
        GameController.deactivateButton();
        ActionPlayPressed?.Invoke();
        GameController.ClearHints();
        foreach (Transform character in GameController.characterList.transform)
        {
            character.GetComponent<ButtonComponent>().deactivateButton();
        }

        GameWindowsManager.BlockScreen();
        GameController.Enemy.GetComponent<EnemyController>().GetPath();
        GameController.Enemy.GetComponent<EnemyController>().CheckSuccess();
        GameController.Enemy.GetComponent<EnemyController>().Move();
    }

    void HintButtonOnClick()
    {
        GameController.editMode = false;
        GameController.deactivateButton();
        if (int.Parse(GameManager.getCountHint()) > 0)
        {
            GameController.ClearHints();
            foreach (Hint hint in GameController.levelGlobal.hints)
            {
                var character = Instantiate(Resources.Load("Characters/" + hint.name) as GameObject, new Vector3(hint.positionX, 0f, hint.positionZ), Quaternion.identity);
                character.transform.GetComponent<BoxCollider>().enabled = false;
                character.transform.GetChild(0).GetComponent<Animator>().enabled = false;
                character.transform.SetParent(GameController.Hints.transform);
            }

            var newHintCount = int.Parse(HintValue.GetComponent<Text>().text) - 1;
            HintValue.GetComponent<Text>().text = newHintCount.ToString();

            GameManager.UseHint();
        }
        else
        {
            GameManager.GameWindowsManager.GetComponent<GameWindowsManager>().windowShop();
        }
    }


}
