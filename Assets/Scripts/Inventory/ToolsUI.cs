using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolsUI : MonoBehaviour
{

    [SerializeField]
    private PlayerInventory inventoryUI;

    [SerializeField]
    private Transform toolsDisplay;

    [SerializeField]
    private ToolUI iconPrefab;

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void OnEnable()
    {
        inventoryUI.OnToolsChanged += RefreshUI;
        RefreshUI();
    }

    private void OnDisable()
    {
        inventoryUI.OnToolsChanged -= RefreshUI;
    }

    private void RefreshUI()
    {
        toolsDisplay.CommitChildAbortion();
        var tools = inventoryUI.GetToolsList();
        foreach (var tool in tools)
        {
            var icon = Instantiate(iconPrefab);
            icon.transform.SetParent(toolsDisplay, false);
            icon.SetTool(tool);
        }
    }

}
