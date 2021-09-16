using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fubix.Combat
{
    public class HitEvent : MonoBehaviour
    {

        [SerializeField] GameObject hitEffect = null;
        GameObject player;
        GameObject enemy;

        AudioSource AudioPlayer;
        public bool isAttacking = false;


        void Awake()
        {
            AudioPlayer = GetComponent<AudioSource>();
            player = GameObject.FindGameObjectWithTag("Player");
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (this.gameObject.tag == "Melee")
            {
                MeleeAttack(other);
            }



            // create hit effect, like sparks or what ever
            if (hitEffect != null && other.tag != "SceneObject")
            {
                Instantiate(hitEffect, transform.position, transform.rotation);
            }
        }



        private void MeleeAttack(Collider other)
        {
            bool playerAttacking = player.GetComponent<PlayerFighter>().isAttacking;
            bool playerBlocking = player.GetComponent<PlayerFighter>().isBlocking;

            bool enemyAttacking = enemy.GetComponent<AiFighter>().isAttacking;
            bool enemyBlocking = enemy.GetComponent<AiFighter>().isBlocking;

            bool isPlayerShield = player.GetComponent<PlayerFighter>().playerCurrentWeapon().GetWeaponType()[0] == "Shield";
            bool isEnemyShield = enemy.GetComponent<AiFighter>().enemyCurrentWeapon().GetWeaponType()[0] == "Shield";

            // if enemy is attacking and you are blocking
            if (isPlayerShield && enemyAttacking == true && playerBlocking == true && other.tag == "Player")
            {
                print("Player blocking event");
                BlockDamage(other, player.GetComponent<PlayerFighter>().playerCurrentWeapon(), enemy.GetComponent<AiFighter>().enemyCurrentWeapon());
            }

            // if player is attacking and enemy is blocking
            else if (isEnemyShield && playerAttacking == true && enemyBlocking == true && other.tag == "Enemy")
            {
                print("enemy blocking event");
                BlockDamage(other, enemy.GetComponent<AiFighter>().enemyCurrentWeapon(), player.GetComponent<PlayerFighter>().playerCurrentWeapon());
            }

            // player hitting enemy
            else if (other.tag == "Enemy" && playerAttacking == true && isPlayerShield == false)
            {
                TakeDamage(other, player.GetComponent<PlayerFighter>().playerCurrentWeapon());
                player.GetComponent<PlayerFighter>().isAttacking = false;
            }

            // enemy hitting player
            else if (other.tag == "Player" && enemyAttacking == true && isEnemyShield == false)
            {
                TakeDamage(other, enemy.GetComponent<AiFighter>().enemyCurrentWeapon());
            }

        }


        private void BlockDamage(Collider other, Weapon blockUsed, Weapon attackUsed)
        {
            // reduce incoming damage by shield damage (aka players currentWeapon)
            float damage = attackUsed.GetDamage() - blockUsed.GetDamage();
            string type = "blocked";

            //play "weapon" sound, because a shield is programically a weapon
            AudioPlayer.clip = blockUsed.GetHitSound();
            AudioPlayer.Play();

            //make sure your not healing yourself if shield damage is more then incoming damage
            if (damage < 0) damage = 0;

            // take away whatever damage is left after shield reduction
            other.GetComponent<Stats>().addHealth(-damage, type);

        }


        private void TakeDamage(Collider other, Weapon weaponUsed)
        {
            float damage = weaponUsed.GetDamage();
            string type = weaponUsed.GetDamageType();

            AudioPlayer.clip = weaponUsed.GetHitSound();
            AudioPlayer.Play();

            other.GetComponent<Stats>().addHealth(-damage, type);
        }
    }
}


