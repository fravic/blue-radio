
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitModeBehaviour : MonoBehaviour {

    //public SphereCollider RadioSphereCollider;

    public enum UnitMode
    {
        Van,
        Tower,
        Tank,
    };

    public UnitMode currentMode;
    public GameObject towerObj;
    public GameObject vanObj;

    // Changes the unit mode from Tower to Van, or vice versa
    public void Toggle()
    {
        if (currentMode == UnitMode.Tank)
        {
            return;
        }

        if (currentMode == UnitMode.Van)
        {
            currentMode = UnitMode.Tower;
            vanObj.GetComponent<Animator>().SetBool("isShrunk", true);
            towerObj.GetComponent<Animator>().SetBool("isShrunk", false);

            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<ClickToMove>().StopMovement();
        }
        else
        {
            currentMode = UnitMode.Van;
            vanObj.GetComponent<Animator>().SetBool("isShrunk", false);
            towerObj.GetComponent<Animator>().SetBool("isShrunk", true);

            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
