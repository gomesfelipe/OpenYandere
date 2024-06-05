using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BtnMaker : MonoBehaviour
{
    public List<string> btnList = new();
    public GameObject btnTemplate;
    public List<GameObject> btns;
    private void Awake()
    {
        if (btnList.Count>0&& btnTemplate!=null)
        {
            foreach( string s in btnList)
            {
                
                    GameObject label = Instantiate(btnTemplate);


                    TextMeshProUGUI text = label.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = s;

                    btns.Add(label);
                    label.transform.SetParent(transform);
            }
        }
    }


}
