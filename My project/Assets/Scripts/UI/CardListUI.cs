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
        GetComponent<RectTransform>().DOAnchorPosY(248,1);             //Y��358�ƶ���248,����1s
    }
    public void DisableCardList()           //���ÿ���
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
