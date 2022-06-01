using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    public float velocidade;
    public float pulo;
    public Protagonistas nome;
    //[HideInInspector]
    public float gravidade = 0.2f;
    [HideInInspector]
    public float descer = 15;
    public float slideLength = 3;
    public ParticleSystem dirtParticles;

}
