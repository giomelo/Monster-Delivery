using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroiPista : MonoBehaviour
{
    [SerializeField]
    private ModuloPista modulo;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            if((int)modulo.nivelPista != 3){
                InstanciarItens.instanciarItens.LimparObjetos(modulo.gameObject);
            }
            Pista.pista.ContinuarPista();
            Invoke(nameof(Desativar), 10f);
        }
    }
    void Desativar(){
        if(!modulo.CompareTag("Primeiro"))
        {
            switch ((int)modulo.nivelPista)
            {
                case 0:
                    Pista.pista.modulosDestruidosFacil.Add(modulo);
                    break;
                case 1:
                    Pista.pista.modulosDestruidosMedio.Add(modulo);
                    break;
                case 2:
                    Pista.pista.modulosDestruidosDificil.Add(modulo);
                    break;
                case 3:
                    Pista.pista.modulosDestruidosNeutros.Add(modulo);
                    break;
            }

            modulo.gameObject.SetActive(false);
        }else{
            modulo.gameObject.SetActive(false);
        }
    }
   
}
