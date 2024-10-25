using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
[Serializable]
public class Attack
{
  public float height;
  public float range;
  public float radius;
  public float cooldownTime = 2f;
  [HideInInspector] public float currentCooldownTime;
  [HideInInspector] public bool forceHit; // sometimes NPCs dont connect with their swings when they should
  [HideInInspector] public GameObject attackTarget; // used for force hitting
  [HideInInspector] public GameObject attacker; // what should the type be for this? what do we need it for? Humanoid? GameObject?
  private Item weapon;
  public string animationEventName;
  public delegate void AttackCallback(bool didHit, bool didDestroy, IDestructible damageable);
  public AttackCallback hitCallback;
  public AttackCallback completeCallback;
  [HideInInspector] public bool isComplete;
  [HideInInspector] public bool didHit;
  [HideInInspector] public bool didDestroy;
  public DamageType[] damageTypes;
  // [Header("VFX")]
  // [SerializeField] private VisualEffectAsset hitVFX;
  // [Header("SFX")]
  // [SerializeField] private AK.Wwise.Event startSFX;
  // [SerializeField] private AK.Wwise.Event hitSFX;

  // maybe add destroy sfx?


  public void Initialize(GameObject attacker, Item weapon, AttackCallback hitCallback, AttackCallback completeCallback, float cooldownTime, bool forceHit, GameObject currentTarget)
  {

    this.attacker = attacker;
    this.weapon = weapon;
    this.hitCallback = hitCallback;
    this.completeCallback = completeCallback;
    this.forceHit = forceHit;
    this.attackTarget = currentTarget;
    this.didHit = false;
    this.isComplete = false;
    this.cooldownTime = cooldownTime;
    this.currentCooldownTime = cooldownTime;
    // @todo intialize on animation start event
    // weapon.StartAttack();
    // startSFX.Post(attacker);
  }

  public void OnAttackStart()
  {
    // @sound move sound to here after removing delay
    // weapon.GetComponentInChildren<AttackTrailVFX>()?.StartAttack();
  }

  public void Update(float dt)
  {
    if (isComplete && currentCooldownTime > 0)
    {
      DecrementCooldown(dt);
    }
    // what else would we want to update?
  }
  // damage, staiminaDrain, 
  // 

  public void OnAttackHit()
  {
    didHit = MeleeAttack(out IDestructible damageable);
    if (didHit)
    {
      // @sound play hitSFX
      // hitSFX.Post(attacker);
      // weapon.GetComponentInChildren<AttackTrailVFX>()?.StopAttack();
    }
    didDestroy = (didHit && damageable == null) || (damageable != null && damageable.GetHealth() <= 0);
    if (hitCallback != null)
    {
      hitCallback(didHit, didDestroy, damageable);
    }
  }

  public void OnSwingComplete()
  {
    // weapon.GetComponentInChildren<AttackTrailVFX>()?.StopAttack();
  }
  public void OnAttackComplete()
  {
    // if (completeCallback != null) 
    isComplete = true;
  }

  // public bool AttackPreview() => MeleeAttack();
  private bool MeleeAttack(out IDestructible damageable)
  {
    Transform transform = attacker.transform;
    Vector3 weaponPosition = transform.position + transform.forward * height + transform.up * range;
    Dictionary<DamageType, int> damages = new Dictionary<DamageType, int>();
    foreach (DamageType dType in damageTypes)
    {
      damages.Add(dType, 5); // still need a way to calculate damage amount, based on weapon level, player strength etc
    }
    damageable = null;
    if (forceHit && attackTarget != null)
    {
      damageable = attackTarget.GetComponent<IDestructible>();
      if (damageable != null)
      {
        Vector3 direction = attackTarget.transform.position - attacker.transform.position;
        // find a way to get hit point
        HitData damage = new HitData()
        {
          damages = damages,
          direction = direction,
          hitPoint = attackTarget.transform.GetChild(0).GetComponent<Collider>().ClosestPoint(weaponPosition)
        };
        damageable.Damage(damage);
        return true;
      }
    }
    if (attacker == null)
    {
      Debug.LogError("ATTACKER IS NULL SOMEHOW?? :(");
      return true;
    }
    Collider[] colliders = Physics.OverlapSphere(weaponPosition, radius, LayerMask.GetMask("damageable", "citizen")); // add player?
    if (colliders.Length == 0) return false;
    // @TODO configure multihit

    foreach (Collider col in colliders)
    {
      // derive damage value from weapon and humanoid
      // Vector3 direction = col.transform.position - attacker.transform.position;
      damageable = col.GetComponentInParent<IDestructible>();
      if (damageable != null)
      {
        Vector3 hitPoint = col.ClosestPoint(weaponPosition);
        HitData damage = new HitData()
        {
          hitPoint = hitPoint,
          direction = attacker.transform.forward,
          damages = damages
        };
        // Debug.Log("hit something, damaging");
        // Debug.Log("damageable: " + damageable);
        // Debug.Log(damageable.GetType().FullName);
        damageable.Damage(damage);
        return true; // if multi hit, dont return here, maybe spread total damage across all objects
      }
    }
    return false;
  }

  private void DecrementCooldown(float dt)
  {
    if (isComplete && currentCooldownTime > 0)
    {
      currentCooldownTime -= dt;
    }
    if (currentCooldownTime <= 0 && completeCallback != null)
    {
      completeCallback(didHit, didDestroy, null);
    }
  }

  public bool IsCooled()
  {
    return isComplete && currentCooldownTime <= 0;
  }

  public Attack Copy() => (Attack)this.MemberwiseClone();
}
