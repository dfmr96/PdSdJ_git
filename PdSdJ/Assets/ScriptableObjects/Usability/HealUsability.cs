using UnityEngine;

[CreateAssetMenu(menuName = "UsabilityData/HealthUsability", fileName = "New HealthUsability", order = 0)]
public class HealUsability : UsabilityData
{
    public int healAmount;
    public override void Use()
    {
        Debug.Log($"{healAmount}HP curado");
    }
}