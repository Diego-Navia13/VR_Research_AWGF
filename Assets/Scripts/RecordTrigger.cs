using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecordTrigger : MonoBehaviour
{

    public UnityEvent onTriggerEnter;
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
}
