using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItemData : ScriptableObject , ICheckable
{
    public new string name;
    public string description;
    public Sprite sprite;
    public int amount;

    public void Check()
    {
        Debug.Log($"Check called. {name}");
    }
}