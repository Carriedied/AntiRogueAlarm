using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class Rogue : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 5f;

    private PlayerAnimator _playerAnimator;

    private float _threshold = 0.1f;
    private int _currentWaypoint = 0;
    private int _numberWaypointInitialPoint = 0;

    private bool _isOutside = true;
    private bool _isAlarmed = false;
    private bool _isRanAway = false;

    private void Awake()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        if (_isOutside == true)
            RunToHouse();

        if (_isAlarmed == true)
            RunAwayHouse();

        if (_isRanAway == true)
            _playerAnimator.Stay();

        if (_currentWaypoint != _waypoints.Length)
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
        _isOutside = false;
        _isAlarmed = true;
    }

    private void RunAwayHouse()
    {
        if ((transform.position - _waypoints[_currentWaypoint].transform.position).sqrMagnitude < _threshold * _threshold)
        {
            _currentWaypoint++;
            _currentWaypoint %= _waypoints.Length;
        }

        Run();

        if ((transform.position - _waypoints[_numberWaypointInitialPoint].transform.position).sqrMagnitude < _threshold * _threshold)
        {
            _isAlarmed = false;
            _isRanAway = true;
        }
    }

    private void RunToHouse()
    {
        if ((transform.position - _waypoints[_currentWaypoint].transform.position).sqrMagnitude < _threshold * _threshold)
        {
            _currentWaypoint++;
        }

        if (_currentWaypoint != _waypoints.Length)
            Run();
    }

    private void Run()
    {
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
    }
}