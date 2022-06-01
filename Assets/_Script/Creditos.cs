using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creditos : MonoBehaviour
{
    [SerializeField]
    private GameObject creditos;
    [SerializeField]
    public Button[] botoesMenuDesativar;
    public static Creditos creditosScript;
    [SerializeField]
    private GameObject[] telas;

    private void Start() {
        creditosScript = this;
    }
     

    public void AbrirCreditos(){
        telas[0].SetActive(true);
         for(int i = 1; i< telas.Length; i++){
            telas[i].SetActive(false);
        }
        creditos.SetActive(true);
       
        for(int i = 0; i< botoesMenuDesativar.Length; i++){
            botoesMenuDesativar[i].enabled = false;
        }
    }
    public void Tela2(){
        telas[0].SetActive(false);
        telas[1].SetActive(true);
    }
    public void VoltarTela2(){
        telas[1].SetActive(false);
        telas[0].SetActive(true);
    }
    public void Tela3(){
        telas[1].SetActive(false);
        telas[2].SetActive(true);
    }
    public void VoltarTela3(){
        telas[2].SetActive(false);
        telas[1].SetActive(true);
    }
    public void Tela4(){
        telas[2].SetActive(false);
        telas[3].SetActive(true);
    }
    public void VoltarTela4(){
        telas[3].SetActive(false);
        telas[2].SetActive(true);
    }
    public void Tela5(){
        telas[3].SetActive(false);
        telas[4].SetActive(true);
    }
    public void VoltarTela5(){
        telas[4].SetActive(false);
        telas[3].SetActive(true);
    }
    public void Tela6(){
        telas[4].SetActive(false);
        telas[5].SetActive(true);
    }
    public void VoltarTela6(){
        telas[5].SetActive(false);
        telas[4].SetActive(true);
    }
    public void FecharCreditos(){
        creditos.SetActive(false);
        for(int i = 0; i< botoesMenuDesativar.Length; i++){
            botoesMenuDesativar[i].enabled = true;
        }
    }
}
