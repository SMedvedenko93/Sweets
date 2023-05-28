using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public List<string> moveList = new List<string>();
    public List<EnemyAction> actionList = new List<EnemyAction>();

    public float startPositionX, startPositionZ;

    public bool endLevel = false;
    public bool active = false;

    public int[,] CharacterCellArray = new int[6, 10];
    public List<CharacterCell> characterCells = new List<CharacterCell>();

    private bool skipLine = false;
    private string skipLineDir = "";

    public string orientation = "right";

    private bool swap = false;


    private int step;
    private bool moveTrue;
    private bool jumpTrue;
    private bool start;
    private bool afterPause;


    private bool moveFinish = false;
    private bool successLevel = false;

    public float speedWalk;
    public float speedJump;
    public float speed;
    private float startTime;
    private float journeyLength ,fractionOfJourney, distCovered;
    private Vector3 startPosition;

    GameWindowsManager GameWindowsManager;
    GameController GameController;

    public static event Action ActionMove;

    // Start is called before the first frame update
    void Start()
    {
        step = 0;
        moveTrue = true;
        jumpTrue = false;
        start = true;
        afterPause = false;

        //startTime = Time.timeSinceLevelLoad;
        startPosition = transform.position;

        GameWindowsManager = GameManager.instance.GetComponent<GameManager>().GameWindowsManager.GetComponent<GameWindowsManager>();
        GameController = GameManager.instance.GetComponent<GameManager>().GameController;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameController.gamePause == true)
        {
            afterPause = true;
        }

        if (GameController.gamePause == false)
        {
            if (active)
            {
                if (step <= actionList.Count)
                {
                    switch (actionList[step].orientation)
                    {
                        case "up":
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            break;
                        case "down":
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            break;
                        case "left":
                            transform.rotation = Quaternion.Euler(0, -90, 0);
                            break;
                        case "right":
                            transform.rotation = Quaternion.Euler(0, 90, 0);
                            break;
                    }

                    switch (actionList[step].type)
                    {
                        case "move":
                            if (GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name == "idle")
                            {
                                GetComponent<Animator>().SetFloat("Action", 1);
                            }
                            else
                            {
                                GetComponent<Animator>().SetFloat("Action", 1, 0.3f, Time.deltaTime);
                            }
                            break;
                        case "hypno":
                            GetComponent<Animator>().SetFloat("Action", 2, 0.3f, Time.deltaTime);
                            break;
                        case "jump":                            
                            if (!jumpTrue)
                            {
                                GetComponent<Animator>().SetTrigger("Jump");
                            }
                            jumpTrue = true;
                            speed = speedJump;
                            break;
                        case "magnet":
                            GetComponent<Animator>().SetFloat("Action", 3, 0.3f, Time.deltaTime);
                            break;
                        case "punch":
                            Debug.Log("punch");
                            break;
                        case "portal":
                            Debug.Log("portal");
                            break;
                    }
                }

                if (afterPause == true)
                {
                    startTime = Time.time;
                    startPosition = transform.position;
                    journeyLength = Vector3.Distance(startPosition, new Vector3(actionList[step].pozX, 0, actionList[step].pozZ));
                    afterPause = false;
                }

                if (step == 0 && start == true)
                {
                    startTime = Time.time;
                    journeyLength = Vector3.Distance(startPosition, new Vector3(actionList[step].pozX, 0, actionList[step].pozZ));
                    start = false;
                }

                if (transform.position == new Vector3(actionList[step].pozX, 0, actionList[step].pozZ))
                {
                    speed = speedWalk;
                    jumpTrue = false;
                    startPosition = transform.position;
                    startTime = Time.time;
                    step++;
                    if (actionList.Count > step)
                    {
                        journeyLength = Vector3.Distance(startPosition, new Vector3(actionList[step].pozX, 0, actionList[step].pozZ));
                    }      
                }

                if (actionList.Count > step)
                {
                    if (transform.position != new Vector3(actionList[step].pozX, 0, actionList[step].pozZ))
                    {
                        distCovered = (Time.time - startTime) * speed;
                        fractionOfJourney = distCovered / journeyLength;

                        transform.position = Vector3.Lerp(startPosition, new Vector3(actionList[step].pozX, 0, actionList[step].pozZ), fractionOfJourney);

                    }
                }

                if (actionList.Count == step)
                {
                    GetComponent<Animator>().SetFloat("Action", 0);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    active = false;
                    moveFinish = true;
                }
            }
            if (moveFinish == true)
            {
                if (successLevel == true)
                {
                    StartCoroutine(trapEnemy());
                    Debug.Log("LEVEL COMPLETE");
                    GameWindowsManager.successLevel();
                }
                else
                {
                    GameManager.instance.GetComponent<GameManager>().AdsManager.GetComponent<AdsInitializer>().LoadRewardedAds();
                    Debug.Log("LEVEL FAILED");
                    GameWindowsManager.failLevel();

                }

                moveFinish = false;
            }
        }
    }

    IEnumerator trapEnemy()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetBool("trap", true);
        GameController.Trap.GetComponent<Animator>().SetBool("active", true);
        GameManager.instance.GetComponent<GameManager>().SoundManager.GetComponent<SoundManager>().SFXManagerSource.GetComponent<SFXManager>().SpringAudio();
    }


    public void Move()
    {
        ActionMove?.Invoke();
        Debug.Log("MOVE");
        active = true;
    }

    public void CheckSuccess()
    {
        var TrapxPos = GameManager.instance.GetComponent<GameManager>().GameController.GetComponent<GameController>().TrapxPos;
        var TrapzPos = GameManager.instance.GetComponent<GameManager>().GameController.GetComponent<GameController>().TrapzPos;

        var EnemyEndxPos = actionList[actionList.Count - 1].pozX;
        var EnemyEndzPos = actionList[actionList.Count - 1].pozZ;

        if (EnemyEndxPos == TrapxPos &&  EnemyEndzPos == TrapzPos)
        {
            GameManager.instance.GetComponent<GameManager>().SaveLevel("next");
            Debug.Log("LEVEL COMPLETE");
            successLevel = true;
        } else {
            Debug.Log("LEVEL FAILED");
            successLevel = false;
        }
    }

    public void GetPath()
    {
        PathGenerator pathGenerator = new PathGenerator();
        pathGenerator.characterCell = GameManager.instance.GetComponent<GameManager>().GameController.GetComponent<GameController>().characterCells;
        pathGenerator.CharacterCellArray = GameManager.instance.GetComponent<GameManager>().GameController.GetComponent<GameController>().CharacterCellArray;
        pathGenerator.moveList = moveList;
        pathGenerator.startPositionX = startPositionX;
        pathGenerator.startPositionZ = startPositionZ;

        actionList = pathGenerator.getPath();
    }

}
