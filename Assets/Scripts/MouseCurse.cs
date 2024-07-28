using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCurse : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    private Vector2 cursePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cursePos=mainCam.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = cursePos;
    }
}
