using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneScript : MonoBehaviour
{
    public Transform player;
    public Transform cameraTarget;
    public float cameraSpeed = 1.5f;
    public float cameraHoldDuration = 3.0f;
    public int cutsceneCounter = 0;

    public bool cutsceneActive = false;
    public bool monsterMove = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartCutscene()
    {
        Debug.Log(cutsceneCounter);
        if (cutsceneCounter > 0)
        {
            return;
        } else if (cutsceneCounter == 0)
        {
            cutsceneActive = true;
            StartCoroutine(PlayCutscene());
        }
    }

    IEnumerator PlayCutscene()
    {
        // storage variables
        float originalSpeed = cameraSpeed;
        cutsceneActive = true;

        // initial wait on the player's position
        yield return new WaitForSeconds(cameraHoldDuration-1.0f);

        // store initial position
        Vector3 initialCameraPosition = Camera.main.transform.position;

        // set target position - z coordinate needs to NOT be copied!
        Vector3 targetPosition = new Vector3(cameraTarget.position.x, cameraTarget.position.y, initialCameraPosition.z);

        // pan to target
        while (Vector3.Distance(Camera.main.transform.position, targetPosition) > 0.1f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, Time.deltaTime * cameraSpeed);
            yield return null;
        }

        // hold camera on target
        yield return new WaitForSeconds(cameraHoldDuration);

        // set camera speed a bit slower for testing purposes
        cameraSpeed = 0.5f;

        // pan camera back to player
        while (Vector3.Distance(Camera.main.transform.position, initialCameraPosition) > 0.1f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, initialCameraPosition, Time.deltaTime * cameraSpeed);
            yield return null;
        }

        // reset variables
        cutsceneActive = false;
        cameraSpeed = originalSpeed;

        // disable the trigger by increasing the counter
        cutsceneCounter += 1;

        // delay monster movement further
        yield return new WaitForSeconds(2.0f);
        monsterMove = true;
    }
}
