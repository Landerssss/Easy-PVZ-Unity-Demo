using System.Threading;
using TMPro;
using UnityEngine;

public class SunFlower : Plants
{
    public  float produceDuration = 3;         // 初次生产只需要3s
    private float produceTimer    = 0f;
    public GameObject SunPrefab;
    private Animator animator;

    public float jumpMinDistance = 0.3f;
    public float jumpMaxDistance = 2;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    protected override void EnableUpdate()
    {
        produceTimer += Time.deltaTime;
        if (produceTimer > produceDuration)
        {
            animator.SetTrigger("IsGrowing");
            produceDuration = 25;
            produceTimer = 0f;
        }

    }
    public void ProduceSun()
    {
        //TODO:算法改进
        //TODO:阳光生成判定有问题                               // 触发动画
        GameObject go =  GameObject.Instantiate(SunPrefab, transform.position, Quaternion.identity);
        float distance = Random.Range(jumpMinDistance, jumpMaxDistance);
        distance = Random.Range(0, 2) < 1 ? -distance : distance;
        Vector3 position = transform.position;
        position.x += distance; 
        go.GetComponent<Sun>().JumpTo(position);
    }

}
