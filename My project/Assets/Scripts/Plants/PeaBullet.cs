using Unity.VisualScripting;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    private float speed = 3;
    private int attackValue = 8;
    public GameObject PeaBulletHit;         //�㶹���Ѷ���
    public void SetAttackValue (int attackValue)        //ʵ������ͨ���㶹���ֽű���̬���ƹ���ֵ
    {
        this.attackValue = attackValue;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    private void Start()
    {
        Destroy(gameObject,10 );          //�ӵ�����10s������
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
            Quaternion randomRotation = Quaternion.Euler(0, 0, randomAngle);            //���������ת
            GameObject go =  GameObject.Instantiate(PeaBulletHit, transform.position,randomRotation);
            Destroy(go,1);
        }
    }
}
