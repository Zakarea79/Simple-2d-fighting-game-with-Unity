using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCracter : MonoBehaviour
{
    private GameObject sprite;
    public void Choose(GameObject cracter)
    {
        if (sprite != null)
        {
            sprite.SetActive(false);
        }
        cracter.SetActive(true);
        sprite = cracter;
    }
    public void Ready()
    {

    }
}
