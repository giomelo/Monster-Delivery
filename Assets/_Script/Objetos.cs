using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetos : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itens;
    private void Start() {
        InstanciarObjetos();
    }
    public void InstanciarObjetos(){
        int sorteiro = Random.Range(0, itens.Length - 1);
        Instantiate(itens[sorteiro],this.transform);
    }
}
