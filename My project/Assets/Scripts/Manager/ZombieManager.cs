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
    public GameObject[] ZombiePrefabList; // 改为数组，存储不同僵尸预制体
    private List<Zombie> zombieList = new List<Zombie>();      //保存所有僵尸
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
    public void ZombiePause()           //可以冰冻僵尸、僵尸进家僵尸暂停
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
        //第一波僵尸   5只
        for (int i = 0; i < 5; i++)
        {
            SpawnRandonZombie();
            yield return new WaitForSeconds(2);
        }

        yield return new WaitForSeconds(10);
        //第二波僵尸   6只
        for (int i = 0; i < 6; i++)
        {
            SpawnRandonZombie();
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(10);
        //第三波僵尸   7只
        for (int i = 0; i < 7; i++)
        {
            SpawnRandonZombie();
            yield return new WaitForSeconds(1);
        }
    }



    private int[] baseSortingOrders = { 10, 20, 30, 40, 50 }; //每行僵尸的order不一样，这样可以下面的僵尸覆盖在上面，默认1，2，3，4，5行对应，10，20，30，40，50order
    private int[] rowZombieCounts   = { 0,  0 , 0 , 0 , 0 };  //用来递增，作为矩阵，同一行每次出现一个僵尸+1
    private void SpawnRandonZombie()
    {
        if(spawnstate == SpawState.Spawning)
        {
            int spawnIndex  = Random.Range(0, SpawnPointList.Length);            // 随机选择生成点（行）
            int zombieIndex = Random.Range(0, ZombiePrefabList.Length);          // 随机选择一个僵尸类型
            GameObject go = GameObject.Instantiate(ZombiePrefabList[zombieIndex], SpawnPointList[spawnIndex].position, Quaternion.identity);
            zombieList.Add(go.GetComponent<Zombie>());
            int sortingOrder = baseSortingOrders[spawnIndex] + rowZombieCounts[spawnIndex];  // 计算当前僵尸的 sortingOrder：基础值 + 该行已生成的僵尸数量
            go.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
            rowZombieCounts[spawnIndex]++;                                                   // 每生成一个僵尸，该行的计数加1
        }

    }
    public void RemoveZombie(Zombie zombie)
    {
        zombieList.Remove(zombie);
    }
}
