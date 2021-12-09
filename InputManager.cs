using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    //bools for Inputs. Used to Interface with Unity's input action system.
    public bool shift_Input; //sprint
    public bool ctrl_Input; // walk
    public bool jump_Input; // jump
    public bool x_Input; // draw/sheathe weapon
    public bool e_Input; // melee attack
    public bool f_Input; // power activate
    public bool q_Input; // ranged attack

    // runs on awake, grabs animation and player locomotion components from gameobject
    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    // runs on enable
    private void OnEnable()
    {
        if (playerControls == null) // sets player controlls to equal the values from the input system int eh editor.
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Shift.performed += i => shift_Input = true;
            playerControls.PlayerActions.Shift.canceled += i => shift_Input = false;

            playerControls.PlayerActions.Ctrl.performed += i => ctrl_Input = true;
            playerControls.PlayerActions.Ctrl.canceled += i => ctrl_Input = false;

            playerControls.PlayerActions.Jump.performed += i => jump_Input = true;
            
            playerControls.PlayerActions.X.performed += i => x_Input = true;

            playerControls.PlayerActions.E.performed += i => e_Input = true;

            playerControls.PlayerActions.F.performed += i => f_Input = true;

            playerControls.PlayerActions.Q.performed += i => q_Input = true;

        }

        playerControls.Enable();

    }
    // turns off controlls when disabled
    private void OnDisable()
    {
        playerControls.Disable();
    }

    //function to run all input functions at once
    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleCrouchInput();
        HandleJumpInput();
        HandleDodgeInput();
        HandleAttackInput();
    }

    // Handles the movement WADS inout
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);

    }

    //handles the input for shift key sprinting
    private void HandleSprintingInput()
    {

        if (shift_Input && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }

        else
        {
            playerLocomotion.isSprinting = false;
        }

    }
    
    //handles input for crouching / walking
    private void HandleCrouchInput()
    {
        if (ctrl_Input)
        {
            playerLocomotion.isCrouching = true;
            
        }
        
        else
        {
            playerLocomotion.isCrouching = false;
            
        }
    }

    //handles input for jumping
    private void HandleJumpInput()
    {
        if (jump_Input)
        {
            jump_Input = false;
            
            playerLocomotion.HandleJumping();
        }
    }

    //resued input function formerly used for dodging, NOW USED FOR WEAPON SWAPPING.
    private void HandleDodgeInput()
    {
        if (x_Input)
        {
            x_Input = false;
            playerLocomotion.HandleDodge();

        }
    }

    //handles inputs for triggering attacks, melee, ranged, and ability activation
    private void HandleAttackInput()
    {
        if (e_Input)
        {
            e_Input = false;
            playerLocomotion.HandleMeleeAttack();
        }

        if (f_Input)
        {
            f_Input = false;
            playerLocomotion.HandlePowerActivation();
        }

        if (q_Input)
        {
            q_Input = false;
            playerLocomotion.HandleRangedAttack();
        }
    }
}
