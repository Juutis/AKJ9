using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tower : Targetable
{
    private float minDistance = 5f;
    private float firingInterval = 1f;

    private float firingTimer = 0f;

    private Energy currentEnergy;

    private bool Connected { get { return currentEnergy != null; } }
    private Goblin currentTarget;
    private HoverIndicator distanceIndicator;

    private void Start()
    {
        distanceIndicator = Prefabs.Instantiate<HoverIndicator>();
        distanceIndicator.transform.parent = transform;
        distanceIndicator.Initialize();
        distanceIndicator.SetColorTint(Color.cyan);
        distanceIndicator.SetSize(minDistance);
        firingTimer = firingInterval + 1f;
    }

    public void Connect(Energy energy)
    {
        currentEnergy = energy;
        Debug.Log("<color=green><b>Connected:</b></color> [{0}] <b>=></b> [{1}]".Format(this, energy));
        distanceIndicator.Show(transform.position);
    }

    public void Disconnect(Energy energy)
    {
        currentEnergy = null;
        distanceIndicator.Hide();
        Debug.Log("<color=red><b>Disconnected:</b></color> [{0}] <b>=></b> [{1}]".Format(this, energy));
    }

    private float TargetDistance(GameObject target)
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    private Goblin GetNextTarget()
    {
        GameObject goblin = GameObject
            .FindGameObjectsWithTag("Goblin")
            .OrderBy(gameObject => TargetDistance(gameObject))
            .Where(gameObject => TargetDistance(gameObject) <= minDistance)
            .FirstOrDefault();
        if (goblin != null)
        {
            return goblin.GetComponent<Goblin>();
        }
        return null;
    }

    private void FireAtCurrentTarget()
    {
        TowerProjectile projectile = Prefabs.Instantiate<TowerProjectile>();
        Vector3 pos = transform.position;
        pos.y = 1.5f;
        projectile.transform.position = pos;
        projectile.Launch(currentTarget);
    }

    private void Update()
    {
        if (Connected)
        {
            firingTimer += Time.deltaTime;
            if (firingTimer > firingInterval)
            {
                firingTimer = 0f;
                if (currentTarget == null)
                {
                    currentTarget = GetNextTarget();
                }
                if (currentTarget != null)
                {
                    FireAtCurrentTarget();
                }
            }
        }
    }
}

