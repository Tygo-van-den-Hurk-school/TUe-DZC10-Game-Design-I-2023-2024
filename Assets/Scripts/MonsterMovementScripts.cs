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
    Vector3 playerSpeed;
    Vector3 lastPos;
    public float truePlayerSpeed;

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
        //Debug.Log(timer);
        if (timer > 600)
        {
            DecideAI();
            timer = 0;
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
        // Temporarily disabled (as in, this range guarantees AI 0)
        currAI = Random.Range(0,1);
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
            if (currAI == 0)
            {
                // Aggresive chasing AI
                var direction = (player.position - transform.position).normalized;
                var distance = Vector3.Distance(player.transform.position, Monster.transform.position);
                var BandingFactor = Mathf.Sqrt(distance);
                Monster.MovePosition(transform.position + direction * Time.deltaTime * speed * BandingFactor);
                // Debug.Log(speed * BandingFactor);
            } else if (currAI == 1)
            {
                // Aggresion based on player speed - does not work
                var direction = (player.position - transform.position).normalized;
                if (lastPos != player.position)
                {
                    playerSpeed = player.position - lastPos;
                    playerSpeed /= Time.deltaTime;
                    lastPos = player.position;
                    var truePlayerSpeed = playerSpeed.normalized;
                }
                var BandingFactor = Mathf.Sqrt(truePlayerSpeed);
                Monster.MovePosition(transform.position + direction * Time.deltaTime * speed * BandingFactor);
            }

        }
        

    }

    // Collision with player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Debug.Log("player lost the game!");
        }   
        
    }
}
