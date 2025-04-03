using UnityEngine;
using UnityEngine.InputSystem.Processors;


enum ZombieState       //TODO:僵尸状态机在这里
{
    Move,
    Eat,
    Die,
    Confusion,            //混乱状态
    Powerful,             //觉醒状态
    Pause                 //僵尸进家门全部停止
}
public class Zombie : MonoBehaviour
{
    ZombieState zombieState = ZombieState.Move;
    private Rigidbody2D rigidbody2;
    public float moveSpeed = 1;            //正常普通僵尸的速度
    private Animator animator;
    public  int   damageValue = 30;        //攻击力
    public  float damageDuration = 2;      //攻击间隔,一般为2s
    private float damageTimer;
    private bool A = false;                //遇到新植物立即攻击一次
    public int  HP = 270;
    public int currentHP;                  //用来改僵尸状态,改成公有
    public GameObject ZombieHeadPrefab;    //僵尸头
    private bool HaveHead = true;          //默认有头，防止投掉多次
    //删除代码bool IsMoving = true;        //防止吃的时候也走

    private Plants currentEatPlant;        //记录当前被吃的植物
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
        rigidbody2.MovePosition(rigidbody2.position + Vector2.left * moveSpeed * Time.deltaTime);   //MovePos直接控制位移
    }

    void ZombieEatUpdate()
    {
        if (A)
        {
            AudioManager.instance.PlayMusic(Config.Eat);
            currentEatPlant.BeAttacked(damageValue); //碰到立即攻击一次
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
    private int plantCollisionCount = 0;                               //防止同时遇到多个植物出现错误

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant"))
        {
            plantCollisionCount++;
            animator.SetBool("IsAttacking", true);
            TranslateToEat();
            currentEatPlant = collision.GetComponent<Plants>();        //记录当前被吃的植物
            A = true;
        }
        else if (collision.CompareTag("House"))                        //僵尸进家门
        {
            GameManager.instance.GameEndFail();
        }
        else if (collision.CompareTag("LawnCleaner"))                  //TODO:僵尸碰到小推车
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
                plantCollisionCount = 0;                   // 防止负数
                zombieState = ZombieState.Move; 
                currentEatPlant = null;
                A = false;
            }
        }
    }
    void TranslateToEat()            //遇到新植物要重置时间
    {
        zombieState = ZombieState.Eat;
        damageTimer = 0;
    }
    public void TranslateToPause()
    {
        zombieState = ZombieState.Pause;
        animator.enabled = false;
        //rigidbody2.bodyType = RigidbodyType2D.Static;         //不触发检测

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
        animator.SetFloat("HPPec", hpPecent);   //TODO:僵尸掉手的状态机
        if (hpPecent <= .2f && HaveHead)
        {
            HaveHead = false;
            GameObject go =  GameObject.Instantiate(ZombieHeadPrefab, transform.position, Quaternion.identity);
            Destroy(go , 3);
        }

    } 
    private void Dead()
    {
        if (zombieState == ZombieState.Die) return;           //防止多次调用
        zombieState = ZombieState.Die;
        GetComponent<Collider2D>().enabled = false;   //防止死后还占用碰撞箱
        ZombieManager.instance.RemoveZombie(this);
        Destroy(gameObject, 2);
        moveSpeed = 0;        //死亡速度将为0 

    }
}
