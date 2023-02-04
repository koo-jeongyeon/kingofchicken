using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    private Animator avatar;
    private Rigidbody rigidbody;

    private float lastAttackTime, lastSkillTime, lastDashTime;

    public bool attacking = false;

    public bool dashing = false;

    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        avatar = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        Speed = 2f;
    }
    
    //h: Horizontal
    //v: Vertical
    private float h, v;

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
            avatar.SetFloat("Speed", (Speed*(h*h+v*v)));
            if (rigidbody)
            {
                Vector3 velocity = new Vector3(h, 0, v);
                velocity *= Speed;
                rigidbody.velocity = velocity;
                
                if (h != 0 && v != 0)
                {
                    transform.rotation = Quaternion.LookRotation(velocity);
                }
            }    
        }
        
    }
}
