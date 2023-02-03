using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditURL : MonoBehaviour
{
    [SerializeField]
    private string url;

    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}
