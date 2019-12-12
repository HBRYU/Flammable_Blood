using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursory : MonoBehaviour
{
    public Sprite[] cursorSprites;
    public Sprite[] cursorSprites_Click;

    public int cursorState;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        if (active)
            Cursor.visible = false;
        else
            Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            Cursor.visible = false;
            Vector2 movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //transform.position = movePos;
            transform.position = movePos;

            if (Input.GetMouseButton(0))
            {
                GetComponent<SpriteRenderer>().sprite = cursorSprites_Click[cursorState];
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = cursorSprites[cursorState];
            }

            SetStates();
        }
        else
            Cursor.visible = true;
    }

    void SetStates()
    {
        if (Input.GetKey("f"))
            cursorState = 1;
        else
            cursorState = 0;
    }
}
