using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;

    private float health;
    private float maxHealth;

    public TextMeshProUGUI UI_health;
    public Image image;
    public GameObject damageMask;

    public float alertHealth;
    public float criticalHealth;

    public Color defaultColor;
    public Color alertColor;
    public Color criticalColor;
    public Color deadColor;

    float lastHealth;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;

        health = player.GetComponent<PlayerStats>().health;
        maxHealth = player.GetComponent<PlayerStats>().maxHealth;

        lastHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        health = player.GetComponent<PlayerStats>().health;
        maxHealth = player.GetComponent<PlayerStats>().maxHealth;

        if(health < lastHealth)
        {
            Debug.Log("H: " + health + ", L: " + lastHealth);
            damageMask.GetComponent<Animator>().SetTrigger("Damaged");
        }
        lastHealth = health;
        

        if (health < 0)
            health = 0;

        string displayText = Mathf.Ceil(health) + "/" + Mathf.Ceil(maxHealth);

        UI_health.text = displayText;

        if(health <= 0)
        {
            image.color = deadColor;
        }
        else if(health <= criticalHealth)
        {
            image.color = criticalColor;
        }
        else if(health <= alertHealth)
        {
            image.color = alertColor;
        }
        else
        {
            image.color = defaultColor;
        }


    }
}
