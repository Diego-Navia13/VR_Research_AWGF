using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopParticlesWhenMocapEnds : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem particles;
    public string stateName; // the name of your Animator state

    void Update()
    {
        // Get current state info
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        // Check if we are in the right state AND it's finished playing
        if (info.IsName(stateName) && info.normalizedTime >= 1.0f)
        {
            if (particles.isPlaying)
                particles.Stop();
        }
    }
}
