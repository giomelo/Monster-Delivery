using System.Collections;
using System.Collections.Generic;
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
            Player.jogador.personagem[Player.jogador.personagemEscolhido].GetComponent<Animator>().SetTrigger("colidiu");
            if(Player.jogador.escolhaEncomenda == 0 && Player.jogador.Vermelho > 0){
                Player.jogador.Vermelho --;
            }else if(Player.jogador.escolhaEncomenda == 1 && Player.jogador.Amarelo > 0){
                Player.jogador.Amarelo --;
            }else if(Player.jogador.escolhaEncomenda ==2 && Player.jogador.Azul > 0){
                Player.jogador.Azul--;
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
