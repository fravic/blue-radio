using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MoveToClickPoint.cs
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    [SerializeField] private GameObject movingIndicator;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;

    private NavMeshAgent agent;


    private GameObject currentIndicator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed;
        agent.acceleration = acceleration;
    }

    public void MoveTo(Vector3 dest)
    {
        UnitModeBehaviour mb = GetComponent<UnitModeBehaviour>();
        // Can't move if not in Van mode
        if (mb.currentMode == UnitModeBehaviour.UnitMode.Tower) {
            return;
        }
        agent.destination = dest;
        Debug.Log("Unit move: " + name + " to: " + dest);
        DestroyIndicator();
        currentIndicator = GameObject.Instantiate(movingIndicator, dest, Quaternion.identity);
    }

    public void StopMovement() {
        if (agent.isActiveAndEnabled) {
            agent.isStopped = true;
        }
        DestroyIndicator();
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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            var targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }

        if (agent.isActiveAndEnabled) {
            float dist = agent.remainingDistance;
            if (dist != Mathf.Infinity &&
                agent.pathStatus == NavMeshPathStatus.PathComplete &&
                agent.remainingDistance == 0) //Arrived
            {
                DestroyIndicator();
            }
        }
    }
}
