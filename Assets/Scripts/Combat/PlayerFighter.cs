using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fubix.Combat;
using Fubix.Core;
using Fubix.Saving;

namespace Fubix.Combat
{
    public class PlayerFighter : MonoBehaviour, ISaveable
    {
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Transform rightFootTransform = null;
        [SerializeField] Transform leftFootTransform = null;
        [SerializeField] GameObject forwardAim = null;
       // private Weapon defualtWeapon = null; // since implamenting saving system I use the weapons name to equip weapon
       // might not need "defualtWeapon"
        public string defualtWeaponName;
        public string chooseWeapon;


        private Weapon currentWeapon = null;
        private Armor currentArmor = null;
        private float baseAtkSpeed = 1f;
        public bool isAttacking = false;
        public bool isBlocking = false;
       
        AudioSource audioSource;


        [SerializeField] Weapon AttackA;
        [SerializeField] Weapon SideAttackA;
        [SerializeField] Weapon UpAttackA;
        [SerializeField] Weapon DownAttackA;

        [SerializeField] Weapon AttackB;
        [SerializeField] Weapon SideAttackB;
        [SerializeField] Weapon UpAttackB;
        [SerializeField] Weapon DownAttackB;

        [SerializeField] Weapon AttackC;
        [SerializeField] Weapon SideAttackC;
        [SerializeField] Weapon UpAttackC;
        [SerializeField] Weapon DownAttackC;
        Vector3 velocity;

        GameObject Log;
        // Start is called before the first frame update
        void Start()
        {

            // Weapon weapon = Resources.Load<Weapon>(defualtWeaponName);
            //  if (currentWeapon == null) EquipeWeapon(weapon);
            Log = GameObject.FindGameObjectWithTag("ConsoleLog");
            audioSource = GetComponent<AudioSource>();
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
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
        }

        public Weapon GetItemSelect(string itemName)
        {
            return Resources.Load<Weapon>(itemName);
        }

        public void EquipeBodyArmor(Armor armor)
        {
            currentArmor = armor;
            Animator animator = GetComponent<Animator>();
            armor.Spawn();
        }

        public void attackA()// attack A
        {
            bool playerControlsEnabled = this.GetComponent<PlayerController>().controlsEneabled;

            if (playerControlsEnabled)
            {
                float HorizontalJoystickPos = GetComponent<PlayerController>().getMobileJoystickHorizontal();
                float VerticalJoystickPos = GetComponent<PlayerController>().getMobileJoystickVertical();

                if (VerticalJoystickPos >= 0.5 )
                {

                    if (UpAttackA == null)
                    {
                        Log.GetComponent<printLog>().consoleLog("Control Not Set");
                        return;
                    }
                    doWeaponEffect(UpAttackA.name);
                }
                else if (VerticalJoystickPos <= -0.5)
                {
                    if (DownAttackA == null)
                    {
                        Log.GetComponent<printLog>().consoleLog("Control Not Set");
                        return;
                    }
                    doWeaponEffect(DownAttackA.name);
                }
                // side attack---------------------------------------------------------
                else if (HorizontalJoystickPos >= 0.5 || HorizontalJoystickPos <= -0.5 )
                {
                    if (SideAttackA == null)
                    {
                        Log.GetComponent<printLog>().consoleLog("Control Not Set");
                        return;
                    }
                    doWeaponEffect(SideAttackA.name);
                }
                // regular attack----------------------------------------------------
                else if (HorizontalJoystickPos >= 0 && HorizontalJoystickPos <= 0.02 &&
                     HorizontalJoystickPos <= 0 && HorizontalJoystickPos >= -0.02)
                {
                    if (AttackA == null)
                    {
                        Log.GetComponent<printLog>().consoleLog("Control Not Set");
                        return;
                    }
                    doWeaponEffect(AttackA.name);
                }

            }
        }

