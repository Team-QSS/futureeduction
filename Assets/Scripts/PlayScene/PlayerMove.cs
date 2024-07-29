using System;
using Unity.Mathematics;
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
            _horizontal = 0;
            _rigid = gameObject.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.D))
                {
                    _horizontal = 0;
                    return;
                }
                _horizontal = -1;
            }
            else if(Input.GetKey(KeyCode.D))
            {
                _horizontal = 1;
            }
            else
            {
                _horizontal = 0;
            }
        }

        private void FixedUpdate()
        {
            _rigid.velocity = new Vector2(_horizontal * speed, _rigid.velocity.y);
            var position = transform.position;
            position = new Vector3(math.clamp(position.x, -8f, 8f), position.y);
            transform.position = position;
        }
    }
}
