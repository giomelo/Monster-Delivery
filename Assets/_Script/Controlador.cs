using System.Collections;
using System.Collections.Generic;
using _Script;
using _Script.Entities;
using UnityEngine;

public class Controlador : MonoBehaviour
{
    public int erros = 0;
    public bool pause = false;
    [SerializeField]
    private GameObject[] erro;
    [SerializeField]
    private float valorBarra = 100;
    [SerializeField]
    private float currentValue;
    [SerializeField]
    private BarraControle barraControle;
    static public Controlador controlador;
    [SerializeField]
    private Transform cameraPlayer;
    [SerializeField]
    public bool zona1 = true, zona2 = false, zona3 = false;
    [SerializeField]
    private AudioSource gameOverGoblin, gameOverMonstro;
    public int encomendasEntregue;
    [SerializeField]
    private GameObject telaGameOverDemitido, telaGameOverOuch;
    [SerializeField]
    private AudioSource musicaGamePlay;
    Vector3 cameraPostion;
    private void Awake() {
        cameraPostion = cameraPlayer.transform.position;
    }

    private void Start(){
        controlador = this;
        Time.timeScale = 0f;
        currentValue = valorBarra;
        zona1 = true;
        zona2 = false;
        zona3 = false;
        InvokeRepeating("BarraDescer", 2, 2.8f);
        InvokeRepeating("GameOver", 1, 1f);
    }
    void BarraDescer(){
        if(!pause){
            currentValue -= 1.5f;
            barraControle.SetValue(currentValue);
        }
    }
   public void Acrescentar(float valor){
        currentValue += valor;
        if(currentValue > valorBarra){
            currentValue = valorBarra;
        }
        barraControle.SetValue(currentValue);
    }
    public void Diminuir(float valor){
        currentValue -= valor;
        barraControle.SetValue(currentValue);
    }
    //Erros HUD
    public void Erros(){
        erro[erros-1].SetActive(false);
    }
    public void GameOver(){
        if(erros >= 5){
            Time.timeScale = 0f;
           telaGameOverDemitido.SetActive(true);
           SalvarPontos.salvarPontos.AtualizaMoedas();
           musicaGamePlay.Stop();
            SalvarPontos.salvarPontos.ChecarPontuacao();
            //SalvarPontos.salvarPontos.SalvarMoedas();
            SalvarPontos.salvarPontos.SalvarEncomendas();
            SalvarPontos.salvarPontos.SalvarDistanciaTotal();
            SalvarPontos.salvarPontos.SalvarPontosCasasMax(Player.Instance.Recompensa);
            SalvarPontos.salvarPontos.SalvarMoedasTotal(Player.Instance.Moedas);
            pause = true;
            HUD.hUD.distanciaMaxGameOver.text = PlayerPrefs.GetInt("GameScene" + "score").ToString();
            HUD.hUD.distanciaDemitido.text = SalvarPontos.salvarPontos.PontuacaoAtual.ToString();
            HUD.hUD.pontosDemitido.text = HUD.hUD.pontos.text.ToString();
            HUD.hUD.moedasDemitido.text = Player.Instance.Moedas.ToString();
            gameOverGoblin.Play();
            PlayerPrefs.Save();
            
        }
        if(barraControle.slider.value <= 0){
             Time.timeScale = 0f;
            telaGameOverOuch.SetActive(true);
            SalvarPontos.salvarPontos.AtualizaMoedas();
            musicaGamePlay.Stop();
            SalvarPontos.salvarPontos.ChecarPontuacao();
            //SalvarPontos.salvarPontos.SalvarMoedas();
            SalvarPontos.salvarPontos.SalvarEncomendas();
            SalvarPontos.salvarPontos.SalvarDistanciaTotal();
            SalvarPontos.salvarPontos.SalvarPontosCasasMax(Player.Instance.Recompensa);
            SalvarPontos.salvarPontos.SalvarMoedasTotal(Player.Instance.Moedas);
            HUD.hUD.distanciaMaxGameOverOuch.text = PlayerPrefs.GetInt("GameScene" + "score").ToString();
            HUD.hUD.distanciaOuch.text = SalvarPontos.salvarPontos.PontuacaoAtual.ToString();
            HUD.hUD.pontosOuch.text = HUD.hUD.pontos.text.ToString();
            HUD.hUD.moedasOuch.text = Player.Instance.Moedas.ToString();
            pause = true;
            gameOverMonstro.Play();
            PlayerPrefs.Save();
           
        }
    }
    public void Reiniciar(){
        Time.timeScale = 1f;
        RenderSettings.skybox = Pista.pista.skyZona1;
        RenderSettings.skybox.SetFloat("_Blend", 0);
        ModuloPista[] modulosInGame = FindObjectsOfType<ModuloPista>();
        foreach(ModuloPista modulos in modulosInGame){
            Destroy(modulos.gameObject);
        }
        currentValue = valorBarra;
        Player.Instance.personagem[Player.Instance.personagemEscolhido].transform.position = new Vector3(0, .4f, 0);
        cameraPlayer.position = cameraPostion;
        erros = 0;
        Pista.pista.GeraPista();
        HUD.hUD.ReiniciarHud();
        zona1 = true;
        zona2 = false;
        zona3 = false;
        currentValue = valorBarra;
        for(int i =0; i< erro.Length; i++){
            erro[i].SetActive(true);
        }
        telaGameOverOuch.SetActive(false);
        telaGameOverDemitido.SetActive(false);
        AudioController.Instance.Music();
        StartCoroutine(Pause());
    }
    public void EscolherPersonagem(){
        Loader.Load(Loader.Scene.GameScene);
    }
    IEnumerator Pause(){
        yield return new WaitForSeconds(0.7f);
        pause = false;
        Time.timeScale = 1f;
    }
    public void VoltarAoMenu(){
        Time.timeScale = 1f;
         Loader.Load(Loader.Scene.MainMenu);
    }

}
