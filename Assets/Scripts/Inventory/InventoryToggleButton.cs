using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryToggleButton : MonoBehaviour
{

    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        PlayerInventory.Instance.OnToolsChanged += () =>
        {
            button.interactable = PlayerInventory.Instance.GetToolsList().Count > 0;
        };
        button.interactable = PlayerInventory.Instance?.GetToolsList()?.Count > 0;
    }

}