        public void attackB()// attack B
        {
            bool playerControlsEnabled = this.GetComponent<PlayerController>().controlsEneabled;

            if (playerControlsEnabled)
            {
                float HorizontalJoystickPos = GetComponent<PlayerController>().getMobileJoystickHorizontal();
                float VerticalJoystickPos = GetComponent<PlayerController>().getMobileJoystickVertical();

                // Up attack
                if (VerticalJoystickPos >= 0.5)
                {
                    if (UpAttackB == null)
                    {
                        Log.GetComponent<printLog>().consoleLog("Control Not Set");
                        return;
                    }
                    doWeaponEffect(UpAttackB.name);
                }
                // Down atttack
                else if (VerticalJoystickPos <= -0.5)
                {
                    if (DownAttackB == null)
                    {
                        Log.GetComponent<printLog>().consoleLog("Control Not Set");
                        return;
                    }
                    doWeaponEffect(DownAttackB.name);
                }
                // side attack---------------------------------------------------------
                else if (HorizontalJoystickPos >= 0.5 || HorizontalJoystickPos <= -0.5)
                {
                    if (SideAttackB == null)
                    {
                        Log.GetComponent<printLog>().consoleLog("Control Not Set");
                        return;
                    }
                    doWeaponEffect(SideAttackB.name);
                }
                // regular attack----------------------------------------------------
                else if (HorizontalJoystickPos >= 0 && HorizontalJoystickPos <= 0.02 &&
                     HorizontalJoystickPos <= 0 && HorizontalJoystickPos >= -0.02)
                {
                    if (AttackB == null)
                    {
                        Log.GetComponent<printLog>().consoleLog("Control Not Set");
                        return;
                    }
                    doWeaponEffect(AttackB.name);
                }

              
            }
        }

        public void attackC()// attack C
        {
            bool playerControlsEnabled = this.GetComponent<PlayerController>().controlsEneabled;

            if (playerControlsEnabled)
            {
                float HorizontalJoystickPos = GetComponent<PlayerController>().getMobileJoystickHorizontal();
                float VerticalJoystickPos = GetComponent<PlayerController>().getMobileJoystickVertical();

                // Up attack
                if (VerticalJoystickPos >= 0.5)
                {
                    if (UpAttackC == null)
                    {
                        Log.GetComponent<printLog>().consoleLog("Control Not Set");
                        return;
                    }
                    doWeaponEffect(UpAttackC.name);
                }
                // Down atttack
                else if (VerticalJoystickPos <= -0.5)
                {
                    if (DownAttackC == null)
                    {
                        Log.GetComponent<printLog>().consoleLog("Control Not Set");
                        return;
                    }
                    doWeaponEffect(DownAttackC.name);
                }
                // side attack---------------------------------------------------------
                else if (HorizontalJoystickPos >= 0.5 || HorizontalJoystickPos <= -0.5)
                {
                    if (SideAttackC == null)
                    {
                        Log.GetComponent<printLog>().consoleLog("Control Not Set");
                        return;
                    }
                    doWeaponEffect(SideAttackC.name);
                }
                // regular attack----------------------------------------------------
                else if (HorizontalJoystickPos >= 0 && HorizontalJoystickPos <= 0.02 &&
                     HorizontalJoystickPos <= 0 && HorizontalJoystickPos >= -0.02)
                {
                    if (AttackC == null)
                    {
                        Log.GetComponent<printLog>().consoleLog("Control Not Set");
                        return;
                    }
                    doWeaponEffect(AttackC.name);
                }

               
            }
        }

