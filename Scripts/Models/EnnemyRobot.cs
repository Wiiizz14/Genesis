using UnityEngine;

namespace Assets.Ressources.Scripts.Models
{
    public class EnnemyRobot : MonoBehaviour
    {
        [SerializeField] private Collider colliderEnnemy;
        [SerializeField] private GameObject firePoint;
        [SerializeField] private float speedMove;
        [SerializeField] private float speedRotation;
        [SerializeField] private float acceleration;
        [SerializeField] private float lookRadius;
        [SerializeField] private float attackRadius;
        [SerializeField] private float reactivity;


        // Getters & Setters
        public Collider ColliderEnnemy
        {
            get
            {
                return this.colliderEnnemy;
            }
        }


        public GameObject FirePoint
        {
            get
            {
                return this.firePoint;
            }
        }


        public float SpeedMove
        {
            get
            {
                return this.speedMove;
            }
            private set
            {
                this.speedMove = value;
            }
        }


        public float SpeedRotation
        {
            get
            {
                return this.speedRotation;
            }
            private set
            {
                this.speedRotation = value;
            }
        }


        public float Acceleration
        {
            get
            {
                return this.acceleration;
            }
            private set
            {
                this.acceleration = value;
            }
        }


        public float LookRadius
        {
            get
            {
                return this.lookRadius;
            }
            private set
            {
                this.lookRadius = value;
            }
        }


        public float AttackRadius
        {
            get
            {
                return this.attackRadius;
            }
            private set
            {
                this.attackRadius = value;
            }
        }


        public float Reactivity
        {
            get
            {
                return this.reactivity;
            }
            private set
            {
                this.reactivity = value;
            }
        }
    }
}
