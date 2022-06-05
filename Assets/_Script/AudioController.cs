using UnityEngine;

namespace _Script
{
    public class AudioController : MonoSingleton<AudioController>
    {
        [SerializeField] private AudioClip encomendaCerta, encomenda, encomendaErrada;

        [SerializeField] private AudioSource music;
        // Start is called before the first frame update

        public void Music()
        {
            music.Play();
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
