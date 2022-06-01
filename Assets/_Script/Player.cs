using System.Collections;
using System.Collections.Generic;
using _Script;
using UnityEngine;
using UnityEngine.UI;

namespace Game.System.Item
{
    
}
public class Player : MonoBehaviour
{
    public static Player jogador;
    public Personagem[] personagem = new Personagem[2];
    private int pontos = 0;
    public int Pontos {get{return pontos;} set{pontos=value;HUD.hUD.AtualizarDistancia();}}
    private int moedas = 0;
    public int Moedas {get{return moedas;} set{moedas=value;HUD.hUD.AtualizarMoedas();}}
    public int recompensa = 0;
    public int Recompensa{get{return recompensa;} set{recompensa=value;HUD.hUD.AtualizaPontos();}}
    private CharacterController character;
    private float yVelocity = 0.0f;
    public int desiredLane = 1;
    [SerializeField]
    private float laneDistance = 2.5f;
    public Encomenda[] encomendaPrefab;
    [HideInInspector]
    public int escolhaEncomenda = 0;
    public int vermelho = 3,azul = 3,amarelo = 3;
    public int Vermelho{get{return vermelho;} set{vermelho=value;HUD.hUD.AtualizarVermelho();}}
    public int Amarelo{get{return amarelo;} set{amarelo=value;HUD.hUD.AtualizarAmarelo();}}
    public int Azul{get{return azul;} set{azul=value;HUD.hUD.AtualizarAzul();}}
    private bool sliding = false;
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
    private Vector3 Scala;
    public bool trocouUmaVez = false;
    [SerializeField]
    private GameObject poderB;
    [SerializeField]
    private List<GameObject> objetosDestruidos = new List<GameObject>();
    
    public bool relogio = false;
    
    private Vector3 dirDireita = new Vector3(1, 0.04f, 2.5f);
    private Vector3 dirEsquerda = new Vector3(-1, 0.04f, 2.5f);
    private void Awake() {
        jogador = this;
    }
    private void Start(){
        jogador = this;
        tempoBrad = PlayerPrefs.GetInt("tempoBrad");
        tempoNix = PlayerPrefs.GetInt("tempoNix");
        tempoAtivarIma = PlayerPrefs.GetInt("tempoIma");
        tempoRelogio = PlayerPrefs.GetInt("tempoRelogio");
        InvokeRepeating("DescerPoder", 1, 5f);
        InvokeRepeating("Velocidade", 1, 3f);
    }

    private void Velocidade(){
        personagem[personagemEscolhido].velocidade += 0.035f;
    }

