using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Rogue : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _stopDistance = 0f;
    [SerializeField] private float _speed = 5f;

    public bool IsAlarmPlaying { get; private set; }

    public Animator animator;

    private int _currentWaypoint = 0;
    private int _numberWaypointEntranceHouse = 2;
    private int _numberWaypointInitialPoint = 0;
    private bool _isInsideHouse = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        IsAlarmPlaying = false;
    }

    private void Update()
    {
        transform.LookAt(_waypoints[_currentWaypoint]);

        if (_isInsideHouse == false && IsAlarmPlaying == false)
        {
            RunToHouse();
        }

        if (IsAlarmPlaying == true && (TryDistanceSuitable(_waypoints[_numberWaypointInitialPoint])) == false)
        {
            RunAwayHouse();
        }
    }

    public void HearAlarm()
    {
        IsAlarmPlaying = true;
    }

    private void RunAwayHouse()
    {
        if (TryDistanceSuitable(_waypoints[_currentWaypoint]))
        {
            _currentWaypoint--;
        }

        Run();

        if (TryDistanceSuitable(_waypoints[_numberWaypointEntranceHouse]))
        {
            _isInsideHouse = false;
        }

        if (TryDistanceSuitable(_waypoints[_numberWaypointInitialPoint]))
        {
            animator.SetBool("isMoveToWaypoint", false);
        }
    }

    private void RunToHouse()
    {
        if (TryDistanceSuitable(_waypoints[_currentWaypoint]))
        {
            _currentWaypoint++;
        }

        Run();

        if (TryDistanceSuitable(_waypoints[_waypoints.Length - 1]))
        {
            _isInsideHouse = true;
        }
    }

    private void Run()
    {
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
    }

    private bool TryDistanceSuitable(Transform target)
    {
        return Vector3.Distance(transform.position, target.position) <= _stopDistance;
    }
}
