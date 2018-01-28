﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnImpact : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(name + " Collided with " + other.name);
        /// TODO: add a check for other player
        if (other.tag == "Unit")
        {
            //other.gameObject.GetComponent<UnitModeBehaviour>().IsConnectedToMotherbase = false;
            other.gameObject.GetComponent<UnitModeBehaviour>().DisconnectAllHouses();
            StartCoroutine(DestroyNextFrame(other.gameObject));
        }
    }

    private IEnumerator DestroyNextFrame(GameObject go)
    {
        go.SetActive(false);
        yield return null;
        Destroy(go);
    }
}
