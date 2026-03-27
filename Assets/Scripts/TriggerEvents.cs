using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Trigger events when a player enters a trigger.
/// </summary>
public class TriggerEvents : MonoBehaviour
{
    public UnityEvent onTrigger;
    public string tagNameCheck;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagNameCheck))
        {
            if (onTrigger != null)
            {
                onTrigger.Invoke();
            }
        }
    }
}