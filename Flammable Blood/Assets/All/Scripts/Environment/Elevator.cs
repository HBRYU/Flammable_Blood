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

    public float speed;

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
        if(Vector2.Distance(transform.position, player.transform.position) <= 0.5 && Input.GetKeyDown("e"))
        {
            move = true;
        }
        if (move)
        {
            if(current_position == Positions[0])
            {
                Move(Positions[1].position);
                Sprites[1].SetActive(true);
                Sprites[0].SetActive(false);
            }
            else
            {
                Move(Positions[0].position);
                Sprites[0].SetActive(true);
                Sprites[1].SetActive(false);
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
            }

            else
            {
                current_position = Positions[0];
            }
        }
    }
}
