using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardListUI : MonoBehaviour
{
    public List<CardTemplate> cardList;

    private void Start()
    {
        DisableCardList(); 
        //ShowCardList();
    }
    public void ShowCardList()
    {
        GetComponent<RectTransform>().DOAnchorPosY(248,1);             //Y从358移动到248,经过1s
    }
    public void DisableCardList()           //禁用卡槽
    {
        foreach (CardTemplate cardTemplate in cardList) 
        {
            cardTemplate.DisableCard();
        }
    }
    void EnableCardList()
    {
        foreach (CardTemplate cardTemplate in cardList)
        {
            cardTemplate.EnableCard();

        }
    }

}
