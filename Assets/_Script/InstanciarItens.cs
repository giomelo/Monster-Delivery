using System;
using UnityEngine;
using Random = UnityEngine.Random;

enum OptionToInstantiateState
{
    Coin,
    Loot,
    PowerUp
}

namespace _Script
{
    public class InstanciarItens : MonoBehaviour
    {
        [SerializeField] private GameObject moedaPrefab;
        [SerializeField] 
        private GameObject[] loots;
        [SerializeField] 
        private GameObject[] powerUps;
        [SerializeField] 
        private OptionToInstantiateState optionToInstantiate;

        private void Start()
        {
            Objetos(transform.parent);
        }
        public void Objetos(Transform modulo)
        {
            switch (optionToInstantiate)
            {
                case OptionToInstantiateState.Coin:
                    GameObject moeda = Instantiate(moedaPrefab);
                    Instanciar(moeda, transform.position, modulo);
                    break;
                case OptionToInstantiateState.Loot:
                    GameObject loot = Instantiate(loots[Random.Range(0,loots.Length)]);
                    Instanciar(loot, transform.position, modulo);
                    break;
                case OptionToInstantiateState.PowerUp:
                    GameObject powerUp = Instantiate(powerUps[Random.Range(0,loots.Length)]);
                    Instanciar(powerUp, transform.position, modulo);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        void Instanciar(GameObject objeto, Vector3 pos, Transform modulo)
        {
            GameObject obj = Instantiate(objeto);
            objeto.transform.parent = modulo;
            objeto.transform.localPosition = pos;
        }
    }
}
