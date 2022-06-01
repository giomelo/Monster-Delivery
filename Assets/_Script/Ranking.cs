using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [SerializeField]
    private Text pontuacaoMax, moedasMax, distanciaMax, distanciaTotal, encomendasEntregue;
    [SerializeField]
    private GameObject telaRanking;

    private void Start() {
        encomendasEntregue.text = PlayerPrefs.GetInt("encomendasEntregue").ToString();
        distanciaMax.text = PlayerPrefs.GetInt("GameScenescore").ToString();
        distanciaTotal.text = PlayerPrefs.GetInt("distanciaTotal").ToString();
        pontuacaoMax.text = PlayerPrefs.GetInt("pontosCasasMax").ToString();
        moedasMax.text = PlayerPrefs.GetInt("moedasTotal").ToString();
    }

    public void AbrirRanking(){
        telaRanking.SetActive(true);
        for(int i = 0; i< Creditos.creditosScript.botoesMenuDesativar.Length; i++){
            Creditos.creditosScript.botoesMenuDesativar[i].enabled = false;
        }
    }

    public void FecharRanking(){
        telaRanking.SetActive(false);
        for(int i = 0; i< Creditos.creditosScript.botoesMenuDesativar.Length; i++){
            Creditos.creditosScript.botoesMenuDesativar[i].enabled = true;
        }
    }
}
