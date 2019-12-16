using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone2Attack : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;
    public LayerMask whatIsGround;
    public LayerMask laserInteract;
    public LayerMask playerLayer;
    public Transform sparkPoint;
    public ParticleSystem ps;
    public AudioSource sparkAudioSource;
    public GameObject contactParticles;
    public LineRenderer lr;
    public SpriteRenderer teslarSR;
    public Color teslarOffColor;
    public Color teslarOnColor;
    public float laserLength;
    private Vector2 laserHitPos;
    public bool attack;
    public float delay;
    private float delay_timer;
    public float delay2;
    private float delay2_timer;
    public float laserDuration;
    private float laserDuration_timer;
    public float damage;
    [HideInInspector]
    public Vector3 tempDir;
    [HideInInspector]
    public GameObject cp;

    private void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;
        lr.enabled = false;
        tempDir = player.transform.position - sparkPoint.position;
        cp = null;
        teslarSR.color = teslarOffColor;
    }

    void Update()
    {
        if (attack)
        {
            Attack();
        }
        else
        {
            lr.enabled = false;
            ps.Clear();
            ps.Stop();
        }
    }

    void Attack()
    {
        
        delay_timer += Time.deltaTime;
        if(delay_timer >= delay)
        {
            delay2_timer += Time.deltaTime;
            if (delay2_timer >= delay2)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                GetComponent<DroneMovement>().chase = false;
                RaycastHit2D hit = Physics2D.Raycast(sparkPoint.position, tempDir, laserLength, laserInteract);
                if (hit.collider != null)
                {
                    laserHitPos = hit.point;
                }
                else
                {
                    float a = tempDir.y / -tempDir.x;
                    Vector2 endPos;
                    if (tempDir.x < 0)
                    {
                        endPos = new Vector2(sparkPoint.position.x - 50, sparkPoint.position.y + 50 * a);
                    }
                    else
                    {
                        endPos = new Vector2(sparkPoint.position.x + 50, sparkPoint.position.y - 50 * a);
                    }
                    laserHitPos = endPos;
                }
                lr.enabled = true;
                lr.SetPosition(0, sparkPoint.position);
                lr.SetPosition(1, laserHitPos);
                laserDuration_timer += Time.deltaTime;

                if(sparkAudioSource.isPlaying == false)
                    sparkAudioSource.Play();

                Vector3 targ = laserHitPos;
                targ.z = 0f;

                Vector2 objectPos = sparkPoint.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;

                float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                sparkPoint.rotation = Quaternion.Euler(new Vector3(sparkPoint.rotation.x, 0, angle));
                    
                
                if (!ps.isPlaying)
                {
                    ps.Play();
                    cp = Instantiate(contactParticles, laserHitPos, Quaternion.identity);
                }
                try
                {
                    cp.transform.position = laserHitPos;
                }
                catch { }

                teslarSR.color = teslarOnColor;

                RaycastHit2D hit2 = Physics2D.Raycast(sparkPoint.position, tempDir, laserLength, laserInteract);
                if(hit2.collider != null && hit2.collider.tag == "Player")
                {
                    player.GetComponent<PlayerStats>().TakeDamage(damage * Time.deltaTime);
                }

                if(laserDuration_timer >= laserDuration)
                {
                    delay_timer = 0;
                    delay2_timer = 0;
                    laserDuration_timer = 0;
                    lr.enabled = false;
                    sparkAudioSource.Stop();
                    ps.Stop();
                    Destroy(cp);
                    teslarSR.color = teslarOffColor;
                    GetComponent<DroneMovement>().chase = true;
                }
            }
        }
        else
        {
            ResetTempDir();
        }
    }

    public void ResetTempDir()
    {
        tempDir = player.transform.position - sparkPoint.position;
    }
}
