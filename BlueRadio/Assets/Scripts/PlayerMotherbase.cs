using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using UnityEngine.Networking;

public class PlayerMotherbase : NetworkBehaviour {

    public Transform unitSpawnPoint;

    public GameObject aggressiveUnitPrefab;
    public GameObject constructionUnitPrefab;

    public Renderer SphereRenderer;

    public const int CONSTRUCT_COST = 200;
    public const int AGGRESIVE_COST = 400;

    public int money;

    private float timeSincePayday = 0.0f;

    public enum TeamType
    {
        ATANDTURF,
        VERIZONE,
        COUNT,
    };

   // [SyncVar]
    private TeamType nextTeam;

    public TeamType team;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        team = ++nextTeam;

        Init(team);
    }

    public void Init(TeamType teamType)
    {
        Debug.Log("initing motherbase...");
        switch (teamType)
        {
            case TeamType.ATANDTURF:
                Debug.Log("initing at&turf");
                SphereRenderer.material.color = Color.blue;
                break;
            case TeamType.VERIZONE:
                Debug.Log("initing VERIZONE");
                SphereRenderer.material.color = Color.red;
                break;
        }
    }

    public void SpawnAggressiveUnit() {
        if (money >= AGGRESIVE_COST) {
            money -= AGGRESIVE_COST;
            var go = Instantiate(aggressiveUnitPrefab, position: unitSpawnPoint.position, rotation: unitSpawnPoint.rotation);
            var agent = go.GetComponent<NavMeshAgent>();
            agent.Warp(unitSpawnPoint.position);
            agent.enabled = true;
            agent.Warp(unitSpawnPoint.position);
        }
    }

    public void SpawnConstructionUnit() {
        if (money >= CONSTRUCT_COST) {
            money -= CONSTRUCT_COST;
            var go = Instantiate(constructionUnitPrefab, position: unitSpawnPoint.position, rotation: unitSpawnPoint.rotation);
            var agent = go.GetComponent<NavMeshAgent>();
            agent.Warp(unitSpawnPoint.position);
            agent.enabled = true;
            agent.Warp(unitSpawnPoint.position);
        }
    }

    private void AddMoney()
    {
        if (Time.time - timeSincePayday > 1)
        {
            money += 10;
            timeSincePayday = Mathf.Round(Time.time);
        }
    }



    // Use this for initialization
    void Start() {
        Camera.main.transform.position = new Vector3(unitSpawnPoint.transform.position.x,
            Camera.main.transform.position.y, unitSpawnPoint.transform.position.z - 70);
    }

    // Update is called once per frame
    void Update() {
        AddMoney();
    }
}
