using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    private GameObject topcanvas;
    public Button firstbtn;

    private void Start()
    {
        SelectFirsButton(firstbtn);
    }
    public void Click(GameObject canvas)
    {
        canvas.SetActive(true);
        topcanvas = canvas;
    }
    public void SelectFirsButton(Button btn)
    {
        btn.Select();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Back()
    {
        SelectFirsButton(firstbtn);
        EventSystem.current.currentSelectedGameObject.GetComponent<Animator>().Play("Normal");  
        topcanvas.SetActive(false);
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
