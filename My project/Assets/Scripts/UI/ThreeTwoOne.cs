using System;
using UnityEngine;

public class ThreeTwoOne : MonoBehaviour
{
    private Action OnComplete;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;       //一开始禁用
    } 
    public void Show321(Action OnComplete)
    {
        this.OnComplete = OnComplete;       //On321UIComplete()的方法存入this.OnComplete里面等待Inovke调用
        animator.enabled = true;
    }
    void OnShowComplete()   //检测是否播放完毕，播放完毕游戏开始
    {
        OnComplete?.Invoke();
        //Invoke() 用来执行委托，当动画完成后，OnComplete不为空执行函数，开始执行  this.OnComplete 
       
    }
}
