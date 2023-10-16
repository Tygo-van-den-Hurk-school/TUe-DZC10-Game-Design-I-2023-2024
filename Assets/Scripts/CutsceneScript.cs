using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneScript : MonoBehaviour
{
    public Transform player;
    public Transform cameraTarget;
    public float cameraSpeed = 1.5f;
    public float cameraHoldDuration = 3.0f;

    public bool cutsceneActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartCutscene()
    {
        cutsceneActive = true;
        Debug.Log("cutscene started");
        StartCoroutine(PlayCutscene());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !cutsceneActive)
        {
            cutsceneActive = true;
            StartCoroutine(PlayCutscene());
            Debug.Log("Collision detected");
        }
    }

    IEnumerator PlayCutscene()
    {
        float originalSpeed = cameraSpeed;

        cutsceneActive = true;
        yield return new WaitForSeconds(cameraHoldDuration);

        while (Vector3.Distance(Camera.main.transform.position, cameraTarget.position) > 0.1f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraTarget.position, Time.deltaTime * cameraSpeed);
            yield return null;
        }

        yield return new WaitForSeconds(cameraHoldDuration);

        cameraSpeed = 0.5f;
        Vector3 initialCameraPosition = Camera.main.transform.position;

        while (Vector3.Distance(Camera.main.transform.position, cameraTarget.position) > 0.1f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraTarget.position, Time.deltaTime * cameraSpeed);
            yield return null;
        }

        Debug.Log("cutscene ended");
        cutsceneActive = false;

        cameraSpeed = originalSpeed;
    }
}
