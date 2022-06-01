using System.Collections.Generic;
using UnityEngine;

namespace ShaderTeste.GrassShader
{
    public class Grass : MonoBehaviour
    {
        [Range(1,1000)]
        public int amount;
        public Material material;

        private Vector3[] points;
        private ComputeBuffer[] buffer = new ComputeBuffer[100];
        [Range(0,100)]
        public float rangeGrass;
        [SerializeField]
        private int index;

        // Start is called before the first frame update
        private void Start()
        {
            CreateGrass();
        }

        private void OnRenderObject() {
            material.SetPass(0);
            Graphics.DrawProceduralNow(MeshTopology.Points, buffer[index].count, 1);
        }

        private void OnDestroy() {
            buffer[index].Dispose();
        }

        public void CreateGrass()
        {
            Debug.Log("grama");
            points = new Vector3[amount];
            buffer[index] = new ComputeBuffer(amount,12);
            for (int i = 0; i < amount; i++)
            {
                points[i] = new Vector3(Random.Range(-rangeGrass,rangeGrass),0,Random.Range(-rangeGrass,rangeGrass)) + transform.position;
            }

            buffer[index].SetData(points);
            material.SetBuffer("buffer",buffer[index]);
        }
    }
}
