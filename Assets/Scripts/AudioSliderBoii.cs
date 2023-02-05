using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliderBoii : MonoBehaviour
{

    AudioSource source;

    [SerializeField]
    private Slider slider;

    private void Start()
    {
        source = GameObject.Find("Music").GetComponent<AudioSource>();

        source.volume = PlayerPrefs.GetFloat("Volume", source.volume);
        slider.value = source.volume;
    }

    public void SetAudio(float value)
    {
        source.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

}
