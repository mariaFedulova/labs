using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject[] slots;

    [SerializeField] GameObject[] prefabs;

    [SerializeField] bool[] isFull;

    [SerializeField] Item[] items;

    [SerializeField] GameObject[] Images;

    public GameObject[] Slots { get => slots; set => slots = value; }
    public bool[] IsFull { get => isFull; set => isFull = value; }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount <= 0)
            {
                isFull[i] = false;
            }
        }
    }

    public void SetInSlot(int id,  int num)
    {
        Instantiate(Images[id], slots[num].transform);
        items[num] = new Item(id);
        
        
    }

    public void DropOutOfSlot(int num)
    {
        int id = items[num].Id;

        GameObject gameObject = Instantiate(prefabs[id]);

        gameObject.transform.position = GetComponent<BagHold>().HoldPoint.position;

        GetComponent<BagHold>().GameObjectFromInventary = gameObject;

        GetComponent<BagHold>().Hold = true;
        
        foreach (Transform child in slots[num].transform)
        {
            Destroy(child.gameObject);
        }

        items[num] = null;
    }

}

[System.Serializable]
public class Item
{
    
    private int id;
    

    public Item(int id)
    {
        this.id = id;
    }

    public int Id { get => id;  }
}
