using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TowerProjectile : MonoBehaviour
{
    private EnergyTypeConfig energyTypeConfig;
    private Goblin target;
    private Vector3 previousTargetPos;
    private float minDistance = 0.01f;
    private float damage = 1;

    bool dying = false;

    [SerializeField]
    GameObject explosion;

    List<GameObject> targetsHit = new List<GameObject>();
    private int bouncesLeft;

    Pooled pooled;
    List<ParticleSystem> particleSystems = new List<ParticleSystem>();

    void Start()
    {
        particleSystems.AddRange(GetComponents<ParticleSystem>());
        particleSystems.AddRange(GetComponentsInChildren<ParticleSystem>());
        pooled = GetComponent<Pooled>();
    }

    public void PlayEffects()
    {
        foreach (var system in particleSystems)
        {
            system.Play();
        }
    }

    public void StopEffects()
    {
        foreach (var system in particleSystems)
        {
            system.Stop();
        }
    }

    public void Launch(Goblin targetGoblin)
    {
        target = targetGoblin;
        dying = false;
        targetsHit = new List<GameObject>();
        bouncesLeft = energyTypeConfig.Bounces;
        PlayEffects();
    }

    void Update()
    {
        if (target != null && !dying)
        {
            Vector3 targetPos = target.transform.position;

            if (!target.IsAlive())
            {
                targetPos = previousTargetPos;
            }

            targetPos.y = 0.5f;

            var targetDir = (targetPos - transform.position).normalized;
            var targetDist = Vector3.Distance(transform.position, targetPos);

            if (targetDist < energyTypeConfig.Speed * Time.deltaTime)
            {
                transform.position = targetPos;
            }
            else
            {
                transform.position = transform.position + targetDir * energyTypeConfig.Speed * Time.deltaTime;
            }

            if (Vector3.Distance(transform.position, targetPos) < minDistance)
            {

                if (energyTypeConfig.ExplodeRange > 0) //projectile is aoe, find targets
                {
                    doAoEDamage();
                }
                else
                {
                    target.TakeDamage(energyTypeConfig);
                }

                if (explosion != null)
                {
                    OneShotEffect expl = ObjectPooler.GetPool(explosion).ActivateObject(transform.position).GetComponent<OneShotEffect>();
                    expl.Play();
                }
                targetsHit.Add(target.gameObject);

                if (!TryBounce())
                {
                    Invoke("Die", 0.5f);
                    StopEffects();
                    dying = true;
                }
            }

            previousTargetPos = targetPos;
        }
    }

    private void doAoEDamage()
    {
        foreach (var candidate in GameObject.FindGameObjectsWithTag("Goblin"))
        {
            var distance = TargetDistance(candidate);
            if (energyTypeConfig.ExplodeRange > distance)
            {
                Goblin newTarget = candidate.GetComponent<Goblin>();
                if (newTarget != null)
                {
                    newTarget.TakeDamage(energyTypeConfig);
                }
            }
        }
    }

    private bool TryBounce()
    {
        if (bouncesLeft <= 0)
        {
            return false;
        }
        var newTarget = getNextBounceTarget();
        if (newTarget != null)
        {
            target = newTarget;
            bouncesLeft--;
            return true;
        }
        return false;
    }

    private void Die()
    {
        pooled.GetPool().DeactivateObject(gameObject);
    }

    private Goblin getNextBounceTarget()
    {
        GameObject nearest = null;
        float nearestDist = float.MaxValue;
        foreach (var candidate in GameObject.FindGameObjectsWithTag("Goblin"))
        {
            if (targetsHit.Contains(candidate))
            {
                continue;
            }
            var distance = TargetDistance(candidate);
            if (distance < nearestDist)
            {
                nearestDist = distance;
                nearest = candidate;
            }
        }

        if (nearest != null && nearestDist < energyTypeConfig.BounceDistance)
        {
            return nearest.GetComponent<Goblin>();
        }
        return null;
    }

    private float TargetDistance(GameObject target)
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    public void SetConfig(EnergyTypeConfig config)
    {
        energyTypeConfig = config;
    }
}
