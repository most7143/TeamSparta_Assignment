using UnityEngine;

public class Character : MonoBehaviour
{
    public bool IsAlive;
    public Animator Animator;

    public float CurrentHP;
    public float MaxHP;

    public float AttackSpeed;
    public float Damage;

    private void Awake()
    {
        CurrentHP = MaxHP;
    }

    public virtual void Hit(float damage)
    {
        CurrentHP -= damage;

        if (CurrentHP <= 0)
        {
            Dead();
        }

        ObjectPool.Instance.SpawnFloaty(transform, damage.ToString());
    }

    public virtual void Dead()
    {
        IsAlive = false;
        gameObject.SetActive(false);
        CurrentHP = MaxHP;
    }
}