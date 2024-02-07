
public interface IDamageable
{
    public float maximumHealth { get; }
    public float currentHealth { get; }

    public void TakeDamage(float damage);
}
