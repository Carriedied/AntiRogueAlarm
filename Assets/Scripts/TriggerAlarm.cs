using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TriggerAlarm : MonoBehaviour
{
    [SerializeField] private TriggerZone _house;
    [SerializeField] private float _fadeDuration;

    private AudioSource _signalingSound;
    private Coroutine _changeVolumeCoroutine;

    private float _maxVolume = 1f;
    private float _minVolume = 0f;

    public event Action SignalingTriggered;

    private void Awake()
    {
        _signalingSound = GetComponent<AudioSource>();
    }

    public void TurnSignaling()
    {
        _signalingSound.Play();

        if (_changeVolumeCoroutine != null)
        {
            StopCoroutine(_changeVolumeCoroutine);
        }

        _changeVolumeCoroutine = StartCoroutine(ChangeVolume(_maxVolume));

        SignalingTriggered?.Invoke();
    }

    public void TurnOffSignaling()
    {
        if (_changeVolumeCoroutine != null)
        {
            StopCoroutine(_changeVolumeCoroutine);
        }

        _changeVolumeCoroutine = StartCoroutine(ChangeVolume(_minVolume));
    }

    private IEnumerator ChangeVolume(float finalVolume)
    {
        float currentTime = 0f;

        while (currentTime < _fadeDuration)
        {
            currentTime += Time.deltaTime;

            _signalingSound.volume = Mathf.Lerp(_signalingSound.volume, finalVolume, currentTime);
            
            yield return null;
        }
    }
}