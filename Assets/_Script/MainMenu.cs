using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu mainMenu;
    public bool primeiravezTutorial = true;
    [SerializeField]
    private Animator animatorBrad;
    private bool primeiraVez = true;
    private void Awake(){
        if(PlayerPrefs.HasKey("primeiravez")){
            primeiraVez = false;
        }else{
            primeiraVez = true;
        }
        if(primeiraVez){
            if(!Application.isEditor){
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("primeiravez", 1);
                primeiraVez = false;
            }
        }
        mainMenu = this;
        Time.timeScale = 1f;
    }
    void Start()
    {
        Teste.Instance.Test();
        InvokeRepeating("BradVariacao", 15, 15);
        if(PlayerPrefs.GetInt("primeiravezTutorial") == 1){
            primeiravezTutorial = false;
        }else{
            primeiravezTutorial = true;
        }
        PlayerPrefs.Save();
    }
    public void Carreagar(){
        if(primeiravezTutorial){
        Loader.Load(Loader.Scene.Tutorial);
        primeiravezTutorial = false;
        PlayerPrefs.SetInt("primeiravezTutorial", 1);
        }else{
        Loader.Load(Loader.Scene.GameScene);
        }
    }
    public void Tutorial(){
        Loader.Load(Loader.Scene.Tutorial);
    }
    void BradVariacao(){
        animatorBrad.SetTrigger("variacao");
    }
}
