using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private TriggerAlarm alarm;

    private void OnTriggerEnter(Collider other)
    {
        OperationAlarm(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OperationAlarm(other);
    }

    private void OperationAlarm(Collider other)
    {
        if (other.TryGetComponent<Rogue>(out Rogue thief))
        {
            alarm.TurnAlarm(thief);
        }
    }
}
