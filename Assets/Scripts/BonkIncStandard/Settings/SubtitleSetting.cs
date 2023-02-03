using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleSetting : Setting<int>
{
    [SerializeField]
    private TextMeshProUGUI optionText;
    [SerializeField]
    private string defaultText = "Subtitle Size: ";

    [SerializeField]
    private int defaultValue = 36;

    [SerializeField]
    private SubSettingInfo[] options;

    private SubSettingInfo currentOption;

    [SerializeField]
    private TextMeshProUGUI subtitleText;

    public override int LoadSetting()
    {
        playerPrefKey = $"setting_{settingName}";
        int returnValue = defaultValue;
        if (PlayerPrefs.HasKey(playerPrefKey))
        {
            returnValue = PlayerPrefs.GetInt(playerPrefKey);
        }
        return returnValue;
    }

    public override void SetSetting(int value)
    {
        PlayerPrefs.SetInt(playerPrefKey, value);
    }

    public override void SetupSetting()
    {
        var val = LoadSetting();
        currentOption = options[FindOption(val)];
        UpdateSettings(currentOption);
    }

    public void ChooseNextOption()
    {
        var pos = FindOption(currentOption.Value);
        var next = (pos + 1) % options.Length;
        UpdateSettings(options[next]);
        SetSetting(options[next].Value);
    }

    private void UpdateSettings(SubSettingInfo option)
    {
        print(option);
        if(optionText != null) optionText.text = defaultText + option.Tag;
        if (subtitleText != null) subtitleText.fontSize = option.Value;
        currentOption = option;
    }

    private int FindOption(int value)
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (value == options[i].Value) return i;
        }
        return 0;
    }

    [System.Serializable]
    private class SubSettingInfo
    {
        [SerializeField]
        private int value;
        [SerializeField]
        private string tag;

        public int Value => value;
        public string Tag => tag;
    }
}
