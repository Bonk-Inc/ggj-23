using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class LoadingScreen : MonoBehaviour
{
    public abstract void OpenLoadingScreen(Action onOpened = null);
    public abstract void CloseLoadingScreen(Action onClosed = null);

}