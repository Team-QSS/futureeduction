using System.Collections.Generic;
using UnityEngine;

public class DeleteLine : MonoBehaviour
{
    private readonly HashSet<GameObject> _triggerObjects = new();
    private void Update()
    {
        if (!Input.GetMouseButton(1)) return;
        foreach (var triggerObject in _triggerObjects) Destroy(triggerObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag($"line")) _triggerObjects.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _triggerObjects.Remove(other.gameObject);
    }
}
