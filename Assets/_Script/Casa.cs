using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using _Script;
using UnityEngine;

public class Casa : MonoBehaviour
{
    public CorTipo corTipo;
    public CorTipo2 corTipo2;
    public int valorRecompensa;
    [SerializeField]
    private bool mistura;
    [SerializeField]
    private int contadorEncomendas = 0;
    [SerializeField]
    private CorTipo[] encomendaEntregue = new CorTipo[2];
    private bool errou = false;
    private bool marcouPonto = false;
    [SerializeField]
    private float valorAcrescentar = 5f;
    [SerializeField]
    private float valorDiminuir = 2.5f;
    [SerializeField]
    private ParticleSystem particulas;
    [SerializeField]
    private GameObject cartinha;
    [SerializeField]
    private GameObject joinhaCima, joinhaBaixo;

    private void Acerto()
    {
        marcouPonto = true;
        cartinha.SetActive(false);
        joinhaCima.SetActive(true);
        Player.jogador.Recompensa += valorRecompensa;
        Controlador.controlador.Acrescentar(valorAcrescentar);
        Controlador.controlador.encomendasEntregue++;
        AudioController.audioController.AcertoEncomenda();
    }

    private void Erros()
    {
        Controlador.controlador.erros ++;
        cartinha.SetActive(false);
        joinhaBaixo.SetActive(true);
        Controlador.controlador.Erros();
        Controlador.controlador.Diminuir(valorDiminuir);
        AudioController.audioController.ErroEncomenda();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Encomenda"))
            AudioController.audioController.Encomenda();
        Instantiate(particulas, this.transform.position + new Vector3(0, 5,0), particulas.transform.rotation);
        if (marcouPonto || errou) return;
        
        if(other.GetComponent<Encomenda>().corTipo == CorTipo.Branco){ // poder da nix acerta sempre que taca
            //👻
            Acerto();
            Destroy(other.gameObject);
            return;
        }
        
        if(!mistura){ // nao mista
            if(other.GetComponent<Encomenda>().corTipo == corTipo){
                Acerto();
            }else
            {
                Erros();
            }
            Destroy(other.gameObject);
        }else{ // mista
            do{
                if(contadorEncomendas < 2){ // se ainda nao entregou as duas encomentas
                    encomendaEntregue[contadorEncomendas] = other.GetComponent<Encomenda>().corTipo;
                    contadorEncomendas++;
                    Destroy(other.gameObject);
                }
            }while(contadorEncomendas < 1);
                
            if(ChecarErro(encomendaEntregue[0])){ // se errou a primeira
                Erros();
                errou = true;
                return;
            }
            if(ChecarErro(encomendaEntregue[1])){ // se errou a segunda
                Erros();
                errou = true;
                return;
            }
            if (contadorEncomendas != 2 || errou) return;
            if(ChecarSeDiferente(encomendaEntregue[0], encomendaEntregue[1])){
                // if(MarcouPonto(encomendaEntregue[0], encomendaEntregue[1])){
                Acerto();
                return;
                //}
            }
            Erros();
            
        }
    }
    bool MarcouPonto(CorTipo enco1, CorTipo enco2){
        switch(this.corTipo2)
        {
            case CorTipo2.Laranja:
                return enco1 == CorTipo.Amarelo && enco2 == CorTipo.Vermelho || enco1 == CorTipo.Vermelho && enco2 ==CorTipo.Amarelo;
            case CorTipo2.Roxo:
                return enco1 == CorTipo.Amarelo && enco2 ==CorTipo.Azul || enco1 == CorTipo.Azul && enco2 ==CorTipo.Amarelo;
            case CorTipo2.Verde:
                return enco1 == CorTipo.Amarelo && enco2 ==CorTipo.Azul || enco1 == CorTipo.Azul && enco2 ==CorTipo.Amarelo;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    bool ChecarErro(CorTipo enco){
        switch(corTipo2)
        {
            case CorTipo2.Laranja:
                return enco == CorTipo.Azul;
            case CorTipo2.Roxo:
                return enco == CorTipo.Amarelo;
            case CorTipo2.Verde:
                return enco == CorTipo.Vermelho;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    bool ChecarSeDiferente(CorTipo enco1, CorTipo enco2)
    {
        return enco1 != enco2;
    }

    public void ReeiniciarCasa(){
        marcouPonto = false;
        cartinha.SetActive(true);
        joinhaBaixo.SetActive(false);
        joinhaCima.SetActive(false);
    }
}
