using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory_SelectPanel : MonoBehaviour
{
    public UI_Inventory UI;
    public string type;
    public int index;
    public GameObject dropItem_Ammo;
    public GameObject dropItem_Deployable;

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
        UI.resetStatsPanel();
        UI.pw.Drop(UI.pw.weapons[index]);
        UI.selectedWeaponIndex = -1;
    }
    void OnAmmoPressDrop()
    {
        /*
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
        */

        GameObject drop = Instantiate(dropItem_Ammo, GM.GetPlayer().transform.position, Quaternion.identity);
        drop.GetComponent<PickableItem>().name = UI.pw.ammo_type[index];
        drop.GetComponent<PickableItem>().count = UI.pw.ammo_count[index];
        UI.pw.ammo_count[index] = 0;
    }
    void OnDeployablePressDrop()
    {
        UI.resetStatsPanel();
        GameObject drop = Instantiate(dropItem_Deployable, GM.GetPlayer().transform.position, Quaternion.identity);
        drop.GetComponent<PickableItem>().name = UI.dm.dplybles_name[index];
        drop.GetComponent<PickableItem>().count = UI.dm.dplybles_count[index];
        UI.dm.dplybles_count[index] = 0;
    }
}
