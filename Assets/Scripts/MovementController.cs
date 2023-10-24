using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterController2D characterController;

    public float movementSpeed = 40f;
    private float movementDirection;    // Direction in which player should move. [-1, 1]; -1 -> left; 1 -> right.

    [Range(0, 1)] [SerializeField] private float slimeSlowDownFactor = 0.5f;    // How much the player slows down when hitting 
    [Range(0, 1)] [SerializeField] private float bushSlowDownFactor = 0.9f;    // How much the player slows down when hitting the bush


    private float speedMultiplier = 1.0f;
    private bool jump = false;          // Whether the player should jump
    private bool crouch = false;        // Whether the player should crouch
    private bool slide = false;         // Whether the player is invoke sliding
    private int bushCollisionCount = 0; // Number of times the player hit a bush
    private bool stunned = false;       // Whether the character is stunned
    private float startingStunnedTime;      // Time stamp when character is first stunned
    public CutsceneScript cutsceneManager;

    public bool characterStunned = false;

    // Update is called once per frame
    void Update()
    {   

        // Read input
        // Disable input if character is stunned
        if (stunned) {
            if (Time.time - startingStunnedTime >= 2.0f) {
                stunned = false;
            }
        } else {
            // Speed multiplier must be reset everytime as speed debuff may get removed upon stunned. 
            speedMultiplier = 1.0f;
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
            
            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                slide = true;
            } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
                slide = false;
            }
        }

        // Bush detection
        // Permanent debuff (-10% speed if player hits a bush, -10% more if hit a bush twice, get stunned but reset speed if hit 3 times)
        switch (bushCollisionCount) {
            case 1:
                speedMultiplier *= 0.9f;
                characterController.m_JumpMultiplier = 0.9f;
                break;
            case 2:
                speedMultiplier *= 0.8f;
                characterController.m_JumpMultiplier = 0.8f;
                break;
            case 3:
                OnStunned();
                bushCollisionCount = 0;  
                break;
            default:
                Debug.LogWarning("No bush collision!");      
                break;
        }

        Debug.Log(speedMultiplier);
        
    }

    private void FixedUpdate()
    {
        // Move the player
        if (cutsceneManager.cutsceneActive == false && characterStunned == false)
        {
            characterController.Move(movementDirection * Time.fixedDeltaTime, crouch, jump, slide);
            jump = false;
        } else if (cutsceneManager.cutsceneActive == true || characterStunned == true)
        {
            characterController.Move(0, crouch, jump, slide);
            jump = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag)
        {
            case "Slime":
                speedMultiplier *= slimeSlowDownFactor;
                characterController.m_JumpMultiplier = slimeSlowDownFactor;
                break;
            case "CutSceneTrigger":
                cutsceneManager.StartCutscene();
                break;
            case "Bush":
                Debug.Log("Player hit the bush!");
                bushCollisionCount += 1;
                break;
            case "Monster":
                Debug.Log("Player lost the game!");
                break;
            case "Rock":
                Debug.Log("Collided with rock");
                OnStunned();
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

    // Characteristics to be set when player is stunned
    private void OnStunned() {
        stunned = true;
        movementDirection = 0;
        startingStunnedTime = Time.time; 
    }
}
