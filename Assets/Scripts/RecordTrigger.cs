using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecordTrigger : MonoBehaviour
{

    public UnityEvent onTriggerEnter, onTriggerExit;
    public string tagNameCheck;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagNameCheck))
        {
            if (onTriggerEnter != null)
            {
                onTriggerEnter.Invoke();
                AudioRecorder audio = other.GetComponent<AudioRecorder>();
                audio.setCanRecord(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagNameCheck))
        {
            if (onTriggerExit != null)
            {
                onTriggerEnter.Invoke();
                AudioRecorder audio = other.GetComponent<AudioRecorder>();
                audio.setCanRecord(false);
            }
        }
    }
}
