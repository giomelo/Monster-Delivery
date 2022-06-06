using System.Collections;
using System.Collections.Generic;
using _Script.Entities;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    [SerializeField]
    private BoxCollider boxCollider;
    [SerializeField]
    private AudioSource somBateu;

    void OnTriggerEnter(Collider other) {
        if(other.transform.CompareTag("Player")){
            somBateu.Play();
            Player.Instance.personagem[Player.Instance.personagemEscolhido].GetComponent<Animator>().SetTrigger("colidiu");
            if(Player.Instance.escolhaEncomenda == 0 && Player.Instance.Vermelho > 0){
                Player.Instance.Vermelho --;
            }else if(Player.Instance.escolhaEncomenda == 1 && Player.Instance.Amarelo > 0){
                Player.Instance.Amarelo --;
            }else if(Player.Instance.escolhaEncomenda ==2 && Player.Instance.Azul > 0){
                Player.Instance.Azul--;
            }
            Invoke("DesativarBox", 0.13f);
        }
    }

    void DesativarBox(){
        boxCollider.enabled = false;
        Invoke("VoltarBox", 1);
        //resto[0].size = new Vector3(0,0,0);
        //resto[1].size = new Vector3(0,0,0);
    }
    void VoltarBox(){
        boxCollider.enabled = true;
    }
}
