using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SalvarPontos : MonoBehaviour
{
    public int pontuacaoAtual;
    public int PontuacaoAtual{get{return pontuacaoAtual;} set{pontuacaoAtual = value; HUD.hUD.AtualizarDistancia();}}
    public int pontuacaoMax;
    public Personagem[] personagem;
    private string nomeDaCena;
    static public SalvarPontos salvarPontos;
    [HideInInspector]
    public int moedas;
    public int Moedas;
    public int tempoRelogio, tempoIma, tempoNix, tempoBrad;
    [SerializeField]
    private int pontosCasasMax;
    [SerializeField]
    private int encomendasEntregues;
    [SerializeField]
    private int distanciaTotal;
    [SerializeField]
    private int moedasTotal;
    string _FileLocation,_FileName; 
    int moedasTotalArquivo;
    private void Awake() {
        salvarPontos = this;
         AtualizaPlayer();
    }
    
    void Start()
    {
        // _FileLocation=Application.dataPath; 
		//_FileName="SaveData.txt"; 	
        //LoadFile();
        // WriteFile();
        pontuacaoAtual = 0;
        pontuacaoMax = 0;
        nomeDaCena = SceneManager.GetActiveScene().name;
        if(PlayerPrefs.HasKey(nomeDaCena + "score")){
            pontuacaoMax = PlayerPrefs.GetInt(nomeDaCena + "score");
        }
        if(PlayerPrefs.HasKey(nomeDaCena + "moedas")){
            Moedas = PlayerPrefs.GetInt(nomeDaCena + "moedas");
        }
        distanciaTotal = PlayerPrefs.GetInt("distanciaTotal");
        moedasTotal = PlayerPrefs.GetInt("moedasTotal");
        InvokeRepeating("Distância", 0, 0.01f);
        //InvokeRepeating("AtualizaMoedas", 0, 0.5f);
       
    }
     public void Distância(){
        pontuacaoAtual = (int)personagem[Player.jogador.personagemEscolhido].transform.position.z;
        Player.jogador.Pontos = pontuacaoAtual;
    }
    public void ChecarPontuacao(){
        if(pontuacaoAtual > pontuacaoMax){
            pontuacaoMax = pontuacaoAtual;
            PlayerPrefs.SetInt(nomeDaCena + "score", pontuacaoMax);
        }
    }
    public void AtualizaPlayer(){
        PlayerPrefs.SetInt( "tempoRelogio", Player.jogador.tempoRelogio);
        PlayerPrefs.SetInt( "tempoIma", Player.jogador.tempoAtivarIma);
        PlayerPrefs.SetInt("tempoNix", Player.jogador.tempoNix);
        PlayerPrefs.SetInt("tempoBrad", Player.jogador.tempoBrad);
    }
    public void AtualizaMoedas(){
        moedas = Player.jogador.Moedas;
        Moedas += moedas;
        PlayerPrefs.SetInt(nomeDaCena + "moedas", Moedas);
    }
    public void SalvarMoedas(){
        Moedas += moedas;
        PlayerPrefs.SetInt(nomeDaCena + "moedas", Moedas);
    }
    public void RecarregarMoedas(){
         PlayerPrefs.SetInt(nomeDaCena + "moedas", Moedas);
    }

    public void SalvarEncomendas(){
        encomendasEntregues += Controlador.controlador.encomendasEntregue;
        PlayerPrefs.SetInt("encomendasEntregue", encomendasEntregues);
    }
    public void SalvarDistanciaTotal(){
        distanciaTotal += pontuacaoAtual;
        PlayerPrefs.SetInt("distanciaTotal", distanciaTotal);
    }
    public void SalvarPontosCasasMax(int valor){
        if(valor > pontosCasasMax){
            pontosCasasMax = valor;
        }
        PlayerPrefs.SetInt("pontosCasasMax", pontosCasasMax);
    }

    public void SalvarMoedasTotal(int valor){
        moedasTotal += valor;
        PlayerPrefs.SetInt("moedasTotal", moedasTotal);
    }
     void WriteFile() 
	{ 
		StreamWriter writer; 
		FileInfo arquivo = new FileInfo(_FileLocation+'/'+_FileName); 
		
		if(!arquivo.Exists) 
		{ 
			writer = arquivo.CreateText(); 
		} 
		else 
		{ 
			arquivo.Delete(); 
			writer = arquivo.CreateText(); 
		} 

        writer.WriteLine(Player.jogador.Moedas);
		writer.Close(); 

	} 
    void LoadFile(){

        int moedas;
        string[] lines = System.IO.File.ReadAllLines(_FileLocation + '/' + _FileName);

        foreach (string line in lines)
        {
            moedas=int.Parse(line);
            AcrescentaMoedas(moedas);
        }

	}
    void AcrescentaMoedas(int valor){
        moedasTotalArquivo += valor;
    }
}
