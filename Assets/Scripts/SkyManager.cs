using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyManager : MonoBehaviour
{
    public float skySpeed;
    // public Material[] skyboxes;
    // public float transitionDuration = 2f;
    // public float timeBetweenTransitions = 10f;
    // private int currentIndex = 0;
    // private bool transitioning = false;

    // void Start()
    // {
    //     StartCoroutine(SkyboxTransitionRoutine());
    // }

    // IEnumerator SkyboxTransitionRoutine()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(timeBetweenTransitions);
    //         currentIndex = (currentIndex + 1) % skyboxes.Length;
    //         StartCoroutine(TransitionToSkybox(skyboxes[currentIndex]));
    //     }
    // }

    // IEnumerator TransitionToSkybox(Material nextSkybox)
    // {
    //     if (transitioning) yield break; // Skip if already transitioning
    //     transitioning = true;

    //     float timer = 0f;
    //     Material currentSkybox = RenderSettings.skybox;

    //     while (timer < transitionDuration)
    //     {
    //         float lerpFactor = timer / transitionDuration;

    //         // Blend between the current and next skyboxes using alpha blending
    //         RenderSettings.skybox.Lerp(currentSkybox, nextSkybox, lerpFactor);
    //         RenderSettings.skybox.SetFloat("_Blend", Mathf.Lerp(0f, 1f, lerpFactor));

    //         timer += Time.deltaTime;
    //         yield return null;
    //     }

    //     RenderSettings.skybox = nextSkybox;
    //     RenderSettings.skybox.SetFloat("_Blend", 0f); // Reset blend for the next transition
    //     transitioning = false;
    // }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skySpeed);
    }
}
