using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }
    public ThreeTwoOne threetwoone;
    public CardListUI  cardListUI; 
    public FailUI failUI;
    private bool IsGameEnd = false;   //��¼��Ϸ�Ƿ����
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
        Sequence sequence = DOTween.Sequence();        // ������������
        sequence.Append(
            Camera.main.transform.DOPath(
                new Vector3[] { currentPosition, new Vector3(4.8f, 0, -10) },
                2
            )
        );
        sequence.AppendInterval(4);          // ��ͣ 2 ��
        sequence.Append(
            Camera.main.transform.DOPath(
                new Vector3[] { new Vector3(4.8f, 0, -10), new Vector3(0, 0, -10) },
                2                        //PathtType.Linear�����ٶȣ�����2sĬ���ȿ����
            )
        ).OnComplete(Show321UI);          //��ͷ��Ϻ󲥷�321��
        sequence.Play();      
    }
    public void GameEndFail()           //��Ϸս��
    {
        if( IsGameEnd ) return;
        IsGameEnd = true;
        failUI.ShowFail();
        ZombieManager.instance.ZombiePause();
        cardListUI.DisableCardList();
        SunManager.instance.StopProduce();

    }
    public void GameEndSuccess()         //��Ϸʤ��
    {
        if( IsGameEnd ) return;
        IsGameEnd = true;
        //TODO:��ʬȫ��ɱ��
        ZombieManager.instance.ZombiePause();
        cardListUI.DisableCardList();
        SunManager.instance.StopProduce();
    }
    void Show321UI()            //�ã�׼������ʼ��
    {
        threetwoone.Show321(On321UIComplete);           //On321UIComplete ��һ�� Action ���͵�ί�У����뵽TreeTwoOne.cs��this.OnComplete��
    }
    void On321UIComplete()
    {
        SunManager.instance.StartProduce();            //��ʼ��������
        ZombieManager.instance.StartSpawn();           //��ʼ���ɽ�ʬ
        cardListUI.ShowCardList();                     //���۷�����
        AudioManager.instance.PlayBgm(Config.LawnMorning);
    }
}