        private void doWeaponEffect(string attackName)
        {
  
            // look weapon up in Resources file folder
            string weaponName = attackName;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            

            // if weapon does not match my class I can't use it, unless its Mundane
            if (weapon.GetClass() != this.GetComponent<Stats>().GetClass() && weapon.GetClass() != "Mundane")
            {
                Log.GetComponent<printLog>().consoleLog( "You can't activate " + weapon.name );
                return;
            }

            // if weapon slot is a shield or other type of defence move.
            if (weapon.GetWeaponType()[0] == "Shield")
            {
                // roll back dodge
                if (weapon.GetWeaponType().Length > 1 && weapon.GetWeaponType()[1] == "DodgeBack")
                {
                    // moves character back
                    this.GetComponent<PlayerController>().moveBack(5f, 0.5f);
                }
                // tells HitEvent.cs I am blocking (negating) damage.
                isBlocking = true;
            }
            else
            {
                // tells HitEvent.cs I am attacking and trying to do damage
                isAttacking = true;
            }

            //----------------------------------------------------------------
            // *** Weapon deals damage during the hit event in HitEvent.cs ***
            //----------------------------------------------------------------

            EquipeWeapon(weapon);

            // play weapon attack sound
            audioSource.clip = currentWeapon.GetAttackSoundClip();
            audioSource.Play();

            // disable player controlls so I don't move around during attack
            this.GetComponent<PlayerController>().controlsEneabled = false;

            // stop player dead in his tracks when I attack
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

            // play animation for equipped weapon & set attack spd based on weapon
            GetComponent<Animator>().speed = currentWeapon.GetAttackSpeed();
            this.GetComponent<Animator>().SetTrigger("attack");

            // pay cost of doing attack
            GetComponent<Stats>().addEndurance(-weapon.GetEnduranceCost(), weapon.GetWeaponType()[0]);
            GetComponent<Stats>().addSpirit(-weapon.GetSpiritCost(), weapon.GetWeaponType()[0]);


        }

        public Weapon playerCurrentWeapon()
        {
            return currentWeapon;
        }


        //----------------------------------------------------------------
        // public assigning of combat controls/attacks/weapons/moves/ect
        //----------------------------------------------------------------

        // A attacks
        public void assignAttackA(Image sprite)
        {
            AttackA = Resources.Load<Weapon>(chooseWeapon);
            sprite.GetComponent<Image>().sprite = AttackA.GetIconImage();
        }
        public void assignSideAttackA(Image sprite)
        {
            SideAttackA = Resources.Load<Weapon>(chooseWeapon);
            sprite.GetComponent<Image>().sprite = SideAttackA.GetIconImage();
        }
        public void assigUpAttackA(Image sprite)
        {
            UpAttackA = Resources.Load<Weapon>(chooseWeapon);
            sprite.GetComponent<Image>().sprite = UpAttackA.GetIconImage();
        }
        public void assignDownAttackA(Image sprite)
        {
            DownAttackA = Resources.Load<Weapon>(chooseWeapon);
            sprite.GetComponent<Image>().sprite = DownAttackA.GetIconImage();
        }

        // B attacks
        public void assignAttackB(Image sprite)
        {
            AttackB = Resources.Load<Weapon>(chooseWeapon);
            sprite.GetComponent<Image>().sprite = AttackB.GetIconImage();
        }
        public void assignSideAttackB(Image sprite)
        {
            SideAttackB = Resources.Load<Weapon>(chooseWeapon);
            sprite.GetComponent<Image>().sprite = SideAttackB.GetIconImage();
        }
        public void assigUpAttackB(Image sprite)
        {
            UpAttackB = Resources.Load<Weapon>(chooseWeapon);
            sprite.GetComponent<Image>().sprite = UpAttackB.GetIconImage();
        }
        public void assignDownAttackB(Image sprite)
        {
            DownAttackB = Resources.Load<Weapon>(chooseWeapon);
            sprite.GetComponent<Image>().sprite = DownAttackB.GetIconImage();
        }
        
