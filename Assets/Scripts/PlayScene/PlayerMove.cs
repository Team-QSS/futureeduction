using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayScene
{
    public class PlayerMove : MonoBehaviour
    {
        private int _horizontal;
        private float _moveElapsedTime;
        private Rigidbody2D _rigid;
        private Queue<GameObject> _moveObjectQueue;
        private GameObject _defaultObj, _movingObjectTemp;
        [SerializeField] private float speed;
        [SerializeField] private float frameRate;

        private void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _moveObjectQueue = new Queue<GameObject>();
            foreach (var o in DrawLine.Animations)
            {
                o.transform.parent = gameObject.transform;
                o.transform.position = transform.position;
                _moveObjectQueue.Enqueue(o);
                o.SetActive(false);
            }
            _defaultObj = gameObject.transform.GetChild(0).gameObject;
            _defaultObj.SetActive(true);
            StartCoroutine(MovingTick());
            _movingObjectTemp = _defaultObj;
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
            _defaultObj.transform.rotation = Quaternion.Euler(0, _horizontal switch
            {
                1 => 0,
                -1 => 180,
                _ => _movingObjectTemp.transform.rotation.y
            }, 0);
        }
        private IEnumerator MovingTick()
        {
            while (true)//그냥 실행시 반복
            {
                yield return new WaitForSeconds(frameRate);
                switch (_horizontal)
                {
                    case not 0:
                    {
                        _movingObjectTemp.SetActive(false);
                        if (_movingObjectTemp != _defaultObj)
                        {
                            _moveObjectQueue.Enqueue(_movingObjectTemp);
                        }
                        _movingObjectTemp = _moveObjectQueue.Dequeue();
                        _movingObjectTemp.SetActive(true);
                        break;
                    }
                    default:
                    {
                        if (_movingObjectTemp != _defaultObj)
                        {
                            _moveObjectQueue.Enqueue(_movingObjectTemp);
                            _movingObjectTemp.SetActive(false);
                        }
                        _movingObjectTemp = _defaultObj;
                        _defaultObj.SetActive(true);
                        break;
                    }
                }
            }
        }
    }
}
