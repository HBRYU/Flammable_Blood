using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pointer : MonoBehaviour
{
    //public Transform pointerPivot;
    public GameObject pointerPrefab;

    public List<Transform> targetPositions;
    public List<GameObject> targetObjects;
    public List<GameObject> pointers;

    public float searchDistance;
    public float rotSpeed;

    public float rectToPosDelta;

    void Start()
    {
        //TEST
        /*
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Box"))
        {
            AddPointer(go, go.transform);
        }
        */
    }

    void Update()
    {
        List<int> deletingIndexes = new List<int>();

        for (int i = 0; i < targetObjects.Count; i++)
        {
            try
            {
                Debug.Log(targetPositions[i].position);
                Vector3 targ = targetPositions[i].position;
                targ.z = 0f;

                Vector2 objectPos = Camera.main.transform.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;

                float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                pointers[i].transform.localRotation = Quaternion.Lerp(pointers[i].transform.localRotation, Quaternion.Euler(new Vector3(pointers[i].transform.localRotation.x, 0, angle)), rotSpeed * Time.deltaTime);

                if (GM.CompareDistance(Camera.main.transform.position, targetPositions[i].position, pointerPrefab.transform.GetChild(0).GetComponent<RectTransform>().localPosition.x / rectToPosDelta) < 1)
                {
                    Vector2 pos = pointers[i].transform.GetChild(0).GetComponent<RectTransform>().localPosition;
                    pos.x = Vector2.Distance(Camera.main.transform.position, targetPositions[i].position) * rectToPosDelta;
                    pointers[i].transform.GetChild(0).GetComponent<RectTransform>().localPosition = pos;
                }
            }
            catch
            {
                Debug.Log("ERR: Target object has been destroyed but pointer has not been deleted. Deleting pointer index: " + i);
                deletingIndexes.Add(i);
            }
        }

        foreach (int i in deletingIndexes)
            RemovePointer(i);
    }

    public void AddPointer(GameObject obj, Transform pos)
    {
        targetObjects.Add(obj);
        targetPositions.Add(pos);
        pointers.Add(Instantiate(pointerPrefab, transform.position, Quaternion.identity, transform));
        pointers[pointers.Count - 1].transform.SetSiblingIndex(GetComponent<UI_Inventory>().inventoryPanel.transform.GetSiblingIndex() - 2);
        Vector3 targ = targetPositions[pointers.Count - 1].position;
        targ.z = 0f;

        Vector2 objectPos = pointers[pointers.Count - 1].transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;

        pointers[pointers.Count - 1].transform.rotation = Quaternion.Euler(new Vector3(pointers[pointers.Count - 1].transform.rotation.x, 0, angle));
    }

    public void RemovePointer(GameObject obj)
    {
        try
        {
            pointers.Remove(pointers[targetObjects.IndexOf(obj)]);
            targetPositions.Remove(targetPositions[targetObjects.IndexOf(obj)]);
            targetObjects.Remove(obj);
        }
        catch
        {
            Debug.Log("ERR: Removing pointer error. Invalid object or shitty algorithm. (obj: " + obj.name + ")");
        }
    }

    public void RemovePointer(int index)
    {
        try
        {
            Destroy(pointers[index]);
            pointers.Remove(pointers[index]);
            targetPositions.Remove(targetPositions[index]);
            targetObjects.Remove(targetObjects[index]);
        }
        catch
        {
            Debug.Log("ERR: Removing pointer error. Invalid object or shitty algorithm. (index: " + index + ")");
        }
    }
}
