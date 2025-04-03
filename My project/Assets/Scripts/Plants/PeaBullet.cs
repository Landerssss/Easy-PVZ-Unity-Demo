using Unity.VisualScripting;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    private float speed = 3;
    private int attackValue = 8;
    public GameObject PeaBulletHit;         //豌豆爆裂动画
    public void SetAttackValue (int attackValue)        //实际上是通过豌豆射手脚本动态控制攻击值
    {
        this.attackValue = attackValue;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    private void Start()
    {
        Destroy(gameObject,10 );          //子弹超过10s就消除
    }
    private void Update()
    {
        transform.Translate(Vector3.right * speed *Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.tag == "Zombie")
        {
            Destroy(this.gameObject);
            collison.GetComponent<Zombie>().BeAttacked(attackValue);
            float randomAngle = Random.Range(0f, 360f);
            Quaternion randomRotation = Quaternion.Euler(0, 0, randomAngle);            //让其随机旋转
            GameObject go =  GameObject.Instantiate(PeaBulletHit, transform.position,randomRotation);
            Destroy(go,1);
        }
    }
}
