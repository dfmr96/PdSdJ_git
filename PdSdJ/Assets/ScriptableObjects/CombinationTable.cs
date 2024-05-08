using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CombinationTable", fileName = "New Combination Table", order = 0)]
public class CombinationTable : ScriptableObject
{
    public List<CombinationData> combinations;
}