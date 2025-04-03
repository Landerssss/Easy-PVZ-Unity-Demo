using UnityEngine;

public class FailUI : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        Hide();
    }
    void Hide()
    {
        animator.enabled = false;
    }
    public void ShowFail()
    {
        animator.enabled = true;
    }

}
