using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Setting <T> : MonoBehaviour
{
    [SerializeField]
    protected string settingName;
    protected string playerPrefKey;

    public abstract void SetupSetting();

    public abstract T LoadSetting();

    public abstract void SetSetting(T value);

    private void Awake()
    {
        SetupSetting();
    }
}
