using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioRecorder : MonoBehaviour
{
    private AudioClip recordedClip;
    [SerializeField] AudioSource audioSource;
    private string directoryPath;
    private float startTime;
    private float recordingLength;
    public bool clipRecorded = false;
    private WishStateController wishState;

    private void Awake()
    {
        wishState = GetComponent<WishStateController>();

        directoryPath = Path.Combine(Application.dataPath, "Recordings");

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    public void hasRecorded(bool state)
    {
        clipRecorded = state;
    }

    public void StartRecording()
    {
        if (clipRecorded) return;
        string device = Microphone.devices[0];
        int sampleRate = 44100;
        int lengthSec = 3599;

        recordedClip = Microphone.Start(device, false, lengthSec, sampleRate);
        startTime = Time.realtimeSinceStartup;
    }

    public void StopRecording()
    {
        if (clipRecorded) return;

        // Safely end microphone (use first device if available)
        if (Microphone.devices.Length > 0)
            Microphone.End(Microphone.devices[0]);
        else
            Microphone.End(null);

        recordingLength = Time.realtimeSinceStartup - startTime;
        recordedClip = TrimClip(recordedClip, recordingLength);
        SaveRecording();

        // Assign it to audio playback
        audioSource.clip = recordedClip;

        // Notify wish system that it is now full
        wishState.SetWishAudio(recordedClip);

        // Lock recording
        clipRecorded = true;

        Debug.Log("Wish successfully recorded and assigned.");

        // If you don't need the trimmed clip after saving, destroy it too:
        //UnityEngine.Object.Destroy(recordedClip);
        //recordedClip = null;
    }

    public void SaveRecording()
    {
        if (recordedClip != null)
        {
            int fileCount = Directory.GetFiles(directoryPath, "*.wav").Length;
            string filePath = Path.Combine(directoryPath, "recording_" + fileCount + ".wav");
            WavUtility.Save(filePath, recordedClip);
            Debug.Log("Recording saved as " + filePath);
        }
        else
        {
            Debug.LogError("No recording found to save.");
        }
    }

    private AudioClip TrimClip(AudioClip clip, float length)
    {
        if (clip == null) return null;

        int samples = Mathf.Clamp((int)(clip.frequency * length), 0, clip.samples);
        float[] data = new float[samples];
        clip.GetData(data, 0);

        AudioClip trimmedClip = AudioClip.Create(clip.name + "_trimmed", samples,
            clip.channels, clip.frequency, false);
        trimmedClip.SetData(data, 0);

        // Free the original clip's native memory — Microphone.Start() creates it
        UnityEngine.Object.Destroy(clip);

        return trimmedClip;
    }

    public void UseRecording()
    {
        if (recordedClip == null)
        {
            Debug.LogWarning("No recording available to use.");

            return;
        }

        audioSource.clip = recordedClip;
        wishState.SetWishAudio(recordedClip);

        Debug.Log("Recording set to AudioSource.");
    }

}