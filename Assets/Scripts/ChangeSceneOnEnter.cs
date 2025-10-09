using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnEnter : MonoBehaviour
{
    public string sceneName; // Name of the scene to load
    public Transform vrCamera; // Reference to the VR camera (drag and drop from the Hierarchy)

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Code was accessed");

    if (other.CompareTag("Player")) // Assuming the player has a "Player" tag
        {
            Debug.Log("Before Scene Change - Position: " + vrCamera.position + " Rotation: " + vrCamera.rotation.eulerAngles);

            SceneManager.LoadScene(sceneName); // Load the specified scene

            Debug.Log("After Scene Change - Position: " + vrCamera.position + " Rotation: " + vrCamera.rotation.eulerAngles);

            // Reset VR camera position and rotation
            vrCamera.position = Vector3.zero;
            vrCamera.rotation = Quaternion.identity;
        }
    }
}

