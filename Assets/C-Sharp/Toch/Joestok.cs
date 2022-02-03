using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using static ListButtonData;
using static Supernova;

public class Joestok : MonoBehaviour
{
    private EventTrigger Et;
    [SerializeField] private string AxisName_H , AxisName_V;
    [SerializeField] private Color PressColor = HexToRgb("919191");
    [SerializeField] private Color normalColor = HexToRgb("FFFFFF");
    [SerializeField] private Sprite PressButton , UpButton;
    private Image BaseColor;
    
    private void Awake() 
    {
        if(AxisName_H != "")
            Axis.Add(AxisName_H , 0);
        if(AxisName_V != "")
            Axis.Add(AxisName_V , 0);
    }

    void Start()
    {
        BaseColor = GetComponent<Image>();
        Et = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((dtat) => 
        {
            DragEvent((PointerEventData)dtat);
        });

        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener((data) => 
        {
            BaseColor.color = normalColor;
            BaseColor.sprite = UpButton;
            transform.position = transform.parent.position;
            Axis[AxisName_H] = 0;
            Axis[AxisName_V] = 0;
        });

        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) => 
        {
            BaseColor.color = PressColor;
            BaseColor.sprite = PressButton;
        });

        Et.triggers.Add(entry);
        Et.triggers.Add(entryDown);
        Et.triggers.Add(entryUp);
    }

    public void DragEvent(PointerEventData data)
    {
        transform.position = data.position;
        float X = Mathf.Clamp(transform.localPosition.x , -80 , 80);
        float Y = Mathf.Clamp(transform.localPosition.y , -80 , 80);
        transform.localPosition = new Vector2(X , Y);
        if(AxisName_H != "")
            Axis[AxisName_H] = Vector3.Normalize(transform.localPosition).x;
        if(AxisName_V != "")
            Axis[AxisName_V] = Vector3.Normalize(transform.localPosition).y;
    }
}
