using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text pontos, moedas, distancia, vermelho, amarelo, azul, numDistMax, distanciaOuch, distanciaDemitido;
    [SerializeField]
    private Personagem personagem;
    [SerializeField]
    private GameObject botaPoderNix, botaoPoderBrad;
    [SerializeField]
    private Menu menu;
    static public HUD hUD;
    [SerializeField]
    private GameObject TelaPause;
   
    private int i = 0;
    [SerializeField]
    private Text contagemText;
    [SerializeField]
    private int contagem = 3;
    public GameObject GameOverTela;
    [SerializeField]
    private GameObject vermselecao, azulselecao, amaselecao;

    public GameObject ima, relogio;
    public Text distanciaMaxGameOver, distanciaMaxGameOverOuch;
    public Text pontosDemitido, pontosOuch;
    public Text moedasDemitido, moedasOuch;
    
    private Color vermelhoCor;
    private Color azulCor;
    private Color amareloCor;
    private void Awake() {
         hUD = this;
    }

    private void Start()
    {
        hUD = this;
       pontos.text = Player.jogador.Recompensa.ToString();
       moedas.text = Player.jogador.Moedas.ToString();
       distancia.text = SalvarPontos.salvarPontos.PontuacaoAtual.ToString(); 
       vermelho.text = Player.jogador.Vermelho.ToString();      
       amarelo.text = Player.jogador.Amarelo.ToString();
       azul.text = Player.jogador.Azul.ToString();
       vermelhoCor = HUD.hUD.vermelho.color;
       azulCor = HUD.hUD.azul.color;
       amareloCor = HUD.hUD.amarelo.color;
    
    }
    public void AtualizaPontos(){
         pontos.text = Player.jogador.Recompensa.ToString();
    }
    public void AtualizarMoedas(){
        moedas.text = Player.jogador.Moedas.ToString();
    }
    public void AtualizarDistancia(){
        distanciaDemitido.text = SalvarPontos.salvarPontos.PontuacaoAtual.ToString();
    }
    public void AtualizarVermelho(){
         vermelho.text = Player.jogador.Vermelho.ToString();
    }
    public void AtualizarAmarelo(){
        amarelo.text = Player.jogador.Amarelo.ToString();
    }
    public void AtualizarAzul(){
        azul.text = Player.jogador.Azul.ToString();     
    }
    public void AtualizarDistanciaMax(){
        numDistMax.text = SalvarPontos.salvarPontos.pontuacaoMax.ToString();
    }
    public void AtualizarPecas(){
        if(i < Player.jogador.pecasEscolhidas.Length)
        {
            Player.jogador.pecasEscolhidas[i].SetActive(false);
            i++;
        }
        if(i >= Player.jogador.pecasEscolhidas.Length){
            i = 0;
        }
    }
    public void VoltarPecas(){
        for(int i = 0; i< Player.jogador.pecasEscolhidas.Length; i++){
            Player.jogador.pecasEscolhidas[i].SetActive(true);
        }
    }
    public void HudPlayer(){
        if((int)Player.jogador.protagonistas == 0){
            botaoPoderBrad.SetActive(true);
             botaPoderNix.SetActive(false);
        }else if((int)Player.jogador.protagonistas == 1){
            botaPoderNix.SetActive(true);
             botaoPoderBrad.SetActive(false);
        }
    }
    public void ReiniciarHud(){
        Player.jogador.Vermelho = 5;
        Player.jogador.Amarelo = 5;
        Player.jogador.Azul = 5;
        Player.jogador.Pontos = 0;
        Player.jogador.Moedas = 0;
        Player.jogador.Recompensa = 0;
        SalvarPontos.salvarPontos.PontuacaoAtual = 0;
    }
    public void MudarSeta(int num){
        switch(num){
            case 0: vermselecao.SetActive(true);
                    amaselecao.SetActive(false);
                    azulselecao.SetActive(false);
                break;
            case 1:  vermselecao.SetActive(false);
                    amaselecao.SetActive(true);
                    azulselecao.SetActive(false);
                break;
            case 2:  vermselecao.SetActive(false);
                    amaselecao.SetActive(false);
                    azulselecao.SetActive(true);
                break;
        }
    }
    public void Pause()
    {
        Debug.Log("Pause");
        Time.timeScale = 0f;
        Controlador.controlador.pause = true;
        TelaPause.SetActive(true);
    }
    public void Retornar(){
        TelaPause.SetActive(false);
        Time.timeScale = 1f;
        contagemText.gameObject.SetActive(true);
        StartCoroutine(Contagem());
    }
   IEnumerator Contagem(){
        while(contagem > 0){
            contagemText.text = contagem.ToString();
            yield return new WaitForSeconds(1f);
            contagem--;
        }
        contagemText.text = "GO!";
        yield return new WaitForSeconds(1f);
        Controlador.controlador.pause = false;
        
        contagemText.gameObject.SetActive(false);
        contagem = 3;
    }
    public void VoltarAoMenu(){
        TelaPause.SetActive(false);
        Loader.Load(Loader.Scene.MainMenu);
    }

    public void SetColorWhite()
    {
        vermelho.color = Color.white;
        azul.color = Color.white;
        amarelo.color = Color.white;
    }

    public void SetColorNormal()
    {
        vermelho.color = vermelhoCor;
        azul.color = azulCor;
        amarelo.color = amareloCor;
    }
}
