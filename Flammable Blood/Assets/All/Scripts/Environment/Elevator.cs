using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private GM _GM_;
    public List<Transform> Positions;
    public List<GameObject> Sprites;
    public Transform init_position;
    private Transform current_position;
    private GameObject player;
    public GameObject pressKey;
    public bool useButton;
    public GameObject[] buttons;
    public GameObject[] pressKeys;
    public Sprite buttonOn;
    public Sprite buttonOff;

    public float speed;

    [HideInInspector]
    public int targetPosition;
    private int movingPosition;

    private bool move;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;
        transform.position = init_position.position;
        if (!Positions.Contains(init_position))
        {
            Debug.Log("List of positions does not contain init_position: " + init_position + ", setting init_position to Positions[0] <" + gameObject + ">");
            init_position = Positions[0];
        }
        current_position = init_position;
        Sprites[0].SetActive(true);
        Sprites[1].SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GM.CompareDistance(transform.position, player.transform.position, 0.5f) <= 0 && !move)
            pressKey.SetActive(true);
        else
            pressKey.SetActive(false);

        if (GM.CompareDistance(transform.position, player.transform.position, 0.5f) <= 0 && Input.GetKeyDown("e"))
        {
            move = true;
        }
        if (move)
        {
            if(current_position == Positions[0])
            {
                Move(Positions[1].position);
                movingPosition = 1;
                Sprites[1].SetActive(true);
                Sprites[0].SetActive(false);
            }
            else
            {
                Move(Positions[0].position);
                movingPosition = 0;
                Sprites[0].SetActive(true);
                Sprites[1].SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (useButton)
        {
            if (GM.CompareDistance(buttons[0].transform.position, player.transform.position, 1.0f) <= 0)
            {
                pressKeys[0].SetActive(true);
                if (Input.GetKeyDown("e") && targetPosition == 1)
                {
                    buttons[0].GetComponent<SpriteRenderer>().sprite = buttonOn;
                    buttons[1].GetComponent<SpriteRenderer>().sprite = buttonOff;
                    move = true;
                    GM.DisplayText2("[Elevator called]", true);
                }
            }
            else
            {
                pressKeys[0].SetActive(false);
            }
            if (GM.CompareDistance(buttons[1].transform.position, player.transform.position, 1.0f) <= 0)
            {
                pressKeys[1].SetActive(true);
                if (Input.GetKeyDown("e") && targetPosition == 0)
                {
                    buttons[0].GetComponent<SpriteRenderer>().sprite = buttonOff;
                    buttons[1].GetComponent<SpriteRenderer>().sprite = buttonOn;
                    move = true;
                    GM.DisplayText2("[Elevator called]", true);
                }
            }
            else
            {
                pressKeys[1].SetActive(false);
            }
        }
    }

    private void Move(Vector2 position)
    {
        if(Mathf.Abs(transform.position.y - position.y) > 0.05)
        {
            GetComponent<Rigidbody2D>().MovePosition(Vector2.MoveTowards(transform.position, position, speed));
        }
        else
        {
            move = false;
            if (current_position == Positions[0])
            {
                current_position = Positions[1];
                targetPosition = 1;
            }

            else
            {
                current_position = Positions[0];
                targetPosition = 0;
            }
        }
    }
}