        // C ATTACKS
        public void assignAttackC(Image sprite)
        {
            AttackC = Resources.Load<Weapon>(chooseWeapon);
            sprite.GetComponent<Image>().sprite = AttackC.GetIconImage();
        }
        public void assignSideAttackC(Image sprite)
        {
            SideAttackC = Resources.Load<Weapon>(chooseWeapon);
            sprite.GetComponent<Image>().sprite = SideAttackC.GetIconImage();
        }
        public void assigUpAttackC(Image sprite)
        {
            UpAttackC = Resources.Load<Weapon>(chooseWeapon);
            sprite.GetComponent<Image>().sprite = UpAttackC.GetIconImage();
        }
        public void assignDownAttackC(Image sprite)
        {
            DownAttackC = Resources.Load<Weapon>(chooseWeapon);
            sprite.GetComponent<Image>().sprite = DownAttackC.GetIconImage();
        }

        public object CaptureState()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

           // data["currentWeapon"] = currentWeapon.name;
          
                if(AttackA) data["AttackA"] = AttackA.name;
                if(SideAttackA) data["SideAttackA"] = SideAttackA.name;
                if(UpAttackA) data["UpAttackA"] = UpAttackA.name;
                if(DownAttackA) data["DownAttackA"] = DownAttackA.name;

                if(AttackB) data["AttackB"] = AttackB.name;
                if(SideAttackB) data["SideAttackB"] = SideAttackB.name;
                if(UpAttackB) data["UpAttackB"] = UpAttackB.name;
                if(DownAttackB) data["DownAttackB"] = DownAttackB.name;

                if(AttackC) data["AttackC"] = AttackC.name;
                if(SideAttackC) data["SideAttackC"] = SideAttackC.name;
                if(UpAttackC) data["UpAttackC"] = UpAttackC.name;
                if(DownAttackC) data["DownAttackC"] = DownAttackC.name;
            
           

         
            return data;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> data = (Dictionary<string, object>)state;
        
            if (data.ContainsKey("AttackA")) AttackA = Resources.Load<Weapon>((string)data["AttackA"]);
            if (data.ContainsKey("SideAttackA")) SideAttackA = Resources.Load<Weapon>((string)data["SideAttackA"]);
            if (data.ContainsKey("UpAttackA")) UpAttackA = Resources.Load<Weapon>((string)data["UpAttackA"]);
            if (data.ContainsKey("DownAttackA")) DownAttackA = Resources.Load<Weapon>((string)data["DownAttackA"]);

            if (data.ContainsKey("AttackB")) AttackB = Resources.Load<Weapon>((string)data["AttackB"]);
            if (data.ContainsKey("SideAttackB")) SideAttackB = Resources.Load<Weapon>((string)data["SideAttackB"]);
            if (data.ContainsKey("UpAttackB")) UpAttackB = Resources.Load<Weapon>((string)data["UpAttackB"]);
            if (data.ContainsKey("DownAttackB")) DownAttackB = Resources.Load<Weapon>((string)data["DownAttackB"]);

            if (data.ContainsKey("AttackC")) AttackC = Resources.Load<Weapon>((string)data["AttackC"]);
            if (data.ContainsKey("SideAttackC")) SideAttackC = Resources.Load<Weapon>((string)data["SideAttackC"]);
            if (data.ContainsKey("UpAttackC")) UpAttackC = Resources.Load<Weapon>((string)data["UpAttackC"]);
            if (data.ContainsKey("DownAttackC")) DownAttackC = Resources.Load<Weapon>((string)data["DownAttackC"]);

        }



        // animator events
        void Hit()
        {
            isAttacking = false;
        }
        void AttackEnd()
        {
            this.GetComponent<PlayerController>().controlsEneabled = true;
            GetComponent<Animator>().speed = baseAtkSpeed;
            isAttacking = false;
            isBlocking = false;
        }
        void Shoot()
        {
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform,
                                               rightFootTransform, leftFootTransform, forwardAim);
            }
            GetComponent<Animator>().speed = baseAtkSpeed;
            audioSource.clip = currentWeapon.GetAttackSoundClip();
            audioSource.Play();
        }
    }

}
