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

    public void PlayBgm(string path)                                     //背景音乐播放
    {
        AudioClip audioClip = Resources.Load<AudioClip>(path);
        audioSource.clip = audioClip;
        audioSource.Play();
        //AudioSource 是一个音响，clip 是一张 CD。
        //audioSource.clip = audioClip; 是把 CD 放入音响。
        //audioSource.Play(); 是按下音响的播放按钮。
    }
    public void PlayMusic(string path,float volume = 1)                   //音效播放
    {
        AudioClip audioClip = Resources.Load<AudioClip>(path);
        AudioSource.PlayClipAtPoint(audioClip,transform.position , volume);
        //音频在指定位置（transform.position）播放，具有 3D 空间效果（受听者位置影响），适合模拟场景中的声音来源。
        //多声道支持：每次调用 PlayMusic 都会创建一个新的临时 AudioSource，允许多个音效同时播放，不会相互覆盖。
    }
}
