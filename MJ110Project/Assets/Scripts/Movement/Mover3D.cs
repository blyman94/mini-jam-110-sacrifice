using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover3D : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private CharacterController characterController;

    [Header("Movement Params")]
    [SerializeField] private float maxSpeed = 5;
    [Tooltip("For instant movement, set acceleration = -1")]
    [SerializeField] private float acceleration = -1;

    private float newSpeed;

    public Vector2 MoveInput { get; set; }

    #region MonoBehaviour Methods
    private void Update()
    {
        CalculateNewMoveSpeed();
        ApplyMotion();
    }
    #endregion

    private void ApplyMotion()
    {
        Vector3 planarMovement =
            ((transform.forward * MoveInput.y) +
            (transform.right * MoveInput.x)).normalized *
            (newSpeed * Time.deltaTime);
        characterController.Move(planarMovement);
    }

    private void CalculateNewMoveSpeed()
    {
        float targetSpeed = maxSpeed;

        if (MoveInput == Vector2.zero)
        {
            targetSpeed = 0.0f;
        }

        if (acceleration != -1 && acceleration > 0)
        {
            float currentPlanarSpeed =
                new Vector3(characterController.velocity.x, 0.0f,
                characterController.velocity.z).magnitude;

            if (currentPlanarSpeed < targetSpeed - 0.1f ||
            currentPlanarSpeed > targetSpeed + 0.1f)
            {
                newSpeed =
                    Mathf.Lerp(currentPlanarSpeed, targetSpeed,
                    acceleration * Time.deltaTime);
            }
            else
            {
                newSpeed = targetSpeed;
            }
        }
        else
        {
            newSpeed = targetSpeed;
        }
    }
}
