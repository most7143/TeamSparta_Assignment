using UnityEngine;

public class Monster : Character
{
    public Rigidbody2D Rigid;

    public int SpawnIndex;
    public float MoveSpeed;

    private bool _isStop;

    private bool _hitDiagnal;
    private bool _hitFloor;
    private bool _hitRight;

    private bool _isJumping;
    private float _distance = 0.2f;

    public float FixY;

    private void Update()
    {
        RayCastRight();
        RayCastFloor();
        RayCastDiagonal();

        if (false == _isStop)
        {
            Move();
        }
        else
        {
            if (_hitRight && _isJumping)
            {
                transform.position += new Vector3(1, 0, 0) * MoveSpeed * Time.deltaTime;
            }
        }

        if (transform.position.y < FixY)
        {
            Rigid.bodyType = RigidbodyType2D.Kinematic;
            transform.position = new Vector3(transform.position.x, FixY);
        }
    }

    private void Move()
    {
        transform.position += new Vector3(-1, 0, 0) * MoveSpeed * Time.deltaTime;
    }

    private void RayCastRight()
    {
        Vector3 offset = new Vector3(-0.6f, 0.8f);

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + offset, transform.right, _distance);

        Color hitColor = Color.yellow;

        if (_hitRight)
        {
            hitColor = Color.red;
        }

        Debug.DrawRay(transform.position + offset, transform.right * _distance, hitColor);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag("Box"))
                {
                    _isStop = true;
                    _hitRight = true;
                    return;
                }
                else if (TryNextMonster(hits[i]))
                {
                    _hitRight = true;

                    if (false == _hitDiagnal)
                    {
                        Jump();
                    }
                    else
                    {
                        _isStop = true;
                    }

                    return;
                }

                if (i == hits.Length - 1)
                {
                    _hitRight = false;
                }
            }
        }
    }

    private void RayCastFloor()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up, _distance);

        Color hitColor = Color.yellow;

        if (_hitFloor)
        {
            hitColor = Color.red;
        }

        Debug.DrawRay(transform.position, transform.up * _distance, hitColor);

        if (Rigid.velocity.y < 0)
        {
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (TryNextMonster(hits[i]))
                    {
                        Rigid.bodyType = RigidbodyType2D.Kinematic;
                        Rigid.velocity = Vector2.zero;
                        _hitFloor = true;

                        _isJumping = false;

                        return;
                    }

                    if (i == hits.Length - 1)
                    {
                        _hitFloor = false;
                    }
                }
            }
        }
    }

    private void RayCastDiagonal()
    {
        Vector2 origin = transform.position + new Vector3(-0.2f, 0.8f);

        Vector2 direction = new Vector2(-1, 1).normalized;

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, _distance * 3);

        Color hitColor = Color.yellow;

        if (_hitDiagnal)
        {
            hitColor = Color.red;
        }

        Debug.DrawRay(origin, direction * _distance * 3f, hitColor);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag("Box") || TryNextMonster(hits[i]))
                {
                    _hitDiagnal = true;
                    return;
                }

                if (i == hits.Length - 1)
                {
                    _hitDiagnal = false;
                }
            }
        }
    }

    private bool TryNextMonster(RaycastHit2D hit)
    {
        if (hit.collider.CompareTag("Monster"))
        {
            if (hit.collider.gameObject != this.gameObject)
            {
                Monster monster = hit.transform.GetComponent<Monster>();

                if (monster.SpawnIndex == SpawnIndex)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void Jump()
    {
        if (false == _isJumping)
        {
            _isJumping = true;
            Rigid.bodyType = RigidbodyType2D.Dynamic;
            Rigid.AddForce(Vector2.up * 4f, ForceMode2D.Impulse);
        }
    }
}