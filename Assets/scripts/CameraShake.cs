using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Range(0f, 1f)] public float magnitudeMin, magnitudeMax;
    [Range(0f, 1f)] public float durationMin, durationMax;
    [Range(0f, 1f)] public float frequencyMin, frequencyMax;
    [SerializeField] private CameraMovement camMovement;

    private Vector2 initialPos;
    private Timer shakeTimer, shakeDuration;
    public void Shake()
    {
        shakeDuration = new Timer(Random.Range(durationMin, durationMax));
        shakeDuration.RestartTimer();
        shakeTimer = new Timer(Random.Range(frequencyMin, frequencyMax));
        shakeTimer.RestartTimer();
        camMovement.enabled = false;
        initialPos = transform.position;
        Debug.Log("SHAKE");
    }
    private void Update()
    {
        if (shakeDuration != null && !shakeDuration.CheckUpdateTimer())
        {
            if (shakeTimer.CheckUpdateTimer())
            {
                Vector2 pos = initialPos + Random.insideUnitCircle * Random.Range(magnitudeMin, magnitudeMax);
                transform.position = new Vector3(pos.x, pos.y, -10);
                shakeTimer = new Timer(Random.Range(frequencyMin, frequencyMax));
            }
            if (shakeDuration.TimerEnded)
            {
                camMovement.enabled = true;
            }
        }
    }
}
