using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    private GameObject player;
    private GM _GM_;

    public int stage = 1;

    public AudioSource _0_Rain;
    public Transform _0_RainMaxVol;
    public Transform _0_RainMinVol;

    public float _1_Movement_Delay;
    private float _1_Movement_Delay_init;
    public GameObject _1_Darken;
    private Color _1_Darken_Color;
    public AudioSource _2_NoiseSFX;
    public GameObject _2_Sight_Module;
    public GameObject _2_Noise;
    public GameObject _2_Barrier;
    public Crate _3_Crate;
    public GameObject _4_Gun;

    void Start()
    {
        _GM_ = GM.GetGM();
        player = GM.GetPlayer();

        _1_Movement_Delay_init = _1_Movement_Delay;
        _1_Darken_Color = _1_Darken.GetComponent<Image>().color;
    }

    void Update()
    {
        if(player.transform.position.x < _0_RainMaxVol.position.x)
        {
            float vol = 1 - (Vector2.Distance(player.transform.position, _0_RainMaxVol.position) / Vector2.Distance(_0_RainMinVol.position, _0_RainMaxVol.position));
            if (vol < 0)
                vol = 0;
            _0_Rain.volume = vol;
            _0_Rain.panStereo = 1-vol;
        }
        else
        {
            _0_Rain.volume = 1;
            _0_Rain.panStereo = 0;
        }


        if (_1_Movement_Delay > 0)
        {
            _1_Movement_Delay -= Time.deltaTime;
            player.GetComponent<PlayerMove>().move = false;
            player.GetComponent<PlayerAnimControl>().disableAnimation = true;
            player.GetComponent<Animator>().SetBool("OnGround", true);
            _1_Darken.SetActive(true);
            Color col = _1_Darken.GetComponent<Image>().color;
            _1_Darken.GetComponent<Image>().color = new Color(col.r, col.g, col.b, _1_Movement_Delay / _1_Movement_Delay_init);
        }
        else
        {
            _1_Darken.SetActive(false);
            _1_Darken.GetComponent<Image>().color = _1_Darken_Color;
            player.GetComponent<PlayerMove>().move = true;
            player.GetComponent<PlayerAnimControl>().disableAnimation = false;

            if (stage == 1)
                stage = 2;
        }

        try
        {
            Item_Module asd = _2_Sight_Module.GetComponent<Item_Module>();
            string addd = asd.name;
        }
        catch
        {
            _2_Noise.GetComponent<Animator>().SetBool("Gone", true);
            _2_NoiseSFX.Stop();
            _2_Barrier.SetActive(false);
            if(stage == 2)
            {
                GM.DisplayText("Visual perception restored.", true);
                stage = 3;
            }
        }


        if(_3_Crate.items.Count == 0 && stage == 3)
        {
            Camera.main.gameObject.GetComponent<CameraMan>().speed = 2f;
            GM.DisplayText2("Press [W] mid-air\nto activate jetpack", true);
            stage = 4;
        }

        if(_4_Gun.GetComponent<Rigidbody2D>().simulated == false && stage == 4)
        {
            player.GetComponent<PlayerMove>().faceMouseMovement = true;
            GM.DisplayText2("Move mouse to look around.", true);
            GM.DisplayText2("Left click to fire.", false);

            stage = 5;
        }
    }

    
}
