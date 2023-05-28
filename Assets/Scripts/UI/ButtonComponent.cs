using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonComponent : MonoBehaviour
{
    private GameManager GameManager;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameManager.instance.GetComponent<GameManager>();
        GetComponent<Button>().onClick.AddListener(ButtonOnClick);
    }

    

    void ButtonOnClick()
    {
        if (active)
        {
            activateButton();
        }
        
        GameManager.SoundManager.GetComponent<SoundManager>().ButtonClickAudio();
    }

    void activateButton()
    {
        GetComponent<Animator>().SetBool("Active", true);
    }

    public void deactivateButton()
    {
        GetComponent<Animator>().SetBool("Active", false);
        GetComponent<Animator>().SetBool("Normal", true);
    }
}
