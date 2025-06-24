using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera; // assign main camera in inspector


    void Start()
    {
        // If no camera is assigned, find the main camera in the scene
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera != null)
        {
            // Make the object look at the camera
            transform.LookAt(mainCamera.transform);
        }

    }
}
