public interface IDamageable
{
    void TakeDamage(int damage, EPhysicalDamageType physicalType, EElementalDamageType elementType);
}

public interface IDamageableElement
{
    void TakeDamage(EPhysicalDamageType damageType);
}

public interface IDamageableDamage
{
    void TakeDamage(int damage);
}

public interface IDamageableHit
{
    void TakeDamage();
}

