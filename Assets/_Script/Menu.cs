using System.Collections;
using System.Collections.Generic;
using _Script.Entities;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject BotaoNix, BotaoBrad;
    public MoveCamera cameraPlayer;
    [SerializeField]
    public GameObject tutorial1, tutorial2, imagem;
    static public Menu menu;
    [SerializeField]
    private AudioSource audioGamePlay, audioNix, audioBrad;

    [SerializeField]
    private Animation fade;
    void Start()
    {
        menu = this;
        Controlador.controlador.pause = true;
        Time.timeScale = 0f;
    }

    public void EscolherNix(){
       Player.Instance.protagonistas = Protagonistas.Nix;
       fade.Play();
       BotaoBrad.SetActive(false);
       BotaoNix.SetActive(false);
       tutorial1.SetActive(false);
       tutorial2.SetActive(false);
       imagem.SetActive(false);
       Player.Instance.EscolhaDePersonagem();
       cameraPlayer.Camera();
       HUD.hUD.HudPlayer();
       Time.timeScale = 1f;
       Controlador.controlador.pause = false;
        audioGamePlay.Play();
    }
     public void EscolherBrad(){
       Player.Instance.protagonistas = Protagonistas.Brad;
       fade.Play();
       BotaoBrad.SetActive(false);
       BotaoNix.SetActive(false);
       tutorial1.SetActive(false);
       tutorial2.SetActive(false);
       imagem.SetActive(false);
       Player.Instance.EscolhaDePersonagem();
        cameraPlayer.Camera();
        HUD.hUD.HudPlayer();
        Time.timeScale = 1f;
       Controlador.controlador.pause = false;
       audioGamePlay.Play();
    }
}
