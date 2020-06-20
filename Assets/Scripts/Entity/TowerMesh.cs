using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMesh : MonoBehaviour
{
    [SerializeField]
    Transform TowerTop;

    private float floatHeight = 0.2f;
    private float floatAmount = 0.1f;
    private float floatFreq = 2.0f;
    private float floatSpeed = 0.3f;
    
    private bool powered = false;
    private Vector3 topOriginalPos;
    private float activateTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        topOriginalPos = TowerTop.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos;
        if (powered)
        {
            targetPos = topOriginalPos + Vector3.up * (floatHeight + Mathf.Sin((Time.time - activateTime - floatHeight/floatSpeed) * floatFreq) * floatAmount * TowerTop.localScale.y);
        }
        else
        {
            targetPos = topOriginalPos;
        }

        float movementPerTick = floatSpeed * Time.deltaTime;
        if (Vector3.Distance(targetPos, TowerTop.position) <= movementPerTick)
        {
            TowerTop.position = targetPos;
        }
        else
        {
            var dir = targetPos - TowerTop.position;
            TowerTop.position = TowerTop.position + dir.normalized * movementPerTick;
        }
    }

    [ContextMenu("Activate")]
    public void Activate()
    {
        if (!powered)
        {
            powered = true;
            activateTime = Time.time;
        }
    }

    [ContextMenu("Deactivate")]
    public void Deactivate()
    {
        if (powered)
        {
            powered = false;
        }
    }
}
