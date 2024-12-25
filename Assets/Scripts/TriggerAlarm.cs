using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TriggerAlarm : MonoBehaviour
{
    [SerializeField] private TriggerZone _house;
    [SerializeField] private float _fadeDuration;

    private AudioSource _alarmSound;
    private Coroutine _fadeVolumeCoroutine;

    private float _maxVolume = 1f;
    private float _minVolume = 0f;

    private void Awake()
    {
        _alarmSound = GetComponent<AudioSource>();
    }

    public void TurnAlarm(Rogue thief)
    {
        _alarmSound.Play();

        _fadeVolumeCoroutine = StartCoroutine(FadeVolume());

        thief.HearAlarm();
    }

    public void TurnOffAlarm()
    {
        if (_fadeVolumeCoroutine != null)
        {
            StopCoroutine(_fadeVolumeCoroutine);
        }

        StartCoroutine(DecreaseVolume());
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

        _alarmSound.Stop();
    }

    private float ChangeVolume(float startVolume, float endVolume, float duration, float currentTime)
    {
        return Mathf.Lerp(startVolume, endVolume, currentTime / duration);
    }
}