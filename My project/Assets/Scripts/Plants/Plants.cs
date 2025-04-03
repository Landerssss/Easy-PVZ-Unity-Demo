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
    public int HP = 100;   //植物血量

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
        GetComponent<Collider2D>().enabled = false;            //重点：防止与地图碰撞器冲突
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
