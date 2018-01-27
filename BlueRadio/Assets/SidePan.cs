using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePan : MonoBehaviour {


    [SerializeField] private float ScrollingSpeed;

    private void MoveCamera(float x, float z)
    {
        float newX = x * ScrollingSpeed * Time.deltaTime;
        float newZ = z * ScrollingSpeed * Time.deltaTime;

        transform.Translate(new Vector3(newX, 0, newZ));
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        int border = 10;

        if (mousePos.x >= Screen.width - border)
        {
            MoveCamera(1, 0);
        }
        else if (mousePos.x <= 0 + border)
        {
            MoveCamera(-1, 0);
        }

        if (mousePos.y >= Screen.height - border)
        {
            MoveCamera(0, 1);
        }
        else if (mousePos.y <= 0 + border)
        {
            MoveCamera(0, -1);
        }
    }
}
