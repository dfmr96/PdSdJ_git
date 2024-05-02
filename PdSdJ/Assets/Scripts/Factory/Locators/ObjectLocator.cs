using System;
using UnityEngine;

namespace Factory
{
    public abstract class ObjectLocator : MonoBehaviour
    {
        [field: SerializeField] public string ObjectName { get; protected set; }

        private void Start()
        {
            name = ObjectName;
        }
    }
}