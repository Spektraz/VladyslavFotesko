using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MainGame
{
    public class JoystickMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float maxVelocity = 3f; 
        [SerializeField] private DynamicJoystick joystick;
        [SerializeField] private Rigidbody rb;

        private void FixedUpdate()
        {
            Vector3 inputDirection = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

            if (inputDirection.magnitude > 0.1f)
            {
                Vector3 force = inputDirection.normalized * moveSpeed * Time.fixedDeltaTime;
                rb.AddForce(-force, ForceMode.VelocityChange);
                Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                if (horizontalVelocity.magnitude > maxVelocity)
                {
                    Vector3 limitedVelocity = horizontalVelocity.normalized * maxVelocity;
                    rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
                }
            }
        }
    }
}

