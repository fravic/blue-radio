using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollisionDetector : MonoBehaviour {

    private UnitModeBehaviour unit;

	// Use this for initialization
	void Start ()
    {
        unit = transform.parent.parent.GetComponent<UnitModeBehaviour>();
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter: " + other.gameObject.tag);
        if (other.gameObject.tag == "PlayerMotherbase")
            unit.IsConnectedToMotherbase = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerMotherbase")
            unit.IsConnectedToMotherbase = false;

        if (unit.IsConnectedToMotherbase && other.gameObject.tag == "PlayerSphere")
        {
            unit.IsConnectedToMotherbase = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!unit.IsConnectedToMotherbase)
        {
            if (other.gameObject.tag == "PlayerSphere")
            {
                if (other.transform.parent.parent.GetComponent<UnitModeBehaviour>().IsConnectedToMotherbase)
                    unit.IsConnectedToMotherbase = true;
            }
        }

        if (other.gameObject.tag == "PlayerMotherbase")
            unit.IsConnectedToMotherbase = true;
    }
}
