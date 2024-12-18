using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TriggerAlarm : MonoBehaviour
{
    [SerializeField] private TriggerZone _house;
    [SerializeField] private Rogue _thief;

    private AudioSource _alarmSound;

    private void Awake()
    {
        _alarmSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _house.OnBurglarEnter += TurnAlarm;
    }

    private void TurnAlarm()
    {
        _house.OnBurglarEnter -= TurnAlarm;

        _alarmSound.Play();

        _thief.HearAlarm();
    }
}