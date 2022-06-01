using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruirPS : MonoBehaviour
{
    [SerializeField]
   private ParticleSystem particula;
 
  private void Start() {
      GameObject.Destroy(gameObject, particula.main.duration);
  }
}
