using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodClick : MonoBehaviour {
    public GameObject info;
    public Image image;
    public int Index;
    // Use this for initialization
    void Start () {
        Index = -1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void appear(GameObject i)
    {
        Image newimage = i.GetComponent<Image>();
        image.sprite = newimage.sprite;
        Index = (int)i.transform.position.z;
    }
}
