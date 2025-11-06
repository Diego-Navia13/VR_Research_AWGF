using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioRecorder : MonoBehaviour
{
    private AudioClip recordedClip;
    [SerializeField] AudioSource audioSource;
    private string directoryPath = "Recordings";
    private float startTime;
    private float recordingLength;
    private bool clipRecorded = false;

    private void Awake()
    {
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
        Microphone.End(null);
        recordingLength = Time.realtimeSinceStartup - startTime;
        recordedClip = TrimClip(recordedClip, recordingLength);
        SaveRecording();
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
        int samples = (int)(clip.frequency * length);
        float[] data = new float[samples];
        clip.GetData(data, 0);

        AudioClip trimmedClip = AudioClip.Create(clip.name, samples,
            clip.channels, clip.frequency, false);
        trimmedClip.SetData(data, 0);

        return trimmedClip;
    }
}