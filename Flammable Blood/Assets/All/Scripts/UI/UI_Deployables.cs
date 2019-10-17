using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Deployables : MonoBehaviour
{
    private DeployablesManager source;

    public TextMeshProUGUI name_text;
    public TextMeshProUGUI count_text;
    public Image icon;

    
    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<DeployablesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(source.activeDPLYBL_count > 0)
        {
            name_text.text = source.activeDPLYBL_name;
            count_text.text = "0" + source.activeDPLYBL_count.ToString();
            icon.enabled = true;
            icon.sprite = source.activeDPLYBL_IMG;
        }
        else
        {
            name_text.text = "EMPTY";
            count_text.text = "";
            icon.enabled = false;
        }
    }
}
