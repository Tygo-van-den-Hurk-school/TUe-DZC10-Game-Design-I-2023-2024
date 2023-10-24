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
    private bool slide = false;         // Whether the player is invoke sliding
    public bool gameOver = false;

    public CutsceneScript cutsceneManager;

    public bool characterStunned = false;

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
        
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            slide = true;
        } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            slide = false;
        }
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
            
            case "Monster":
                Debug.Log("Player lost the game!");
                gameOver = true;
                break;
            case "Rock":
                // This doesn't work yet
                Debug.Log("Collided with rock");
                StunHandler();
                break;
            case "Log":
                if (!crouch) {
                    Debug.LogWarning("Triggered collision with log but not crouching!");
                } else {
                    Debug.LogWarning("Triggered collision with log and is crouching");
                }
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

    IEnumerator StunHandler()
    {
        characterStunned = true;
        
        yield return new WaitForSeconds(1.5f);

        characterStunned = false;
    }
}
