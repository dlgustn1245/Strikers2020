using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    float scrollSpeed = 0.3f;
    Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (GameController.Instance.gameOver)
        {
            scrollSpeed = 0.0f;
        }

        float offsetY = material.mainTextureOffset.y + scrollSpeed * Time.deltaTime;
        Vector2 groundOffset = new Vector2(0.0f, offsetY);
        material.mainTextureOffset = groundOffset;
    }
}
