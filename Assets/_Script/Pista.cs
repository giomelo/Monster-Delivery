using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pista : MonoBehaviour
{
    public ModuloPista[] listaModulos;
    public int tamanhoPista;
    [SerializeField]
    private List<ModuloPista> modulosFacil = new List<ModuloPista>();
    [SerializeField]
    private List<ModuloPista> modulosMedio = new List<ModuloPista>();
    [SerializeField]
    private List<ModuloPista> modulosDificil = new List<ModuloPista>();
    [SerializeField]
    private List<ModuloPista> modulosNeutros = new List<ModuloPista>();
    public List<ModuloPista> modulosDestruidosFacil = new List<ModuloPista>();
    public List<ModuloPista> modulosDestruidosMedio = new List<ModuloPista>();
    public List<ModuloPista> modulosDestruidosDificil = new List<ModuloPista>();
    public List<ModuloPista> modulosDestruidosNeutros = new List<ModuloPista>();
    
    [SerializeField]
    private int contador = 2;
    Transform conector;
    static public Pista pista;
    [SerializeField]
    private int contadorDeModulos = 0;
    private List<char> dificuldade = new List<char>(){'F', 'N', 'M', 'N', 'F', 'N', 'M', 'N', 'D', 'N', 'F','N', 'D', 'N',
                                                            'M','N', 'F', 'N', 'D', 'N', 'M', 'N', 'D'};
    [SerializeField]
    private int indice = 0;
    [SerializeField]
    private int zona = 1;
    public Material skyZona1, skyZona3;
    private bool skyChanged = false;

    private void Start()
    {
        zona = 1;
        pista = this;
        skyZona1.SetFloat("_Blend", 0);
        GeraPista();
    }

    private void FixedUpdate()
    {
        
        if (!skyChanged) return;
        RenderSettings.skybox.SetFloat("_Blend" , Mathf.Lerp(RenderSettings.skybox.GetFloat("_Blend"),1,Time.deltaTime));
        if (RenderSettings.skybox.GetFloat("_Blend") == 1)
        {
            skyChanged = false;
        }

    }

    public void GeraPista(){
        Debug.Log("pista");
        ModuloPista copia = Instantiate(listaModulos[0]);
        conector = copia.conector;
        for(int i = 1; i<tamanhoPista; i++){
            int moduloSorteado = Random.Range(1,listaModulos.Length -1);
             copia = Instantiate(listaModulos[moduloSorteado]);
             //ultimo = copia;
             copia.transform.position = conector.position;
             conector = copia.conector;
             contadorDeModulos++;
        }
        Checar();
    }
    public void ContinuarPista(){
        //Checar();
        List<ModuloPista> listaEscolhida = new List<ModuloPista>();
        List<ModuloPista> listaEscolhida2 = new List<ModuloPista>();
        switch(dificuldade[indice]){
            case 'F': listaEscolhida = modulosDestruidosFacil;
                      listaEscolhida2 = modulosFacil;
                break;
            case 'M': listaEscolhida = modulosDestruidosMedio;
                      listaEscolhida2 = modulosMedio;
                break;
            case 'D': listaEscolhida = modulosDestruidosDificil;
                      listaEscolhida2 = modulosDificil; 
                break;
            case 'N': listaEscolhida = modulosDestruidosNeutros;
                      listaEscolhida2 = modulosNeutros;
                break;
        }
        if(listaEscolhida.Count > contador){
           for(int i =0; i < contador; i++){
               //pegar um dos modulos e spawnar dnvo
               ModuloPista modulo = listaEscolhida[Random.Range(0, listaEscolhida.Count)];
               if((int)modulo.nivelPista != 3){//neutro
                foreach (Casa item in modulo.casas)
                {
                 item.ReeiniciarCasa();
                }
               }
               modulo.ChecarZona();
               modulo.transform.position = conector.position;
               modulo.gameObject.SetActive(true);
               if((int)modulo.nivelPista != 3){
                   InstanciarItens.instanciarItens.Objetos(modulo.transform);
               }
               conector = modulo.conector;
               //remover da lista o modulo
               listaEscolhida2.Add(modulo);
               listaEscolhida.Remove(modulo);
               contadorDeModulos++;
               Checar();
           }
        }else{
            ModuloPista modulo = listaEscolhida2[Random.Range(0, listaEscolhida2.Count)];
            ModuloPista copia = Instantiate(modulo, conector.position, modulo.transform.rotation);
            copia.gameObject.SetActive(true);
            if((int)copia.nivelPista != 3){//neutro
                foreach (Casa item in copia.casas)
                {
                  item.ReeiniciarCasa();
                }
            }
            copia.ChecarZona();
            //modulo.transform.position = conector.position;
            conector = copia.conector;
            listaEscolhida2.Remove(modulo);
            contadorDeModulos++;
            Checar();
            
        }

    }
    void Checar(){
      if(contadorDeModulos == tamanhoPista){
          indice=1;//N
      }else if(contadorDeModulos == tamanhoPista + 1){
          indice=2;//M
      }else if(contadorDeModulos ==  tamanhoPista + 4){
          indice=3;//N
      }else if(contadorDeModulos == tamanhoPista + 5){
          indice=4;//F
      }else if(contadorDeModulos == tamanhoPista + 7){
          indice=5;//N
      }else if(contadorDeModulos == tamanhoPista + 8){
          indice=6;//M
      }else if(contadorDeModulos == tamanhoPista + 10){
          indice=7;//N
      }else if(contadorDeModulos == tamanhoPista + 11){
          indice=8;//D
      } else if(contadorDeModulos == tamanhoPista + 12){
          indice=9;//N
      } else if(contadorDeModulos == tamanhoPista + 13){
          indice=10;//F
      } else if(contadorDeModulos == tamanhoPista + 16){
          indice=11;//N
      } else if(contadorDeModulos == tamanhoPista + 17){
          indice=12;//D
      } else if(contadorDeModulos == tamanhoPista + 20){
          indice=13;//N
      } else if(contadorDeModulos == tamanhoPista + 21){
          indice=14;//M
      } else if(contadorDeModulos == tamanhoPista + 25){
          indice=15;//N
      } else if(contadorDeModulos == tamanhoPista + 27){
          indice=16;//F
      } else if(contadorDeModulos == tamanhoPista + 29){
          indice=17;//N
      } else if(contadorDeModulos == tamanhoPista + 30){
          indice=18;//D
      } else if(contadorDeModulos == tamanhoPista + 32){
          indice=19;//N
      } else if(contadorDeModulos == tamanhoPista + 33){
         switch (zona)
         {
             case 1:
                 Controlador.controlador.zona1 = false;
                 Controlador.controlador.zona2 = true;
                 // RenderSettings.skybox.SetFloat("_Blend" , Mathf.Lerp(skyZona2.GetFloat("_Blend"),1,Time.deltaTime));
                 skyChanged = true;
                 zona++;
                 break;
             case 2:
                 Controlador.controlador.zona2 = false;
                 Controlador.controlador.zona3 = true;
                 RenderSettings.skybox = skyZona3;
                 skyChanged = true;
                 zona++;
                 break;
         }
          indice = 0;
          contadorDeModulos = 1;
      }
    }
}
