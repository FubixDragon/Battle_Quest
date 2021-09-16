using System.Collections;
using System.Collections.Generic;
using Fubix.Core;
using UnityEngine;

namespace Fubix.Combat
{
    public class AiFighter : MonoBehaviour
    {
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Transform rightFootTransform = null;
        [SerializeField] Transform leftFootTransform = null;
        [SerializeField] GameObject forwardAim = null;
        // private Weapon defualtWeapon = null; // since implamenting saving system I use the weapons name to equip weapon
        // might not need "defualtWeapon"
        public string defualtWeaponName;



        //  [SerializeField] Button AttackButton;
        private Weapon currentWeapon = null;
        private Armor currentArmor = null;
        public bool isAttacking = false;
        public bool isBlocking = false;
        AudioSource audioSource;


        [SerializeField] Weapon AttackA;
        [SerializeField] Weapon SideAttackA;
        [SerializeField] Weapon UpAttackA;
        [SerializeField] Weapon DownAttackA;

        [SerializeField] Weapon AttackB;
        [SerializeField] Weapon SideAttackB;

        [SerializeField] Weapon AttackC;
        [SerializeField] Weapon SideAttackC;



        // Start is called before the first frame update
        void Start()
        {
            Weapon weapon = Resources.Load<Weapon>(defualtWeaponName);
            if (currentWeapon == null) EquipeWeapon(weapon);

            audioSource = GetComponent<AudioSource>();
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                attackA();
            }
        }


        public void EquipeWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform,
                         rightFootTransform, leftFootTransform, animator, forwardAim);
            //AttackButton.image.sprite = currentWeapon.GetIconImage();
        }


        public void EquipeBodyArmor(Armor armor)
        {
            currentArmor = armor;
            Animator animator = GetComponent<Animator>();
            armor.Spawn();
        }




        public void attackA()// attack A
        {
            bool playerControlsEnabled = this.GetComponent<AiController>().controlsEneabled;




            doWeaponEffect(AttackA.name);


            isAttacking = true;
            audioSource.clip = currentWeapon.GetAttackSoundClip();
            audioSource.Play();
            this.GetComponent<AiController>().controlsEneabled = false;
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

            GetComponent<Animator>().speed = currentWeapon.GetAttackSpeed();
            this.GetComponent<Animator>().SetTrigger("attack");

        }






        private void doWeaponEffect(string attackName)
        {
            string weaponName = attackName;
            Weapon weapon = Resources.Load<Weapon>(weaponName);

            if (weapon.GetWeaponType()[0] == "Shield")
            {
                isBlocking = true;
            }

            EquipeWeapon(weapon);

            GetComponent<Stats>().addEndurance(-weapon.GetEnduranceCost(), weapon.GetWeaponType()[0]);
            GetComponent<Stats>().addSpirit(-weapon.GetSpiritCost(), weapon.GetWeaponType()[0]);
        }




        public Weapon enemyCurrentWeapon()
        {
            return currentWeapon;
        }


        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipeWeapon(weapon);
        }








        // animator events
        void Hit()
        {
            isAttacking = false;
        }
        void AttackEnd()
        {
            this.GetComponent<AiController>().controlsEneabled = true;
            isBlocking = false;
        }
        void Shoot()
        {
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform,
                                               rightFootTransform, leftFootTransform, forwardAim);
            }
            audioSource.clip = currentWeapon.GetAttackSoundClip();
            audioSource.Play();
        }
    }
}




