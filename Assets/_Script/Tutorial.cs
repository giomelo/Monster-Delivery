using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject[] telas;
    public void Tela1Voltar(){
        Loader.Load(Loader.Scene.MainMenu);
    }
    public void Tela1Prox(){
        telas[0].SetActive(false);
        telas[1].SetActive(true);
    }
    public void Tela2Prox(){
        telas[1].SetActive(false);
        telas[2].SetActive(true);
    }
     public void Tela2Voltar(){
        telas[1].SetActive(false);
        telas[0].SetActive(true);
    }
    public void Tela3Prox(){
        telas[2].SetActive(false);
        telas[3].SetActive(true);
    }
    public void Tela3Voltar(){
        telas[2].SetActive(false);
        telas[1].SetActive(true);
    }
    public void Tela4Prox(){
        telas[3].SetActive(false);
        telas[4].SetActive(true);
        
    }
    public void Tela4Voltar(){
        telas[3].SetActive(false);
        telas[2].SetActive(true);
    }
    public void Tela5Prox(){
        if(PlayerPrefs.GetInt("primeiravezTutorial") == 1){
            Loader.Load(Loader.Scene.GameScene);
        }else{
            Loader.Load(Loader.Scene.MainMenu);
        }
    }
    public void Tela5Voltar(){
        telas[4].SetActive(false);
        telas[3].SetActive(true);
    }
}
