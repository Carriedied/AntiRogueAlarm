using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TriggerAlarm : MonoBehaviour
{
    [SerializeField] private TriggerZone _house;
    [SerializeField] private float _fadeDuration;

    private AudioSource _alarmSound;
    private Coroutine _ChangeVolumeCoroutine;
    public event Action OnAlarmTriggered;

    private float _maxVolume = 1f;
    private float _minVolume = 0f;

    private void Awake()
    {
        _alarmSound = GetComponent<AudioSource>();
    }

    public void TurnAlarm()
    {
        _alarmSound.Play();

        if (_ChangeVolumeCoroutine != null)
        {
            StopCoroutine(_ChangeVolumeCoroutine);
        }

        _ChangeVolumeCoroutine = StartCoroutine(ChangeVolume(_minVolume, _maxVolume));

        OnAlarmTriggered?.Invoke();
    }

    public void TurnOffAlarm()
    {
        if (_ChangeVolumeCoroutine != null)
        {
            StopCoroutine(_ChangeVolumeCoroutine);
        }

        StartCoroutine(ChangeVolume(_maxVolume, _minVolume));
    }

    private IEnumerator ChangeVolume(float initialVolume, float finalVolume)
    {
        float currentTime = 0f;

        while (currentTime < _fadeDuration)
        {
            currentTime += Time.deltaTime;

            _alarmSound.volume = Mathf.Lerp(initialVolume, finalVolume, currentTime / _fadeDuration);

            yield return null;
        }
    }
}