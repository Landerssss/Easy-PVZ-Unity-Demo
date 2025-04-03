using UnityEngine;
using DG.Tweening;

public class Sun : MonoBehaviour
{
    public float moveDuration = 1;
    public int   point = 50;

    public void LinearTo(Vector3 targerPosition)   //�ƶ�
    {
        transform.DOMove(targerPosition, moveDuration);
    }
    public void JumpTo(Vector3 targetPosition)    //�����浯һ��
    {
        Vector3 centerPosition = (transform.position + targetPosition)/2;
        float distance = Vector3.Distance( transform.position, targetPosition );
        centerPosition.y += (distance / 2);
        transform.DOPath(new Vector3[] { transform.position, centerPosition, targetPosition },
            moveDuration, PathType.CatmullRom).SetEase(Ease.OutQuad);
    }
    public void OnMouseDown()
    {
        transform.DOMove(SunManager.instance.GetSunPointTextPos(), moveDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete( 
            ()=>
        { 
            Destroy(this.gameObject);
            SunManager.instance.AddSun(point);//��������ټ�����
        } 
        );

    }
}
