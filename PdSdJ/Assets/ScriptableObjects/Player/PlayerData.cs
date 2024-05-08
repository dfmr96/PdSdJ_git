using UnityEngine;

namespace ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Player/PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public float maxHealth;
        public float speedMultiplier;
        public float angularSpeed;
        public float pushingSpeed;
        public float walkingSpeed;
        public float runSpeed;
    }
}