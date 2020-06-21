using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tower : Targetable
{
    //[SerializeField]
    private MeshRenderer towerTopRenderer;
    private readonly string towerTopName = "tower2_top";

    private float minDistance = 5f;
    private float firingInterval = 1f;

    private float firingTimer = 0f;

    private Energy currentEnergy;

    private bool Connected { get { return currentEnergy != null; } }
    private Goblin currentTarget;
    private HoverIndicator distanceIndicator;

    private TowerMesh towerMesh;
    private EnergyTypeConfig config;


    private void Start()
    {
        distanceIndicator = Prefabs.Instantiate<HoverIndicator>();
        distanceIndicator.transform.parent = transform;
        distanceIndicator.Initialize();
        Color color = Color.cyan;
        color.a = 0.3f;
        distanceIndicator.SetColor(color);
        distanceIndicator.SetSize(minDistance);
        towerMesh = GetComponentInChildren<TowerMesh>();
        firingTimer = firingInterval + 1f;

    }

    public void Connect(Energy energy)
    {
        currentEnergy = energy;
        Debug.Log("<color=green><b>Connected:</b></color> [{0}] <b>=></b> [{1}]".Format(this, energy));
        distanceIndicator.Show(transform.position);
        towerMesh.Activate();

        foreach (Transform nested in transform)
        {
            foreach (Transform t in nested)
            {
                if (t.name == towerTopName)
                {
                    towerTopRenderer = t.GetComponent<MeshRenderer>();
                }
            }
        }

        EnergyTypes type = energy.EnergyType.Type;
        config = Configs.main.EnergyTypeConfigs[type];

        Material[] materials = towerTopRenderer.materials;
        materials[1] = config.CrystalMaterial;
        towerTopRenderer.sharedMaterials = materials;
        towerTopRenderer.materials = materials;
    }

    public void Disconnect(Energy energy)
    {
        // TODO: set default material?
        towerMesh.Deactivate();
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
        GameObject nearest = null;
        float nearestDist = float.MaxValue;
        foreach (var candidate in GameObject.FindGameObjectsWithTag("Goblin"))
        {
            var distance = TargetDistance(candidate);
            if (distance < nearestDist)
            {
                nearestDist = distance;
                nearest = candidate;
            }
        }

        if (nearest != null && nearestDist < minDistance)
        {
            return nearest.GetComponent<Goblin>();
        }
        return null;
    }

    private void FireAtCurrentTarget()
    {
        if (!config) return; //config should exist if connection has been made

        TowerProjectile projectile = ObjectPooler.GetPool(config.Projectile).ActivateObject(transform.position).GetComponent<TowerProjectile>();
        Vector3 pos = transform.position;
        pos.y = 1.5f;
        projectile.transform.position = pos;
        projectile.SetConfig(config);
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

