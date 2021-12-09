using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    // These are variables for tracking what the host Objects hitbox, rigidbody (for physics impulses), renderer & collider, and gibs.
    private Rigidbody hostRigidbody;

    [Header("Host Information & gibs object")]
    public GameObject hostHitBox;
    public BoxCollider hostCollider;
    public AudioClip deathNoise;
    public GameObject hostBody;
    public AudioClip hitNoise;
    public GameObject gibs;
    private AudioSource audioSource;


    [Header("Object Type. Set if this is a NPC with HP or a Environmental Object")] // this is to track what the host game object is supposed to be. "objects" are environmental pieces with no HP. Ënemies are NPCs with AI and HP.
    public bool isEnemy;
    public bool isObject;


    [Header("Damage Tracking. Set HP to 0 for Objects")]
    public float HP;
    public bool hasBeenHit;
    public float dmgTimer;
    public float dmgCoolDown = 1.0f; // this float represents the numbers of seconds that must pass before the object can be "hit" again and have a reaction. This is to prevent spam & to change hit windows independantly between enemy types.



    [Header("Damage Type Numbers")] // these are the damage values the game object holding this script will take based on each weapon type
    public float dmgTakenFromSword;
    public float dmgTakenFromDagger;
    public float dmgTakenFromAxe;
    public float dmgTakenFromPistol;
    public float dmgTakenFromShotgunPellet;

    

    private void Awake()
    { 
        hostRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        // check to make sure host can be only a Object or a Enemy. This is to prevent it triggering multiple functions & running the wrong code.
        if (isEnemy)
        {
            isObject = false;
        }
        if (isObject)
        {
            isEnemy = false;
        }
    }



    void FixedUpdate()
    {
        //every cycle of the fixed update function, sets dmgTimer to the number of seconds that have passed since it was last updated. Used for dmg tracking.
        dmgTimer = dmgTimer + Time.deltaTime;
        transform.position = hostBody.transform.position;
        gibs.transform.position = hostBody.transform.position + new Vector3(0, -1, 0);
    }


    private void OnTriggerEnter(Collider other)
    {
        // checks if the damage cooldown time limit has elapsed. if it has, hasBeenHit becomes false & the host can be hit again.
        if (dmgTimer >= dmgCoolDown)
        {
            hasBeenHit = false;
        }

        // checks for if the Entity has been hit within the dmg cooldown, if it is an object, and the collided object has "playerWeapon" in its tag. Will trigger with any weapon.
        // Objects have NO HP value and will simply break instantly, spawning their gibs and disablign their collider & renderer.

        if (other.tag.Contains ("playerWeapon") && isObject == true && hasBeenHit==false)
        {
            print("Yout hit me!");

            //sets the dmg cooldown timer to 0 & hasBeenHit to true. 
            dmgTimer = 0.0f;
            hasBeenHit = true;

            //sets the host object's renderer & hitbox to inactive && sets the gibs object to active.
            HpCheck();
        }





        // checks for if the Entity has been hit within the dmg cooldown, if it is an enemy, and the collided object has "playerWeapon" in its tag. Will trigger with any weapon.
        else if(other.tag.Contains ("playerWeapon") && isEnemy == true && hasBeenHit == false)
        {
            print("Yout hit me!");

            //sets the dmg cooldown timer to 0 & hasBeenHit to true.
            dmgTimer = 0.0f;
            hasBeenHit = true;
            audioSource.PlayOneShot(hitNoise);



            //checks for dmg source. subtracts dmg from hp based on the tag of the weapon that collided
            if (other.tag == "playerWeaponDagger")
            {
                HP -= dmgTakenFromDagger;
            }
            if (other.tag == "playerWeaponSword")
            {
                HP -= dmgTakenFromSword;
            }

            // Checks to see if the host's HP is 0. if so, turn off the collider & renderer, then sets the gibs object to active.
            HpCheck();
        }


    }

    //functions to track the corisponding dmg incoming from different ranged attacks. 
    public void HitByShotgun(float powerbonus)
    {
            HP -= dmgTakenFromShotgunPellet + powerbonus;
            HpCheck();
        audioSource.PlayOneShot(hitNoise);
    }
    public void HitByRevolver(float powerbonus)
    {
            HP -= dmgTakenFromPistol + powerbonus;
            HpCheck();
        audioSource.PlayOneShot(hitNoise);
    }

    // function that runs the HP check to see if health is below or at 0 / the target is an object with no HP
    public void HpCheck()
    {
        if (HP <= 0.0f || isObject) // hides body, show ragdoll/gibs, play death noise, despawn in 10 seconds.
        {
            hostBody.SetActive(false);
            hostCollider.enabled = false;
            gibs.SetActive(true);
            audioSource.PlayOneShot(deathNoise);
            Destroy(gameObject, 10);
        }
    }
}
