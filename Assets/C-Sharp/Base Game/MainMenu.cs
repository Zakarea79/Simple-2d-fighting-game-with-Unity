﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private GameObject topcanvas;
    public Button firstbtn;
    public void Click(GameObject canvas)
    {
        //فعال کردن بخشی که کلیک شده
        canvas.SetActive(true);
        //canvas تنظیم بالا ترین
        topcanvas = canvas;
    }
    private void Start()
    {
        //انتخاب اولین دکمه تا بتوان با دکمه کیبرد انتخاب رو عوض کرد
        firstbtn.Select();
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
