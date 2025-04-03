using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    private AudioSource audioSource;
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBgm(string path)                                     //�������ֲ���
    {
        AudioClip audioClip = Resources.Load<AudioClip>(path);
        audioSource.clip = audioClip;
        audioSource.Play();
        //AudioSource ��һ�����죬clip ��һ�� CD��
        //audioSource.clip = audioClip; �ǰ� CD �������졣
        //audioSource.Play(); �ǰ�������Ĳ��Ű�ť��
    }
    public void PlayMusic(string path,float volume = 1)                   //��Ч����
    {
        AudioClip audioClip = Resources.Load<AudioClip>(path);
        AudioSource.PlayClipAtPoint(audioClip,transform.position , volume);
        //��Ƶ��ָ��λ�ã�transform.position�����ţ����� 3D �ռ�Ч����������λ��Ӱ�죩���ʺ�ģ�ⳡ���е�������Դ��
        //������֧�֣�ÿ�ε��� PlayMusic ���ᴴ��һ���µ���ʱ AudioSource����������Чͬʱ���ţ������໥���ǡ�
    }
}
