using UnityEngine;

namespace RPG.UI
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] private DamageText damageTextPrefab = null;
        void Start()
        {
            Spawn(30);
        }

        private void Spawn(float damage)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab,transform);
        }
    
    }
}
