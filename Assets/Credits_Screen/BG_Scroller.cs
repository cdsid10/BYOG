using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scroller : MonoBehaviour
{
    [SerializeField] Renderer BG_Renderer;
    [SerializeField] float scrollSpeed;

    private void Update()
    {
        Vector2 offset = BG_Renderer.material.mainTextureOffset;
        offset = offset + new Vector2(scrollSpeed * Time.deltaTime, 0);
        BG_Renderer.material.mainTextureOffset = offset;
    }
}
