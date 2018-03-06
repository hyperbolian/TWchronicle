using UnityEngine;
using UnityEngine.UI;

public class Shopclick : MonoBehaviour
{
    public GameObject info;
    public Image image;
    public GameObject[] Shop;
    public int Index;
    // Use this for initialization
    void Start()
    {
        info.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void appear(GameObject i)
    {
        Image newimage = i.GetComponent<Image>();
        info.SetActive(true);
        image.sprite = newimage.sprite;
        Shop[0].SetActive(true);
        Shop[1].SetActive(true);
        Index = (int)i.transform.position.z;
    }
    public void diappear()
    {
        info.SetActive(false);
        Shop[0].SetActive(false);
        Shop[1].SetActive(false);
    }
    public int index()
    {
        return Index;
    }
}