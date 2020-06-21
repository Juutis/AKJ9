using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TowerProjectile : MonoBehaviour
{
    private EnergyTypeConfig energyTypeConfig;
    private Goblin target;
    private float minDistance = 0.01f;
    private float damage = 1;

    bool damaged = false;

    [SerializeField]
    GameObject explosion;

    List<GameObject> targetsHit = new List<GameObject>();
    private int bouncesLeft;

    public void Launch(Goblin targetGoblin)
    {
        target = targetGoblin;
        damaged = false;
        targetsHit = new List<GameObject>();
        bouncesLeft = energyTypeConfig.Bounces;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 targetPos = target.transform.position;
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
            
            if (Vector3.Distance(transform.position, targetPos) < minDistance && !damaged) {

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
                    var expl = Instantiate(explosion);
                    expl.transform.position = transform.position;
                }
                targetsHit.Add(target.gameObject);

                if (!TryBounce()) 
                {
                    Invoke("Die", 0.5f);
                    damaged = true;
                }
            }
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

    private void Die() {
        Destroy(gameObject);
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
