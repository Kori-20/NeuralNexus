public interface IDamageable
{
    void CalculateDamage(int damage, EPhysicalDamageType physicalType, EElementalDamageType elementType);
}

public interface IDamageableElement
{
    void CalculateDamage(EPhysicalDamageType damageType);
}

public interface IDamageableDamage
{
    void CalculateDamage(int damage);
}

public interface IDamageableHit //Can be used to easily debug 
{
    void CalculateDamage();
}

