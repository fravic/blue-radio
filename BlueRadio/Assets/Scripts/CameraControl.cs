using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    [SerializeField] private float scrollingSpeed;

	void Update () {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.position += scrollingSpeed * new Vector3(x, 0.0f, z);
    }
}
