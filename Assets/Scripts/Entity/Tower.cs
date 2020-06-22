using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float maxDistance = 5f;
    private float firingInterval = 1f;

    private float firingTimer = 0f;
    
    private List<Energy> currentEnergies = new List<Energy>();
    private List<BillboardIcon> effectIcons = new List<BillboardIcon>();
    private List<BillboardIcon> damageIcon = new List<BillboardIcon>();

    private bool Connected { get { return currentEnergies.Count > 0; } }
    public bool AcceptConnections { get { return currentEnergies.Count < 2; } }

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
        distanceIndicator.SetSize(maxDistance);
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
        currentEnergies.Add(energy);
        UpdateEnergies();
        UpdateIcons();
    }

    public void Disconnect(Energy energy)
    {
        currentEnergies.Remove(energy);
        UpdateEnergies();
        UpdateIcons();
    }

    private void UpdateIcons() {
        foreach(BillboardIcon icon in effectIcons) {
            icon.Die();
        }
        effectIcons.Clear();
        int index = 0;
        foreach(Energy energy in currentEnergies) {
            BillboardIcon icon = Prefabs.Instantiate<BillboardIcon>();
            icon.Initialize(energy.Icon, GetIconPosition(index, icon.RT), energy.Title, energy.Message);
            effectIcons.Add(icon);
            index += 1;
        }
    }

    private Vector3 GetIconPosition(int index, RectTransform rt) {
        Vector3 pos = transform.position;
        pos.y = 3f;
        if (index == 1) {
            pos.y += rt.sizeDelta.y * 2 * rt.localScale.y;
        }
        return pos;
    }

    private void UpdateEnergies()
    {
        if (!Connected)
        {
            Reset();
        }
        else
        {
            EnergyTypes type = currentEnergies[0].EnergyType.Type;
            config = Configs.main.EnergyTypeConfigs[type];

            if (currentEnergies.Count > 1)
            {
                config = config.GetCombo(currentEnergies[1].EnergyType.Type);
            }

            distanceIndicator.Show(transform.position);
            towerMesh.Activate();

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

            Color color = config.EffectColor;
            color.a = 0.05f;
            distanceIndicator.SetColor(color);
        }
    }

    private void Reset()
    {
        Material[] materials = towerTopRenderer.materials;
        materials[1] = origMaterial;
        towerTopRenderer.sharedMaterials = materials;
        towerTopRenderer.materials = materials;
        towerMesh.Deactivate();
        distanceIndicator.Hide();
        topEffect.Stop();
        topLight.enabled = false;
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

        if (nearest != null && nearestDist < maxDistance)
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
                if (currentTarget == null || !currentTarget.IsAlive() || TargetDistance(currentTarget.gameObject) > maxDistance)
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

