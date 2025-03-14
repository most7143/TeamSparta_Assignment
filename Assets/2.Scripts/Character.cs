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

    public virtual void Attack()
    {
    }

    public virtual void Hit(float damage)
    {
        CurrentHP -= damage;

        if (CurrentHP <= 0)
        {
            Dead();
        }
    }

    public virtual void Dead()
    {
        this.IsAlive = false;
        gameObject.SetActive(false);
        CurrentHP = MaxHP;
    }

    private void Activate()
    {
    }

    private void Deactivate()
    {
    }
}