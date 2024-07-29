using System;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    private SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sr.color = ColorSelector.GetColor();
        transform.position = mainCam.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
    }
}
