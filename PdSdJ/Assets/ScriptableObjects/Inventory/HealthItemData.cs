using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemData/HealthItemData", fileName = "New HealthItemData", order = 0)]
public class HealthItemData : InventoryItemData , IHealer
{
    public int healAmount;

    public int HealAmount => healAmount;

    public void Heal()
    {
        Debug.Log($"{HealAmount}HP healed");
    }
}