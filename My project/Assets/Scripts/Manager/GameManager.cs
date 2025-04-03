using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }
    public ThreeTwoOne threetwoone;
    public CardListUI  cardListUI; 
    public FailUI failUI;
    private bool IsGameEnd = false;   //记录游戏是否结束
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        GameStart();
    }
    void GameStart()
    {
        Vector3 currentPosition = Camera.main.transform.position;
        Sequence sequence = DOTween.Sequence();        // 创建动画序列
        sequence.Append(
            Camera.main.transform.DOPath(
                new Vector3[] { currentPosition, new Vector3(4.8f, 0, -10) },
                2
            )
        );
        sequence.AppendInterval(4);          // 暂停 2 秒
        sequence.Append(
            Camera.main.transform.DOPath(
                new Vector3[] { new Vector3(4.8f, 0, -10), new Vector3(0, 0, -10) },
                2                        //PathtType.Linear线性速度，这里2s默认先快后慢
            )
        ).OnComplete(Show321UI);          //镜头完毕后播放321！
        sequence.Play();      
    }
    public void GameEndFail()           //游戏战败
    {
        if( IsGameEnd ) return;
        IsGameEnd = true;
        failUI.ShowFail();
        ZombieManager.instance.ZombiePause();
        cardListUI.DisableCardList();
        SunManager.instance.StopProduce();

    }
    public void GameEndSuccess()         //游戏胜利
    {
        if( IsGameEnd ) return;
        IsGameEnd = true;
        //TODO:僵尸全部杀完
        ZombieManager.instance.ZombiePause();
        cardListUI.DisableCardList();
        SunManager.instance.StopProduce();
    }
    void Show321UI()            //好，准备，开始！
    {
        threetwoone.Show321(On321UIComplete);           //On321UIComplete 是一个 Action 类型的委托，传入到TreeTwoOne.cs的this.OnComplete里
    }
    void On321UIComplete()
    {
        SunManager.instance.StartProduce();            //开始生产阳光
        ZombieManager.instance.StartSpawn();           //开始生成僵尸
        cardListUI.ShowCardList();                     //卡槽放下来
        AudioManager.instance.PlayBgm(Config.LawnMorning);
    }
}

