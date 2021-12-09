using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    CameraManager cameraManager;
    PlayerLocomotion playerLocomotion;

    public bool isInteracting;
    public bool isUsingRootMotion;

    //grabs nessicary componets from game object
    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();

    }

    // runs on physics cycle clock to prevetn framerate based erros
    private void FixedUpdate()
    {
        //runs input tracking & movement script
        inputManager.HandleAllInputs();
        playerLocomotion.HandleAllMovement();
    }

    // runs after all updates are finished, tells camera to follow & sends nessicary data too and from the animator to other scripts
    private void LateUpdate() 
    {
        cameraManager.HandleAllCameraMovement();

        isInteracting = animator.GetBool("isInteracting");
        isUsingRootMotion = animator.GetBool("isUsingRootMotion");

        playerLocomotion.isJumping = animator.GetBool("isJumping");
        //playerLocomotion.isCrouching = animator.GetBool("isCrouching");
        animator.SetBool("isGrounded", playerLocomotion.isGrounded);
    }

}
