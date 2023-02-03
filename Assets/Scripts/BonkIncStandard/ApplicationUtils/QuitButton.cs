using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_EDITOR || UNITY_WEBGL
        gameObject.SetActive(false);
#endif
    }

    public void Quit()
    {
        Application.Quit();
    }
}