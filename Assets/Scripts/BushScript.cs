using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Temp to stop compilation errors
public float movementSpeed = 100.0f;

public class BushScript : MonoBehaviour
{
    public float BushSpeed = 0.8f;
    public int CurrBranches = 0;
    public int timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1;
        if(timer >= 600)
        {
            CurrBranches = 0;
            timer = 0;
        }
        if(CurrBranches > 5)
        {
            CurrBranches = 5;
        }
        
    }

    // Collision trigger
    void OnTriggerEnter2D(Collider2D col)
    {
        movementSpeed = movementSpeed * BushSpeed;
        CurrBranches += 1;
    }
}
