using UnityEngine;

[CreateAssetMenu(menuName = "CombinableInfo", fileName = "New CombinableInfo", order = 0)]
public class CombinableInfo : ScriptableObject
{
    public CombinationTable combinationTable;
        
    public InventoryItemData GetCombinationResult(InventoryItemData itemA, InventoryItemData itemB)
    {
        for (int i = 0; i < combinationTable.combinations.Count; i++)
        {
            if (itemA == combinationTable.combinations[i].itemA && itemB == combinationTable.combinations[i].itemB)
            {
                return combinationTable.combinations[i].combinationResult;
            }

            if (itemA == combinationTable.combinations[i].itemB && itemB == combinationTable.combinations[i].itemA)
            {
                return combinationTable.combinations[i].combinationResult;
            }
        }
        Debug.Log("No se puede combinar");
        return null;
    }
}