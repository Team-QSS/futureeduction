using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeleteLine : MonoBehaviour
{
    private readonly HashSet<GameObject> _triggerObjects = new();
    private void Update()
    {
        if (!Input.GetMouseButton(1) || !UiManager.canEdit) return;
        foreach (var triggerObject in _triggerObjects.Where(triggerObject => DrawLine.Objects.Remove(triggerObject))) Destroy(triggerObject, 0.001f);
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
