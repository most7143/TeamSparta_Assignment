using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;

    public GameObject Monster;
    public GameObject Bullet;

    private List<GameObject> _monsters = new();

    private List<GameObject> _bullets = new();

    public static ObjectPool Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        for (int i = 0; i < 20; i++)
        {
            GameObject monster = Instantiate(Monster);

            if (monster != null)
            {
                _monsters.Add(monster);
                monster.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < 20; i++)
        {
            GameObject bullet = Instantiate(Bullet);

            if (bullet != null)
            {
                _bullets.Add(bullet);
                bullet.gameObject.SetActive(false);
            }
        }
    }

    public GameObject SpawnMonster()
    {
        for (int i = 0; i < _monsters.Count; i++)
        {
            if (false == _monsters[i].gameObject.activeSelf)
            {
                return _monsters[i];
            }
        }

        GameObject monster = Instantiate(Monster);

        if (monster != null)
        {
            _monsters.Add(monster);

            return monster;
        }

        return null;
    }

    public GameObject SpawnBullet()
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            if (false == _bullets[i].gameObject.activeSelf)
            {
                return _bullets[i];
            }
        }

        return null;
    }

    public Transform NearToMonster(Transform transform)
    {
        float distance = 100f;
        int targetIndex = 0;

        for (int i = 0; i < _monsters.Count; i++)
        {
            if (_monsters[i].activeSelf)
            {
                if (Vector2.Distance(transform.position, _monsters[i].transform.position) <= distance)
                {
                    distance = Vector2.Distance(transform.position, _monsters[i].transform.position);
                    targetIndex = i;
                }
            }
        }

        return _monsters[targetIndex].transform;
    }
}