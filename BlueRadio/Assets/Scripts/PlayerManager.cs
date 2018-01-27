using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : Singleton<PlayerManager> {

    private GameObject[] selectedUnits = new GameObject[1];

    private void ClickToMove()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if ((selectedUnits[0] != null) && 
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000))
            {
                selectedUnits[0].GetComponent<ClickToMove>().MoveTo(hit.point);            
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
                    selectedUnits[0] = hit.collider.gameObject;
                    Debug.Log("Unit selected: " + hit.collider.gameObject);
                }
            }
        }
    }

    void Update()
    {
        ClickToSelect();
        ClickToMove();        
    }
}
