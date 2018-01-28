using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnImpact : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(name + " Collided with " + other.name);
        /// TODO: add a check for other player
        if (other.tag == "Unit")
        {
            Destroy(other.gameObject);
        }
    }
}
