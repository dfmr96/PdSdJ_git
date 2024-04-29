using UnityEngine;

[CreateAssetMenu(menuName = "ItemData/WeaponItemData", fileName = "New WeaponItemData", order = 0)]
public class WeaponItemData : InventoryItemData
{
    public int damage;
    public float reloadSpeed;
    public float fireRate;
    public AmmoItemData compatibleAmmo;
}