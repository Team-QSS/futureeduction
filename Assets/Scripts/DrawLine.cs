using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;

    private Camera _mainCam;
    private LineRenderer _lr;

    private void Start()
    {
        _mainCam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            (_lr = Instantiate(linePrefab).GetComponent<LineRenderer>()).positionCount = 0;
        
        if (Input.GetMouseButton(0))
            _lr.SetPosition(_lr.positionCount++,_mainCam.ScreenToWorldPoint(Input.mousePosition) - Vector3.back * 10);
    }
}
