using UnityEngine;

namespace _Script
{
    public class LoaderCallBack : MonoBehaviour
    {
        private bool isFirstUpdate = true;
        private void Start() {
            Time.timeScale = 1f;
        }
        private void Update(){
            if(isFirstUpdate){
                isFirstUpdate = false;
                Loader.LoaderCallBack();
            }
        }
    }
}
