using UnityEngine;

public class Monster : Character
{
    public Rigidbody2D Rigid;
    public BoxCollider2D Collider;

    public int SpawnIndex;
    public float MoveSpeed;

    private bool _isStop;

    private bool _hitRight;

    private bool _isJumping;
    private float _distance = 0.2f;

    private void Update()
    {
        RayCastRight();

        if (false == _isStop)
        {
            Move();
        }

        if (Rigid.velocity.y == 0)
        {
            _isJumping = false;
        }
    }

    public void Init()
    {
        _isStop = false;
        _isJumping = false;
        _hitRight = false;
        Rigid.velocity = Vector3.zero;
    }

    private void Move()
    {
        transform.position += new Vector3(-1, 0, 0) * MoveSpeed * Time.deltaTime;
    }

    private void RayCastRight()
    {
        Vector3 offset = new Vector3(-0.8f, 0.8f);

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

                    Jump();

                    return;
                }

                if (i == hits.Length - 1)
                {
                    _hitRight = false;
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
            Rigid.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
    }
}