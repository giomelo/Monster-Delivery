using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform[] alvo;
    private int alvoEscolhido;
    public void Camera()
    {
        alvoEscolhido = (int) Player.jogador.protagonistas;
    }

    private void FixedUpdate()
    {
        SegueCamera();
    }

    private void SegueCamera(){
        
        Vector3 posicaoAlvo;
        if(alvo[alvoEscolhido].GetComponent<CharacterController>().isGrounded){
            posicaoAlvo = new Vector3(this.transform.position.x,
                                            alvo[alvoEscolhido].position.y + 9.75f,
                                            alvo[alvoEscolhido].position.z -18.5f);
        }else{
            posicaoAlvo = new Vector3(this.transform.position.x,
                                            this.transform.position.y,
                                            alvo[alvoEscolhido].position.z -18.5F);
        }
        this.transform.position = posicaoAlvo;
    }
}
