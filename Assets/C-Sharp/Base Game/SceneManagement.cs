using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//پیدا کردن چهار گوشه کمرا (استفاده شده)
//نیاز به تعققیرات
public class SceneManagement : MonoBehaviour
{
    public Vector2 cornerCamerMain;
    public Transform Player1;
    public Transform CornerShow , CornerShowDown ,medile;
    void Update()
    {
        Vector2 cornerCamer = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));
        CornerShow.position = cornerCamer;
        CornerShowDown.position = new Vector2(cornerCamer.x , - cornerCamer.y);

         float x = (CornerShow.position.x + CornerShowDown.position.x) / 2;
         float y = (CornerShow.position.y + CornerShowDown.position.y) / 2;
         cornerCamerMain = new Vector2(x , y);
         medile.position = cornerCamerMain;
    }
}
