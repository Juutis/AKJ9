using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tower : Targetable
{
    //[SerializeField]
    private MeshRenderer towerTopRenderer;
    private MeshRenderer towerBottomRenderer;
    private readonly string towerTopName = "tower2_top";
    private readonly string towerBottomName = "tower2_base";
    private readonly string topLightName = "Crystal Light";
    private readonly string topEffectName = "TopEnergy";
    private readonly string bottomEffectName = "BottomEnergy";

    private float minDistance = 5f;
    private float firingInterval = 1f;

    private float firingTimer = 0f;

    private Energy currentEnergy;

    private bool Connected { get { return currentEnergy != null; } }
    private Goblin currentTarget;
    private HoverIndicator distanceIndicator;

    private TowerMesh towerMesh;
    private EnergyTypeConfig config;

    private Light topLight;
    private ParticleSystem topEffect;
    private ParticleSystemRenderer bottomEffect;

    private Material origMaterial;


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

        setReferences(transform);

        origMaterial = towerTopRenderer.materials[1];
    }

    private void setReferences(Transform trans)
    {
        foreach (Transform t in trans)
        {
            if (t.name == towerTopName)
            {
                towerTopRenderer = t.GetComponent<MeshRenderer>();
            }
            if (t.name == towerBottomName)
            {
                towerBottomRenderer = t.GetComponent<MeshRenderer>();
            }
            if (t.name == topLightName)
            {
                topLight = t.GetComponent<Light>();
            }
            if (t.name == topEffectName)
            {
                topEffect = t.GetComponent<ParticleSystem>();
            }
            if (t.name == bottomEffectName)
            {
                bottomEffect = t.GetComponent<ParticleSystemRenderer>();
            }

            setReferences(t);
        }
    }

    public void Connect(Energy energy)
    {
        currentEnergy = energy;
        Debug.Log("<color=green><b>Connected:</b></color> [{0}] <b>=></b> [{1}]".Format(this, energy));
        distanceIndicator.Show(transform.position);
        towerMesh.Activate();

        EnergyTypes type = energy.EnergyType.Type;
        config = Configs.main.EnergyTypeConfigs[type];

        Material[] materials = towerTopRenderer.materials;
        materials[1] = config.CrystalMaterial;
        towerTopRenderer.sharedMaterials = materials;
        towerTopRenderer.materials = materials;

        materials = towerBottomRenderer.materials;
        materials[1] = config.CrystalMaterial;
        towerBottomRenderer.sharedMaterials = materials;
        towerBottomRenderer.materials = materials;

        var main = topEffect.main;
        main.startColor = config.EffectColor;

        bottomEffect.trailMaterial = config.CrystalMaterial;

        topLight.color = config.EffectColor;

        topEffect.Play();
        topLight.enabled = true;
    }

    public void Disconnect(Energy energy)
    {
        // TODO: set default material?
        towerMesh.Deactivate();
        currentEnergy = null;
        distanceIndicator.Hide();
        topEffect.Stop();
        topLight.enabled = false;

        Debug.Log("<color=red><b>Disconnected:</b></color> [{0}] <b>=></b> [{1}]".Format(this, energy));
        Reset();
    }

    private void Reset()
    {
        Material[] materials = towerTopRenderer.materials;
        materials[1] = origMaterial;
        towerTopRenderer.sharedMaterials = materials;
        towerTopRenderer.materials = materials;
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

