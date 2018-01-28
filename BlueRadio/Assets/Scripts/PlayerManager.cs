using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : Singleton<PlayerManager> {

    [SerializeField] private GameObject selectedUnitIcon;

    public Transform unitSpawnPoint;

    public GameObject aggressiveUnitPrefab;
    public GameObject constructionUnitPrefab;

    public const int UNIT_COST = 200;

    public int money;

    private List<GameObject> selectedUnits = new List<GameObject>();

    private float leftButtonTime;

    private float timeSincePayday = 0.0f;

    public void Start() {
        //selectedUnits.Add(new GameObject());
    }

    public void SpawnAggressiveUnit() {
        if (money >= UNIT_COST) {
            money -= UNIT_COST;
            var go = Instantiate(aggressiveUnitPrefab, position: unitSpawnPoint.position, rotation: unitSpawnPoint.rotation);
            var agent = go.GetComponent<NavMeshAgent>();
            agent.Warp(unitSpawnPoint.position);
            agent.enabled = true;
            agent.Warp(unitSpawnPoint.position);
        }
    }

    public void SpawnConstructionUnit() {
        if (money >= UNIT_COST) {
            money -= UNIT_COST;
            var go = Instantiate(constructionUnitPrefab, position: unitSpawnPoint.position, rotation: unitSpawnPoint.rotation);
            var agent = go.GetComponent<NavMeshAgent>();
            agent.Warp(unitSpawnPoint.position);
            agent.enabled = true;
            agent.Warp(unitSpawnPoint.position);
        }
    }

    private void ClickToMove()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.tag == "Terrain")
                {
                    foreach (var unit in selectedUnits)
                        unit.GetComponent<ClickToMove>().MoveTo(hits[i].point);
                }
            }
        }
    }

    private void ClickToSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.collider.tag == "Unit")
                {
                    selectedUnits.Add(hit.collider.gameObject);
                    hit.collider.gameObject.GetComponent<UnitModeBehaviour>().selectedIndicator.SetActive(true);
                }
            }
        }
    }

    public void Deselect(GameObject go)
    {
        selectedUnits.Remove(go);
    }


    private void Deselect()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            foreach (var unit in selectedUnits)
            {
                unit.GetComponent<UnitModeBehaviour>().selectedIndicator.SetActive(false);
            }
            selectedUnits.Clear();
        }
    }


    private void MouseDownTimings()
    {
        if (Input.GetMouseButtonUp(0))
            leftButtonTime = Time.time;
    }

    private void TransformToTower()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach(var unit in selectedUnits)
                unit.GetComponent<UnitModeBehaviour>().Toggle();
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


    void Update()
    {
        Deselect();
        MouseDownTimings();
        ClickToSelect();
        ClickToMove();
        TransformToTower();
        AddMoney();
    }
}
