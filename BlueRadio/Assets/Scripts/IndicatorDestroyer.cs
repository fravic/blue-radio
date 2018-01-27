using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorDestroyer : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Unit")
        {
            Destroy(gameObject);
        }
    }
}
