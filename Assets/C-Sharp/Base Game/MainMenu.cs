using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameObject topcanvas;
    public void Click(GameObject canvas)
    {
        //فعال کردن بخشی که کلیک شده
        canvas.SetActive(true);
        //canvas تنظیم بالا ترین
        topcanvas = canvas;
    }
    void Update()
    {
        if (topcanvas != null)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                topcanvas.SetActive(false);
            }
        }
    }
}
