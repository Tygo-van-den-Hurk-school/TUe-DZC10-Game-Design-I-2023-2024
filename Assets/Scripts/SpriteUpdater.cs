using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteUpdater : MonoBehaviour {
    public Sprite monsterState1;
    public Sprite monsterState2;
    public int currentMonsterState = 0;
    private float spriteUpdateDelay = 0.0f;
    private bool isDelaySet = false;

    // Start is called before the first frame update
    void Start() {
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = new Vector3(7.0f, 7.0f, 1.0f);
        if (!isDelaySet) {
            spriteUpdateDelay = Time.time; 
            isDelaySet = true;
        } 
        
        if (Time.time - spriteUpdateDelay >= 0.75f) {
            // Toggle between 2 monster animations to simulate flying behavior
            if (currentMonsterState == 0) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = monsterState1;
                currentMonsterState = 1;
            } else {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = monsterState2;
                currentMonsterState = 0;
            }
            isDelaySet = false;
        }

        
    }
}
