//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    [SerializeField] private float scrollingSpeed;
    [SerializeField] private float panningSpeed;
    [SerializeField] private float panBorder;
    [SerializeField] private bool togglePan;

    private void MoveCamera(float x, float z)
    {
        float newX = x * panningSpeed * Time.deltaTime;
        float newZ = z * panningSpeed * Time.deltaTime;

        transform.position += new Vector3(newX, 0, newZ);
    }

    void TrySidePan() {
        Vector3 mousePos = Input.mousePosition;

        int border = 10;

        if (mousePos.x >= Screen.width - panBorder)
        {
            MoveCamera(1, 0);
        }
        else if (mousePos.x <= 0 + panBorder)
        {
            MoveCamera(-1, 0);
        }

        if (mousePos.y >= Screen.height - panBorder)
        {
            MoveCamera(0, 1);
        }
        else if (mousePos.y <= 0 + panBorder)
        {
            MoveCamera(0, -1);
        }
    }

    void CapCameraPos()
    {
        float xmin = -275;
        float xmax = 47;
        float zmin = -180;
        float zmax = 197;

        Debug.Log(transform.position.x + " " + transform.position.y + " " + transform.position.z);

        if (transform.position.x < xmin)
            transform.position = new Vector3(xmin, transform.position.y, transform.position.z);
        else if (transform.position.x > xmax)
            transform.position = new Vector3(xmax, transform.position.y, transform.position.z);

        if (transform.position.z < zmin)
            transform.position = new Vector3(transform.position.x, transform.position.y, zmin);
        else if (transform.position.z > zmax)
            transform.position = new Vector3(transform.position.x, transform.position.y, zmax);
    }

	void Update () {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.position += scrollingSpeed * new Vector3(x, 0.0f, z);
        if (togglePan)
            TrySidePan();
        CapCameraPos();
    }
}
