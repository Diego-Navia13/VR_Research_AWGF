using UnityEngine;

public class ToggleMultipleLightsOnEnter : MonoBehaviour
{
    public Light[] lights; // Array of lights to turn on sequentially
    public GameObject[] mocaps;
    public Light directionalLight; // Main directional light in the environment
    private int currentLightIndex = 0; // Start with the first light
    private bool isPlayerInside = false; // Tracks if the player is inside the trigger
    private float switchInterval = 0.5f; // Interval between light switches
    private float timer = 0f; // Timer to control light switching

    void Start()
    {
        // Ensure all lights are off when the scene starts
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
        // Same for mocap captures
        foreach (GameObject obj in mocaps)
        {
            obj.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerInside)
        {
            timer += Time.deltaTime;

            if (timer >= switchInterval)
            {
                // Turn off all lights first
                foreach (Light light in lights)
                {
                    light.enabled = false;
                }

                // Turn on the current light in the cycle
                lights[currentLightIndex].enabled = true;

                // Advance to the next light, looping back to the start
                currentLightIndex = (currentLightIndex + 1) % lights.Length;

                // Reset the timer
                timer = 0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;

            // Turn off the main directional light if assigned
            if (directionalLight != null)
            {
                directionalLight.enabled = false;
            }
            foreach (GameObject obj in mocaps)
            {
                obj.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;

            // Turn off all lights
            foreach (Light light in lights)
            {
                light.enabled = false;
            }

            foreach (GameObject obj in mocaps)
            {
                obj.SetActive(false);
            }

            // Turn the main directional light back on when leaving
            if (directionalLight != null)
            {
                directionalLight.enabled = true;
            }
        }
    }
}
