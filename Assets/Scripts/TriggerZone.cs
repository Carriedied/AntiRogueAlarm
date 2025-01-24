using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private TriggerAlarm _alarm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rogue thief))
        {
            thief.SubscribeToAlarm(_alarm);
            _alarm.TurnSignaling();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Rogue thief))
        {
            thief.UnsubscribeFromAlarm(_alarm);
            _alarm.TurnOffSignaling();
        }
    }
}
