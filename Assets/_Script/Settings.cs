using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Settings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixerSfx;
     [SerializeField]
    private AudioMixer audioMixerMusica;
    [SerializeField]
    private float valorSfxincial = 0;
    [SerializeField]
    private float valorMusicainicial = 0;
    [SerializeField]
    private GameObject telaConfig, telaPause;
    private void Start() {
        PlayerPrefs.SetFloat("volumeSfx", valorSfxincial);
        PlayerPrefs.SetFloat("volumeMusica", valorMusicainicial);
        audioMixerSfx.SetFloat("Sfx", valorSfxincial);
        audioMixerMusica.SetFloat("Musica", valorMusicainicial);
    }
    public void VolumeSfx(float volume){
        audioMixerSfx.SetFloat("Sfx", volume);
        PlayerPrefs.SetFloat("volumeSfx", volume);
    }
    public void VolumeMusica(float volume){
        audioMixerMusica.SetFloat("Musica", volume);
        PlayerPrefs.SetFloat("volumeMusica", volume);
    }

    public void AbrirConfig(){
        telaConfig.SetActive(true);
        for(int i = 0; i< Creditos.creditosScript.botoesMenuDesativar.Length; i++){
            Creditos.creditosScript.botoesMenuDesativar[i].enabled = false;
        }
    }
    public void AbrirConfigPause(){
        telaConfig.SetActive(true);
        telaPause.SetActive(false);
    }
    public void FecharConfigPause(){
        telaConfig.SetActive(false);
        telaPause.SetActive(true);
    }
    public void FecharConfig(){
        telaConfig.SetActive(false);
        for(int i = 0; i< Creditos.creditosScript.botoesMenuDesativar.Length; i++){
            Creditos.creditosScript.botoesMenuDesativar[i].enabled = true;
        }
    }
}
