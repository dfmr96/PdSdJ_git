using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotViewer : MonoBehaviour
{
    [SerializeField] private InventoryItemData itemData;
    [SerializeField] private Image image;
    [SerializeField] private Image checkImage;
    [SerializeField] private TMP_Text amount;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;

    [ContextMenu("RefreshData")]
    public void RefreshData()
    {
        image.sprite = itemData.sprite;
        checkImage.sprite = itemData.sprite;
        amount.SetText($"{itemData.amount}");
        itemName.SetText($"{itemData.name}");
        itemDescription.SetText($"{itemData.description}");
    }

    private void OnEnable()
    {
        RefreshData();
    }

    [ContextMenu("Check item")]
    public void Check()
    {
        itemData.Check();
    }

    [ContextMenu("Use item")]
    public void Use()
    {
        switch (itemData)
        {
            case IHealer healItemData:
                healItemData.Heal();
                break;
            case IEquipable weaponItemData:
                weaponItemData.Equip();
                break;
        }

        //else if (itemData is )
    }
}
