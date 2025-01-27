using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private static readonly int isMoveToWaypoint = Animator.StringToHash("isMoveToWaypoint");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Stay()
    {
        _animator.SetBool(isMoveToWaypoint, false);
    }
}
