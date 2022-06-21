using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Script
{
    public class Pista : MonoBehaviour
    {
        public ModuloPista[] listaModulos;
        public int tamanhoPista;
        [SerializeField]
        private List<ModuloPista> modulosFacil = new List<ModuloPista>();
        [SerializeField]
        private List<ModuloPista> modulosMedio = new List<ModuloPista>();
        [SerializeField]
        private List<ModuloPista> modulosDificil = new List<ModuloPista>();
        [SerializeField]
        private List<ModuloPista> modulosNeutros = new List<ModuloPista>();
        public List<ModuloPista> modulosDestruidosFacil = new List<ModuloPista>();
        public List<ModuloPista> modulosDestruidosMedio = new List<ModuloPista>();
        public List<ModuloPista> modulosDestruidosDificil = new List<ModuloPista>();
        public List<ModuloPista> modulosDestruidosNeutros = new List<ModuloPista>();
    
        [SerializeField]
        private int contador = 2;
        Transform conector;
        static public Pista pista;
        [SerializeField]
        private int contadorDeModulos = 0;
        private List<char> dificuldade = new List<char>(){'F', 'N', 'M', 'N', 'F', 'N', 'M', 'N', 'D', 'N', 'F','N', 'D', 'N',
            'M','N', 'F', 'N', 'D', 'N', 'M', 'N', 'D'};
        [SerializeField]
        private int indice = 0;
        [SerializeField]
        private int zona = 1;
        public Material skyZona1, skyZona3;
        private bool skyChanged = false;

        private const int MODULES_NEXT_ZONE = 33;

        private void Start()
        {
            zona = 1;
            pista = this;
            skyZona1.SetFloat("_Blend", 0);
            GeraPista();
        }

        private void FixedUpdate()
        {
            if (!skyChanged) return;
            RenderSettings.skybox.SetFloat("_Blend" , Mathf.Lerp(RenderSettings.skybox.GetFloat("_Blend"),1,Time.deltaTime / 2));
            if (RenderSettings.skybox.GetFloat("_Blend") == 1)
            {
                skyChanged = false;
            }
        }

        public void GeraPista()
        {
            ModuloPista copia = Instantiate(listaModulos[0]);
            conector = copia.conector;
            for(int i = 1; i<tamanhoPista; i++)
            {
                int moduloSorteado = Random.Range(1,listaModulos.Length -1);
                copia = Instantiate(listaModulos[moduloSorteado]);
                //ultimo = copia;
                copia.transform.position = conector.position;
                conector = copia.conector;
                contadorDeModulos++;
            }
            Checar();
        }
        public void ContinuarPista()
        {
            //Checar();
            List<ModuloPista> listaEscolhida = new List<ModuloPista>();
            List<ModuloPista> listaEscolhida2 = new List<ModuloPista>();
            switch(dificuldade[indice])
            {
                case 'F': listaEscolhida = modulosDestruidosFacil;
                    listaEscolhida2 = modulosFacil;
                    break;
                case 'M': listaEscolhida = modulosDestruidosMedio;
                    listaEscolhida2 = modulosMedio;
                    break;
                case 'D': listaEscolhida = modulosDestruidosDificil;
                    listaEscolhida2 = modulosDificil; 
                    break;
                case 'N': listaEscolhida = modulosDestruidosNeutros;
                    listaEscolhida2 = modulosNeutros;
                    break;
            }
            if(listaEscolhida.Count > contador)
            {
                for(int i =0; i < contador; i++){
                    //pegar um dos modulos e spawnar dnvo
                    ModuloPista modulo = listaEscolhida[Random.Range(0, listaEscolhida.Count)];
                    if(modulo.nivelPista != NivelPista.Outros)
                    {
                        foreach (Casa item in modulo.casas)
                        {
                            item.ReeiniciarCasa();
                        }
                    }
                    modulo.ChecarZona();
                    modulo.transform.position = conector.position;
                    modulo.gameObject.SetActive(true);
                    if(modulo.nivelPista != NivelPista.Outros)
                    {
                        InstanciarItens.instanciarItens.Objetos(modulo.transform);
                    }
                    conector = modulo.conector;
                    //remover da lista o modulo
                    listaEscolhida2.Add(modulo);
                    listaEscolhida.Remove(modulo);
                    contadorDeModulos++;
                    Checar();
                }
            }else{
                ModuloPista modulo = listaEscolhida2[Random.Range(0, listaEscolhida2.Count)];
                ModuloPista copia = Instantiate(modulo, conector.position, modulo.transform.rotation);
                copia.gameObject.SetActive(true);
                if(copia.nivelPista != NivelPista.Outros)
                {
                    foreach (Casa item in copia.casas)
                    {
                        item.ReeiniciarCasa();
                    }
                }
                copia.ChecarZona();
                //modulo.transform.position = conector.position;
                conector = copia.conector;
                listaEscolhida2.Remove(modulo);
                contadorDeModulos++;
                Checar();
            }

        }
        void Checar()
        {
            if (indice < dificuldade.Count -1)
                indice++;
            else
                indice = 0;
            if (contadorDeModulos != tamanhoPista + MODULES_NEXT_ZONE) return;
            switch (zona)
            {
                case 1:
                    Controlador.controlador.zona1 = false;
                    Controlador.controlador.zona2 = true;
                    // RenderSettings.skybox.SetFloat("_Blend" , Mathf.Lerp(skyZona2.GetFloat("_Blend"),1,Time.deltaTime));
                    skyChanged = true;
                    zona++;
                    break;
                case 2:
                    Controlador.controlador.zona2 = false;
                    Controlador.controlador.zona3 = true;
                    RenderSettings.skybox = skyZona3;
                    skyChanged = true;
                    zona++;
                    break;
            }
            indice = 0;
            contadorDeModulos = 1;
        }
    }
}
