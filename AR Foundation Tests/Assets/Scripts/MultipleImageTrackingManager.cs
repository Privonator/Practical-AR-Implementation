using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultipleImageTrackingManager : MonoBehaviour
{
    [SerializeField] GameObject[] prefabsToSpawn;

    private ARTrackedImageManager _arTrackedImageManager;
    private Dictionary<string, GameObject> _arObjects;

    //get Reference to the ARTrackedImageManager
    private void Awake()
    {
       _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        _arObjects = new Dictionary<string, GameObject>();

    }
    //ListenToEventsRelatedtoChanges in TrackedImageManager
    private void Start()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;

        //spawn the game objects per image and hide them
        foreach (GameObject prefab in prefabsToSpawn)
        {
            GameObject newARObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            
            newARObject.name = prefab.name;
            newARObject.gameObject.SetActive(false);
            _arObjects.Add(newARObject.name, newARObject);

        }
    }
    private void OnDestroy()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        //identify the changes with the tracked image
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            //Show, Hide, or position the gameObject on the tracked image
            UpdateTrackedImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            //Show, Hide, or position the gameObject on the tracked image
            UpdateTrackedImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            //Show, Hide, or position the gameObject on the tracked image
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
        }
    }
    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        if(trackedImage.trackingState is TrackingState.Limited or TrackingState.None)
        {
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
            return;
        }
        //Show, Hide, or position the gameObject on the tracked image
        if(prefabsToSpawn != null)
        {
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(true);
            _arObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
        }
    }
}
