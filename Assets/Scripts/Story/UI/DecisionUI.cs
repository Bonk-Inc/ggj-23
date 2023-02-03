using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionUI : MonoBehaviour
{

    [SerializeField]
    private TMPro.TextMeshProUGUI titleUI;

    [SerializeField]
    private Image image;

    public Decision Decision { get; private set; }

    public void SetDecision(Decision decision)
    {
        Decision = decision;
        image.sprite = decision.Image;
        titleUI.text = decision.Title;
    }

}
