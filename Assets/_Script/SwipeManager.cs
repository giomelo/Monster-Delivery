using UnityEngine;

namespace _Script
{
    public class SwipeManager : MonoBehaviour
    {
        public static bool tap, swipeLeft, swipeRight, swipeUp, swipeDown, tapLeft, tapRight,doubletap;
        private bool isDraging = false;
        private Vector2 startTouch, swipeDelta;
        private Touch touch;
        private Vector2 beginTouchPosition, endTouchPosition;
        public float MaxDubbleTapTime = 1;
        float NewTime;
        [SerializeField]
        float valorAcelerator;
        float valorAceleratorAtual;
        float valorDesejado;
        [SerializeField]
        DeviceOrientation deviceOrientationInicial;
        [SerializeField]
        //DeviceOrientation deviceOrientation;
        public static bool rodou = false;
        void Start()
        {
            valorAceleratorAtual = Input.acceleration.z * 10;
            Input.gyro.enabled = true;
            //deviceOrientationInicial = Input.deviceOrientation;
        }

        private void Update() {
            tap = swipeDown = swipeUp = swipeLeft = swipeRight = tapLeft = tapRight = doubletap = rodou = false;
            //deviceOrientation = Input.deviceOrientation;
            /*if(deviceOrientation == DeviceOrientation.FaceDown){
            if(!umavez)
            Debug.Log("Baixo");
            rodou = true;
            umavez = true;
        }else{
            Player.Instance.trocouUmaVez = false;
            rodou = false;
            umavez = false;
        }*/
        
            #region Mobile Input
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    tap = true;
                    isDraging = true;
                    startTouch = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    isDraging = false;
                    Reset();
                }
            }
            #endregion
            //Calculate the distance
            swipeDelta = Vector2.zero;
            if (isDraging)
            {
                if (Input.touches.Length < 0)
                    swipeDelta = Input.touches[0].position - startTouch;
                else if (Input.GetMouseButton(0))
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
            if(swipeDelta.magnitude > 4){
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                if(Mathf.Abs(x) > Mathf.Abs(y)){
                    //Left or Right

                    if(x < 0){
                        swipeLeft = true;
                    }else{
                        swipeRight = true;
                    }
                    
                }else{
                    //Up or Down

                    if(y < 0){
                        swipeDown = true;
                    }else{
                        swipeUp = true;
                    }
                }
                Reset();
            }
            if(swipeDown || swipeUp || swipeLeft || swipeRight){
                tapLeft = false;
                tapRight = false;
            }
            if(Input.touchCount > 0){
                touch = Input.GetTouch(0);
                switch(touch.phase){
                    case TouchPhase.Began:
                        beginTouchPosition = touch.position;
                        break;
                    case TouchPhase.Ended:
                        endTouchPosition = touch.position;
                        if(beginTouchPosition == endTouchPosition){
                            if(touch.position.x > Screen.width/2){
                                tapRight = true;
                            }else{
                                tapLeft = true;
                            }
                        }else{
                            tapRight = false;
                            tapLeft = false;
                        }
                        break;
                }
            }
            /* if (Input.touchCount > 0) {
             Touch touch = Input.GetTouch (0);
             if(touch.phase == TouchPhase.Ended) {
                 TapCount += 1;
                 Debug.Log("tocou");
             }
             if (TapCount == 1){
                 NewTime = Time.time + MaxDubbleTapTime;
             }else if(TapCount == 2 && Time.time <= NewTime){
                 doubletap = true;
                 Debug.Log("Dubble tap"); 
                 TapCount = 0;
             }
         }
         if (Time.time > NewTime) {
             TapCount = 0;
         } */
        }
    
        private void Reset() {
            startTouch = swipeDelta = Vector2.zero;
            isDraging = false;
        }
    }
}
