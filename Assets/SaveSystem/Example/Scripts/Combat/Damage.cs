public enum DamageableType
{
  Empty,
  Tree,
  TreeLog,
  CopperNode,
  TinNode,
  IronNode,
  BuildePiece,
  EnemyBuildPiece,
  EnemyCitizen,
  Player
}

public enum DamageModifier
{
  Immune,
  Resistant,
  Normal,
  Vulnerable,
  // very vulnerable?

}

public enum DamageType
{
  Axe,
  Blunt,
  Slashing,
  Piercing
  // etc
}

[System.Serializable]
public struct DamageModifiers
{
  public DamageModifier axe;
  public DamageModifier blunt;
  public DamageModifier slashing;
  public DamageModifier piercing;
}