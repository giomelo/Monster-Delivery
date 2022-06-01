using ShaderTeste.GrassShader;
using UnityEditor;
using UnityEngine;

namespace Editor
{
   [CustomEditor(typeof(Grass))]
   public class GrassEditor : UnityEditor.Editor
   {
      public override void OnInspectorGUI()
      {
         base.OnInspectorGUI();

         var grass = (Grass) target;

         if (GUILayout.Button("Generate Grass"))
         {
            grass.CreateGrass();
         }
      }
   }
}
