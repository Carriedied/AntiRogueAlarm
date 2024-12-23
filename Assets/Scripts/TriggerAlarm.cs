using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TriggerAlarm : MonoBehaviour
{
    [SerializeField] private TriggerZone _house;
    [SerializeField] private float _fadeDuration = 5f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _minVolume = 0f;

    private AudioSource _alarmSound;

    private bool _isAlarmPlaying = false;

    private void Awake()
    {
        _alarmSound = GetComponent<AudioSource>();

        _alarmSound.volume = _minVolume;
    }

    public void TurnAlarm(Rogue thief)
    {
        if (_isAlarmPlaying == true)
        {
            StartCoroutine(DecreaseVolume());

            _isAlarmPlaying = false;

            return;
        }

        _alarmSound.Play();

        _isAlarmPlaying = true;

        StartCoroutine(FadeVolume());

        thief.HearAlarm();
    }

    private IEnumerator FadeVolume()
    {
        float currentTime = 0f;

        while (currentTime < _fadeDuration)
        {
            currentTime += Time.deltaTime;

            _alarmSound.volume = ChangeVolume(_minVolume, _maxVolume, _fadeDuration, currentTime);

            yield return null;
        }

        _alarmSound.volume = _maxVolume;
    }

    private IEnumerator DecreaseVolume()
    {
        float currentTime = 0f;

        while (currentTime < _fadeDuration)
        {
            currentTime += Time.deltaTime;

            _alarmSound.volume = ChangeVolume(_maxVolume, _minVolume, _fadeDuration, currentTime);

            yield return null;
        }

        _alarmSound.volume = _minVolume;

        _alarmSound.Stop();
    }

    private float ChangeVolume(float startVolume, float endVolume, float duration, float currentTime)
    {
        return Mathf.Lerp(startVolume, endVolume, currentTime / duration);
    }
}