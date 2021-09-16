using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fubix.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Vector3 newPosition;
        [SerializeField] float moveForce = 0.85f;
        [SerializeField] float speed = 10f;
        [SerializeField] float force = 0f;
        protected Joystick joystick;


        public bool controlsEneabled = true;
        private float otherMoveTime = 0f;

        Quaternion Left;
        Quaternion Right;


        Rigidbody ridgidbody;

        void Start()
        {
            speed = GetComponent<Stats>().GetSpeed();

            joystick = FindObjectOfType<Joystick>();
            ridgidbody = this.GetComponent<Rigidbody>();

            Left = new Quaternion(transform.rotation.x, -transform.rotation.y, transform.rotation.z, transform.rotation.w);
            Right = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            
        }


        void Update()
        {
            MoveTo();
            UpdateAnimator();
        }



        private void MoveTo()
        {
            rotateWithJoystick();

            if (otherMoveTime > 0)// 0.1 so that it ends a little before reaching to height or it looks jerky
            {
                // count down timer for how long this type of movement should happen
                otherMoveTime -= Time.deltaTime;
                transform.Translate(Vector3.back * moveForce * Time.deltaTime);

                // kep this in case I want to use code later, don't want to lose it
                // transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, moveForce);
            }
            else
            {
                // used with above commented code to help keep stuff anchored
               //  newPosition.y = 0;
                 otherMoveTime = 0;

                if (controlsEneabled)
                {
                    speed = GetComponent<Stats>().GetSpeed();

                    ridgidbody.velocity = new Vector3(joystick.Horizontal * speed,
                              (ridgidbody.velocity.y),
                               joystick.Vertical * speed);

                }

            }
        }
        public void moveBack(float speed, float time)
        {
            otherMoveTime = time;
            moveForce = speed;

        }
      

        private void rotateWithJoystick()
        {
            if (joystick.Horizontal < 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Left, Time.deltaTime * 50);
            }
            // turns transform to the right
            if (joystick.Horizontal > 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Right, Time.deltaTime * 50);
            }
        }

        private void UpdateAnimator()
        {
            Vector3 mobileVelocity = ridgidbody.velocity;
            float animSpeed = -mobileVelocity.x;
            GetComponent<Animator>().SetFloat("forwardSpeed", animSpeed);// incase I don't want to use "Apply Root Motion"

        }
        public float getMobileJoystickHorizontal()
        {
            return joystick.Horizontal;
        }

        public float getMobileJoystickVertical()
        {
            return joystick.Vertical;
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

