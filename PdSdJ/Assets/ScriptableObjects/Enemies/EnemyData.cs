using UnityEngine;

namespace ScriptableObjects.Enemies
{
    [CreateAssetMenu(fileName = "New EnemyData", menuName = "Enemy/New EnemyData", order = 0)]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string Name { get; private set;}
        [field: SerializeField] public float Health { get; private set;}
        [field: SerializeField] public float Damage { get; private set;}
        [field: SerializeField] public float Speed { get; private set;}
        [field: SerializeField] public Enemy EnemyPrefab { get; private set;}
    }
}