using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    PlayerManager playerManager;
    PlayerLocomotion playerLocomotion;

    int horizontal;
    int vertical;
    

    // grabs animator component and required scripts from game object on start
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }


    public void PlayTargetAnimation( string targetAnimation, bool isInteracting, bool useRootMotion = false) // function created to trigger & crossfade between animations. Used to trigger all animations across all scripts.
    {
        animator.SetBool("isInteracting", isInteracting); // sets the player to interacting when triggering animations to prevent overlapping
        animator.SetBool("isUsingRootMotion", useRootMotion);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting) // updates animator values to transfer betwwen animations based on how much movement they player is doing. Framework for possible controller analog stick support.
    {
        //Movement Snapping
        float snappedHorizontal;
        float snappedVertical;

        // snaps horizontal movement data from controls to the nearest trigger so the value will always trigger an animation even if not exactly the correct value
        #region Snapped Horizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }

        else if (horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1;
        }

        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }

        else if (horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1;
        }

        else
        {
            snappedHorizontal = 0;
        }
        #endregion

        // snaps horizontal movement data from controls to the nearest trigger so the value will always trigger an animation even if not exactly the correct value
        #region Snapped Vertical

        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }

        else if (verticalMovement > 0.55f)
        {
            snappedVertical = 1;
        }

        else if (verticalMovement < 0 && verticalMovement > -1f)
        {
            snappedVertical = -1f;
        }

        else
        {
            snappedVertical = 0;
        }
        #endregion
        // if sprinting, snap to sprinting horizontal value 
        if (isSprinting)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 2;
        }

        //sends the animator the data
        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);

    }

    private void OnAnimatorMove() // when the animator runs, and if root motion is being used, sets player body's velocity to match that from animation. 
    {
        if (playerManager.isUsingRootMotion)
        {
            playerLocomotion.playerRigidbody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / Time.deltaTime;
            playerLocomotion.playerRigidbody.velocity = velocity;

        }
    }
}
