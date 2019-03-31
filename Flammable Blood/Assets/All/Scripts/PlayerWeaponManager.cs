using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public GameObject gunFolder;
    public int maxGunCount;
    public List<GameObject> guns;
    // Start is called before the first frame update
    void Start()
    {

        if(guns.Count > maxGunCount)
        {
            while(guns.Count > maxGunCount)
            {
                guns[guns.Count - 1].GetComponent<WeaponStats>().Drop();
                guns.Remove(guns[guns.Count - 1]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
