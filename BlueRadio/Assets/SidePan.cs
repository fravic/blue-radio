using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePan : MonoBehaviour
{


    [SerializeField]
    private float ScrollingSpeed;

    private void MoveCamera(float x, float y)
    {
        float newX = x * ScrollingSpeed * Time.deltaTime;
        float newY = y * ScrollingSpeed * Time.deltaTime;

        transform.Translate(new Vector3(newX, newY, 0));
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