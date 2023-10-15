using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraOffset;
    public float cameraSpeedHorizontal = 0.9f;
    public float cameraSpeedVertical = 0.2f;

    private Vector3 targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        // Position at the start of the game
        targetPosition = player.position + cameraOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Set a new target position
        targetPosition = new Vector3(player.position.x + cameraOffset.x, player.position.y + cameraOffset.y, transform.position.z);
        // Get new speed values
        float speedX = Time.deltaTime * cameraSpeedHorizontal;
        float speedY = Time.deltaTime * cameraSpeedVertical;
        // Lerp to smooth camera
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPosition.x, speedX),
        Mathf.Lerp(transform.position.y, targetPosition.y, speedY),
        transform.position.z);

        //TODO: make offset adjust for player's facing direction?
    }
}
