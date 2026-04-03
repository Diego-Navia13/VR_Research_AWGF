using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WishStateController : MonoBehaviour
{
    [Header("Wish State")]
    public bool hasWish = false;

    [Header("Audio")]
    public AudioSource wishRecording;

    [Header("Visuals")]
    public Renderer wishRenderer;
    public Color emptyColor = Color.gray;

    private Color filledColor;
    public float forceAmount = 10f;
    private Rigidbody rb;

    void Start()
    {
        // If the wish already has audio assigned at start
        if (wishRecording != null && wishRecording.clip != null)
        {
            hasWish = true;
            AssignRandomFilledColor();
        }
        else
        {
            SetEmptyVisual();
        }
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * forceAmount, ForceMode.Impulse);
    }

    // Called when user finishes recording
    public void SetWishAudio(AudioClip newClip)
    {
        wishRecording.clip = newClip;
        hasWish = true;

        AssignRandomFilledColor();
    }

    void AssignRandomFilledColor()
    {
        filledColor = Random.ColorHSV(
            0.6f, 1f,
            0.6f, 1f,
            0.6f, 1f
        );

        wishRenderer.material.color = filledColor;
    }

    void SetEmptyVisual()
    {
        wishRenderer.material.color = emptyColor;
    }

    // Controlled playback
    public void PlayWish()
    {
        if (!hasWish) return;

        if (!wishRecording.isPlaying)
            wishRecording.Play();
    }

    public void StopWish()
    {
        if (wishRecording.isPlaying)
            wishRecording.Stop();
    }
}