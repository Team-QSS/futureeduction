using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] private Camera mainCam;

    private void Update()
    {
        transform.position = mainCam.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
    }
}
