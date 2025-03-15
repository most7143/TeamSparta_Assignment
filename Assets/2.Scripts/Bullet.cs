using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;

    public float Speed;

    public float AliveTime;

    private void OnEnable()
    {
        StartCoroutine(ProcessAlive());
    }

    private void Update()
    {
        transform.position += transform.right * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Monster monster = collision.GetComponent<Monster>();

            if (monster != null)
            {
                monster.Hit(Damage);
            }
        }
    }

    private IEnumerator ProcessAlive()
    {
        yield return new WaitForSeconds(AliveTime);
        this.gameObject.SetActive(false);
    }
}