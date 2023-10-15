using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterMovementScripts : MonoBehaviour
{
    Transform player;
    bool isChasing;
    float speed = 25;
    Rigidbody2D Monster;
    int timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        Monster = GetComponent<Rigidbody2D>();
        isChasing = true;          // can be used later to turn chasing off for whatever reason
    }

    // Update is called once per frame
    void Update()
    {   
        timer += 1;
        Debug.Log(timer);
        if (timer > 600)
        {
            Chase();
        }
    }


    // Handles chase movement
    void Chase()
    {
        if (!isChasing)
            return;
        
        var direction = (player.position - transform.position).normalized;
        var distance = Vector3.Distance(player.transform.position, Monster.transform.position);
        var BandingFactor = Mathf.Sqrt(distance);
        Monster.MovePosition(transform.position + direction * Time.deltaTime * speed * BandingFactor);
        // Debug.Log(speed * BandingFactor);
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
