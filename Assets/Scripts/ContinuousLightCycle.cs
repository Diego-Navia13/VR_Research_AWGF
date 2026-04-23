using UnityEngine;

public class ContinuousLightCycle : MonoBehaviour
{
    public Light[] lights;
    public float switchInterval = 0.5f;

    private int currentLightIndex = 0;
    private float timer = 0f;

    void Start()
    {
        if (lights == null || lights.Length == 0)
        {
            Debug.LogWarning("No lights assigned!");
            return;
        }

        // Turn ON only the first light
        for (int i = 0; i < lights.Length; i++)
        {
            if (lights[i] != null)
                lights[i].enabled = (i == 0);
        }
    }

    void Update()
    {
        Debug.Log("Switching to light: " + currentLightIndex);
        if (lights == null || lights.Length == 0) return;

        timer += Time.deltaTime;

        if (timer >= switchInterval)
        {
            if (lights[currentLightIndex] != null)
                lights[currentLightIndex].enabled = false;

            currentLightIndex = (currentLightIndex + 1) % lights.Length;

            if (lights[currentLightIndex] != null)
                lights[currentLightIndex].enabled = true;

            timer = 0f;
        }
    }
}