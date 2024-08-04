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
        private Rigidbody2D _rigid;
        [SerializeField] private float speed;
        private Queue<GameObject> _moveObjectQueue;
        private GameObject _defaultObj;
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
            _horizontal = Input.GetKey(KeyCode.A) ? Input.GetKey(KeyCode.D) ? 0 : -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
        }

        private void FixedUpdate()
        {
            _rigid.velocity = new Vector2(_horizontal * speed, _rigid.velocity.y);
            var position = transform.position;
            transform.position = new Vector3(Math.Clamp(position.x, -8f, 8f), position.y);
        }

        private GameObject _movingObjectTemp;
        private void NextFrame()
        {
            if (_horizontal == 1)
            { 
                _movingObjectTemp.SetActive(false);
                if (_movingObjectTemp != _defaultObj)
                {
                    _moveObjectQueue.Enqueue(_movingObjectTemp);
                    _movingObjectTemp = _moveObjectQueue.Dequeue();
                    _movingObjectTemp.SetActive(true);
                }
                _movingObjectTemp.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (_horizontal == -1)
            {
                _movingObjectTemp.SetActive(false);
                if (_movingObjectTemp != _defaultObj)
                {
                    _moveObjectQueue.Enqueue(_movingObjectTemp);
                    _movingObjectTemp = _moveObjectQueue.Dequeue();
                    _movingObjectTemp.SetActive(true);
                }
                _movingObjectTemp.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                if (_movingObjectTemp != _defaultObj)
                {
                    _moveObjectQueue.Enqueue(_movingObjectTemp);
                    _movingObjectTemp.SetActive(false);
                }
                _movingObjectTemp = _defaultObj;
                _defaultObj.SetActive(true);
            }
        }

        private float _moveElapsedTime;
        private IEnumerator MovingTick()
        {
            while (true)//그냥 실행시 반복
            {
                yield return new WaitForSeconds(frameRate);
                NextFrame();
            }
        }
    }
}
