using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChooseCracter : MonoBehaviour
{
    private GameObject sprite;
    private void Start()
    {
        PlayerPrefs.SetString("EnemyCracter", "Dadghah"); 

        Debug.Log(PlayerPrefs.GetString("EnemyCracter")); 
        Enemy(PlayerPrefs.GetString("EnemyCracter"));
    }
    void Enemy(string name)
    {
        GameObject.Find(name).gameObject.GetComponent<Image>().enabled = true;
    }
    public void Choose(GameObject cracter)
    {
        if (sprite != null)
        {
            sprite.SetActive(false);
        }
        cracter.SetActive(true);
        sprite = cracter;
    }
    public void Ready(string scenename)
    {
        PlayerPrefs.SetString("PlayerCracter",sprite.name);
    }
}
