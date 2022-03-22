using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureTiling : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _velocityX;
    [SerializeField] private float _velocityY;


    void Update()
    {
        _renderer.material.SetTextureOffset("_BaseMap", new Vector2(_velocityX * Time.time, _velocityY * Time.time));
    }
}
