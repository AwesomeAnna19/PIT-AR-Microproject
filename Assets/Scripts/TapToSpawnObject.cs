using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class tapToShowObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Enables Enhanced Touch Support.
    void Start()
    {
        EnhancedTouchSupport.Enable();
    }

    // Update is called once per frame
    // Checks for touch input and toggles the visibility of the jumpscare object.
    void Update()
    {
        // If there are no active touches, exit the method.
        if (Touch.activeTouches.Count == 0)
        {
            return;
        }

        // Get the first active touch.
        Touch touch = Touch.activeTouches[0];

        // If the touch just began, perform a raycast to check for hits and show the jumpscare object.
        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
            // Create a ray from the touch position.
            Ray ray = Camera.main.ScreenPointToRay(touch.screenPosition);

            // Perform the raycast.
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Try to get the ShowObject component from the hit object.
                ShowObject tapToShowObject = hit.transform.GetComponent<ShowObject>();

                // If the component exists, call the ShowJumpscareObject method.
                if (tapToShowObject != null)
                {
                    tapToShowObject.ShowJumpscareObject();
                }
            }
        }
    }
}
