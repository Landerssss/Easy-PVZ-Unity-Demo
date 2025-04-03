using UnityEngine;

enum PlantState
{
    Disable,
    Enable
}


public class Plants : MonoBehaviour
{
    private PlantState plantState = PlantState.Disable;
    public  PlantType plantType   = PlantType.SunFlower;
    public int HP = 100;   //ֲ��Ѫ��

    private void Update()
    {
        switch (plantState)
        {
            case PlantState.Disable:
                DisableUpdate();
                break;
            case PlantState.Enable:
                EnableUpdate();
                break;
            default:
                break;
        }
    }
    private void Start()
    {
        TranslateToDisabled();
    }
    void DisableUpdate()
    {

    }
    protected virtual void EnableUpdate()
    {

    }
    void TranslateToDisabled()
    {
        plantState = PlantState.Disable; 
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;            //�ص㣺��ֹ���ͼ��ײ����ͻ
    }
    public void TranslateToEnable()
    {
        plantState = PlantState.Enable;
        GetComponent<Animator>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }
    public void BeAttacked(int damage)
    {
        this.HP -= damage;
        if (this.HP <= 0)
        {
            Die();
        }

    }
    private void Die()
    {
        Destroy(gameObject);
    }

}
