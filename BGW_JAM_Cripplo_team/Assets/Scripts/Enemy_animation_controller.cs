using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_animation_controller : MonoBehaviour {

    Enemy_Behaviour enemy_behaviour;
    Animator animator;
    Enemy_state prev_state;
    Enemy_direction prev_dir;
    bool facing_left = false;

    // Use this for initialization
    void Start()
    {
        enemy_behaviour = GetComponentInParent<Enemy_Behaviour>();
        animator = GetComponent<Animator>();
        //animator.Play("IdleFront");
        //prev_state = Enemy_state.E_IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        if (prev_state != enemy_behaviour.e_state || prev_dir != enemy_behaviour.e_dir_state)
            StateChange();

        prev_state = enemy_behaviour.e_state;
        prev_dir = enemy_behaviour.e_dir_state;
    }

    void StateChange()
    {
        switch (enemy_behaviour.e_state)
        {
            case (Enemy_state.E_IDLE):
                PlayAnimationStart("Idle");
                break;
            case (Enemy_state.E_WALK):
                PlayAnimationStart("Walk");
                break;
            case (Enemy_state.E_HAPPY):
                animator.Play("Happy");
                break;
        }
    }

    void PlayAnimationStart(string next_anim)
    {
        switch (enemy_behaviour.e_dir_state)
        {
            case (Enemy_direction.E_UP):
                animator.Play(next_anim + "Back");
                break;
            case (Enemy_direction.E_DOWN):
                animator.Play(next_anim + "Front");
                break;
            case (Enemy_direction.E_RIGHT):
                if (facing_left)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    facing_left = false;
                }
                animator.Play(next_anim + "Side");
                break;
            case (Enemy_direction.E_LEFT):
                if (!facing_left)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    facing_left = true;
                }
                animator.Play(next_anim + "Side");
                break;
        }
    }
}
