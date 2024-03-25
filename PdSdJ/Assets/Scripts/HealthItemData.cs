using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemData/HealthItemData", fileName = "New HealthItemData", order = 0)]
public class HealthItemData : InventoryItemData
{
    public void Use()
    {
        usabilityData.Use();
    }
}