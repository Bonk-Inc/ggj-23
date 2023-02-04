using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolUI : MonoBehaviour
{

    [SerializeField]
    private TMPro.TextMeshProUGUI amountDisplay;


    [SerializeField]
    private Image icon;

    public void SetTool(Item item)
    {
        amountDisplay.text = item.amount.ToString();
        icon.sprite = item.data.Icon;
    }
}
