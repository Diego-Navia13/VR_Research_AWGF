using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopNamedParticlesWithAnimator : MonoBehaviour
{
    public Animator animator;
    public string stateName = "YourStateName";
    public string particleName = "PS1-CeciStarborn";

    private ParticleSystem[] namedParticles;

    void Start()
    {
        // Get all ParticleSystems in children with the given name
        ParticleSystem[] allParticles = GetComponentsInChildren<ParticleSystem>(true);
        namedParticles = System.Array.FindAll(allParticles, ps => ps.name == particleName);

        if (namedParticles.Length == 0)
            Debug.LogWarning("No particle systems found with name: " + particleName);
    }

    void Update()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        // Stop all named particles when animation ends
        if (info.IsName(stateName) && info.normalizedTime >= 1.0f)
        {
            foreach (var ps in namedParticles)
            {
                if (ps.isPlaying)
                    ps.Stop();
            }
        }
    }
}
