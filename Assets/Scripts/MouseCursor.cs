using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private Camera _mainCam;
    private SpriteRenderer _sr;
    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _mainCam = Camera.main;
    }

    private void Update()
    {
        _sr.color = ColorSelector.Script.ColorSelector.GetColor();
        transform.position = _mainCam.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
    }
}
