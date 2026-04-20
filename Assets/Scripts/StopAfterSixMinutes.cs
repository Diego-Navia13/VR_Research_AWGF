using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StopAfterSixMinutes : MonoBehaviour
{
    public ParticleSystem ps;
    public float duration = 360f; // 6 minutes = 360 seconds

    void Start()
    {
        StartCoroutine(StopEmission());
    }

    IEnumerator StopEmission()
    {
        yield return new WaitForSeconds(duration);

        // Stop emitting new particles
        var emission = ps.emission;
        emission.enabled = false;

        // Optional: turn off looping
        var main = ps.main;
        main.loop = false;
    }
}