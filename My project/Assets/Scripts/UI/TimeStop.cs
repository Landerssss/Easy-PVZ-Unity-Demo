using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Button slowTimeButton;            //TODO:��Ϸδ��ʼ����
    public KeyCode keyBoard3 = KeyCode.Alpha3;
    public float slowTimeScale = 0.3f;     // ������ 3/10 ��ʱ������
    public float normalTimeScale = 1f;     // ����ʱ������
    private bool isSlowing = false;        // ����Ƿ����ڼ���
    void Start()
    {
        slowTimeButton.onClick.AddListener(StartSlowTime);        // �󶨰�ť�ĵ���¼�
    }
    private void Update()
    {
        if(Input.GetKeyDown(keyBoard3))
        {
            slowTimeButton.onClick.Invoke();              //����3����ʱͣ
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
