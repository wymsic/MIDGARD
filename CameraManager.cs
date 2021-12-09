using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    InputManager inputManager;


    public Transform targetTransform; // object camera follows
    public Transform cameraPivot; // Object the camera uses to pivot
    public Transform cameraTransform; //transform for the acutall camera object in the game

    public LayerMask collisionLayers;

    private float defaultPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;


    // how far the camera offsets off of collided objects
    public float cameraCollisionOffset = 0.2f; 
    public float minimumCollisionOffset = 0.2f;
    public float cameraCollisionRadius = 0.2f;
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2;
    public float cameraPivotSpeed = 2;

    public float lookAngle;
    public float pivotAngle;

    public float minimumPivotAngle = -35;
    public float maximumPivotAngle = 35;

    //grabs nessicary components from gam eobject
    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    //runs all camera related movement functions
    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }


    // makes the camera follow the player transform slowy
    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }

    //rotates the camera based on mouse input from the input system
    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotaion;

        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);


        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotaion = Quaternion.Euler(rotation);
        transform.rotation = targetRotaion;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotaion = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotaion;


    }

    //function to move camera a way from objects to prevetn clipping
    private void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition =- (distance - cameraCollisionOffset);

        }

        if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition = targetPosition - minimumCollisionOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
        
    }
}
