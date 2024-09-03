using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCode : MonoBehaviour
{
    [SerializeField] private bool _goingLeft;
    private Rigidbody2D _rigid;
    private bool _alive = true;
    private RaycastHit2D _ray;
    [SerializeField] private float speed;
    void Start()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(Moving());
    }

    public void Death()
    {
        _alive = false;
        transform.localScale = new Vector2(1, 0.4f);
        speed = 0;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
    private IEnumerator Moving()
    {
        while (_alive)
        {
            _ray = Physics2D.Raycast(transform.position, _goingLeft ? Vector2.left : Vector2.right, 2f, LayerMask.GetMask("Floor"));
            if (_ray.collider && _ray.collider.CompareTag("Floor"))
            {
                _goingLeft = !_goingLeft;
            }
            _rigid.velocity = new Vector2((_goingLeft ? -1 : 1) * speed, _rigid.velocity.y);
            yield return new WaitForSeconds(1f);
        }

        yield break;
    }
}
