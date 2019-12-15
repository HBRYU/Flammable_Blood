using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory_SelectPanel : MonoBehaviour
{
    public UI_Inventory UI;
    public string type;
    public int index;
    public GameObject dropCrate;

    public void OnPressDrop()
    {
        if (type == "Weapon")
            OnWeaponPressDrop();
        if (type == "Ammo")
            OnAmmoPressDrop();
        if (type == "Deployable")
            OnDeployablePressDrop();
        gameObject.SetActive(false);
    }

    void OnWeaponPressDrop()
    {
        UI.pw.Drop(UI.pw.weapons[index]);
        UI.selectedWeaponIndex = -1;
    }
    void OnAmmoPressDrop()
    {
        GameObject crate = Instantiate(dropCrate, UI.player.transform.position, Quaternion.identity);
        crate.GetComponent<Crate>().items = new List<Item>();
        Item item = new Item();
        item.name = UI.pw.ammo_type[index];
        item.type = "Ammo";
        item.count = UI.pw.ammo_count[index];
        item.obj = null;
        item.module = new Module();
        crate.GetComponent<Crate>().items.Add(item);
        UI.pw.ammo_count[index] = 0;
    }
    void OnDeployablePressDrop()
    {

    }
}
