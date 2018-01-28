using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class PlayerMotherbase : MonoBehaviour {

  public Transform unitSpawnPoint;

  public GameObject aggressiveUnitPrefab;
  public GameObject constructionUnitPrefab;

  public const int CONSTRUCT_COST = 200;
  public const int AGGRESIVE_COST = 400;

  public int money;

  private float timeSincePayday = 0.0f;

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
  void Start () {
        Camera.main.transform.position = new Vector3(unitSpawnPoint.transform.position.x,
            Camera.main.transform.position.y, unitSpawnPoint.transform.position.z - 70);
  }

  // Update is called once per frame
  void Update () {
    AddMoney();
  }
}
