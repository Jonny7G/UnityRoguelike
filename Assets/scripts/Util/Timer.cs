using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Timer
{
    public float Duration { get => _duration; private set => _duration = value; }
    [SerializeField] private float _duration;
    public float CurrentDuration { get; private set; }
    public bool Active { get; set; }
    public bool ShouldRestart { get; set; }
    public System.Action OnTimerEnded;
    public bool TimerEnded { get => CurrentDuration >= Duration; }

    public Timer(float duration, System.Action onTimerEnded, bool ShouldRestart)
    {
        this.Duration = duration;
        this.OnTimerEnded = onTimerEnded;
        this.ShouldRestart = ShouldRestart;
    }
    public Timer(float duration)
    {
        this.Duration = duration;
        OnTimerEnded = null;
        ShouldRestart = false;
    }
    public void SetDuration(float newDuration)
    {
        Duration = newDuration;
    }
    /// <summary>
    /// add delta time
    /// </summary>
    public void UpdateTimer()
    {
        UpdateTimer(Time.deltaTime);
    }
    /// <summary>
    /// add specific amount
    /// </summary>
    /// <param name="amount"></param>
    public void UpdateTimer(float amount)
    {
        CurrentDuration += amount;
        if (CurrentDuration >= Duration)
        {
            if (ShouldRestart)
            {
                CurrentDuration = 0f;
            }
            OnTimerEnded?.Invoke();
        }
    }
    public bool CheckUpdateTimer()
    {
        if (TimerEnded)
        {
            return true;
        }
        else
        {
            UpdateTimer();
            return false;
        }
    }
    public void RestartTimer()
    {
        CurrentDuration = 0f;
    }
}