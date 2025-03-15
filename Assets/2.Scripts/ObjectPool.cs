using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;

    public GameObject Monster;
    public GameObject Bullet;
    public GameObject UIFloaty;

    private List<GameObject> _monsters = new();

    private List<GameObject> _bullets = new();

    private List<GameObject> _floaty = new();

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
                monster.SetActive(false);
            }
        }

        for (int i = 0; i < 20; i++)
        {
            GameObject bullet = Instantiate(Bullet);

            if (bullet != null)
            {
                _bullets.Add(bullet);
                bullet.SetActive(false);
            }
        }

        for (int i = 0; i < 15; i++)
        {
            GameObject floaty = Instantiate(UIFloaty);
            if (floaty != null)
            {
                _floaty.Add(floaty);
                floaty.SetActive(false);
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
                float targetDistance = Vector2.Distance(transform.position, _monsters[i].transform.position);
                if (targetDistance <= distance)
                {
                    distance = targetDistance;
                    targetIndex = i;
                }
            }
        }

        return _monsters[targetIndex].transform;
    }

    public void SpawnFloaty(Transform trans, string value)
    {
        for (int i = 0; i < _floaty.Count; i++)
        {
            if (false == _floaty[i].gameObject.activeSelf)
            {
                UIFloaty floaty = _floaty[i].GetComponent<UIFloaty>();

                if (floaty != null)
                {
                    floaty.gameObject.SetActive(true);
                    floaty.Spawn(value, trans.position);
                    return;
                }
            }
        }
    }
}