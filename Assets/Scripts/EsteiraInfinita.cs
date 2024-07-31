using UnityEngine;

public class EsteiraInfinita : MonoBehaviour
{
    public float velocidade = 5f; // Ajuste a velocidade da esteira

    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float offset = Time.time * velocidade;
        material.mainTextureOffset = new Vector2(0, offset);
    }
}
