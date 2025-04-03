using UnityEngine;
using UnityEngine.InputSystem.Processors;


enum ZombieState       //TODO:��ʬ״̬��������
{
    Move,
    Eat,
    Die,
    Confusion,            //����״̬
    Powerful,             //����״̬
    Pause                 //��ʬ������ȫ��ֹͣ
}
public class Zombie : MonoBehaviour
{
    ZombieState zombieState = ZombieState.Move;
    private Rigidbody2D rigidbody2;
    public float moveSpeed = 1;            //������ͨ��ʬ���ٶ�
    private Animator animator;
    public  int   damageValue = 30;        //������
    public  float damageDuration = 2;      //�������,һ��Ϊ2s
    private float damageTimer;
    private bool A = false;                //������ֲ����������һ��
    public int  HP = 270;
    public int currentHP;                  //�����Ľ�ʬ״̬,�ĳɹ���
    public GameObject ZombieHeadPrefab;    //��ʬͷ
    private bool HaveHead = true;          //Ĭ����ͷ����ֹͶ�����
    //ɾ������bool IsMoving = true;        //��ֹ�Ե�ʱ��Ҳ��

    private Plants currentEatPlant;        //��¼��ǰ���Ե�ֲ��
    private void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHP = HP;
    }
    private void Update()
    {
        switch (zombieState)
        {
            case ZombieState.Move:
                ZombieMoveUpdate();
                break;
            case ZombieState.Eat:
                ZombieEatUpdate();
                break;
            case ZombieState.Die:
                break;

        }
    }

    void ZombieMoveUpdate()
    {
        rigidbody2.MovePosition(rigidbody2.position + Vector2.left * moveSpeed * Time.deltaTime);   //MovePosֱ�ӿ���λ��
    }

    void ZombieEatUpdate()
    {
        if (A)
        {
            AudioManager.instance.PlayMusic(Config.Eat);
            currentEatPlant.BeAttacked(damageValue); //������������һ��
            A = false;
        }
        damageTimer += Time.deltaTime;
        if (damageTimer >= damageDuration &&currentEatPlant != null)
        {
            AudioManager.instance.PlayMusic(Config.Eat);
            damageTimer = 0;
            currentEatPlant.BeAttacked( damageValue );
        }
    }
    void ZombieDieUpdate()
    {

    }
    private int plantCollisionCount = 0;                               //��ֹͬʱ�������ֲ����ִ���

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant"))
        {
            plantCollisionCount++;
            animator.SetBool("IsAttacking", true);
            TranslateToEat();
            currentEatPlant = collision.GetComponent<Plants>();        //��¼��ǰ���Ե�ֲ��
            A = true;
        }
        else if (collision.CompareTag("House"))                        //��ʬ������
        {
            GameManager.instance.GameEndFail();
        }
        else if (collision.CompareTag("LawnCleaner"))                  //TODO:��ʬ����С�Ƴ�
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant"))
        {
            plantCollisionCount--;
            if (plantCollisionCount <= 0)
            {
                animator.SetBool("IsAttacking", false);
                plantCollisionCount = 0;                   // ��ֹ����
                zombieState = ZombieState.Move; 
                currentEatPlant = null;
                A = false;
            }
        }
    }
    void TranslateToEat()            //������ֲ��Ҫ����ʱ��
    {
        zombieState = ZombieState.Eat;
        damageTimer = 0;
    }
    public void TranslateToPause()
    {
        zombieState = ZombieState.Pause;
        animator.enabled = false;
        //rigidbody2.bodyType = RigidbodyType2D.Static;         //���������

    }
    public void BeAttacked(int damage)
    {
        if(currentHP <= 0) return;
        this.currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = -1;
            Dead();
        }
        float hpPecent = currentHP * 1f / HP;
        animator.SetFloat("HPPec", hpPecent);   //TODO:��ʬ���ֵ�״̬��
        if (hpPecent <= .2f && HaveHead)
        {
            HaveHead = false;
            GameObject go =  GameObject.Instantiate(ZombieHeadPrefab, transform.position, Quaternion.identity);
            Destroy(go , 3);
        }

    } 
    private void Dead()
    {
        if (zombieState == ZombieState.Die) return;           //��ֹ��ε���
        zombieState = ZombieState.Die;
        GetComponent<Collider2D>().enabled = false;   //��ֹ����ռ����ײ��
        ZombieManager.instance.RemoveZombie(this);
        Destroy(gameObject, 2);
        moveSpeed = 0;        //�����ٶȽ�Ϊ0 

    }
}
