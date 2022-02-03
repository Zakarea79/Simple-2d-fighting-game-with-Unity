using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ListButtonData;

public static class ListButtonData
{
    public static Dictionary<string , bool> Button_Press = new Dictionary<string, bool>();
    public static Dictionary<string , bool> Button_Down = new Dictionary<string, bool>();
    public static Dictionary<string , bool> Button_Up = new Dictionary<string, bool>();
    public static Dictionary<string , float> Axis = new Dictionary<string, float>();
}

public static class ZInput
{
    
    public static bool GetKeyDown(string key)
    {
        bool temp = Button_Down[key];
        Button_Down[key] = false;
        return temp;
    }

    public static bool GetKeyUp(string key)
    {
        bool temp = Button_Up[key];
        Button_Up[key] = false;
        return temp;
    }

    public static bool GetKeyPress(string key)
    {
        return Button_Press[key];
    }

    public static float GetAxis(string NameAxis)
    {
        return Axis[NameAxis];
    }
}