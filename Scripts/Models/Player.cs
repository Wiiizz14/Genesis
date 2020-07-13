using UnityEngine;

namespace Assets.Ressources.Scripts.Models
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private GameObject firePoint;
        [SerializeField] private float speedMove;
        [SerializeField] private float speedRotation;


        // Getters & Setters
        public CharacterController Controller
        {
            get
            {
                return this.controller;
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


    }
}
