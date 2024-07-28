using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    public GameObject linePrefab;
    private LineRenderer lr;
    private List<Vector2> points = new List<Vector2>();
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(linePrefab);
            lr = go.GetComponent<LineRenderer>();
            points.Add(mainCam.ScreenToWorldPoint(Input.mousePosition));
            lr.positionCount = 1;
            lr.SetPosition(0, points[0]);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 pos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            
            points.Add(pos);
            lr.positionCount++;
            lr.SetPosition(lr.positionCount-1,pos);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            points.Clear();
        }
    }
}
