using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScrollUV : MonoBehaviour
{

    public int materialIndex = 0;
    public Vector2 uvAnimationRate = new Vector2(0.0f, 1.0f);
    public string textureName = "_MainTex";

    Vector2 uvOffset = Vector2.zero;

    void LateUpdate()
    {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (GetComponent<Renderer>().enabled)
        {
            GetComponent<Renderer>().material.SetTextureOffset(textureName, uvOffset);
        }
    }
}
