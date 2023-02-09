using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //기본체력
    public int startingHealth = 100;
    //현재체력
    public int currentHealth;
    //체력게이지
    public Slider healthSlider;
    //데미지를 입을때 화면을 빨갛게 만들기위한 투명 이미지
    public Image damageImkage;
    //데미지를 입었을때 재생할 오디오
    public AudioClip deathClip;
    
    Animator anim;
    //효과음 재생
    AudioSource playerAudio;
    //플레이어의 움직임 관리
    PlayerMovement PlayerMovement;
    //플레이어가 죽었는지 체크
    bool isDead;
    // Start is called before the first frame update

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        PlayerMovement = GetComponent<PlayerMovement>();
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0 && !isDead)
        {
            Death();    
        }
        else
        {
            //anim.SetTrigger("Damege");
        }
    }

    void Death()
    {
        isDead = true;
       // anim.SetTrigger("Die");
       PlayerMovement.enabled = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
