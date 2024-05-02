using UnityEngine;

namespace Inventory
{
    public abstract class InventoryItemData : ScriptableObject , ICheckable
    {
        public new string name;
        public string description;
        public Sprite sprite;
        public int amount;
        public UsabilityData usabilityData;
        public CombinableInfo combinableInfo;
    

        public void Check()
        {
            Debug.Log($"Check called. {name}");
        }
    }
}