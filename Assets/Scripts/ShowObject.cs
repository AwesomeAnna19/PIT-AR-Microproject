using UnityEngine;

public class ShowObject : MonoBehaviour
{
    // Reference to the jumpscare GameObject to be shown.
    public GameObject jumpscareObject;

    // Method to show the jumpscare object.
    public void ShowJumpscareObject()
    {
        // Activate the GameObject when this method is called.
        jumpscareObject.SetActive(!jumpscareObject.activeSelf);
    }
}
