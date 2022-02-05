using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
        topcanvas.SetActive(false);
        SelectFirsButton(firstbtn);
    }
    public void Ready(string scenename)
    {
        SceneManager.LoadScene(scenename);
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
