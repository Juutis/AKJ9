using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITargetMouse : MonoBehaviour
{
    private LayerMask targetableLayer;
    private LayerMask groundLayer;
    private ShowIndicatorOnTargetable indicator;
    private LineFollowsMouse lineFollow;
    private bool LeftClick { get { return Input.GetMouseButtonDown(0); } }
    private bool RightClick { get { return Input.GetMouseButtonDown(1); } }
    private bool CancelKeyDown { get { return Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Q); } }
    private RaycastHit hit;

    private Energy currentEnergy = null;
    private bool Connecting { get { return currentEnergy != null; } }

    List<Energy> energies = new List<Energy>();

    void Start()
    {
        InitializeComponents();

        foreach (var obj in GameObject.FindGameObjectsWithTag("Energy"))
        {
            energies.Add(obj.GetComponent<Energy>());
        }
    }

    public void StartConnecting(Energy energy)
    {
        currentEnergy = energy;
        lineFollow.SetStart(currentEnergy.transform.position);
    }

    private void InitializeComponents()
    {
        targetableLayer = LayerMask.GetMask("UI");
        groundLayer = LayerMask.GetMask("Water");
        indicator = Prefabs.Instantiate<ShowIndicatorOnTargetable>();
        indicator.Initialize();
        lineFollow = Prefabs.Instantiate<LineFollowsMouse>();
        lineFollow.Initialize();
        indicator.transform.parent = transform;
        lineFollow.transform.parent = transform;
    }

    private T GetHoveredTarget<T>()
    {
        if (Tools.MouseCast(out hit, targetableLayer))
        {
            if (hit.collider != null)
            {
                TargetableSphere targetable = hit.collider.GetComponent<TargetableSphere>();
                return targetable.Parent.GetComponent<T>();
            }
        }
        return default(T);
    }

    private void HandleConnecting()
    {
        Tower hoveredTower = GetHoveredTarget<Tower>();
        if (hoveredTower != null)
        {
            indicator.ShowAt(hoveredTower.transform.position);
            lineFollow.SetEnd(hoveredTower.transform.position);
            if (LeftClick)
            {
                if (currentEnergy.CurrentTower != hoveredTower)
                {
                    currentEnergy.Connect(hoveredTower);
                    currentEnergy = null;
                    lineFollow.Hide();
                    indicator.Hide();
                }
                else
                {
                    lineFollow.Hide();
                    currentEnergy = null;
                }
            }
        }
        else
        {
            indicator.Hide();
            bool mouseHitGround = Tools.MouseCast(out hit, groundLayer);
            if (mouseHitGround)
            {
                lineFollow.SetEnd(hit.point);
            }
            if (RightClick || CancelKeyDown) {
                lineFollow.Hide();
                currentEnergy = null;
            }
            if (LeftClick)
            {
                currentEnergy.Disconnect();
                lineFollow.Hide();
                currentEnergy = null;
            }
        }
    }

    private void HandleHoveringConnectables()
    {
        Energy hoveredEnergy = GetHoveredTarget<Energy>();
        if (hoveredEnergy != null)
        {
            indicator.ShowAt(hoveredEnergy.transform.position);
            if (LeftClick)
            {
                StartConnecting(hoveredEnergy);
            }
        }
        else
        {
            indicator.Hide();
        }
    }

    private void HandleShortcutConnectables()
    {
        Energy selectedEnergy = null;
        foreach (var energy in energies)
        {
            if (Input.GetKeyDown(energy.shortcut))
            {
                selectedEnergy = energy;
            }
        }
        if (selectedEnergy != null)
        {
            StartConnecting(selectedEnergy);
        }
    }

    void Update()
    {
        HandleShortcutConnectables();
        if (Connecting)
        {
            HandleConnecting();
        }
        else
        {
            HandleHoveringConnectables();
        }
    }
}
