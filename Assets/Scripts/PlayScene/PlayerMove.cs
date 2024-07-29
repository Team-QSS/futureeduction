using System;
using UnityEngine;

namespace PlayScene
{
    public class PlayerMove : MonoBehaviour
    {
        private int _horizontal;
        private Rigidbody2D _rigid;
        [SerializeField] private float speed;

        private void Start() => _rigid = GetComponent<Rigidbody2D>();

        private void Update() => _horizontal = Input.GetKey(KeyCode.A) ? Input.GetKey(KeyCode.D) ? 0 : -1 : Input.GetKey(KeyCode.D) ? 1 : 0;

        private void FixedUpdate()
        {
            _rigid.velocity = new Vector2(_horizontal * speed, _rigid.velocity.y);
            var position = transform.position;
            transform.position = new Vector3(Math.Clamp(position.x, -8f, 8f), position.y);
        }
    }
}
