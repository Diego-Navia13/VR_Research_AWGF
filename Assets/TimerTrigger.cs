using UnityEngine;
using UnityEngine.Events;

public class TimerTrigger : MonoBehaviour
{
    [Header("Timer Settings")]
    public float targetTime = 60.0f;
    public bool startOnAwake = true;
    private bool isRunning = false;

    [Header("Events")]
    public UnityEvent onTimerEnd;

    void Start()
    {
        if (startOnAwake)
        {
            StartTimer();
        }
    }

    void Update()
    {
        if (!isRunning) return;

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            isRunning = false;
            TimerEnded();
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    void TimerEnded()
    {
        Debug.Log("Timer finished!");

        if (onTimerEnd != null)
        {
            onTimerEnd.Invoke();
        }
    }
}