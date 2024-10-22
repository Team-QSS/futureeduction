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
    public List<GameObject> Animations = null;
    private ObjectSelectManager _objSelectManager;


    private void Awake()
    {
        Animations = new List<GameObject>();
        var o = AnimHolder.Instance.animations[0];
        var i = o.transform.childCount;
        for (var j = 0; j < i; j++)
        {
            Objects.Add(o.transform.GetChild(j).gameObject);
        }
    }

    private void Start()
    {
        foreach (var o in AnimHolder.Instance.animations)
        {
            o.SetActive(true);
        }
        _objSelectManager = GetComponent<ObjectSelectManager>();
        _mainCam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            SaveAction();
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

    public void Submit()
    {
        SaveAction();
        AnimHolder.Instance.InGameSizing();//크기 인게임 크기로 변경
        foreach (var o in AnimHolder.Instance.animations)
        {
            var list = new HashSet<Vector2>();
            var children = o.GetComponentsInChildren<EdgeCollider2D>();
            float radius = 0;
            foreach (var c in children)
            {
                if (c.gameObject != o)
                {
                    var line = c.GetComponent<LineRenderer>();
                    line.startWidth = 0.1f;
                    line.endWidth = 0.1f;
                    radius = Math.Max(line.startWidth / 2, radius);
                    foreach (var point in c.points) if (list.All(p => (p - point).magnitude > radius * 20)) list.Add(point);
                }
            }
            Destroy(o.GetComponent<EdgeCollider2D>());
            var col = o.AddComponent<EdgeCollider2D>();
            col.points = list.ToArray();
            col.edgeRadius = radius;
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<CircleCollider2D>());
        }
        SceneManager.LoadScene("Play");
    }

    public void SaveAction()
    {
        SavedObject = AnimHolder.Instance.animations[_objSelectManager._editingTemp];
        foreach (var o in Objects) o.transform.parent = SavedObject.transform;
        Objects.Clear();
    }
}
