using UnityEngine;
using UnityEngine.UI;


enum CardState   //��Ƭ״̬����
{
    Disable,
    Cooling,
    WaitingSun,
    Ready
}
public enum PlantType     //�ɱ���ֲ��ֲ�����ƣ�����
{
    SunFlower,
    PeaShooter,
    Potato,
    Wallnut,
    Jalpeno,
    Blover,
    SpikeWeed
}

public class CardTemplate : MonoBehaviour
{
    //��ȴ->�ȴ�����->���Ա����
    private CardState cardState = CardState.Disable;    
    public PlantType planType = PlantType.SunFlower;
    public GameObject CardLight;
    public GameObject CardGray;
    public Image CardMask;

    [SerializeField]
    private float cdTime      = 2;
    private float currentTime = 0;       //��ʱ����ʼ��0��ʼ
    [SerializeField]
    private int needSunPoint = 50;
    private void Start()
    {
        TransalteToReady();        //Ĭ��һ��ʼCD����
    }
    private void Update()
    {
        switch(cardState)
        {
            case CardState.Cooling:
                CoolingUpdate();
                break;
            case CardState.WaitingSun:
                WaitingSunUpdate();
                break;
            case CardState.Ready:
                ReadyUpdate();
                break;
            default:
                break;
        }
    }
    void CoolingUpdate()
    {
        currentTime += Time.deltaTime;
        CardMask.fillAmount = (cdTime - currentTime) / cdTime;
        if( currentTime >= cdTime )
        {
            TransalteToWaitingSun();
        }
    }
    void WaitingSunUpdate()
    {
        if( needSunPoint<= SunManager.instance.SunPoint)
        {
            TransalteToReady();
        }
    }
    void ReadyUpdate()
    {
        if (needSunPoint > SunManager.instance.SunPoint)
        {
            TransalteToWaitingSun();
        }
    }
    void TransalteToCooling()
    {
        cardState = CardState.Cooling;
        currentTime = 0;               //���¿�ʼ��ȴ
        CardLight.SetActive(false);
        CardGray.SetActive(true);
        CardMask.gameObject.SetActive(true);
    }
    void TransalteToWaitingSun()
    {
        cardState = CardState.WaitingSun;
        CardLight.SetActive(false);
        CardGray.SetActive(true);
        CardMask.gameObject.SetActive(false);
    }
    void TransalteToReady()
    {
        cardState = CardState.Ready;
        CardLight.SetActive(true);
        CardGray.SetActive(false );
        CardMask.gameObject.SetActive(false);
    }


    public void OnClick()        //��Ƭ�����
    {
        if (cardState == CardState.Disable) return;
        if (needSunPoint > SunManager.instance.SunPoint) return;
        //��������  ���Ա��������ֲ

        bool isSuccess = HandManager.instance.AddPlant(planType);
        if (isSuccess)
        {
            TransalteToCooling();
            SunManager.instance.SubSun(needSunPoint);
        }
    }

    public void DisableCard()
    {
       cardState = CardState.Disable;
       
    }
    public void EnableCard()
    {
        TransalteToReady();
    }
}
