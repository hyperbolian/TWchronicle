using UnityEngine;

using System.Collections;

using System.Collections.Generic;
using UnityEngine.UI;




public class CardCreator : MonoBehaviour

{
    public Sprite sprite;
    public GameObject card;
    private void Start()
    {


    }
    private void Update()
    {

    }
    public void CardCreate()
    {
        GameObject NewCard = Instantiate(card);
        NewCard.transform.SetParent(card.transform.parent);
        NewCard.transform.position = new Vector3 (300,100,0);
        Image image = NewCard.GetComponent<Image>();
        image.sprite = Resources.Load("chara_back",typeof(Sprite)) as Sprite;
    }


}
