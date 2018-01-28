using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : Singleton<PlayerManager> {

    public GameObject aggressiveUnitPrefab;
    public GameObject constructionUnitPrefab;

    public const int UNIT_COST = 200;

    public int money;

    private List<GameObject> selectedUnits = new List<GameObject>();

    private float leftButtonTime;

    public void Start() {
        //selectedUnits.Add(new GameObject());
    }

    public void SpawnAggressiveUnit() {
        if (money >= UNIT_COST) {
            money -= UNIT_COST;
            Instantiate(aggressiveUnitPrefab);
        }
    }

    public void SpawnConstructionUnit() {
        if (money >= UNIT_COST) {
            money -= UNIT_COST;
            Instantiate(constructionUnitPrefab);
        }
    }

    private void ClickToMove()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000))
            {
                foreach(var unit in selectedUnits)
                    unit.GetComponent<ClickToMove>().MoveTo(hit.point);
            }
        }
    }

    private void ClickToSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000))
            {
                if (hit.collider.tag == "Unit")
                {
                    selectedUnits.Add(hit.collider.gameObject);
                    Debug.Log("Unit selected: " + hit.collider.gameObject);
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

    void Update()
    {
        Deselect();
        MouseDownTimings();
        ClickToSelect();
        ClickToMove();
        TransformToTower();
    }
}
