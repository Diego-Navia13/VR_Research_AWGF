using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSceneController : MonoBehaviour
{
    float timer = 0f;
    ParticleSystem[] systems;

    void Start()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("TimedParticles");
        systems = new ParticleSystem[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            systems[i] = objects[i].GetComponent<ParticleSystem>();
            systems[i].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 10f && timer < 480f)
        {
            foreach (var ps in systems)
            {
                if (!ps.isPlaying)
                    ps.Play();
            }
        }
        else
        {
            foreach (var ps in systems)
            {
                if (ps.isPlaying)
                    ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }
}