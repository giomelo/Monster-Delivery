using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class Loader 
{
    public enum Scene{
        GameScene,
        MainMenu,
        Loading,
        Tutorial
    }
  private static Action onLoaderCallBack;
   public static void Load(Scene scene){
       onLoaderCallBack = () =>{
       SceneManager.LoadSceneAsync(scene.ToString());
       };
       SceneManager.LoadSceneAsync(Scene.Loading.ToString());
   }

   public static void LoaderCallBack(){
       if(onLoaderCallBack != null){
           onLoaderCallBack();
           onLoaderCallBack = null;
       }
   }
}
