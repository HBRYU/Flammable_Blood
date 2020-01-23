using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    private GameObject player;
    private GM _GM_;

    public int stage = 1;

    public Transform trigger_1;
    public Transform trigger_2;

    public GameObject credits_1;
    public GameObject credits_2;

    public AudioSource BGM_1;
    public AudioSource BGM_2;
    private bool BGM_2_Flag;

    public AudioSource _0_Rain;
    public float _0_Rain_Volume;
    public Transform _0_RainMaxVol;
    public Transform _0_RainMinVol;

    private bool _0_WASD_flag;
    private bool _0_SHIFT_flag;

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

    public GameObject[] _5_Enemies;
    public GameObject _5_Barrier;

    void Start()
    {
        _GM_ = GM.GetGM();
        player = GM.GetPlayer();

        _1_Movement_Delay_init = _1_Movement_Delay;
        _1_Darken_Color = _1_Darken.GetComponent<Image>().color;
        GM.GetUI().GetComponent<UI_HelpText>().displayTime = 3.2f;
    }

    void Update()
    {
        if(player.transform.position.x > trigger_1.position.x)
        {
            credits_1.SetActive(true);
            credits_1.GetComponent<Animator>().SetTrigger("Start");
        }
        if(player.transform.position.x > trigger_2.position.x)
        {
            credits_2.SetActive(true);
            credits_2.GetComponent<Animator>().SetTrigger("Title");
            BGM_1.gameObject.GetComponent<Animator>().SetTrigger("Start");
            if (!BGM_2_Flag)
            {
                BGM_2.Play();
                BGM_2_Flag = true;
            }
        }

        if(player.transform.position.x < _0_RainMaxVol.position.x)
        {
            float vol = _0_Rain_Volume - (Vector2.Distance(player.transform.position, _0_RainMaxVol.position) / Vector2.Distance(_0_RainMinVol.position, _0_RainMaxVol.position));
            if (vol < 0)
                vol = 0;
            _0_Rain.volume = vol;
            _0_Rain.panStereo = 1-vol;
        }
        else
        {
            _0_Rain.volume = _0_Rain_Volume;
            _0_Rain.panStereo = 0;
        }

        if(Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            _0_WASD_flag = true;
        }
        if (!_0_WASD_flag)
            GM.DisplayText("Press [W/A/S/D] to move", true);
        else
        {
            if(!_0_SHIFT_flag)
                GM.DisplayText("Hold [SHIFT] to run", true);
            _0_SHIFT_flag = true;
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
            if (GM.CompareDistance(player.transform.position, _2_Sight_Module.transform.position, 1.2f) <= 0)
                GM.DisplayText("Press [E] to pick up or interact with items", true);
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
            GM.DisplayText("Press [W] mid-air to activate jetpack", true);
            GM.DisplayText("Be careful not to run out of fuel", false);
            stage = 4;
        }

        if(_4_Gun.GetComponent<Rigidbody2D>().simulated == false && stage == 4)
        {
            player.GetComponent<PlayerMove>().faceMouseMovement = true;
            GM.DisplayText2("Move mouse to look around.", true);
            GM.DisplayText2("Left click to fire.", false);

            GM.GetUI().GetComponent<UI_HelpText>().displayTime = 3f;

            stage = 5;
        }

        if(stage == 5)
        {
            try
            {
                foreach(GameObject g in _5_Enemies)
                {
                    g.GetComponent<EnemyStats>().health = g.GetComponent<EnemyStats>().health;
                }
            }
            catch
            {
                _5_Barrier.SetActive(false);
                stage = 6;
            }
        }
    }

    
}
