using System;
using UnityEngine;

public class ThreeTwoOne : MonoBehaviour
{
    private Action OnComplete;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;       //һ��ʼ����
    } 
    public void Show321(Action OnComplete)
    {
        this.OnComplete = OnComplete;       //On321UIComplete()�ķ�������this.OnComplete����ȴ�Inovke����
        animator.enabled = true;
    }
    void OnShowComplete()   //����Ƿ񲥷���ϣ����������Ϸ��ʼ
    {
        OnComplete?.Invoke();
        //Invoke() ����ִ��ί�У���������ɺ�OnComplete��Ϊ��ִ�к�������ʼִ��  this.OnComplete 
       
    }
}
