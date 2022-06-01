using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuloPista : MonoBehaviour
{
    public Transform conector;
    public NivelPista nivelPista;
    public GameObject[] zonasObj = new GameObject[3];

    [SerializeField]
    public Casa[] casas;

    public void ChecarZona(){
        if(Controlador.controlador.zona2){
            zonasObj[0].SetActive(false);
            zonasObj[1].SetActive(true);
            zonasObj[2].SetActive(false);
        }else if(Controlador.controlador.zona3){
            zonasObj[0].SetActive(false);
           zonasObj[1].SetActive(false);
           zonasObj[2].SetActive(true);
        }
    }
}
