using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoadSettings : MonoBehaviour
{

    [SerializeField]
    private AudioSetting[] settings;
    [SerializeField]
    private SubtitleSetting titleSetting;

    private void Start()
    {
        foreach (var setting in settings)
        {
            setting.LoadSetting();
            titleSetting.LoadSetting();
        }
    }
}
