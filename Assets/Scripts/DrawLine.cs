using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    [SerializeField] public Slider slider;
    private Camera _mainCam;
    private LineRenderer _lr;
    private readonly HashSet<Vector2> _points = new();
    private EdgeCollider2D _col;
    public static readonly List<GameObject> Objects = new();
    public static GameObject SavedObject;
    public static List<GameObject> Animations = new();
    
    
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
            SavedObject.transform.position = new Vector3(0, -10000);
            if (!SavedObject) return;
            SavedObject.transform.position = transform.position;
            var list = new HashSet<Vector2>();
            var children = SavedObject.GetComponentsInChildren<EdgeCollider2D>();
            float radius = 0;
            foreach (var c in children)
            {
                var line = c.GetComponent<LineRenderer>();
                line.startWidth *= 0.1f;
                line.endWidth *= 0.1f;
                radius = Math.Max(line.startWidth / 2, radius);
                foreach (var point in c.points) if (list.All(p => (p - point).magnitude > radius * 20)) list.Add(point);
                Destroy(c);
            }
            var col = SavedObject.AddComponent<EdgeCollider2D>();
            col.points = list.ToArray();
            col.edgeRadius = radius;
            SavedObject.transform.localScale = new Vector3(0.1f, 0.1f);
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<CircleCollider2D>());
            Animations.Add(SavedObject);
            DontDestroyOnLoad(SavedObject);
            SceneManager.LoadScene("Play");
        }
        if (!UiManager.CanEdit) return;
        if (Input.GetMouseButtonDown(0))
        {
            var go = Instantiate(linePrefab);
            _col = go.GetComponent<EdgeCollider2D>();
            (_lr = go.GetComponent<LineRenderer>()).positionCount = 0;
            _lr.startColor = ColorSelector.Script.ColorSelector.GetColor();
            _lr.endColor = ColorSelector.Script.ColorSelector.GetColor();
            _lr.startWidth = slider.value;
            _lr.endWidth = slider.value;
            Objects.Add(go);
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 pos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            if (_lr.IsDestroyed())
            {
                _points.Clear();
                return;
            }
            if (!_points.Contains(pos)) _lr.SetPosition(_lr.positionCount++,pos);
            _points.Add(pos);
            _col.points = _points.ToArray();
        }
        else if (Input.GetMouseButtonUp(0)) _points.Clear();
    }
}
