using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// To see the ObjectPrefab struct in the Inspector.
[System.Serializable]

// Struct to hold the name and prefab of an object.
public struct ObjectPrefabs
{
    public string name;
    public GameObject prefab;
}

public class TrackImage : MonoBehaviour
{
    // List of object prefabs to be tracked.
    public List<ObjectPrefabs> objectPrefabs = new List<ObjectPrefabs>();

    // Dictionary to keep track of instantiated prefabs.
    private Dictionary<string, GameObject> instantiatedPrefabs;

    // Reference to the ARTrackedImageManager component in Unity.
    private ARTrackedImageManager trackedImageManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the ARTrackedImageManager component.
        trackedImageManager = GetComponent<ARTrackedImageManager>();

        // Listens for changes in tracked images.
        trackedImageManager.trackablesChanged.AddListener(OnTrackedImageChanged);

        // Initialize the dictionary.
        instantiatedPrefabs = new Dictionary<string, GameObject>();
    }

    // Method called when tracked images change.
    private void OnTrackedImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        // Handle added tracked images.
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            foreach (ObjectPrefabs obj in objectPrefabs)
            {
                if ((obj.name == trackedImage.referenceImage.name) && (!instantiatedPrefabs.ContainsKey(obj.name)))
                {
                    instantiatedPrefabs[obj.name] = Instantiate(obj.prefab, trackedImage.transform);
                }
            }
        }

        // Handle updated tracked images.
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                instantiatedPrefabs[trackedImage.referenceImage.name].SetActive(true);
                Debug.Log("This image was found: " + trackedImage.referenceImage.name);
            }
            else
            {
                instantiatedPrefabs[trackedImage.referenceImage.name].SetActive(false);
                Debug.Log("Can't find this image: " + trackedImage.referenceImage.name);
            }
        }

        // Handle removed tracked images.
        foreach (KeyValuePair<TrackableId, ARTrackedImage> pair in eventArgs.removed)
        {
            ARTrackedImage trackedImage = pair.Value;

            Destroy(instantiatedPrefabs[trackedImage.referenceImage.name]);
            instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
            Debug.Log("Removed image: " + trackedImage.referenceImage.name);
        }
    }
}
