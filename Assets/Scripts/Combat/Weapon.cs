using System;
using UnityEngine;


namespace Fubix.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] bool isHandWeapon = true;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        [SerializeField] AudioClip AttackSound = null;
        [SerializeField] AudioClip HitSound = null;
        [SerializeField] Sprite IconImage = null;

        // weapon basic stats
        [SerializeField] float WeaponRange = 0f;
        [SerializeField] float KnockBack = 0f;
        [SerializeField] float WeaponDamage = 0f;
        [SerializeField] string WeaponClass = "";
        [SerializeField] float AttackSpeed = 1f;
        [SerializeField] float ArielKnockBack = 0f;
        [SerializeField] string DamageType = null;
        [SerializeField] string[] WeaponType = null;
        [SerializeField] float EnduranceCost = 0f;
        [SerializeField] float SpiritCost = 0f;

        const string weaponName = "Weapon";


        public void Spawn(Transform rightHand, Transform leftHand,
                          Transform rightFoot, Transform leftFoot,
                          Animator animator, GameObject forwardAim)
        {
            DestroyOldWeapon(rightHand, leftHand, rightFoot, leftFoot);

            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand, 
                                                       rightFoot, leftFoot, forwardAim);
                GameObject weapon = Instantiate(equippedPrefab, handTransform);
                weapon.name = weaponName;
            }

            var overridController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overridController != null)
            {
                animator.runtimeAnimatorController = overridController.runtimeAnimatorController;
            }


        }



        private void DestroyOldWeapon(Transform rightHand, Transform leftHand,
                                      Transform rightFoot, Transform leftFoot)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if(oldWeapon == null)
            {
                oldWeapon = rightFoot.Find(weaponName);
            }
            if(oldWeapon == null)
            {
                oldWeapon = leftFoot.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);

        }


        private Transform GetTransform(Transform rightHand, Transform leftHand,
                                        Transform rightFoot, Transform leftFoot, 
                                        GameObject forwardAim)
        {
            Transform handTransforom;

            if (isHandWeapon)
            {
                if (isRightHanded) handTransforom = rightHand;
                else handTransforom = leftHand;
            }
            else
            {
                if (isRightHanded) handTransforom = rightFoot;
                else handTransforom = leftFoot;
            }
            
            return handTransforom;
        }



        public bool HasProjectile()
        {
            return projectile != null;
        }

       

        public void LaunchProjectile(Transform rightHand, Transform leftHand,
                                     Transform rightFoot, Transform leftFoot, GameObject forwardAim)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand, 
                                                                                 rightFoot, leftFoot, 
                                                                                 forwardAim).position, Quaternion.identity);
            projectileInstance.SetTarget(forwardAim, WeaponDamage, WeaponType);

        }

        public GameObject giveProjectileTarget(GameObject gameTarget)
        {
            return gameTarget;
        }


    

        //------------------ Weapon stats ---------
        public float GetRange()
        {
            return WeaponRange;
        }

        public float GetKnockBack()
        {
            return KnockBack;
        }

        public float GetDamage()
        {
            return WeaponDamage;
        }
        public string GetClass()
        {
            return WeaponClass;
        }
        public float GetAttackSpeed()
        {
            return AttackSpeed;
        }
        public float GetArielKnockBack()
        {
            return ArielKnockBack;
        }

        public string GetDamageType()
        {
            return DamageType;
        }

        public float GetEnduranceCost()
        {
            return EnduranceCost;
        }

        public float GetSpiritCost()
        {
            return SpiritCost;
        }

        public string[] GetWeaponType()
        {
            return WeaponType;
        }

        public Sprite GetIconImage()
        {
            return IconImage;
        }
        public AudioClip GetAttackSoundClip()
        {
            return AttackSound;
        }
        public AudioClip GetHitSound()
        {
            return HitSound;
        }


    }
}
