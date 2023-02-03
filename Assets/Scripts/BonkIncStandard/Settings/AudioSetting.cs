using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Events;

public class AudioSetting : Setting<float>
{
    [SerializeField]
    private string mixerSetting = "volume";

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private float minDB = -80, maxDB = 0;

    [SerializeField]
    private UnityEvent OnVolumeSet;

    private void SetVolume()
    {
        mixer.SetFloat(mixerSetting, LinearToLog(slider.value));

        SetSetting(LinearToLog(slider.value));
        OnVolumeSet?.Invoke();
    }

    private float LogToLinear(float log)
    {
        return Mathf.Pow(10, (log / 20));
    }

    private float LinearToLog(float linear)
    {
        return Mathf.Log10(linear) * 20;
    }

    public override float LoadSetting()
    {
        playerPrefKey = $"vol_{settingName}";
        float startValue;
        if (PlayerPrefs.HasKey(playerPrefKey))
        {
            startValue = PlayerPrefs.GetFloat(playerPrefKey);
            mixer.SetFloat(mixerSetting, startValue);
        }
        else
        {
            mixer.GetFloat(mixerSetting, out startValue);
        }
        return startValue;
    }

    public override void SetupSetting()
    {
        if (slider == null) return;
        slider.maxValue = LogToLinear(maxDB);
        slider.minValue = LogToLinear(minDB);

        slider.value = LogToLinear(LoadSetting());

        slider.onValueChanged.AddListener((val) => SetVolume());
    }

    public override void SetSetting(float value)
    {
        PlayerPrefs.SetFloat(playerPrefKey, value);
    }
}