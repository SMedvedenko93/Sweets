using Newtonsoft.Json;
using System.Collections;
using UnityEngine;

public class LoadingGameScreen : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayCoroutine());
    }


    IEnumerator DelayCoroutine()
    {
    yield return new WaitForSeconds(3);
    ScreenManager.instance.ShowMainScreen();
    }
}
