using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseManager : MonoBehaviour
{
    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "no_ads":
                removeAds();
                break;
            case "1_hint":
                add1Hiht();
                break;
            case "5_hint":
                add5Hiht();
                break;
            case "10_hint":
                add10Hint();
                break;
        }
    } 

    public void add1Hiht()
    {
        GameManager.instance.GetComponent<GameManager>().ChangeHint(1);
        Debug.Log("add 1 hint");
    }

    public void add5Hiht()
    {
        GameManager.instance.GetComponent<GameManager>().ChangeHint(5);
        Debug.Log("add 5 hints");
    }

    public void add10Hint()
    {
        GameManager.instance.GetComponent<GameManager>().ChangeHint(10);
        Debug.Log("add 10 hints");
    }

    public void removeAds()
    {
        GameManager.instance.GetComponent<GameManager>().removeAds();
    }
}
