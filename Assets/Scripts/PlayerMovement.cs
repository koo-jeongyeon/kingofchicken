using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    private Animator avatar;
    private Rigidbody rigidbody;

    private float lastAttackTime, lastSkillTime, lastRunTime;

    public bool attacking = false;
    
    public bool running = false;

    public bool skill = false;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        avatar = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        speed = 2f;
    }
    
    //h: Horizontal
    //v: Vertical
    float h, v;

    public void OnStickChanged(Vector2 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }
    public void OnKeybordChanged()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }
    // Update is called once per frame
    void Update()
    {
        OnKeybordChanged();
        if (avatar)
        {
            avatar.SetFloat("Speed", (speed*(h*h+v*v)));
            if (rigidbody)
            {
                Vector3 velocity = new Vector3(h, 0, v);
                velocity *= speed;
                rigidbody.velocity = velocity;
                
                if (h != 0 && v != 0)
                {
                    transform.rotation = Quaternion.LookRotation(velocity);
                }
            }    
        }
        
    }

    public void OnRunDown()
    {
        running = true;
        speed = 10f;
        StartCoroutine(StartRun());
    }

    public void OnRunUp()
    {
        avatar.SetBool("Run",false);
        running = false;
        speed = 2f;
    }

    IEnumerator StartRun()
    {
        if (Time.time - lastSkillTime > 1f)
        {
            lastSkillTime = Time.time;
            while (running)
            {
                avatar.SetBool("Run",true);
                yield return new WaitForSeconds(1f);
            }
        }

    }
    
    public void OnSkillDown()
    {
        if (Time.time - lastSkillTime > 1f)
        {
            avatar.SetTrigger("Skill");
            skill = true;
            lastSkillTime = Time.time;
        };
    }

    public void OnSkillOn()
    {
        skill = false;
    }
    
    
    
}
