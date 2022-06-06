using System.Collections;
using System.Collections.Generic;
using _Script.Entities;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int valor;
    private string nome;
    [HideInInspector]
    public bool ima = false;
    private float velocidade = 0;
    [SerializeField]
    private ParticleSystem coinParticle;
    [SerializeField]
    private AudioClip moedinha;
    private void Awake() {
         nome = this.tag;
    }
    private void FixedUpdate() {
         if(this.CompareTag("Moeda") && ima){
            ExecutarIma();
         }
    }
    private void Start() {
        nome = this.tag;
        if(this.CompareTag("Moeda")){
            StartCoroutine("Rodar");
        }
    }
    IEnumerator Rodar(){
        yield return new WaitForSeconds(0.03f);
        this.transform.Rotate(0,0,200* Time.deltaTime);
        StartCoroutine("Rodar");
    }
    void OnTriggerEnter(Collider other)
    {
        switch(nome){
            case "Moeda":
        if(other.gameObject.CompareTag("Player")){
            Instantiate(coinParticle, Player.Instance.personagem[Player.Instance.personagemEscolhido].transform.position + new Vector3(0, 0.3f, 0), coinParticle.transform.rotation);
            Player.Instance.Moedas += valor;
            AudioSource.PlayClipAtPoint(moedinha, Player.Instance.personagem[Player.Instance.personagemEscolhido].transform.position);
            Destroy(this.gameObject);

        }
        break;
        case "Ima":
            if(other.gameObject.CompareTag("Player")){
            Player.Instance.Ima();
            Destroy(this.gameObject);
         }
        break;
        case "Relogio":
            if(other.gameObject.CompareTag("Player")){
            if(!Player.Instance.relogio){
            ExecutarRelogio();
            }
            this.gameObject.SetActive(false);
        }
        break;
        }
    }

    void ExecutarRelogio(){
            HUD.hUD.relogio.SetActive(true);
            Player.Instance.relogio = true;
            velocidade = Player.Instance.personagem[Player.Instance.personagemEscolhido].velocidade;
            Player.Instance.personagem[Player.Instance.personagemEscolhido].velocidade = Player.Instance.personagem[Player.Instance.personagemEscolhido].velocidade/2;
            //velocidadeEncomenda = velocidade;
            //encomenda.velocidade = encomenda.velocidade/2;
            Invoke("VoltarVelocidade", Player.Instance.tempoRelogio);
    }
    void VoltarVelocidade(){
        Player.Instance.personagem[Player.Instance.personagemEscolhido].velocidade = velocidade;
        Player.Instance.relogio = false;
        HUD.hUD.relogio.SetActive(false);
        //encomenda.velocidade = velocidadeEncomenda;
        Destroy(this.gameObject);
    }
    
    void ExecutarIma(){
        this.transform.position = Vector3.Lerp(this.transform.position, Player.Instance.personagem[Player.Instance.personagemEscolhido].transform.position, Time.deltaTime * 7);
    }
}
