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
    private BaseTower castle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = FindObjectsOfType<Camera>().First();
    }

    // Update is called once per frame
    void FixedUpdate()
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

        agent.SetDestination(castle.transform.position);

        if(Vector3.Distance(transform.position, castle.transform.position) < 1.5f)
        {
            Debug.Log("I hit you!");
            castle.TakeDamage(1);
            Destroy(gameObject);
        }
    }

    public void SetTarget(BaseTower target)
    {
        this.castle = target;
    }
}
