using System.Collections;
using System.Collections.Generic;
using _Script.Entities;
using UnityEngine;

public class LootEncomenda : MonoBehaviour
{
   public CorTipo corTipo;
   public int quantidade;
    private void OnTriggerEnter(Collider other) {
        if((int)this.corTipo == 0){
            Player.Instance.Amarelo += quantidade;
        }else if((int)this.corTipo == 1){
            Player.Instance.Vermelho += quantidade;
        }else if((int)this.corTipo == 2){
            Player.Instance.Azul += quantidade;
        }
        Destroy(this.gameObject);
    }
}
