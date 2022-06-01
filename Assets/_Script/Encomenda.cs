using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encomenda : MonoBehaviour
{
    [SerializeField]
    public CorTipo corTipo;
    public float velocidade = 15f;

    private void Start()
    {
        StartCoroutine("Rodar");
    }

    private IEnumerator Rodar()
    {
        yield return new WaitForSeconds(1f);
        this.transform.Rotate(1* Time.deltaTime,0,1* Time.deltaTime);
        StartCoroutine("Rodar");
    }
}