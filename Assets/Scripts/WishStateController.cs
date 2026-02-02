using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WishStateController : MonoBehaviour
{
    [Header("Wish State")]
    public bool hasWish = false;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Visuals")]
    public Renderer wishRenderer;
    public Color emptyColor = Color.gray;

    private Color filledColor;

    void Start()
    {
        // If the wish already has audio assigned at start
        if (audioSource != null && audioSource.clip != null)
        {
            hasWish = true;
            AssignRandomFilledColor();
        }
        else
        {
            SetEmptyVisual();
        }
    }

    // Called when user finishes recording
    public void SetWishAudio(AudioClip newClip)
    {
        audioSource.clip = newClip;
        hasWish = true;

        AssignRandomFilledColor();
    }

    void AssignRandomFilledColor()
    {
        filledColor = Random.ColorHSV(
            0f, 1f,
            0.6f, 1f,
            0.6f, 1f
        );

        wishRenderer.material.color = filledColor;
    }

    void SetEmptyVisual()
    {
        wishRenderer.material.color = emptyColor;
    }

    // Controlled playback (not hover)
    public void PlayWish()
    {
        if (!hasWish) return;

        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void StopWish()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}