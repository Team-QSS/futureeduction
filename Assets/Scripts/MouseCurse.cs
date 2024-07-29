using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseCurse : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    private Vector2 cursePos;
    void Update()
    {
        cursePos=mainCam.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = cursePos;
    }
    
}
