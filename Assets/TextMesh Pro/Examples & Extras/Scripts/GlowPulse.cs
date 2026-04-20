using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlowPulse : MonoBehaviour
{
    public float speed = 2f;      // How fast it pulses
    public float minGlow = 0.5f;  // Minimum glow
    public float maxGlow = 2f;    // Maximum glow

    private Material mat;
    private int glowID;

    void Start()
    {
        mat = GetComponent<TextMeshPro>().fontMaterial;
        glowID = Shader.PropertyToID("_GlowPower");
    }

    void Update()
    {
        float glow = Mathf.Lerp(minGlow, maxGlow,
            (Mathf.Sin(Time.time * speed) + 1f) / 2f);
        mat.SetFloat(glowID, glow);
    }
}