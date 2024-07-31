using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsteiraInfinitaParallax : MonoBehaviour
{
    public float velocidade = 1f;
    public MeshRenderer meshRenderer;

    void Start()
    {
    }

    void Update()
    {
        float offset = Time.time * velocidade;
        meshRenderer.material.mainTextureOffset += new Vector2(velocidade * Time.deltaTime, 0);
    }
}

