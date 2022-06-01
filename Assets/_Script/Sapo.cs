using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapo : MonoBehaviour
{
    [SerializeField]
    private Transform sapo;
    [SerializeField]
    private float velocidade;
    [SerializeField]
    private int chance;
    private Vector3 sapoPosInicial;
    [SerializeField]
    private Vector3 sapoPosFinal;
    [SerializeField]
    private bool podeMover = false;
    [SerializeField]
    private bool direita, esquerda;

    private void Start() {
        //velocidade *= Time.deltaTime;
        InvokeRepeating("MoverSapo", 1, 0.02f);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            Debug.Log("colidiu");
            sapoPosInicial = sapo.transform.position;
            chance = Random.Range(0,11);
            if(chance >= 3){
                podeMover = true;
            }
        }
    }

    void MoverSapo(){
        if(podeMover && direita){
        sapo.Translate(-sapo.transform.right * velocidade * Time.deltaTime);
        }else if(podeMover && esquerda){
         sapo.Translate(sapo.transform.right * velocidade * Time.deltaTime);
        }
       if(direita && sapo.transform.position.x >= sapoPosFinal.x){
            podeMover = false;
            sapo.GetComponent<Animator>().SetTrigger("viradinhaR");
            Invoke("ReiniciarSapo", 10);

        }else if(esquerda && sapo.transform.position.x <= sapoPosFinal.x){
            podeMover = false;
            sapo.GetComponent<Animator>().SetTrigger("viradinhaL");
            Invoke("ReiniciarSapo", 10);
        }
    }

    void ReiniciarSapo(){
        sapo.transform.position = sapoPosInicial;
         sapo.GetComponent<Animator>().SetTrigger("voltar");
    }
}
