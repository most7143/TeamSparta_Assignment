using UnityEngine;

public class Gun : MonoBehaviour
{
    public float AttackSpeed;

    public Transform Point;

    [HideInInspector] public Transform Target;

    private float _currentTime;

    private void Update()
    {
        if (Target != null)
        {
            TargetOn();

            _currentTime += Time.deltaTime;

            if (_currentTime >= AttackSpeed)
            {
                Shot();
                _currentTime = 0f;
            }
        }
        else
        {
            Transform target = ObjectPool.Instance.NearToMonster(transform);

            if (target != null)
            {
                Target = target;
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

            bullet.transform.position = Point.position;

            Vector3 dir = Target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void TargetOn()
    {
        Vector3 dir = Target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}