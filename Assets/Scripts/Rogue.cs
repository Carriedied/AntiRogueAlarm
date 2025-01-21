using System.Collections;
using UnityEngine;

public enum RogueState
{
    Outside,
    Inside,
    Alarmed,
    RanAway
}

[RequireComponent(typeof(Animator))]
public class Rogue : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 5f;

    private Animator _animator;
    private RogueState _currentState = RogueState.Outside;

    private int _currentWaypoint = 0;
    private int _numberWaypointInitialPoint = 0;
    private float _amountTimeLookAround = 2f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (_currentState)
        {
            case RogueState.Outside:
                RunToHouse();
                break;
            case RogueState.Inside:
                break;
            case RogueState.Alarmed:
                RunAwayHouse();
                break;
            case RogueState.RanAway:
                _animator.SetBool("isMoveToWaypoint", false);
                break;
        }

        transform.LookAt(_waypoints[_currentWaypoint]);
    }

    public void SubscribeToAlarm(TriggerAlarm alarm)
    {
        alarm.OnAlarmTriggered += HearAlarm;
    }

    public void UnsubscribeFromAlarm(TriggerAlarm alarm)
    {
        alarm.OnAlarmTriggered -= HearAlarm;
    }

    private void HearAlarm()
    {
        StartCoroutine(LookAround(_amountTimeLookAround));
    }

    private IEnumerator LookAround(float timeLookAround)
    {
        yield return new WaitForSeconds(timeLookAround);

        _currentState = RogueState.Alarmed;
    }

    private void RunAwayHouse()
    {
        if (transform.position == _waypoints[_currentWaypoint].transform.position)
        {
            _currentWaypoint--;

            if (_currentWaypoint == _numberWaypointInitialPoint)
            {
                _currentState = RogueState.RanAway;
            }
        }

        Run();
    }

    private void RunToHouse()
    {
        if (transform.position == _waypoints[_currentWaypoint].transform.position)
        {
            _currentWaypoint++;
        }

        Run();

        if (transform.position == _waypoints[_waypoints.Length - 1].transform.position)
        {
            _currentState = RogueState.Inside;
        }
    }

    private void Run()
    {
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
    }
}