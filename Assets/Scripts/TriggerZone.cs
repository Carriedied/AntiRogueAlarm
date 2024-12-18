using System;
using System.Collections;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public event Action OnBurglarEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Thief"))
        {
            OnBurglarEnter?.Invoke();
        }
    }
}
