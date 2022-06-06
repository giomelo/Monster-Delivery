using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Script.Entities
{
    public class Player : MonoSingleton<Player>
    {
        public Personagem[] personagem = new Personagem[2];
        private int pontos = 0;
        public int Pontos {get{return pontos;} set{pontos=value;HUD.hUD.AtualizarDistancia();}}
        private int moedas = 0;
        public int Moedas {get{return moedas;} set{moedas=value;HUD.hUD.AtualizarMoedas();}}
        public int recompensa = 0;
        public int Recompensa{get{return recompensa;} set{recompensa=value;HUD.hUD.AtualizaPontos();}}
        public CharacterController character;
    
        public Encomenda[] encomendaPrefab;
        [HideInInspector]
        public int escolhaEncomenda = 0;
        public int vermelho = 3,azul = 3,amarelo = 3;
        public int Vermelho{get{return vermelho;} set{vermelho=value;HUD.hUD.AtualizarVermelho();}}
        public int Amarelo{get{return amarelo;} set{amarelo=value;HUD.hUD.AtualizarAmarelo();}}
        public int Azul{get{return azul;} set{azul=value;HUD.hUD.AtualizarAzul();}}
       
        public Protagonistas protagonistas;
        [HideInInspector]
        public int personagemEscolhido;
        private Transform ima;
        [Header("Teste")]
        private const int layerMask = 1 << 8;
        private const int layerMask2 = 1 << 9;

        [SerializeField]
        private bool powerIma = false;
        [SerializeField]
        private bool poderPronto = false;
        [SerializeField]
        private bool usandoPoder = false;
        private int atual;
        [SerializeField]
        private int valorPoder = 800;
        [SerializeField]
        private GameObject[] pecasNix;
        [HideInInspector]
        public GameObject[] pecasEscolhidas;
        [SerializeField]
        private GameObject[] pecasBrad;
        [SerializeField]
        private GameObject colliderBrad;
        public int tempoBrad = 7, tempoNix = 7;
        [SerializeField]
        private bool bradPoder = false;
        [SerializeField]
        private Button brad,nix;
        public int tempoAtivarIma = 6;
        public int tempoRelogio = 5;
        
        public bool trocouUmaVez = false;
        [SerializeField]
        private GameObject poderB;
        [SerializeField]
        private List<GameObject> objetosDestruidos = new List<GameObject>();
    
        public bool relogio = false;
        [SerializeField]
        private PlayerMove playerMove;
    
        private Vector3 dirDireita = new Vector3(1, 0.04f, 2.5f);
        private Vector3 dirEsquerda = new Vector3(-1, 0.04f, 2.5f);
        private void Start(){
            tempoBrad = PlayerPrefs.GetInt("tempoBrad");
            tempoNix = PlayerPrefs.GetInt("tempoNix");
            tempoAtivarIma = PlayerPrefs.GetInt("tempoIma");
            tempoRelogio = PlayerPrefs.GetInt("tempoRelogio");
            InvokeRepeating("DescerPoder", 1, 3f);
            InvokeRepeating("Velocidade", 1, 3f);
        }

        private void Velocidade(){
            personagem[personagemEscolhido].velocidade += 0.035f;
        }

        private void Update()
        {
            if (Controlador.controlador.pause) return;
            playerMove.Movimenta();
            playerMove.Movimenta_X();
            Atirar();
            //Slide();
        }
        private void FixedUpdate() {
            if(powerIma){
                HUD.hUD.ima.SetActive(true);
                ExecutarIma();
            }else{
                HUD.hUD.ima.SetActive(false);
            }
            if(bradPoder){
                ExecutarBrad();
            }
        }
        public void EscolhaDePersonagem()
        {
            switch ((int)protagonistas)
            {
                case 0:
                    personagemEscolhido = 0;
                    personagem[0].gameObject.SetActive(true);
                    //personagem[1].gameObject.SetActive(false);
                    pecasEscolhidas = pecasBrad;
                    break;
                case 1:
                    personagemEscolhido = 1;
                    //personagem[0].gameObject.SetActive(false);
                    personagem[1].gameObject.SetActive(true);
                    pecasEscolhidas = pecasNix;
                    break;
            }

            character = personagem[personagemEscolhido].GetComponent<CharacterController>();
        }
        public void PoderNix()
        {
            if (!poderPronto) return;
            HUD.hUD.SetColorWhite();
            StartCoroutine(AtivarNix());
            poderPronto = false;
        }

        private void DescerPoder()
        {
            if (poderPronto) return;
            if (usandoPoder) return;
            valorPoder -= 10;
            if(valorPoder % 100 == 0 || valorPoder <= 0){
                HUD.hUD.AtualizarPecas();
            }
            if(valorPoder <= 0){
                poderPronto = true;
                brad.interactable = true;
                nix.interactable = true;
                valorPoder = 800;
            }
        }
        public void PoderBrad()
        {
            if (!poderPronto) return;
            personagem[personagemEscolhido].GetComponent<Animator>().SetBool("poder", true);
            StartCoroutine(AtivarBrad());
            poderPronto = false;
        }

        private IEnumerator AtivarBrad(){
            bradPoder = true;
            usandoPoder = true;
            yield return new WaitForSeconds(tempoBrad);
            bradPoder = false;
            personagem[personagemEscolhido].GetComponent<Animator>().SetBool("poder", false);
            usandoPoder = false;
            brad.interactable = false;
            HUD.hUD.VoltarPecas();
            poderB.SetActive(false);
            Invoke("VoltarObjetos", 3);
        
        }
        void VoltarObjetos(){
            foreach(GameObject objs in objetosDestruidos){
                objs.SetActive(true);
            }
        }

        private void ExecutarBrad(){ 
            poderB.SetActive(true);
            Collider[] col = Physics.OverlapSphere(personagem[personagemEscolhido].transform.position, 4f, layerMask2);
            foreach(Collider colisor in col){
                objetosDestruidos.Add(colisor.gameObject);
                colisor.gameObject.SetActive(false);
            }
        }

    
        private void Atirar(){
       
            //int i = 0;
            if (!usandoPoder)
            {
                if(Input.GetKeyDown(KeyCode.Z) || character.isGrounded &&  SwipeManager.swipeDown){
                    //if(!trocouUmaVez){
                    trocouUmaVez = true;
                    escolhaEncomenda++;
                    if(escolhaEncomenda > encomendaPrefab.Length -2){
                        escolhaEncomenda = 0;
                    }
                    HUD.hUD.MudarSeta(escolhaEncomenda);
                    // }
                }
            }

#if UNITY_EDITOR
            if(Input.GetMouseButtonDown(1)){
                if(ChecarEncomendas(escolhaEncomenda)){
                    TacarDireita();
                }
             
            }else if(Input.GetMouseButtonDown(0)){
                if(ChecarEncomendas(escolhaEncomenda)){
                    TacarEsquerda();
                }
            }
        
#else
            if(SwipeManager.tapRight){
           if(ChecarEncomendas(escolhaEncomenda)){
             TacarDireita();
           }
             
        }else if(SwipeManager.tapLeft){
            if(ChecarEncomendas(escolhaEncomenda)){
                TacarEsquerda();
            }
        }
#endif
        }

        private void TacarDireita()
        {
            Encomenda encomenda = (Encomenda)Instantiate(encomendaPrefab[escolhaEncomenda],personagem[personagemEscolhido].transform.GetChild(1).position, Quaternion.Euler(19.713f, -137.913f, -100.055f));
            personagem[personagemEscolhido].GetComponent<Animator>().SetTrigger("tacouDireita");
             
            encomenda.transform.GetComponent<Rigidbody>().AddForce(dirDireita * encomenda.velocidade, ForceMode.Impulse);
            Destroy(encomenda.gameObject, 5);
            if(!usandoPoder)
                DiminuirEncomendas(escolhaEncomenda);
        }

        private void TacarEsquerda()
        {
            Encomenda encomenda = (Encomenda)Instantiate(encomendaPrefab[escolhaEncomenda],personagem[personagemEscolhido].transform.GetChild(1).position, Quaternion.Euler(25.4f, 143.7f, 86.2f));
            personagem[personagemEscolhido].GetComponent<Animator>().SetTrigger("tacouEsquerda");
            encomenda.transform.GetComponent<Rigidbody>().AddForce(dirEsquerda * encomenda.velocidade, ForceMode.Impulse);
            Destroy(encomenda.gameObject, 5);
            if(!usandoPoder)
                DiminuirEncomendas(escolhaEncomenda);
        }
   
        IEnumerator AtivarNix(){
            atual = escolhaEncomenda;
            usandoPoder = true;
            escolhaEncomenda = 3;
            yield return new WaitForSeconds(tempoNix);
            escolhaEncomenda = atual;
            HUD.hUD.SetColorNormal();
            usandoPoder = false;
            nix.interactable = false;
            HUD.hUD.VoltarPecas();
        }
        bool ChecarEncomendas(int numero)
        {
            switch (numero)
            {
                case 0:
                    return Vermelho > 0;
                case 1:
                    return Amarelo > 0;
                case 2:
                    return Azul > 0;
                case 3:
                    return true;
                default:
                    throw new System.Exception("Erro");
            }
        }

        private void DiminuirEncomendas(int escolhaEncomenda)
        {
            switch (escolhaEncomenda)
            {
                case 0 when Vermelho > 0:
                    Vermelho--;
                    break;
                case 1 when Amarelo > 0:
                    Amarelo--;
                    break;
                case 2 when Azul > 0:
                    Azul--;
                    break;
            }
        }
        public void Ima(){
            powerIma = true;
            StartCoroutine(tempoIma());
        }
        public IEnumerator tempoIma(){
            yield return new WaitForSeconds(tempoAtivarIma);
            powerIma = false;
        }
        void ExecutarIma(){
            Collider[] col = Physics.OverlapSphere(personagem[personagemEscolhido].transform.position, 4f, layerMask);
            foreach(Collider colisor in col){
                colisor.gameObject.GetComponent<Item>().ima = true;
            }
        }
    }
}
  