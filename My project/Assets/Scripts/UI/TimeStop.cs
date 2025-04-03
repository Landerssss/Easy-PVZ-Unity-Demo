using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Button slowTimeButton;            //TODO:游戏未开始禁用
    public KeyCode keyBoard3 = KeyCode.Alpha3;
    public float slowTimeScale = 0.3f;     // 减慢到 3/10 的时间流速
    public float normalTimeScale = 1f;     // 正常时间流速
    private bool isSlowing = false;        // 标记是否正在减速
    void Start()
    {
        slowTimeButton.onClick.AddListener(StartSlowTime);        // 绑定按钮的点击事件
    }
    private void Update()
    {
        if(Input.GetKeyDown(keyBoard3))
        {
            slowTimeButton.onClick.Invoke();              //按键3触发时停
        }
    }
    void StartSlowTime()
    {
        if (!isSlowing)
        {
            isSlowing = true;
            Time.timeScale = slowTimeScale;
            AudioSource[] allAudio = FindObjectsOfType<AudioSource>();
            foreach (AudioSource audio in allAudio) audio.pitch = 0.3f;
        }
        else RestoreTime();
    }
    void RestoreTime()
    {
        isSlowing = false;
        Time.timeScale = normalTimeScale;
        AudioSource[] allAudio = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in allAudio) audio.pitch = 1f;
    }
}
