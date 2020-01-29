using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC_Dialogue : MonoBehaviour
{
    public GameObject dialogue_panel;
    public GameObject PressKey_panel;
    public bool talk;
    public bool pause;
    public bool pause_flag;
    public bool EToInteract;
    public GameObject ETI_panel;
    public float ETI_checkDistance;
    public int defaultState;

    public List<Dialogue> dialogues;
    public int index;
    public float delay_timer;
    private float intervalDelay_timer;

    public bool spaceBetweenDialogues;

    public TextMeshProUGUI textBox;
    [TextArea]
    public string displayText;

    void Start()
    {
        if (spaceBetweenDialogues)
        {
            for(int i = 0; i < dialogues.Count - 1; i++)
            {
                dialogues[i].text += " ";
            }
        }
        dialogues.Add(new Dialogue());
        displayText = dialogues[0].text;
        GetComponent<Animator>().SetInteger("State", dialogues[index].state);
        textBox.text = string.Empty;
        textBox.color = GetComponent<NPC>().dialugueColor;
        PressKey_panel.SetActive(false);
        ETI_panel.SetActive(false);
    }

    void Update()
    {
        if (EToInteract && (GM.CompareDistance(transform.position, GM.GetPlayer().transform.position, ETI_checkDistance) <= 0))
        {
            if (!talk)
            {
                ETI_panel.SetActive(true);
            }
            if (Input.GetKey(KeyCode.E))
            {
                ETI_panel.SetActive(false);
                talk = true;
            }
        }
        else
        {
            ETI_panel.SetActive(false);
        }


        if (talk)
        {
            dialogue_panel.SetActive(true);
            if(delay_timer >= dialogues[index].delay)
            {
                if (dialogues[index].clear)
                {
                    if(displayText != dialogues[index].text)
                        textBox.text = string.Empty;
                    displayText = dialogues[index].text;
                }
                if (displayText.Length > textBox.text.Length)
                {

                    if (intervalDelay_timer > dialogues[index].intervalDelay)
                    {
                        for (int i = 0; i < dialogues[index].typeLetterCount; i++)
                        {
                            try
                            {
                                textBox.text += displayText[textBox.text.Length];
                                //Debug.Log(displayText[helpText.text.Length]);
                            }
                            catch { }
                        }
                        if(GetComponent<NPC>().dialogueSFX != null)
                            GetComponent<AudioSource>().PlayOneShot(GetComponent<NPC>().dialogueSFX);
                        intervalDelay_timer = 0;
                    }
                    else
                    {
                        intervalDelay_timer += Time.deltaTime;
                    }
                }
                else
                {
                    //  EXIT Dialogue Settings  ----------------------------------
                    if (dialogues[index].pause && pause_flag == false)
                    {
                        pause = true;
                        pause_flag = true;
                    }
                    //------------------------------------------------------------

                    if ((!dialogues[index].pressKey || (dialogues[index].pressKey && Input.GetKey(KeyCode.Space))) &&!pause)
                    {
                        PressKey_panel.SetActive(false);
                        pause_flag = false;

                        index += 1;

                        //  INIT Dialogue Settings   ---------------------------------
                        if (dialogues[index].lockPlayerPosition)
                            GM.GetPlayer().GetComponent<PlayerMove>().move = false;
                        if (dialogues[index].unlockPlayerPosition)
                            GM.GetPlayer().GetComponent<PlayerMove>().move = true;
                        if (dialogues[index].facePlayer)
                        {
                            if(GM.GetPlayer().transform.position.x >= transform.position.x)
                            {
                                Quaternion rotator = transform.localRotation;
                                rotator.y = 180;
                                transform.rotation = rotator;
                            }
                            else
                            {
                                Quaternion rotator = transform.localRotation;
                                rotator.y = 0;
                                transform.rotation = rotator;
                            }
                        }
                        if (dialogues[index].faceRight)
                        {
                            Quaternion rotator = transform.localRotation;
                            rotator.y = 180;
                            transform.rotation = rotator;
                        }
                        if (dialogues[index].faceLeft)
                        {
                            Quaternion rotator = transform.localRotation;
                            rotator.y = 0;
                            transform.rotation = rotator;
                        }
                        if(dialogues[index].triggerNPCs.Count > 0)
                        {
                            foreach(GameObject g in dialogues[index].triggerNPCs)
                            {
                                g.GetComponent<NPC_Dialogue>().talk = true;
                            }
                        }
                        if (dialogues[index].unpauseNPCs.Count > 0)
                        {
                            foreach (GameObject g in dialogues[index].triggerNPCs)
                            {
                                g.GetComponent<NPC_Dialogue>().pause = false;
                            }
                        }

                        //--------------------------------------------------------


                        GetComponent<Animator>().SetInteger("State", dialogues[index].state);
                        if (dialogues.Count - 1 > index)
                        {
                            if (dialogues[index].clear)
                            {
                                //displayText = dialogues[index].text;
                            }
                            else
                            {
                                displayText += dialogues[index].text;
                            }

                            delay_timer = 0;
                        }
                        else
                        {
                            talk = false;
                        }
                    }
                    else
                    {
                        PressKey_panel.SetActive(true);
                    }
                }
            }
            else
            {
                delay_timer += Time.deltaTime;
            }
        }
        else
        {
            dialogue_panel.SetActive(false);
            index = 0;
            displayText = string.Empty;
            textBox.text = string.Empty;
            delay_timer = 0;
            intervalDelay_timer = 0;
            GetComponent<Animator>().SetInteger("State", defaultState);
        }

    }
}
