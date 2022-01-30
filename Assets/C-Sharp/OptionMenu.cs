using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class OptionMenu : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropDown;
    public Dropdown vsyncDropDown;
    public Dropdown texturequalityDropDown;
    public Dropdown antialiasingDropDown;
    public Slider musicSlider;
    public Button applyBtn;

    //اصلی عوض بشهausio source این باید با 
    public AudioSource audioSource;
    //

    public Resolution[] resolutions;//dropdown برای پر کردن 
    public GameSetting gameSetting;//برای ذخیره اطلاعات در فایل جیسون


    private void OnEnable()
    {
        gameSetting = new GameSetting();

        resolutions = Screen.resolutions;//مقدار دهی آرایه با رزولوشن های ممکن
        foreach (var item in resolutions)
        {//dropdown مقدار دهی آیتم های 
            resolutionDropDown.options.Add(new Dropdown.OptionData(item.ToString()));
        }

        //اظافه کردن ایونت های مزبوط به هر قسمت منو
        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullScreenChange(); });
        texturequalityDropDown.onValueChanged.AddListener(delegate { OnTextureQalityChange(); });
        resolutionDropDown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        vsyncDropDown.onValueChanged.AddListener(delegate { OnVsyncChange(); });
        antialiasingDropDown.onValueChanged.AddListener(delegate { OnAntiAliasingChange(); });
        musicSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        applyBtn.onClick.AddListener(delegate { ApplyBtn_Click(); });


        LoadSetting();
    }


    public void OnFullScreenChange()
    {
        gameSetting.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropDown.value].width, resolutions[resolutionDropDown.value].height, Screen.fullScreen);
        gameSetting.resolutionindex = resolutionDropDown.value;
    }

    public void OnVsyncChange()
    {
        gameSetting.vsync = QualitySettings.vSyncCount = vsyncDropDown.value;
    }

    public void OnTextureQalityChange()
    {
        gameSetting.texturequality = QualitySettings.masterTextureLimit = texturequalityDropDown.value;
    }

    public void OnMusicVolumeChange()
    {
        gameSetting.musicvolume = audioSource.volume = musicSlider.value;
    }

    public void OnAntiAliasingChange()
    {
        gameSetting.antialiasing = QualitySettings.antiAliasing = (int)Mathf.Pow(2, antialiasingDropDown.value);
    }

    public void ApplyBtn_Click()
    {
        SaveSetting();
    }

    public void SaveSetting()//ذخیره اطلاعات در فایل جیسون
    {
        string jsondata = JsonUtility.ToJson(gameSetting,true);
        File.WriteAllText(Application.persistentDataPath + "/gameSetting.json", jsondata);
    }

    public void LoadSetting()//وارد کردن اطلاعات از فایل جیسون
    {
        gameSetting = JsonUtility.FromJson<GameSetting>(File.ReadAllText(Application.persistentDataPath + "/gameSetting.json"));
        fullscreenToggle.isOn = gameSetting.fullscreen;
        resolutionDropDown.value = gameSetting.resolutionindex;
        vsyncDropDown.value = gameSetting.vsync;
        texturequalityDropDown.value = gameSetting.texturequality;
        antialiasingDropDown.value = gameSetting.antialiasing;
        musicSlider.value = gameSetting.musicvolume;

        Screen.fullScreen = gameSetting.fullscreen;

        resolutionDropDown.RefreshShownValue();
    }
}
