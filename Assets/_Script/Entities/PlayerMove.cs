using System.Collections;
using UnityEngine;

namespace _Script.Entities
{
    public class PlayerMove : MonoBehaviour
    {
        public int desiredLane = 1;
        [SerializeField]
        private float laneDistance = 2.5f;
       
        private float yVelocity = 0.0f;
        private bool sliding = false;
        private Vector3 Scala;
        private void Direita()
        {
            desiredLane++;
            if(desiredLane == 3){
                desiredLane = 2;
            }
        }
        private void Esquerda()
    {
        desiredLane--;
        if(desiredLane == -1){
            desiredLane = 0;
        }
    }
    public void Movimenta_X()
    {
        //CONTROLAR OS CAMINHOS
        #if UNITY_EDITOR
            
                if(Input.GetButtonDown("Direita"))
                {
                    Direita();
                    
                }else
                if(Input.GetButtonDown("Esquerda"))
                {
                    Esquerda();
                }
       #else  
        if(SwipeManager.swipeRight ){
                    Direita();
                }else
                if(SwipeManager.swipeLeft){
                   Esquerda();
                }
       #endif
        
        //MOVER
        
            Vector3 targetPosition = Player.Instance.personagem[Player.Instance.personagemEscolhido].transform.position.z * Player.Instance.personagem[Player.Instance.personagemEscolhido].transform.forward + 
                                     Player.Instance.personagem[Player.Instance.personagemEscolhido].transform.position.y * Player.Instance.personagem[Player.Instance.personagemEscolhido].transform.up;
            switch (desiredLane)
            {
                case 0:
                    targetPosition += Vector3.left * laneDistance;
                    break;
                case 2:
                    targetPosition += Vector3.right * laneDistance;
                    break;
            }
            Vector3 currentPos = Player.Instance.personagem[Player.Instance.personagemEscolhido].transform.position;
            
            Player.Instance.personagem[Player.Instance.personagemEscolhido].transform.position =  Vector3.Lerp(currentPos, targetPosition, 0.5f);
        }

        public void Movimenta(){
            /*if(character.isGrounded){
                personagem[personagemEscolhido].dirtParticles.Play();
            }else {
                personagem[personagemEscolhido].dirtParticles.Stop();
            }*/
            Vector3 moveDirection;
            moveDirection = new Vector3(0, 0, 1);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= Player.Instance.personagem[Player.Instance.personagemEscolhido].velocidade;
            
            #if UNITY_EDITOR
            if(Input.GetButtonDown("Jump") && Player.Instance.character.isGrounded /*&& !sliding*/)
            {
                Player.Instance.personagem[Player.Instance.personagemEscolhido].GetComponent<Animator>().SetTrigger("podePular");
                moveDirection.z *= 3;
                yVelocity = Player.Instance.personagem[Player.Instance.personagemEscolhido].pulo;
            }
            if(!Player.Instance.character.isGrounded && Input.GetKeyDown(KeyCode.S)){
                    yVelocity -= Player.Instance.personagem[Player.Instance.personagemEscolhido].descer;
            }
         
            #else
             if(SwipeManager.swipeUp && character.isGrounded /*&& !sliding*/){
                personagem[personagemEscolhido].GetComponent<Animator>().SetTrigger("podePular");
                moveDirection.z *= 5;
                yVelocity = personagem[personagemEscolhido].pulo;
            }
            if(!character.isGrounded && SwipeManager.swipeDown){
                yVelocity -= personagem[personagemEscolhido].descer;
            }
            #endif
            yVelocity -= Player.Instance.personagem[Player.Instance.personagemEscolhido].gravidade * Time.deltaTime;
            moveDirection.y = yVelocity;
            Player.Instance.character.Move(moveDirection * Time.deltaTime);
        }

        private void Slide(){
            if(Player.Instance.character.isGrounded && Input.GetKeyDown(KeyCode.S) /*SwipeManager.swipeDown*/ && !sliding){
                Scala = Player.Instance.personagem[Player.Instance.personagemEscolhido].transform.localScale;
                var newScale = new Vector3(1.2f, 0.66f,1.2f);
                Player.Instance.personagem[Player.Instance.personagemEscolhido].transform.localScale = new Vector3(Scala.x, 1*Scala.y/2, Scala.z) ;
                sliding = true;
                StartCoroutine(VoltarSlide());   
            }
        }

        private IEnumerator VoltarSlide()
        {
             yield return new WaitForSeconds(Player.Instance.personagem[Player.Instance.personagemEscolhido].slideLength);
            //Vector3 scale = new Vector3(1.2f, 1.2f,1.2f);
            Player.Instance.personagem[Player.Instance.personagemEscolhido].transform.localScale = Scala;
            sliding = false;
        }
    
    }
}