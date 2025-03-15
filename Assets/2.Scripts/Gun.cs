using UnityEngine;

public class Gun : MonoBehaviour
{
    public float Damage;

    public float AttackSpeed;

    public Transform Point;

    private Transform _target;

    private float _currentTime;

    private void Update()
    {
        if (_target == null || false == _target.gameObject.activeSelf)
        {
            _target = ObjectPool.Instance.NearToMonster(transform);
        }

        if (_target != null)
        {
            TargetOn();

            _currentTime += Time.deltaTime;

            if (_currentTime >= AttackSpeed)
            {
                Shot();
                _currentTime = 0f;
            }
        }
    }

    public void Shot()
    {
        GameObject obj = ObjectPool.Instance.SpawnBullet();

        if (obj != null)
        {
            Bullet bullet = obj.GetComponent<Bullet>();

            bullet.gameObject.SetActive(true);
            bullet.Damage = Damage;
            bullet.transform.position = Point.position;
            bullet.transform.rotation = transform.rotation;
        }
    }

    public void TargetOn()
    {
        Vector3 dir = _target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}