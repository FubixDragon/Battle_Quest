using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fubix.Core
{
    public class AiController : MonoBehaviour
    {
        [SerializeField] Vector3 newPosition;
        [SerializeField] float moveForce = 0.85f;
        [SerializeField] float speed = 10f;
        [SerializeField] float force = 0f;
        protected Joystick joystick;


        public bool controlsEneabled = true;
        private Vector3 velocity;

        Quaternion Left;
        Quaternion Right;


        Rigidbody ridgidbody;

        void Start()
        {
            speed = GetComponent<Stats>().GetSpeed();

            ridgidbody = this.GetComponent<Rigidbody>();

            Left = new Quaternion(transform.rotation.x, -transform.rotation.y, transform.rotation.z, transform.rotation.w);
            Right = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);

        }


        void Update()
        {
            UpdateAnimator();
        }



        
        

        private void UpdateAnimator()
        {
            Vector3 mobileVelocity = ridgidbody.velocity;
            float animSpeed = -mobileVelocity.x;
            GetComponent<Animator>().SetFloat("forwardSpeed", animSpeed);// incase I don't want to use "Apply Root Motion"

        }
        


        // animator trigger events



        void End()
        {

        }



        // after getting hit
        void Recovered()
        {


        }



        void FootL()
        {

        }
        void FootR()
        {

        }


    }
}


