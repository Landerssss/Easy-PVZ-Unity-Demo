using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpawState
{
    NotStart,
    Spawning,
    End
}
enum ZombiePrefabList
{
    CommonZombie1,
    CommonZombie2,
}

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager instance { get; private set; }
    private SpawState spawnstate = SpawState.NotStart;
    public Transform[] SpawnPointList;
    public GameObject[] ZombiePrefabList; // ��Ϊ���飬�洢��ͬ��ʬԤ����
    private List<Zombie> zombieList = new List<Zombie>();      //�������н�ʬ
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //StartSpawn();
    }
    public void StartSpawn()
    {
        spawnstate = SpawState.Spawning;
        StartCoroutine(SpawnZombie());
    }
    public void ZombiePause()           //���Ա�����ʬ����ʬ���ҽ�ʬ��ͣ
    {
        spawnstate = SpawState.End;
        foreach (Zombie zombie in zombieList)
        {
            zombie.TranslateToPause();
        }
    }
    IEnumerator SpawnZombie()
    {
        yield return new WaitForSeconds(10);
        //��һ����ʬ   5ֻ
        for (int i = 0; i < 5; i++)
        {
            SpawnRandonZombie();
            yield return new WaitForSeconds(2);
        }

        yield return new WaitForSeconds(10);
        //�ڶ�����ʬ   6ֻ
        for (int i = 0; i < 6; i++)
        {
            SpawnRandonZombie();
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(10);
        //��������ʬ   7ֻ
        for (int i = 0; i < 7; i++)
        {
            SpawnRandonZombie();
            yield return new WaitForSeconds(1);
        }
    }



    private int[] baseSortingOrders = { 10, 20, 30, 40, 50 }; //ÿ�н�ʬ��order��һ����������������Ľ�ʬ���������棬Ĭ��1��2��3��4��5�ж�Ӧ��10��20��30��40��50order
    private int[] rowZombieCounts   = { 0,  0 , 0 , 0 , 0 };  //������������Ϊ����ͬһ��ÿ�γ���һ����ʬ+1
    private void SpawnRandonZombie()
    {
        if(spawnstate == SpawState.Spawning)
        {
            int spawnIndex  = Random.Range(0, SpawnPointList.Length);            // ���ѡ�����ɵ㣨�У�
            int zombieIndex = Random.Range(0, ZombiePrefabList.Length);          // ���ѡ��һ����ʬ����
            GameObject go = GameObject.Instantiate(ZombiePrefabList[zombieIndex], SpawnPointList[spawnIndex].position, Quaternion.identity);
            zombieList.Add(go.GetComponent<Zombie>());
            int sortingOrder = baseSortingOrders[spawnIndex] + rowZombieCounts[spawnIndex];  // ���㵱ǰ��ʬ�� sortingOrder������ֵ + ���������ɵĽ�ʬ����
            go.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
            rowZombieCounts[spawnIndex]++;                                                   // ÿ����һ����ʬ�����еļ�����1
        }

    }
    public void RemoveZombie(Zombie zombie)
    {
        zombieList.Remove(zombie);
    }
}
