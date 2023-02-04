using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField]
    private PlayerInventory inventory;

    [SerializeField]
    private Player stats;

    [SerializeField]
    private TMPro.TextMeshProUGUI moneyDisplay;

    [SerializeField]
    private TMPro.TextMeshProUGUI healthPotionDisplay;

    [SerializeField]
    private TMPro.TextMeshProUGUI staminaPotionDisplay;

    [SerializeField]
    private int healthPotionIncrease = 50, staminaPotionIncrease = 50;

    private void Update()
    {
        moneyDisplay.text = inventory.GetItemCount(ItemType.Money).ToString();
        healthPotionDisplay.text = inventory.GetItemCount(ItemType.HealthPotion).ToString();
        staminaPotionDisplay.text = inventory.GetItemCount(ItemType.StaminaPotion).ToString();
    }

    public void UseHealthPotion()
    {
        inventory.RemoveItem(ItemType.HealthPotion);
        stats.Health.Increase(healthPotionIncrease);
    }

    public void UseStaminaPotion()
    {
        inventory.RemoveItem(ItemType.StaminaPotion);
        stats.Health.Increase(staminaPotionIncrease);
    }

}
