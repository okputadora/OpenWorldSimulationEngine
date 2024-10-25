public interface IDestructible
{
  public void Damage(HitData hitData);
  // Damage effects

  public bool HasDamage();

  public DropTable GetDrop();

  public int GetHealth();
}