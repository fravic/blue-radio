using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {

    private GameObject[] selectedUnits;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000))
            {
                if (hit.collider.tag == "Unit")
                {
                    selectedUnits = new GameObject[] { hit.collider.gameObject };
                    Debug.Log("Unit selected: " + hit.collider.gameObject);
                }
            }
        }
    }
}
