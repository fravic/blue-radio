using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MoveToClickPoint.cs
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private GameObject movingIndicator;

    private GameObject currentIndicator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000))
            {
                agent.destination = hit.point;
                if (currentIndicator != null)
                {
                    GameObject.Destroy(currentIndicator);
                }
                currentIndicator = GameObject.Instantiate(movingIndicator, hit.point, Quaternion.identity);
            }
        }
    }
}