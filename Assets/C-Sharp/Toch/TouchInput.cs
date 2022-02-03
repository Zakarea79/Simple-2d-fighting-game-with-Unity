using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Supernova;
using static ListButtonData;

public class TouchInput : MonoBehaviour
{
    private EventTrigger Et;
    [SerializeField] private string Keycode;
    [SerializeField] private Color PressColor = HexToRgb("919191");
    [SerializeField] private Color normalColor = HexToRgb("FFFFFF");
    [SerializeField] private Sprite PressButton , UpButton;

    private Image BaseColor;

    private void Awake() 
    {
        Button_Down.Add(Keycode , false);   
        Button_Up.Add(Keycode , false);   
        Button_Press.Add(Keycode , false);   
    }
    void Start()
    {
        BaseColor = GetComponent<Image>();
        Et = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener((data) => 
        {
            Button_Up[Keycode] = true;
            Button_Press[Keycode] = false;
            BaseColor.color = normalColor;
            BaseColor.sprite = UpButton;
        });

        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) => 
        {
            Button_Down[Keycode] = true;
            Button_Press[Keycode] = true;
            BaseColor.color = PressColor;
            BaseColor.sprite = PressButton;
        });

        Et.triggers.Add(entryDown);
        Et.triggers.Add(entryUp);
    }
}
