using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadosSalvos : MonoBehaviour
{
  public int moedas, tempoRelogio, tempoIma, tempoNix, tempoBrad;
  public static DadosSalvos dadosSalvos;
  private void Awake() {
       dadosSalvos = this;
  }
  private void Start() {
      moedas = PlayerPrefs.GetInt("GameScene" + "moedas");
      tempoRelogio = PlayerPrefs.GetInt("tempoRelogio");
      tempoIma = PlayerPrefs.GetInt("tempoIma");
      tempoNix = PlayerPrefs.GetInt("tempoNix");
      tempoBrad = PlayerPrefs.GetInt("tempoBrad");
  }
}
