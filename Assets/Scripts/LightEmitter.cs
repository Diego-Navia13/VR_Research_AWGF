using UnityEngine;

public class LightEmitter : MonoBehaviour
{
    public Light pointLight;

    public float maxIntensity = 3f;
    public float movementSensitivity = 4f;

    public float movementThreshold = 0.02f;

    public float speedSmoothing = 4f;
    public float lightSmoothing = 3f;

    public float zeroSnapThreshold = 0.01f;

    Vector3 lastPosition;
    float smoothedSpeed;
    float currentIntensity;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;

        // Deadzone
        if (speed < movementThreshold)
            speed = 0f;

        // If no movement, force speed to zero instead of smoothing forever
        if (speed == 0f)
        {
            smoothedSpeed = 0f;
        }
        else
        {
            smoothedSpeed = Mathf.Lerp(smoothedSpeed, speed, Time.deltaTime * speedSmoothing);
        }

        float targetIntensity = Mathf.Clamp(smoothedSpeed * movementSensitivity, 0, maxIntensity);

        currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime * lightSmoothing);

        // Snap completely off
        if (targetIntensity == 0f && currentIntensity < zeroSnapThreshold)
            currentIntensity = 0f;

        pointLight.intensity = currentIntensity;
    }
}