using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walking : MonoBehaviour
{

    public Transform Target;

    NavMeshAgent mAgent;

    // Use this for initialization
    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Ground"))
            {
                Target.position = hit.point;
                mAgent.SetDestination(Target.position);
            }

        }
    }
}
