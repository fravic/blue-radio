using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MoveToClickPoint.cs
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class ClickToMove : NetworkBehaviour
{
    [SerializeField] private GameObject movingIndicator;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;

    private NavMeshAgent agent;


    private GameObject currentIndicator;
    private float creationTime;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        var agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);
        agent.enabled = true;
        agent.Warp(transform.position);
        Debug.Log("Spawn unit " + name + " on client");
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed;
        agent.acceleration = acceleration;

        agent.Warp(transform.position);
        agent.enabled = true;
        agent.Warp(transform.position);
        Debug.Log("Spawn unit " + name + " on client");

    }

    [Command]
    public void CmdMoveTo(Vector3 dest)
    {
        UnitModeBehaviour mb = GetComponent<UnitModeBehaviour>();
        // Can't move if not in Van mode
        if (mb.currentMode == UnitModeBehaviour.UnitMode.Tower)
        {
            return;
        }
        agent.destination = dest;
        Debug.Log("Unit move: " + name + " to: " + dest);
        DestroyIndicator();
        currentIndicator = GameObject.Instantiate(movingIndicator, dest, Quaternion.identity);
    }


    public void MoveTo(Vector3 dest)
    {
        //CmdMoveTo(dest);

        UnitModeBehaviour mb = GetComponent<UnitModeBehaviour>();
        // Can't move if not in Van mode
        if (mb.currentMode == UnitModeBehaviour.UnitMode.Tower)
        {
            return;
        }
        agent.destination = dest;
        Debug.Log("Unit move: " + name + " to: " + dest);
        DestroyIndicator();
        Debug.Log(agent.remainingDistance);
        currentIndicator = GameObject.Instantiate(movingIndicator, dest, Quaternion.identity);
        creationTime = Time.time;
    }

    private void OnDestroy()
    {
        StopMovement();
        PlayerManager.Instance.Deselect(gameObject);
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
        if (agent.isActiveAndEnabled) {

            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit))
            {
                var targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
            }

            float dist = agent.remainingDistance;
            if (dist != Mathf.Infinity &&
                agent.pathStatus == NavMeshPathStatus.PathComplete &&
                agent.remainingDistance == 0 && Time.time - creationTime > 0.1) //Arrived
            {
                DestroyIndicator();
            }
        }
    }
}
