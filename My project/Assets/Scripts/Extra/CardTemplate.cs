using UnityEngine;
using UnityEngine.UI;


enum CardState   //卡片状态声明
{
    Disable,
    Cooling,
    WaitingSun,
    Ready
}
public enum PlantType     //可被种植的植物名称，类型
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
    //冷却->等待阳光->可以被点击
    private CardState cardState = CardState.Disable;    
    public PlantType planType = PlantType.SunFlower;
    public GameObject CardLight;
    public GameObject CardGray;
    public Image CardMask;

    [SerializeField]
    private float cdTime      = 2;
    private float currentTime = 0;       //计时器初始从0开始
    [SerializeField]
    private int needSunPoint = 50;
    private void Start()
    {
        TransalteToReady();        //默认一开始CD好了
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
        currentTime = 0;               //重新开始冷却
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


    public void OnClick()        //卡片被点击
    {
        if (cardState == CardState.Disable) return;
        if (needSunPoint > SunManager.instance.SunPoint) return;
        //消耗阳光  可以被点击，种植

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
