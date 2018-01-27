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

    public void MoveTo(Vector3 dest)
    {
        agent.destination = dest;
        Debug.Log("Unit move: " + name + " to: " + dest);
        DestroyIndicator();
        currentIndicator = GameObject.Instantiate(movingIndicator, dest, Quaternion.identity);
    }

    private void DestroyIndicator()
    {
        if (currentIndicator != null)
        {
            GameObject.Destroy(currentIndicator);
            currentIndicator = null;
        }
    }

    private void Update()
    {
        float dist = agent.remainingDistance;
        if (dist != Mathf.Infinity && 
            agent.pathStatus == NavMeshPathStatus.PathComplete && 
            agent.remainingDistance == 0) //Arrived
        {
            DestroyIndicator();
        }
    }
}