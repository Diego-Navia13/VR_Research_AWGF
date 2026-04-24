using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Accessibility;

public class AudioRecorder : MonoBehaviour
{
    private AudioClip recordedClip;
    [SerializeField] AudioSource audioSource;
    private string directoryPath;
    private float startTime;
    private float recordingLength;
    public bool clipRecorded = false;
    private bool canRecord = false;
    public WishStateController wishState;
    private const int MAX_RECORDING_SECONDS = 10;
    public AudioSource comeCloserSFX;

    private void Awake()
    {
        //wishState = GetComponent<WishStateController>();

        directoryPath = Path.Combine(Application.dataPath, "Recordings");

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    private void hasRecorded(bool state)
    {
        clipRecorded = state;
    }

    private void playComeCloser()
    {
        if (comeCloserSFX != null && !clipRecorded)
        {
            comeCloserSFX.Play();
        }
    }

    public void setCanRecord(bool state)
    {
        Debug.Log("State of canRecord prior to change: " + this.canRecord.ToString());
        this.canRecord = state;
        Debug.Log("State changed to " + this.canRecord.ToString());
        Debug.Log($"[Trigger] {gameObject.name} | ID: {GetInstanceID()}");
    }

    public void Update()
    {
        //Debug.Log("State is currently " + canRecord.ToString());
    }

    public void StartRecording()
    {
        if (clipRecorded || !canRecord)
        {
            if (!canRecord) { playComeCloser(); }
            Debug.Log("I can't record yet");
            Debug.Log("State of canRecord: " + canRecord.ToString());
            Debug.Log("State of clipRecorded: " + clipRecorded.ToString());
            return;
        }
        string device = Microphone.devices[0];
        int sampleRate = 44100;
        int lengthSec = MAX_RECORDING_SECONDS;

        recordedClip = Microphone.Start(device, false, lengthSec, sampleRate);
        startTime = Time.realtimeSinceStartup;
    }

    public void StopRecording()
    {
        if (clipRecorded || !canRecord) return;

        string device = Microphone.devices[0];

        int position = Microphone.GetPosition(device);

        if (position <= 0)
        {
            Debug.LogWarning("No audio captured!");
            Microphone.End(device);
            return;
        }

        Microphone.End(device);

        float[] data = new float[position * recordedClip.channels];
        recordedClip.GetData(data, 0);

        AudioClip newClip = AudioClip.Create("RecordedClip", position,
            recordedClip.channels, recordedClip.frequency, false);

        newClip.SetData(data, 0);

        recordedClip = newClip;

        SaveRecording();

        audioSource.clip = recordedClip;
        wishState.SetWishAudio(recordedClip);

        clipRecorded = true;

        Debug.Log("Wish successfully recorded and assigned.");
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