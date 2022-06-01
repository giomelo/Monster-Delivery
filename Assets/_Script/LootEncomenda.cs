using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootEncomenda : MonoBehaviour
{
   public CorTipo corTipo;
   public int quantidade;
    private void OnTriggerEnter(Collider other) {
        if((int)this.corTipo == 0){
            Player.jogador.Amarelo += quantidade;
        }else if((int)this.corTipo == 1){
            Player.jogador.Vermelho += quantidade;
        }else if((int)this.corTipo == 2){
            Player.jogador.Azul += quantidade;
        }
        Destroy(this.gameObject);
    }
}
