using UnityEngine;
using UnityEngine.UI;

public class HandClick : MonoBehaviour
{
    public GameObject info;
    public Image image;
    public GameObject[] Use;
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
        Use[0].SetActive(true);
        Use[1].SetActive(true);
        Index = (int)i.transform.position.z;
    }
    public void diappear()
    {
        info.SetActive(false);
        Use[0].SetActive(false);
        Use[1].SetActive(false);
    }
    public int index()
    {
        return Index;
    }
}