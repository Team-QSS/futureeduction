using UnityEngine;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Camera _mainCam;
    private SpriteRenderer _sr;
    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _mainCam = Camera.main;
    }

    private void Update()
    {
        _sr.color = ColorSelector.GetColor();
        transform.position = _mainCam.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
        gameObject.transform.localScale = new Vector3(slider.value, slider.value, 1);

    }
}
