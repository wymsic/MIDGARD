using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;
    private AudioSource audioSource;

    Vector3 moveDirection;
    Transform cameraObject;
    public Rigidbody playerRigidbody;

    private Killable killable;

    public float timer;

    [Header("Weapons")]
    public GameObject swordModel;
    public GameObject daggerModel;
    public GameObject pistolModel;
    public GameObject shotgunModel;

    public GameObject swordHitbox;
    public AudioClip swingNoise;
    public AudioClip drawNoise;
    public AudioClip sheathNoise;

    public AudioClip shotgunFire;
    public ParticleSystem shotgunParticle;

    public AudioClip pistolFire;
    public ParticleSystem pistolParticles;

    public bool hasSword;
    public bool hasDagger;

    public bool hasSG;
    public bool hasR;

    public float sgRange;
    public float rRange;

    private float gunRange;

    public float modelShowTime;
    public bool hasDrawn;

    public float abilityDMGIncrease;
    private float extraDMG;
    public float abilityLength;
    public GameObject powerVFX;
    public AudioClip abilitySound;

    [Header("Falling")]

    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffSet = 0.0f;
    public LayerMask groundLayer;
    public ParticleSystem landindVFX;

    [Header("Movement Flags")]

    public bool isSprinting;
    public bool isGrounded;
    public bool isCrouching;
    public bool isJumping;

    [Header("Movement Speeds")]
    public float walkingSpeed = 3;
    public float runningSpeed = 5;
    public float sprintingSpeed = 10;
    public float rotationSpeed = 15;

    [Header("Jumping")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;



    private void Awake()
    {

        timer = abilityLength; // pre sets the ability timer to prevent the player having to wait before casting after Respawning. 

        // Grabs components from player objec
        audioSource = GetComponent<AudioSource>();
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;

        playerRigidbody.Sleep(); // Sleeps the player ridgid body for the first frame of the gam eloading to prevent physics Jank

        // Sets the range of the players ranged attack to be equal to the range of what ever gunt hey have equipped
        if (hasSG)
        {
            gunRange = sgRange;
        }
        if (hasR)
        {
            gunRange = rRange;
        }
        //


    }
    private void FixedUpdate() // runs evry cycle of the physics engine to prevent framerate's from controlling objects 
    {
        timer = timer + Time.deltaTime; // time for tracking ability uptime

        if (timer >= abilityLength) // If the timer has been active for longer than the cooldown of the ability , remove dmg bonus & turn off VFX
        {
            powerVFX.SetActive(false);
            extraDMG = 0.0f;
        }
    }

    public void HandleAllMovement() // function to trigger ALL movement functions.
    {

        HandelFallingAndLanding(); // triggers the function for falling and landing functionality

        if (playerManager.isInteracting) // prevents the below functions from running if the player in interacting
            return;
        HandleMovement(); // handles player movement
        HandleRotation(); // handles player rotation
    }


    // Player Movement
    private void HandleMovement() 
    {
        if (isJumping) // if player is moving, return and dont move them
            return;

        moveDirection = cameraObject.forward * inputManager.verticalInput; // sets the movement direction to be where the camera is pointing
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput; // sets movement to use the horizontal input from player
        moveDirection.Normalize(); // normalizes the noisey data from above sets so the player move smoothley
        moveDirection.y = 0; // prevents noise from chaning the players y value.
        
        if (isSprinting) // if sprinting bool is triggered, add sprint speed to movement
        {
            moveDirection = moveDirection * sprintingSpeed;
        }

        if (isCrouching) // if crouch/walk key is pressed, add walkign speed to movement.
        {
            moveDirection = moveDirection * walkingSpeed;
            

        }

        else // if no extra input is given, set movement to the basic running speed.
        {
            if (inputManager.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * runningSpeed;
            }
            

        }

        if (isGrounded && !isJumping) // if the player is on the ground & not jumping, translate input from player to character movement. IE no in air control or during jump anim.
        {
            Vector3 movementVelocity = moveDirection;
            playerRigidbody.velocity = movementVelocity;
        }
        

    }
    
    // function for handling player rotation
    private void HandleRotation() 
    {

        if (isJumping) // if they are jumping, return to prevent character from rotating in the air
            return;


        Vector3 targetDirection = Vector3.zero; // create a V3 for aiming the player body rotation.

        targetDirection = cameraObject.forward * inputManager.verticalInput; // rotates player to face camera direction
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput; // adds in horizontal input
        targetDirection.Normalize(); // normalizes to prevent unwanted noise in data
        targetDirection.y = 0; // halts any unwanted y rotation

        // math to make sure the target direction (the way we want the player to rotate) to the player rotation.
        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
        //
    }
    
    // function for falling anim, landing anim, and ground checking
    private void HandelFallingAndLanding() 
    {
        //ray cast used for ground checking + setting its position
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPosition;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;
        targetPosition = transform.position;


        if (!isGrounded && !isJumping) // triggers if off of the ground and not jumping
        {
            if (!playerManager.isInteracting) // if not interacting (aka in other anim) play falling anim
            {
                animatorManager.PlayTargetAnimation("Falling", true); 
            }

            animatorManager.animator.SetBool("isUsingRootMotion", false); // disallow root motion from host anim

            inAirTimer = inAirTimer + Time.deltaTime; // tracks how long the player is in the air
            playerRigidbody.AddForce(transform.forward * leapingVelocity); // ads slight forward velocity to the player character so they launch forward reliably.
            playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer); // makes the player gain velocity int he air, makign them fall faster the longer they are in the air.
        }

        if (Physics.SphereCast(rayCastOrigin, 1f, Vector3.down, out hit, 0.1f, groundLayer)) // sphere cast for ground check, runs when touching ground layermask
        {
            if (!isGrounded && playerManager.isInteracting) // if not grounded & interacting, play landing anim + VFX & SFX
            {
                animatorManager.PlayTargetAnimation("Land", true);
                landindVFX.Play();
                print("Ground Check Ran, timer reset");
                
            }
            // Tell the game the player is landed & reset the in air timer.
            isGrounded = true; 
            inAirTimer = 0;
            
        }
        else // catch all so that if the player is in a weird state, force ground check to run again
        {
            isGrounded = false;
        }
    }
    
    // function for running jump functionality
    public void HandleJumping() 
    {
        if (isGrounded) // runs only if the player is grounded
        {
            
            // tells the animator that the player is jumping, play anim, & move the player transform out of the ground to prevent ground check from running mid jump.
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jump", false);
            transform.position = transform.position + new Vector3( 0, 0.1f,0);
            

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight); // sets jumpign velocity to be the sqr root of -2 * the gravity strength and jumpheight of the player.
            Vector3 playerVelocity = moveDirection; // sets player velocity
            playerVelocity.y = jumpingVelocity; // ads jumping force to the characters y velocity
            playerRigidbody.velocity = playerVelocity; // sets the ridgid body velocity to the player velocity
        }
    }


    public void HandleDodge() // THIS WAS RECYCLED TO HANDLE WEAPON DRAW & SHEATHING. NAME UNCHANGED FOR COMPATIBILITY.
    {
        if (playerManager.isInteracting) // returns if the player in interracting
            return;
        if (!hasSword && !hasDagger) // returns if the player does not have a sword or dagger
            return;
        if (!hasDrawn) // if the function triggers & the player has not draw their weapons, play drawing anim + sound & show weapon modle
        {
            animatorManager.PlayTargetAnimation("Dodge", true, true);
            Invoke("showMelee", modelShowTime);
            hasDrawn = true;
            print("drawn");
            audioSource.PlayOneShot(drawNoise);
        }

        else if (hasDrawn) // if the function is triggered and the HAVE drawn their weapon, playing sheathign anim + sfx & hide weapon modle.
        {
            animatorManager.PlayTargetAnimation("Sheath", true, true);
            Invoke("hideMelee", modelShowTime);
            hasDrawn = false;
            print("sheath");
            audioSource.PlayOneShot(sheathNoise);
        }    
    }
    
    // function for running melee attacks
    public void HandleMeleeAttack() 
    {
        if (playerManager.isInteracting) // if interacting OR if you have not drawn a weapon return
            return;
        if (!hasDrawn)
            return;

        if (hasDrawn) // if weapon is drawn, play attack anim, play swing noise, turn on hitbox for duration of attack anim.
        {
            animatorManager.PlayTargetAnimation("Attack", true, true);
            Invoke("showHitbox", 0.5f);
            audioSource.PlayOneShot(swingNoise);
            print("attack");
            Invoke("hideHitbox", 0.6f);
        }        
    }
    
    // function for running ranged attacks
    public void HandleRangedAttack() 
    {

        //stops the player from doing attack during action
        if (playerManager.isInteracting)
            return;
        //stops the player from attacking without a gun 
        if (!hasSG && !hasR)
            return;

        Invoke("showGun", 0.2f);

        //plays fire anim
        animatorManager.PlayTargetAnimation("Shoot Gun", true, true);

        //runs rangedAttack after 1.5f to sync with anim.
        Invoke("rangedAttack", 1.5f);
        Invoke("hideGun", 3.5f);
        

    }

    // function to run ability activation
    public void HandlePowerActivation() 
    {
        if (playerManager.isInteracting) // if interacting or ability cooldown not met, return
            return;
        if (timer <= abilityLength)
            return;

        // play SFX & VFX, play anim, reset timer value, and add extra dmg to the ability dmg counter 
        audioSource.PlayOneShot(abilitySound);
        animatorManager.PlayTargetAnimation("Ability Activate", true, true);
        powerVFX.SetActive(true);
        timer = 0f;
        extraDMG = abilityDMGIncrease;

    }

    // runs the code for the raycast attacks, based on what the player has equipped.
    void rangedAttack() 
    {

        if (hasR) // runs if they have the revolver pistol
        {
            //plays vfx & sfx
            audioSource.PlayOneShot(pistolFire);
            pistolParticles.Play();

            //fires a raycast forward from the player with the range of the weapon
            RaycastHit hit;
            Vector3 gunShot = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, gunShot, out hit, gunRange)) 
            {
                if (hit.transform.transform.CompareTag("destructable")) // trigger when the hit object has "destructable" as a tag.
                {
                    killable = hit.transform.GetComponent<Killable>(); // finds and runs code fromt h killable script to trigger damage, passing it the damage of the revoler + any extra dmg from ability
                    killable.HitByRevolver(extraDMG);       
                }
            }

            //Raycast Debug Draw
            Debug.DrawRay(transform.position, gunShot, Color.green, 5f);
        }
        if (hasSG) // runs if they has the shotgun
        {
            //plays VFX & SFX
            audioSource.PlayOneShot(shotgunFire);
            shotgunParticle.Play();


            //Fires multiple rays in 5 direction in a cone infront of the player. Each pellet ray checks for the destructable tag and search->runs the killable script's damager trigger, passing itthe shogun's pellet dmg + extra ability dmg.
            RaycastHit hit;
            Vector3 gunShot = transform.TransformDirection(Vector3.forward);
            //pellet 1
            if (Physics.Raycast(transform.position, gunShot, out hit, gunRange))
            {
                if (hit.transform.transform.CompareTag("destructable"))
                {
                    killable = hit.transform.GetComponent<Killable>();
                    killable.HitByShotgun(extraDMG);
                }
            }
            //pellet 2
            Vector3 gunShot2 = transform.TransformDirection(0.5f,0,1);
            if (Physics.Raycast(transform.position, gunShot2, out hit, gunRange))
            {
                if (hit.transform.transform.CompareTag("destructable"))
                {
                    killable = hit.transform.GetComponent<Killable>();
                    killable.HitByShotgun(extraDMG);
                }
            }
            //pellet 3
            Vector3 gunShot3 = transform.TransformDirection(-0.5f,0,1);
            if (Physics.Raycast(transform.position, gunShot3, out hit, gunRange))
            {
                if (hit.transform.transform.CompareTag("destructable"))
                {
                    killable = hit.transform.GetComponent<Killable>();
                    killable.HitByShotgun(extraDMG);
                }
            }
            //pellet 4
            Vector3 gunShot4 = transform.TransformDirection(0,0.5f,1);
            if (Physics.Raycast(transform.position, gunShot4, out hit, gunRange))
            {
                if (hit.transform.transform.CompareTag("destructable"))
                {
                    killable = hit.transform.GetComponent<Killable>();
                    killable.HitByShotgun(extraDMG);
                }
            }
            //pellet 5
            Vector3 gunShot5 = transform.TransformDirection(0,-0.5f,1);
            if (Physics.Raycast(transform.position, gunShot5, out hit, gunRange))
            {
                if (hit.transform.transform.CompareTag("destructable"))
                {
                    killable = hit.transform.GetComponent<Killable>();
                    killable.HitByShotgun(extraDMG);
                }
            }



            //Raycast Debug draw
            Debug.DrawRay(transform.position, gunShot, Color.green,5f);
            Debug.DrawRay(transform.position, gunShot2, Color.green,5f);
            Debug.DrawRay(transform.position, gunShot3, Color.green,5f);
            Debug.DrawRay(transform.position, gunShot4, Color.green,5f);
            Debug.DrawRay(transform.position, gunShot5, Color.green,5f);
        }

        
    }



    // Shows and hides the melee weapon based on what the player has equipped

    void showMelee()
    {
        if (hasSword && !hasDagger)
        {
            swordModel.SetActive(true);
        }

        if (hasDagger && !hasSword)
        {
            daggerModel.SetActive(true);
        }

    }
    void hideMelee()
    {
        if (hasSword && !hasDagger)
        {
            swordModel.SetActive(false);
        }

        if (hasDagger && !hasSword)
        {
            daggerModel.SetActive(false);
        }
    }
    //



    // Shows and hides the hitbox for the melee weapons.
    void showHitbox()
    {
        swordHitbox.SetActive(true);
    }
    void hideHitbox()
    {
        swordHitbox.SetActive(false);
    }
    //


    // Shows and hides the ranged weapon  based on what the player has equipped
    void showGun()
    {

        if (hasR & !hasSG)
        {
            pistolModel.SetActive(true);
        }
        if (hasSG && !hasR)
        {
            shotgunModel.SetActive(true);
        }
    }

    void hideGun()
    {
        if (hasR & !hasSG)
        {
            pistolModel.SetActive(false);
        }
        if (hasSG && !hasR)
        {
            shotgunModel.SetActive(false);
        }
    }
    //
}
