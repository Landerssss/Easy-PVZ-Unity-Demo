using TMPro;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    public static SunManager instance {  get; private set; }
    public TextMeshProUGUI SunPointText;
    public float produceTime;          //�����������
    private float produceTimer;
    private Vector3 SunPointTextPos;   //����λ��
    public GameObject SunPrefab;

    private bool IsStartProduce = false;
    [SerializeField]
    private int sunPoint;
    public int SunPoint
    {
        get { return sunPoint; }
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        UpdateSunPointText();
        CalculateSunPointText();
        //StartProduce();                        //��ʼ�󵹼�ʱ������ſ�ʼ����
    }
    private void Update()
    {
        //TODO:ע��ҹ������������

        if (IsStartProduce)
        {
            ProduceSun();
        }
    }
    public void StartProduce()
    {
        IsStartProduce = true;
    }
    public void StopProduce()
    {
        IsStartProduce = false;
    }
    private void UpdateSunPointText()
    {
        SunPointText.text = SunPoint.ToString();
    }
    public void SubSun(int point)       //��������
    
    {
        sunPoint -= point;
        UpdateSunPointText();

    }
    public void AddSun(int point)       //��������
    {
        sunPoint += point;
        UpdateSunPointText();
    }
    public Vector3 GetSunPointTextPos()
    {
        return SunPointTextPos;
    }
    private void CalculateSunPointText() //�������������
    {
        Vector3 position = Camera.main.ScreenToWorldPoint( SunPointText.transform.position );
        position.z = 0;
        SunPointTextPos = position;
    }


    void ProduceSun()
    {
        produceTimer += Time.deltaTime;
        if(produceTimer >produceTime)
        {
            produceTimer = 0;
            produceTime = 24;
            Vector3 position = new Vector3(Random.Range(-5, 5.5f), 6.2f, 0);
            GameObject go = GameObject.Instantiate(SunPrefab,position,Quaternion.identity);

            position.y = Random.Range(-3.6f, 3);
            go.GetComponent<Sun>().LinearTo(position);
        }
    }

}
