using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : Singleton<PlayerManager> {

    private List<GameObject> selectedUnits = new List<GameObject>();

    private float leftButtonTime;

    public void Start() {
        //selectedUnits.Add(new GameObject());
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

    private void Deselect()
    {
        if (Input.GetMouseButtonUp(0) && Time.time - leftButtonTime < 0.5f)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000))
            {
                if (hit.collider.tag != "Unit")
                {
                    selectedUnits.Clear();
                }
            }
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
        MouseDownTimings();
        ClickToSelect();
        ClickToMove();
        TransformToTower();
        Deselect();
    }
}
