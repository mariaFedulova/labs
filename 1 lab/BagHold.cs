using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagHold : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] Sprite spriteHold;
    [SerializeField] Sprite spriteOut;

    [SerializeField] bool hold;
    [SerializeField] float distance = 2f;
    RaycastHit2D hit;
    [SerializeField] Transform holdPoint;

    private GameObject gameObjectFromInventary;

    public Transform HoldPoint { get => holdPoint; set => holdPoint = value; }
    public bool Hold { get => hold; set => hold = value; }
    public GameObject GameObjectFromInventary { get => gameObjectFromInventary; set => gameObjectFromInventary = value; }

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!Hold)
            {
                Physics2D.queriesStartInColliders = false;
                hit = Physics2D.Raycast(transform.position, Vector2.up * transform.localScale.x, distance);

                if (hit.collider != null)
                {
                    Hold = true;     
                }
            }
            else
            {
                Hold = false;

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                    {
                        hit.collider.gameObject.GetComponent<Rigidbody2D>().transform.position = new Vector2(transform.localScale.x, 1) * 2;
                        if (hit.collider.gameObject.CompareTag("GoldenBag"))
                            hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = spriteOut;
                    }
                }
                else
                {
                    if (GameObjectFromInventary != null)
                    {
                        GameObjectFromInventary.transform.position = new Vector2(transform.localScale.x, 1) * 2;
                        if (GameObjectFromInventary.CompareTag("GoldenBag")) GameObjectFromInventary.GetComponent<SpriteRenderer>().sprite = spriteOut;
                    }
                    
                }
            }
        }
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("GoldenBag"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (Hold) { hit.collider.gameObject.GetComponent<GoldenBag>().Money += 10; MoneyCollect.MoneyCount -= 10; }
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (Hold) { hit.collider.gameObject.GetComponent<GoldenBag>().Money -= 10; MoneyCollect.MoneyCount += 10; }
                }
            }
        }
        else
        {
            if (GameObjectFromInventary != null)
            {
                if (GameObjectFromInventary.CompareTag("GoldenBag"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (Hold) { gameObjectFromInventary.GetComponent<GoldenBag>().Money += 10; MoneyCollect.MoneyCount -= 10; }
                    }
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        if (Hold) { gameObjectFromInventary.GetComponent<GoldenBag>().Money -= 10; MoneyCollect.MoneyCount += 10; }
                    }
                }
            }
            
        }
       
        
        if (Hold)
        {
           if(hit.collider != null) 
                hit.collider.gameObject.transform.position = HoldPoint.position;

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("GoldenBag"))
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = spriteHold;
            }
            else
            {
                if (GameObjectFromInventary != null)
                {
                    if (gameObjectFromInventary.CompareTag("GoldenBag"))
                        gameObjectFromInventary.GetComponent<SpriteRenderer>().sprite = spriteHold;
                }
                
            }
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                for (int i = 0; i < inventory.Slots.Length; i++)
                {
                    if (inventory.IsFull[i] == false)
                    {
                        inventory.IsFull[i] = true;

                        inventory.SetInSlot(hit.collider.gameObject.GetComponent<SlotButton>().Id, i);
                        Destroy(hit.collider.gameObject);

                        Hold = false;
                        break;
                    }
                }
            }
        }        
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * transform.localScale.x * distance);
    }

}
