using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnImpact : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(name + " Collided with " + collision.gameObject.name);
        /// TODO: add a check for other player
        if (collision.collider.tag == "Unit")
        {
            Destroy(collision.collider.gameObject);
        }
    }
}
