public class Player : Character
{
    public Gun Gun;

    private void Start()
    {
        InitGun();
    }

    public void InitGun()
    {
        Gun.AttackSpeed = AttackSpeed;
        Gun.Damage = Damage;
    }
}