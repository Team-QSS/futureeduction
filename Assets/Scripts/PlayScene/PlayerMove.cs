using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

namespace PlayScene
{
    public class PlayerMove : MonoBehaviour
    {
        private int _horizontal;
        private float _moveElapsedTime;
        private Rigidbody2D _rigid;
        private Queue<GameObject> _moveObjectQueue;
        private GameObject _defaultObj, _jumpObj, _movingObjectTemp;
        private bool isJumping = false;
        private bool alive = true;
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float frameRate;
        [SerializeField] private CinemachineVirtualCamera vCam;
        public static Vector2 PlayerPosition;

        private void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _moveObjectQueue = new Queue<GameObject>();
            var i = 0;
            foreach (var o in DrawLine.Animations)
            {
                o.transform.parent = gameObject.transform;
                o.transform.position = transform.position;
                o.GetComponent<EdgeCollider2D>().enabled = true;
                if (i > 1)
                {
                    _moveObjectQueue.Enqueue(o);
                }
                o.SetActive(false);
                i++;
            }
            _defaultObj = gameObject.transform.GetChild(0).gameObject;
            _jumpObj = gameObject.transform.GetChild(1).gameObject;
            _defaultObj.SetActive(true);
            StartCoroutine(MovingTick());
            _movingObjectTemp = _defaultObj;
        }

        private void Update()
        {
            if (alive)
            {
                _horizontal = Input.GetKey(KeyCode.A)
                    ? Input.GetKey(KeyCode.D) ? 0 : -1
                    : Input.GetKey(KeyCode.D) ? 1 : 0;
                gameObject.transform.localScale = _horizontal == 0
                    ? gameObject.transform.localScale
                    : new Vector3(-_horizontal, gameObject.transform.localScale.y);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (isOnGround && !isJumping) StartCoroutine(jump());
                }
            }
            else
            {
                _horizontal = 0;
            }
        }
        private void FixedUpdate()
        {
            PlayerPosition = transform.position;
            _rigid.velocity = new Vector2(_horizontal * speed, _rigid.velocity.y);
        }
        private IEnumerator MovingTick()
        {
            while (true)//그냥 실행시 반복
            {
                if (!isJumping)
                {
                    yield return new WaitForSeconds(frameRate);
                    if (isJumping)
                        continue;
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
                yield return null;
            }
        }

        private IEnumerator jump()
        {
            _rigid.AddForce(Vector2.up * jumpForce);
            isJumping = true;
            _movingObjectTemp.SetActive(false);
            _jumpObj.SetActive(true);
            yield return new WaitForSeconds(0.5f); 
            while (!isOnGround)
            {
                yield return null;
            }
            _movingObjectTemp.SetActive(true);
            _jumpObj.SetActive(false);
            isJumping = false;
        }

        private bool isOnGround
        {
            get
            {
                RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, LayerMask.GetMask("Floor"));
                if (!ray) return false;
                return ray.transform.CompareTag("Floor");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.CompareTag("enemy"))
            {
                if (math.abs(other.transform.position.x - transform.position.x) < 1f &&
                    transform.position.y - other.transform.position.y > 0.3f)
                {
                    other.transform.parent.GetComponent<EnemyCode>().Death();
                }
                else
                {
                    if(alive)
                        StartCoroutine(Death());
                }
            }
            else if (other.transform.CompareTag("Goal"))
                Goal();
            
        }
        private void Goal()
        {
            
        }
        private IEnumerator Death()
        {
            speed = 0;
            alive = false;
            _rigid.velocity = new Vector2(0f, 0f);
            yield return new WaitForSeconds(0.5f);
            _rigid.AddForce(new Vector2(0f,jumpForce));
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            foreach (var o in gameObject.transform.GetComponentsInChildren<EdgeCollider2D>())
            {
                o.enabled = false;
            }
            vCam.Follow = null;
            yield return null;
        }
    }
}
