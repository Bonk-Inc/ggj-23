using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockColor : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private Color startColor = Color.green;

    [SerializeField]
    private Color endColor = Color.red;

    private void Update()
    {
        image.color = Color.Lerp(endColor, startColor, image.fillAmount);
    }
}
