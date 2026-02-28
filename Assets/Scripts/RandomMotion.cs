using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RandomMotion : MonoBehaviour
{
    public float forceStrength = 5f;   // How strong the motion is
    public float noiseScale = 1f;      // Speed/variation of movement

    private Rigidbody rb;
    private Vector3 randomOffset;      // Each object gets its own noise offset

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        // Assign a random offset so each object is unique
        randomOffset = new Vector3(
            Random.Range(0f, 100f),
            Random.Range(0f, 100f),
            Random.Range(0f, 100f)
        );
    }

    void FixedUpdate()
    {
        float t = Time.time * noiseScale;

        // Sample Perlin Noise using unique offsets for each object
        float x = Mathf.PerlinNoise(t + randomOffset.x, 0f) - 0.5f;
        float y = Mathf.PerlinNoise(0f, t + randomOffset.y) - 0.5f;
        float z = Mathf.PerlinNoise(t + randomOffset.z, t + randomOffset.z) - 0.5f;

        Vector3 force = new Vector3(x, y, z);
        rb.AddForce(force * forceStrength);
    }
}