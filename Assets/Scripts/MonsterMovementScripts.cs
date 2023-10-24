using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterMovementScripts : MonoBehaviour
{
    Transform player;
    public bool isChasing = false;
    float speed = 25;
    Rigidbody2D Monster;

    int timer = 0;
    public int currAI = 0;
    float timeBetweenAIChanges = 3.0f;
    float timeSinceLastAIChange = 0.0f;
    float smoothingSpeed = 0.5f;

    Vector3 playerSpeed;
    Vector3 lastPos;
    Vector3 targetOffset = Vector3.zero;

    public CutsceneScript cutscenescript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        Monster = GetComponent<Rigidbody2D>();
        isChasing = false;          // can be used later to turn chasing off for whatever reason
        lastPos = player.position;
    }

    // Update is called once per frame
    void Update()
    {   
        timer += 1;
        timeSinceLastAIChange += Time.deltaTime;
        if (timeSinceLastAIChange >= timeBetweenAIChanges)
        {
            DecideAI();
            timeSinceLastAIChange = 0.0f;
        }
        if (cutscenescript.monsterMove == true)
        {
            isChasing = true;
        }
        if (isChasing == true)
        {
            Chase();
        }
    }

    // Decide which AI to use
    void DecideAI()
    {
        currAI = Random.Range(0,5);
        Debug.Log(currAI);
    }

    // Handles chase movement
    void Chase()
    {
        if (isChasing == false) 
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
            var playerPosition = player.transform.position;
            var desiredPosition = new Vector3(playerPosition.x + targetOffset.x, playerPosition.y + targetOffset.y, playerPosition.z);
            // chasing movement
            var direction = (desiredPosition - transform.position).normalized;
            var distance = Vector3.Distance(desiredPosition, Monster.transform.position);
            var BandingFactor = Mathf.Log(2, Mathf.Sqrt(distance));
            Monster.MovePosition(transform.position + direction * Time.deltaTime * speed * BandingFactor);
            // Debug.Log(speed * BandingFactor)

        }
        

    }
}
