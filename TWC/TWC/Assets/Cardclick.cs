
using UnityEngine;
using UnityEngine.UI;

public class Cardclick : MonoBehaviour {
    public GameObject info;
    public Image image;
    public int Index;
	// Use this for initialization
	void Start () {
        info.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

	}
    public void appear(Image newimage)
    {
        info.SetActive(true);
        image.sprite = newimage.sprite;
        Index = (int)info.transform.position.z;
    }
    public void diappear()
    {
        info.SetActive(false);
    }
}
