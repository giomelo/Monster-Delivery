using UnityEngine;

namespace _Script
{
    public class ModuloPista : MonoBehaviour
    {
        public Transform conector;
        public NivelPista nivelPista;
        public GameObject[] zonasObj = new GameObject[3];
        public InstanciarItens[] objects;
        [SerializeField]
        public Casa[] casas;

        public void ChecarZona()
        {
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
        public void LimparObjetos()
        {
            Component[] item = transform.GetComponentsInChildren<Item>();
            for(int i = 0; i < item.Length; i++)
            {
                if(item[i] != null)
                {
                    Destroy(item[i].gameObject);
                }
            }
            Component[] loot = transform.GetComponentsInChildren<LootEncomenda>();
            for(int i = 0; i < loot.Length; i++)
            {
                if(loot[i] != null)
                {
                    Destroy(loot[i].gameObject);
                }
            }
        }
        
    }
}
