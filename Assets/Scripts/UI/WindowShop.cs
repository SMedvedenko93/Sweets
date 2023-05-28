using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowShop : MonoBehaviour
{
    public GameObject Overlay;

    public GameObject RemoveAdsButton;
    public GameObject RestoreButton;
    public GameObject CloseButton;

    private GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameManager.instance.GetComponent<GameManager>();

        RemoveAdsButton.GetComponent<Button>().onClick.AddListener(RemoveAdsButtonOnClick);
        RestoreButton.GetComponent<Button>().onClick.AddListener(RestoreButtonOnClick);

        CloseButton.GetComponent<Button>().onClick.AddListener(CloseButtonOnClick);
    }

    private void CloseButtonOnClick()
    {
        Overlay.SetActive(false);
        gameObject.SetActive(false);
    }

    private void RestoreButtonOnClick()
    {

    }

    private void RemoveAdsButtonOnClick()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
