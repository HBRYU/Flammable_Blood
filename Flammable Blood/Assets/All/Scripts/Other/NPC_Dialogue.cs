using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC_Dialogue : MonoBehaviour
{
    public GameObject UI;
    public bool talk;
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
    }

    void Update()
    {
        if (talk)
        {
            UI.SetActive(true);
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
                    index += 1;
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
            }
            else
            {
                delay_timer += Time.deltaTime;
            }
        }
        else
        {
            UI.SetActive(false);
            index = 0;
            displayText = string.Empty;
            textBox.text = string.Empty;
            delay_timer = 0;
            intervalDelay_timer = 0;
            GetComponent<Animator>().SetInteger("State", defaultState);
        }

    }
}
