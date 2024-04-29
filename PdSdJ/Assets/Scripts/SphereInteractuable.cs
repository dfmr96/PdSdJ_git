using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereInteractuable : MonoBehaviour, IInteractuable
{
    public void Interact()
    {
        Debug.Log($"{gameObject.name}");
    }
}
