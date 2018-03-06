using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Timers;

public class MSNcontrol : MonoBehaviour {
    public Text text;
    public string temp;
    public Time time;
    public GameObject m;
	// Use this for initialization
	void Start()
    {
        Invoke("d",2);
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    public IEnumerator close()
    {
        yield return new WaitForSeconds(2);
        text.text = "";
        m.SetActive(false);
    }
    public void d()
    {
        Destroy(m);
       // m.SetActive(false);
    }

}
