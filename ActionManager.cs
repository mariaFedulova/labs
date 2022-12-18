using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionManager : MonoBehaviour

{
    [SerializeField] bool hold;
    [SerializeField] float distance = 2f;
    RaycastHit2D hit;

    [SerializeField] GameObject pageDealer;

    [SerializeField] DealerInteraction dealer;

    public GameObject PageDealer { get => pageDealer; set => pageDealer = value; }

    // Start is called before the first frame update
    void Start()
    {
        dealer.OnPlayerDetectEvent += OpenPageDealer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!hold)
            {
                Physics2D.queriesStartInColliders = false;
                hit = Physics2D.Raycast(transform.position, Vector2.up * transform.localScale.x, distance);

                if (hit.collider != null)
                {
                    hold = true;
                }
            }

            else
            {
                hold = false;
                if (hit.collider != null)
                {
                    hit.collider.gameObject.GetComponent<OpenObjectPage>().ClosePage();
                }
            }

            if (hold)
            {
                hit.collider.gameObject.GetComponent<OpenObjectPage>().OpenPage();
            }
        }
    }

    public void OpenPageDealer() 
    {
        PageDealer.SetActive(true);
    
    }

    public void CloseDealerPage()
    {
        PageDealer.SetActive(false);
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * transform.localScale.x * distance);
    }
}
