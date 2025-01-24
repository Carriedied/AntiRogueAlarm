using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TriggerAlarm : MonoBehaviour
{
    [SerializeField] private TriggerZone _house;
    [SerializeField] private float _fadeDuration;

    public event Action SignalingTriggering;
    private AudioSource _signalingSound;
    private Coroutine _changeVolumeCoroutine;

    private float _maxVolume = 1f;
    private float _minVolume = 0f;

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

        _changeVolumeCoroutine = StartCoroutine(ChangeVolume(_minVolume, _maxVolume));

        SignalingTriggering?.Invoke();
    }

    public void TurnOffSignaling()
    {
        if (_changeVolumeCoroutine != null)
        {
            StopCoroutine(_changeVolumeCoroutine);
        }

        StartCoroutine(ChangeVolume(_maxVolume, _minVolume));
    }

    private IEnumerator ChangeVolume(float initialVolume, float finalVolume)
    {
        float currentTime = 0f;

        while (currentTime < _fadeDuration)
        {
            currentTime += Time.deltaTime;

            _signalingSound.volume = Mathf.Lerp(initialVolume, finalVolume, currentTime / _fadeDuration);

            yield return null;
        }
    }
}