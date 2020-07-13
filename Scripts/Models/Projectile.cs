using UnityEngine;

namespace Assets.Ressources.Scripts.Models
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float fireRate;
        [SerializeField] private float speed;
        [SerializeField] private int damageBase;


        // Getters & Setters
        public float FireRate
        {
            get
            {
                return this.fireRate;
            }
            private set
            {
                this.fireRate = value;
            }
        }


        public float Speed
        {
            get
            {
                return this.speed;
            }
            set
            {
                this.speed = value;
            }
        }

        
        public int DamageBase
        {
            get
            {
                return this.damageBase;
            }
        }
        
    }
}
