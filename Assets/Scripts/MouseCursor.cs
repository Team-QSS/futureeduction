using System;
using UnityEngine;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Slider slider;
    private SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sr.color = ColorSelector.GetColor();
        transform.position = mainCam.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
        gameObject.transform.localScale = new Vector3(slider.value, slider.value, 1);

    }
}
