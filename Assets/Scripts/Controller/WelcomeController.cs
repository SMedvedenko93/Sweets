using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeController : MonoBehaviour
{
    [SerializeField]
    private GameObject slideLeft, slideRight, characterList, tutorial;
    [SerializeField]
    private Texture[] images;
    [SerializeField]
    private string[] texts;
    [SerializeField]
    private Position[] positions;

    private GameManager GameManager;
    private int slideIndex; 

    enum Position { LEFT = 1, RIGHT = 2};

    void Start()
    {
        GameManager = GameManager.instance.GetComponent<GameManager>();
        //GameManager.SoundManager.GetComponent<SoundManager>().StartMusicGame();

        characterList.SetActive(false);
        tutorial.SetActive(false);

        slideIndex = 0;

        NextSlide();

        slideLeft.GetComponent<Button>().onClick.AddListener(SlideOnClick);
        slideRight.GetComponent<Button>().onClick.AddListener(SlideOnClick);
    }

    public void SlideOnClick()
    {
        slideIndex = slideIndex + 1;
        if (slideIndex >= texts.Length)
        {
            gameObject.SetActive(false);
            characterList.SetActive(true);
            tutorial.SetActive(true);
            GameManager.ChangeWelcome();
        }
        else
        {
            NextSlide();
        } 
    }

    void NextSlide()
    {
        if (positions[slideIndex] == Position.LEFT)
        {
            slideLeft.SetActive(true);
            slideRight.SetActive(false);
            slideLeft.GetComponent<RawImage>().texture = images[slideIndex];
            slideLeft.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = texts[slideIndex];
        }
        else
        {
            slideLeft.SetActive(false);
            slideRight.SetActive(true);
            slideRight.GetComponent<RawImage>().texture = images[slideIndex];
            slideRight.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = texts[slideIndex];
        }


    }
}
