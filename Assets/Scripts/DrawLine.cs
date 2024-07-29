using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;

    private Camera _mainCam;
    private LineRenderer _lr;
    private readonly List<Vector2> _points = new();
    private EdgeCollider2D _col;
    public static readonly List<GameObject> Objects = new();
    public static GameObject SavedObject;
    
    private void Start()
    {
        _mainCam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            SavedObject = new GameObject();
            foreach (var o in Objects) o.transform.parent = SavedObject.transform;
            Objects.Clear();
        }
        if (!UiManager.CanEdit) return;
        if (Input.GetMouseButtonDown(0))
        {
            var go = Instantiate(linePrefab);
            _col = go.GetComponent<EdgeCollider2D>();
            (_lr = go.GetComponent<LineRenderer>()).positionCount = 0;
            _lr.startColor = ColorSelector.Script.ColorSelector.GetColor();
            _lr.endColor = ColorSelector.Script.ColorSelector.GetColor();
            Objects.Add(go);
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 pos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            _points.Add(pos);
            _lr.SetPosition(_lr.positionCount++,pos);
            _col.points = _points.ToArray();
        }
        else if (Input.GetMouseButtonUp(0)) _points.Clear();
    }
}
