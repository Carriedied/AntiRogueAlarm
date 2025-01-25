using Assets.Scripts;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Rogue : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 5f;

    private Animator _animator;
    private RogueState _currentState = RogueState.Outside;
    private WaitForSeconds _waitTime = new WaitForSeconds(2f);

    private int _numberLastElementWaypoints;
    private int _currentWaypoint = 0;
    private int _numberWaypointInitialPoint = 0;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _numberLastElementWaypoints = _waypoints.Length - 1;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case RogueState.Outside:
                RunToHouse();
                break;

            case RogueState.Alarmed:
                RunAwayHouse();
                break;

            case RogueState.RanAway:
                _animator.SetBool(PlayerAnimator.Params.IsMoveToWaypoint, false);
                break;
        }

        transform.LookAt(_waypoints[_currentWaypoint]);
    }

    public void SubscribeToAlarm(TriggerAlarm alarm)
    {
        alarm.SignalingTriggered += HearAlarm;
    }

    public void UnsubscribeFromAlarm(TriggerAlarm alarm)
    {
        alarm.SignalingTriggered -= HearAlarm;
    }

    private void HearAlarm()
    {
        StartCoroutine(LookAround());
    }

    private IEnumerator LookAround()
    {
        yield return _waitTime;

        _currentState = RogueState.Alarmed;
    }

    private void RunAwayHouse()
    {
        if (transform.position == _waypoints[_currentWaypoint].transform.position)
        {
            _currentWaypoint++;
            _currentWaypoint %= _waypoints.Length;
        }

        Run();

        if (transform.position == _waypoints[_numberWaypointInitialPoint].position)
        {
            _currentState = RogueState.RanAway;
        }
    }

    private void RunToHouse()
    {
        if (transform.position == _waypoints[_currentWaypoint].position)
        {
            _currentWaypoint++;
        }

        Run();

        if (transform.position == _waypoints[_numberLastElementWaypoints].position)
        {
            _currentState = RogueState.Inside;
        }
    }

    private void Run()
    {
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
    }
}