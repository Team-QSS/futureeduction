using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayScene
{
    public class PlayerMove : MonoBehaviour
    {
        private int _horizontal;
        private Rigidbody2D _rigid;
        [SerializeField] private float speed;

        private void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            if (!DrawLine.SavedObject) return;
            var o = Instantiate(DrawLine.SavedObject, transform);
            o.transform.position = transform.position;
            var list = new HashSet<Vector2>();
            var children = o.GetComponentsInChildren<EdgeCollider2D>();
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
            var col = o.AddComponent<EdgeCollider2D>();
            col.points = list.ToArray();
            col.edgeRadius = radius;
            o.transform.localScale = new Vector3(0.1f, 0.1f);
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<CircleCollider2D>());
        }

        private void Update()
        {
            _horizontal = Input.GetKey(KeyCode.A)
                ? Input.GetKey(KeyCode.D) ? 0 : -1
                : Input.GetKey(KeyCode.D) ? 1 : 0;
            
        }

        private void FixedUpdate()
        {
            _rigid.velocity = new Vector2(_horizontal * speed, _rigid.velocity.y);
            var position = transform.position;
            transform.position = new Vector3(Math.Clamp(position.x, -8f, 8f), position.y);
        }
    }
}
