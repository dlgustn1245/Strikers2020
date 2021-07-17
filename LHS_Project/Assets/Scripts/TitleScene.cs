using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    float scrollSpeed = 0.05f;
    Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    
    void Update()
    {
        float offsetY = mat.mainTextureOffset.y + scrollSpeed * Time.deltaTime;
        Vector2 groundOffset = new Vector2(0.0f, offsetY);
        mat.mainTextureOffset = groundOffset;
    }
}
