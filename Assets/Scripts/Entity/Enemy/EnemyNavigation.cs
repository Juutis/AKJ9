using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    private NavMeshAgent agent;
    private Vector3 target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = FindObjectsOfType<Camera>().First();
    }

    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }

        }*/

        agent.SetDestination(target);

    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }
}
