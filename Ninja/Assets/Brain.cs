using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Brain : MonoBehaviour
{
    int Dnalenth = 5;
    public DNA dNA;
    public GameObject eyes;
    bool seeupwall = false;
    bool seedownwall = false;
    bool seetop = false;
    bool seebottom = false;
    Vector3 startpos;
    public float timealive = 0;
    public float distancetravelled = 0;
    public int crash = 0;
    bool alive = true;
    Rigidbody2D rb;



    public void init()
    {
        dNA = new DNA(Dnalenth, 200);//Now we are using force from -200 to 200
        this.transform.Translate(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
        startpos = this.transform.position;
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "top" || collision.gameObject.tag == "bottom" ||
            collision.gameObject.tag == " upwall" || collision.gameObject.tag == "downwall")
        {
            crash++;
        }
        else if (collision.gameObject.tag == "dead")
        {
            alive = false;
        }

    }

    public void Update()
    {
        if (!alive)
            return;
        seeupwall = false;
        seedownwall = false;
        seetop = false;
        seebottom = false;
        RaycastHit2D hit = Physics2D.Raycast(eyes.transform.position, eyes.transform.forward, 1.0f);
        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 1.0f, Color.red);
        Debug.DrawRay(eyes.transform.position, eyes.transform.up * 1.0f, Color.red);
        Debug.DrawRay(eyes.transform.position, -eyes.transform.up * 1.0f, Color.red);
        if(hit.collider!=null)
        {
            if(hit.collider.gameObject.tag=="upwall")
            {
                seeupwall = true;
            }
            else if(hit.collider.gameObject.tag == "downwall")
            {
                seedownwall = true;
            }
        }

        hit = Physics2D.Raycast(eyes.transform.position, eyes.transform.up, 1.0f);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "top")
            {
                seetop = true;
            }
        }
        timealive =PopulationManager.elapsed;
        }
    private void FixedUpdate()
    {
        if (!alive)
            return;
        float upforce = 0f;
        float forwardforce = 1.0f;
        if (seeupwall)
            upforce = dNA.getgene(0);
        else if (seedownwall)
            upforce = dNA.getgene(1);
        else if (seetop)
            upforce = dNA.getgene(2);
        else if (seebottom)
            upforce = dNA.getgene(3);
        else
            upforce = dNA.getgene(4);//default one
        rb.AddForce(this.transform.right * forwardforce);
        rb.AddForce(this.transform.up * upforce);
        distancetravelled = Vector3.Distance(startpos, this.transform.position);

    }
    
}
