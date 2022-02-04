using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ZInput.GetKeyDown("Mosht"))
        {
            print("Down");
        }
        if(ZInput.GetKeyPress("Mosht"))
        {
            print("Press");
        }
        if(ZInput.GetKeyUp("Mosht"))
        {
            print("up");
        }

        float X = (ZInput.GetAxis("X"));
        float Y = (ZInput.GetAxis("Y"));
        print($"X : {X} , Y : {Y}");
    }
}
