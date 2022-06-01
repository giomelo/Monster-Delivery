using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loja : MonoBehaviour
{
    [SerializeField]
    private GameObject loja, menu;
    [SerializeField]
    private Text valorMoeda;
    [SerializeField]
    private int precoIma = 35, precoRelogio = 50, precoNix = 80, precoBrad = 80;
    [SerializeField]
    private Text imatext, relogiotext, nixtext, bradtex;
    [SerializeField]
    private int contadorI = 0, contadorR = 0, contadorB= 0 , contadorN = 0;
    [SerializeField]
    private GameObject[] tracinhosIma = new GameObject[4];
    [SerializeField]
    private GameObject[] tracinhosRelogio = new GameObject[4];
    [SerializeField]
    private GameObject[] tracinhosBrad = new GameObject[4];
    [SerializeField]
    private GameObject[] tracinhosNix = new GameObject[4];
    [SerializeField]
    private Button[] botoesMenuDesativar;
    private void Awake() {
        PrefsPrecos();
    }
    void Start(){
        AtualizarPrefsPrecos();
        valorMoeda.text = PlayerPrefs.GetInt("GameScenemoedas").ToString();
        imatext.text = PlayerPrefs.GetInt("precoIma").ToString();
        relogiotext.text = PlayerPrefs.GetInt("precoRelogio").ToString();
        nixtext.text = PlayerPrefs.GetInt("precoNix").ToString();
        bradtex.text = PlayerPrefs.GetInt("precoBrad").ToString();
        Tracinho();
    }
    void Tracinho(){
        switch(contadorI){
            case 1: tracinhosIma[0].SetActive(true);
                break;
            case 2: for(int i =0; i< 2; i++){
                tracinhosIma[i].SetActive(true);
            }
            break;
            case 3:  for(int i =0; i< 3; i++){
                tracinhosIma[i].SetActive(true);
            }
            break;
            case 4: for(int i =0; i< 4; i++){
                tracinhosIma[i].SetActive(true);
            }
            break;
        }
         switch(contadorR){
            case 1: tracinhosRelogio[0].SetActive(true);
                break;
            case 2: for(int i =0; i< 2; i++){
                tracinhosRelogio[i].SetActive(true);
            }
            break;
            case 3:  for(int i =0; i< 3; i++){
                tracinhosRelogio[i].SetActive(true);
            }
            break;
            case 4: for(int i =0; i< 4; i++){
                tracinhosRelogio[i].SetActive(true);
            }
            break;
        }
         switch(contadorN){
            case 1: tracinhosNix[0].SetActive(true);
                break;
            case 2: for(int i =0; i< 2; i++){
                tracinhosNix[i].SetActive(true);
            }
            break;
            case 3:  for(int i =0; i< 3; i++){
                tracinhosNix[i].SetActive(true);
            }
            break;
            case 4: for(int i =0; i< 4; i++){
                tracinhosNix[i].SetActive(true);
            }
            break;
        }
         switch(contadorB){
            case 1: tracinhosBrad[0].SetActive(true);
                break;
            case 2: for(int i =0; i< 2; i++){
                tracinhosBrad[i].SetActive(true);
            }
            break;
            case 3:  for(int i =0; i< 3; i++){
                tracinhosBrad[i].SetActive(true);
            }
            break;
            case 4: for(int i =0; i< 4; i++){
                tracinhosBrad[i].SetActive(true);
            }
            break;
        }
    }
    void AtualizarPrefsPrecos(){
        precoIma = PlayerPrefs.GetInt("precoIma");
        precoRelogio = PlayerPrefs.GetInt("precoRelogio");
        precoNix = PlayerPrefs.GetInt("precoNix");
        precoBrad = PlayerPrefs.GetInt("precoBrad");
        contadorB = PlayerPrefs.GetInt("contadorB");
        contadorI = PlayerPrefs.GetInt("contadorI");
        contadorN = PlayerPrefs.GetInt("contadorN");
        contadorR = PlayerPrefs.GetInt("contadorR");
    }
    public void AbrirLoja(){
        loja.SetActive(true);
        for(int i = 0; i< botoesMenuDesativar.Length; i++){
            botoesMenuDesativar[i].enabled = false;
        }
    }
    public void FecharLoja(){
        loja.SetActive(false);
        for(int i = 0; i< botoesMenuDesativar.Length; i++){
            botoesMenuDesativar[i].enabled = true;
        }
    }

    public void Ima(){
        if(contadorI < 4)
        if(DadosSalvos.dadosSalvos.moedas > precoIma){
        DadosSalvos.dadosSalvos.moedas -= precoIma;
        DadosSalvos.dadosSalvos.tempoIma += 2;
        precoIma += precoIma* 1/3;
        AtualizarMoedas();
        AtualizarPrecos();
        AtualizaPlayer();
        contadorI++;
        Tracinho();
        PrefsPrecos();
        }
    }
    public void Relogio(){
        if(contadorR < 4)
        if(DadosSalvos.dadosSalvos.moedas > precoRelogio){
        DadosSalvos.dadosSalvos.moedas -= precoRelogio;
        precoRelogio += precoRelogio * 1/3;
        DadosSalvos.dadosSalvos.tempoRelogio += 2;
        AtualizarMoedas();
        AtualizarPrecos();
        AtualizaPlayer();
        contadorR++;
        Tracinho();
        PrefsPrecos();
        }
    }
    public void Nix(){
        if(contadorN < 4)
        if(DadosSalvos.dadosSalvos.moedas > precoNix){
        DadosSalvos.dadosSalvos.moedas -= precoNix;
        precoNix += precoNix * 1/3;
        DadosSalvos.dadosSalvos.tempoNix += 3;
        AtualizarMoedas();
        AtualizarPrecos();
        AtualizaPlayer();
        contadorN++;
        Tracinho();
        PrefsPrecos();
        }
    }
    public void Brad(){
        if(contadorB< 4)
        if(DadosSalvos.dadosSalvos.moedas > precoBrad){
        DadosSalvos.dadosSalvos.moedas -= precoBrad;
        precoBrad += precoBrad * 1/3;
        DadosSalvos.dadosSalvos.tempoBrad += 3;
        AtualizarMoedas();
        AtualizarPrecos();
        AtualizaPlayer();
        contadorB++;
        Tracinho();
        PrefsPrecos();
        }
    }

    void AtualizarMoedas(){
        valorMoeda.text = DadosSalvos.dadosSalvos.moedas.ToString();
        PlayerPrefs.SetInt("GameScene" + "moedas", DadosSalvos.dadosSalvos.moedas);
    }
    void AtualizaPlayer(){
        PlayerPrefs.SetInt( "tempoNix", DadosSalvos.dadosSalvos.tempoNix);
        PlayerPrefs.SetInt("tempoBrad", DadosSalvos.dadosSalvos.tempoBrad);
        PlayerPrefs.SetInt( "tempoRelogio", DadosSalvos.dadosSalvos.tempoRelogio);
        PlayerPrefs.SetInt("tempoIma", DadosSalvos.dadosSalvos.tempoIma);
    }

    void AtualizarPrecos(){
        imatext.text = precoIma.ToString();
        relogiotext.text = precoRelogio.ToString();
        nixtext.text = precoNix.ToString();
        bradtex.text = precoBrad.ToString();
    }
    void PrefsPrecos(){
        PlayerPrefs.SetInt("precoIma", precoIma);
        PlayerPrefs.SetInt("precoRelogio", precoRelogio);
        PlayerPrefs.SetInt("precoNix", precoNix);
        PlayerPrefs.SetInt("precoBrad", precoBrad);
        PlayerPrefs.SetInt("contadorI", contadorI);
        PlayerPrefs.SetInt("contadorR", contadorR);
        PlayerPrefs.SetInt("contadorN", contadorN);
        PlayerPrefs.SetInt("contadorB", contadorB);
    }
}
