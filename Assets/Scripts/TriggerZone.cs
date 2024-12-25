using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private TriggerAlarm _alarm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rogue thief))
        {
            _alarm.TurnAlarm(thief);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Rogue thief))
        {
            _alarm.TurnOffAlarm();
        }
    }
}
