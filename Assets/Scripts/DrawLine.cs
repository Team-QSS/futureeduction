using System;
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
        if(!UiManager.canEdit) return;
        if (Input.GetMouseButtonDown(0))
        {
            var go = Instantiate(linePrefab);
            (_lr = go.GetComponent<LineRenderer>()).positionCount = 0;
            (_lr = go.GetComponent<LineRenderer>()).startColor = ColorSelector.GetColor();
            (_lr = go.GetComponent<LineRenderer>()).endColor = ColorSelector.GetColor();
            _col = go.GetComponent<EdgeCollider2D>();
            Objects.Add(go);
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 pos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            _points.Add(pos);
            _lr.SetPosition(_lr.positionCount++,pos);
            _col.points = _points.ToArray();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _points.Clear();
        }

    }
}
