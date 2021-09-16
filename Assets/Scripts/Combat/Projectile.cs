using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fubix.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float lifeAfterImpact = 1f;

        private float parentDamage = 0f;
        private string[] parentDamageType;


        GameObject target = null;

        private void Awake()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

            if (isHoming)
            {
                transform.LookAt(target.transform.position);
            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }


        public void SetTarget(GameObject target, float damage, string[] type)
        {
            parentDamage = damage;
            parentDamageType = type;

            this.target = target;
            transform.LookAt(target.transform.position);
        }




        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Stats>())
            {
                other.GetComponent<Stats>().addHealth(-parentDamage, parentDamageType[0]);
            }

            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, transform.rotation);
            }
            speed = 0;
            Destroy(gameObject, lifeAfterImpact);
        }

    }
}
