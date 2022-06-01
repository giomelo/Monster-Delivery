using UnityEngine;

namespace _Script
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController audioController;
        [SerializeField] private AudioClip encomendaCerta, encomenda, encomendaErrada;
        // Start is called before the first frame update
        private void Awake()
        {
            audioController = this;
        }

        public void AcertoEncomenda()
        {
            AudioSource.PlayClipAtPoint(encomendaCerta, Player.jogador.personagem[Player.jogador.personagemEscolhido].transform.position, 300);
        }
        public void ErroEncomenda()
        {
            AudioSource.PlayClipAtPoint(encomendaErrada, Player.jogador.personagem[Player.jogador.personagemEscolhido].transform.position, 100);
        }
        public void Encomenda()
        {
            AudioSource.PlayClipAtPoint(encomenda, Player.jogador.personagem[Player.jogador.personagemEscolhido].transform.position, 100);
        }
    }
}
