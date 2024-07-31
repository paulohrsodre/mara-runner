using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoParallax : MonoBehaviour
{
    public float velocidade = 1f; // Ajuste a velocidade do efeito de paralaxe
    public float larguraSrite;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        larguraSrite = GetComponent<SpriteRenderer>().bounds.size.z;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * velocidade, larguraSrite);
        transform.position = startPosition + Vector3.forward * newPosition;
    }
}
