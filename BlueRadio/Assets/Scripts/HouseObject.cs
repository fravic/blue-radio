using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseObject : MonoBehaviour
{
    public int influence; // 0= none, 1= blue, red=2
    public GameObject influenceIndicator;

    void Start()
    {
        influence = 0;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerSphere")
        {
            //Debug.Log("TRIGGERED " + other.gameObject.tag);

            if (other.gameObject.transform.parent.parent.GetComponent<UnitModeBehaviour>().IsConnectedToMotherbase)
            {
                influence = 1;
                influenceIndicator.SetActive(true);
                influenceIndicator.GetComponent<Renderer>().material.color = Color.blue;
                other.gameObject.transform.parent.parent.GetComponent<UnitModeBehaviour>().activatedHouses.Add(this);
            }
            else
            {
                influence = 0;
                influenceIndicator.SetActive(false);
            }
        }
        else if (other.gameObject.tag == "EnemySphere")
        {
            if (other.gameObject.transform.parent.parent.GetComponent<UnitModeBehaviour>().IsConnectedToMotherbase)
            {
                influence = 2;
                influenceIndicator.SetActive(true);
                influenceIndicator.GetComponent<Renderer>().material.color = Color.red;
                other.gameObject.transform.parent.parent.GetComponent<UnitModeBehaviour>().activatedHouses.Add(this);
            }
            else
            {
                influence = 0;
                influenceIndicator.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        func(other);
    }

    public void DisconnectHouse()
    {
        influence = 0;
        influenceIndicator.SetActive(false);
    }


    public void func(Collider other)
    {
        if (other.gameObject.tag == "PlayerSphere" || other.gameObject.tag == "EnemySphere")
        {
            influence = 0;
            influenceIndicator.SetActive(false);
        }
    }
}
