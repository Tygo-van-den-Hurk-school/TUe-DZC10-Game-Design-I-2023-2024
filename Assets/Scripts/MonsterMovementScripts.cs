using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterMovementScripts : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;     // player position at any given time
    [SerializeField] private float stalkingSpeed = 5f;      // the speed at which the AI moves while stalking the player
    [SerializeField] private float chasingSpeed = 20f;      // the speed at which the AI moves while chasing the player
    Rigidbody2D Monster;

    private enum CurrentBehavior
    {
        Idle,
        Stalking,
        Hiding,
        Chasing
    };
    private CurrentBehavior currentBehavior;

    float smoothingSpeed = 0.5f;

    Vector3 playerSpeed;
    Vector3 lastPos;
    Vector3 targetOffset = Vector3.zero;

    [SerializeField] private CutsceneScript cutsceneScript;

    void Start()
    {
        Monster = GetComponent<Rigidbody2D>();
        currentBehavior = CurrentBehavior.Idle;
        lastPos = playerTransform.position;
    }

    void Update()
    {
        DecideAI();
        ActAI();
    }

    // Decide which AI to use
    void DecideAI()
    {
        
    }

    // Act accordingly to the current specified behavior
    void ActAI()
    {
        switch (currentBehavior)
        {
            case CurrentBehavior.Idle:
                Idle();
                break;

            case CurrentBehavior.Stalking:
                break;

            case CurrentBehavior.Chasing:
                Chase();
                break;

            case CurrentBehavior.Hiding:
                Hide();
                break;

            default: break;
        }
    }

    // Enemy idling
    void Idle()
    {

    }

    // Enemy hiding from player after being spotted
    void Hide()
    {

    }

    // Handles chase movement
    void Chase()
    {
        // do not chase the player perfectly, rather overshoot/undershoot their
        // position by a little bit and then rubber band into place
        /*if (isChasing == false) 
        {
            return;
        } else if (isChasing == true)
        {
            Vector3 desiredOffset = Vector3.zero;

            switch (currAI)
            {
                case 0:
                    desiredOffset = new Vector3(0.0f, 0.0f, 0.0f);
                    break;
                case 1:
                    desiredOffset = new Vector3(-0.07f, 0.0f, 0.0f);
                    break;
                case 2:
                    desiredOffset = new Vector3(-0.05f, 0.05f, 0.0f);
                    break;
                case 3:
                    desiredOffset = new Vector3(-0.05f, -0.05f, 0.0f);
                    break;
                case 4:
                    desiredOffset = new Vector3(0.02f, 0.04f, 0.0f);
                    break;
                default:
                    desiredOffset = new Vector3(-0.01f, 0.0f, 0.0f);
                    break;
            }
            // declare some useful variables
            targetOffset = Vector3.Lerp(targetOffset, desiredOffset, smoothingSpeed * Time.deltaTime);
            var playerPosition = playerTransform.position;
            var desiredPosition = new Vector3(playerPosition.x + targetOffset.x, playerPosition.y + targetOffset.y, playerPosition.z);
            // chasing movement
            var direction = (desiredPosition - transform.position).normalized;
            var distance = Vector3.Distance(desiredPosition, Monster.transform.position);
            var BandingFactor = Mathf.Log(2, Mathf.Sqrt(distance));
            Monster.MovePosition(transform.position + direction * Time.deltaTime * speed * BandingFactor);
            // Debug.Log(speed * BandingFactor)

        }*/
        

    }
}