    private void Update()
    {
        if (Controlador.controlador.pause) return;
        Movimenta();
        Movimenta_X();
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
        switch ((int)Player.jogador.protagonistas)
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

    private void Direita()
    {
        desiredLane++;
        if(desiredLane == 3){
            desiredLane = 2;
        }
    }

    private void Esquerda()
    {
        desiredLane--;
        if(desiredLane == -1){
            desiredLane = 0;
        }
    }
    void Movimenta_X(){
        
        //CONTROLAR OS CAMINHOS
#if UNITY_EDITOR
        
            if(Input.GetButtonDown("Direita"))
            {
                Direita();
                
            }else
            if(Input.GetButtonDown("Esquerda"))
            {
                Esquerda();
            }
   #else  
    if(SwipeManager.swipeRight ){
                Direita();
            }else
            if(SwipeManager.swipeLeft){
               Esquerda();
            }
   #endif
        
        //MOVER
        Vector3 targetPosition = personagem[personagemEscolhido].transform.position.z * personagem[personagemEscolhido].transform.forward + personagem[personagemEscolhido].transform.position.y * personagem[personagemEscolhido].transform.up;
        switch (desiredLane)
        {
            case 0:
                targetPosition += Vector3.left * laneDistance;
                break;
            case 2:
                targetPosition += Vector3.right * laneDistance;
                break;
        }
        personagem[personagemEscolhido].transform.position = targetPosition;
    }

    private void Movimenta(){
        /*if(character.isGrounded){
            personagem[personagemEscolhido].dirtParticles.Play();
        }else {
            personagem[personagemEscolhido].dirtParticles.Stop();
        }*/
        Vector3 moveDirection;
        moveDirection = new Vector3(0, 0, 1);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= personagem[personagemEscolhido].velocidade;
        
        #if UNITY_EDITOR
        if(Input.GetButtonDown("Jump") && character.isGrounded /*&& !sliding*/){
               personagem[personagemEscolhido].GetComponent<Animator>().SetTrigger("podePular");
               moveDirection.z *= 5;
               yVelocity = personagem[personagemEscolhido].pulo;
        }
        if(!character.isGrounded && Input.GetKeyDown(KeyCode.S)){
                yVelocity -= personagem[personagemEscolhido].descer;
        }
     
        #else
         if(SwipeManager.swipeUp && character.isGrounded /*&& !sliding*/){
            personagem[personagemEscolhido].GetComponent<Animator>().SetTrigger("podePular");
            moveDirection.z *= 5;
            yVelocity = personagem[personagemEscolhido].pulo;
        }
        if(!character.isGrounded && SwipeManager.swipeDown){
            yVelocity -= personagem[personagemEscolhido].descer;
        }
        #endif
        yVelocity -= personagem[personagemEscolhido].gravidade * Time.deltaTime;
        moveDirection.y = yVelocity;
        character.Move(moveDirection * Time.deltaTime);
    }

    private void Slide(){
        if(character.isGrounded && Input.GetKeyDown(KeyCode.S) /*SwipeManager.swipeDown*/ && !sliding){
            Debug.Log("sliding");
            Scala = personagem[personagemEscolhido].transform.localScale;
             var newScale = new Vector3(1.2f, 0.66f,1.2f);
            personagem[personagemEscolhido].transform.localScale = new Vector3(Scala.x, 1*Scala.y/2, Scala.z) ;
            sliding = true;
            StartCoroutine(VoltarSlide());   
        }
    }

    private IEnumerator VoltarSlide(){
         yield return new WaitForSeconds(personagem[personagemEscolhido].slideLength);
        //Vector3 scale = new Vector3(1.2f, 1.2f,1.2f);
        personagem[personagemEscolhido].transform.localScale = Scala;
        sliding = false;
    }
    
    private void Atirar(){
       
         //int i = 0;
         if(!usandoPoder)       
             /* if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began){    
                      touches[i] = Input.GetTouch(0);
                      contadorTouches++;
                      i++;
                      Debug.Log("tocou");
                }
            if(touches[0].phase == TouchPhase.Ended && touches[1].phase == TouchPhase.Ended){
                touches[0].deltaTime = tempo1;
                touches[1].deltaTime = tempo2;
                Debug.Log(tempo1 + tempo2);
            if(tempo2 - tempo1 <= timeDoubleTap){
                SwipeManager.doubletap = true;
                Debug.Log("doubletap");
                 escolhaEncomenda++;
             if(escolhaEncomenda > encomendaPrefab.Length -2){
                 escolhaEncomenda = 0;
             }
              HUD.hUD.MudarSeta(escolhaEncomenda);
 
             }
             
            }*/
            
             if(Input.GetKeyDown(KeyCode.Z) ||character.isGrounded &&  SwipeManager.swipeDown){
                 //if(!trocouUmaVez){
                 trocouUmaVez = true;
                 escolhaEncomenda++;
                 if(escolhaEncomenda > encomendaPrefab.Length -2){
                     escolhaEncomenda = 0;
                 }
                 HUD.hUD.MudarSeta(escolhaEncomenda);
                 // }
             }
         /*for (int i = 0; i < Input.touchCount; i++)
                {
             if (Input.GetTouch(i).phase == TouchPhase.Began){
                 if(Input.GetTouch(i).position.x > Screen.height/2){
                     SwipeManager.tapRight = true;
                 }else{
                     SwipeManager.tapLeft = true;
                 }
             }            	
         }*/
        
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
  