using System;
using UnityEngine;

namespace _Script
{
    public class ColliderHouse : MonoBehaviour
    {
        [SerializeField]
        private GameObject paricle;

        private void OnTriggerEnter(Collider other)
        {
            paricle.SetActive(true);
        }
    }
    
    
}
