using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Inventory;
using UnityEngine;

public class SphereInteractuable : MonoBehaviour, IInteractuable
{
    public void Interact()
    {
        Debug.Log($"{gameObject.name}");
    }
}
