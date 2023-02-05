using System;
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

    public event Action<Decision> OnChosen;

    private bool canAfford = true;
    private bool isStoreCard = true;
    private int price = -1;

    public void SetDecision(Decision decision)
    {
        Decision = decision;
        image.sprite = decision.Image;
        titleUI.text = decision.Title;
    }

    public void SetStoreVisuals(Sprite sprite, string title, int price)
    {
        isStoreCard = true;
        this.price = price;
        UpdateAfforability();
        PlayerInventory.Instance.OnMoneyChanged += UpdateAfforability;

        image.sprite = sprite;
        titleUI.text = $"{title} ({price})";
    }

    private void UpdateAfforability()
    {
        canAfford = PlayerInventory.Instance.GetItemCount(ItemType.Money) >= price;
        if (!canAfford)
        {
            image.color = Color.gray;
        }
        else
        {
            image.color = Color.white;
        }
    }

    private void OnDisable()
    {
        if (PlayerInventory.Instance != null)
            PlayerInventory.Instance.OnMoneyChanged -= UpdateAfforability;
    }

    public void Choose()
    {
        if (isStoreCard && !canAfford)
        {
            return;
        }

        OnChosen?.Invoke(Decision);
    }


}
