using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterController2D characterController;

    public float movementSpeed = 40f;
    private float movementDirection;    // Direction in which player should move. [-1, 1]; -1 -> left; 1 -> right.

    [Range(0, 1)] [SerializeField] private float slimeSlowDownFactor = 0.5f;    // How much the player slows down when hitting 

    private float speedMultiplier = 1.0f;

    private bool jump = false;          // Whether the player should jump
    private bool crouch = false;        // Whether the player should crouch

    // Update is called once per frame
    void Update()
    {
        // Read input
        movementDirection = Input.GetAxis("Horizontal") * movementSpeed * speedMultiplier;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    private void FixedUpdate()
    {
        // Move the player
        characterController.Move(movementDirection * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag)
        {
            case "Slime":
                speedMultiplier *= slimeSlowDownFactor;
                characterController.m_JumpMultiplier = slimeSlowDownFactor;
                break;

            default:
                Debug.LogWarning("Triggered collision with object with unknown tag: \"" + collision.tag + "\".");
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        speedMultiplier = 1.0f;
        characterController.m_JumpMultiplier = 1.0f;
    }
}
