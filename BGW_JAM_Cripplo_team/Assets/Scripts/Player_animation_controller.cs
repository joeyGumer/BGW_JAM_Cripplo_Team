using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_animation_controller : MonoBehaviour
{
    Player_behaviour player_behaviour;
    Animator animator;
    PLAYER_STATE prev_state;
    PLAYER_DIRECTION prev_dir;
    bool facing_left = false;

	// Use this for initialization
	void Start ()
    {
        player_behaviour = GetComponentInParent<Player_behaviour>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*
        animator.SetBool("left", false);
        animator.SetBool("right", false);
        animator.SetBool("up", false);
        animator.SetBool("down", false);
        switch (player_behaviour.p_dir_state)
        {
            case (PLAYER_DIRECTION.P_UP):
                animator.SetBool("up", true);
                break;
            case (PLAYER_DIRECTION.P_DOWN):
                animator.SetBool("down", true);
                break;
            case (PLAYER_DIRECTION.P_RIGHT):
                animator.SetBool("right", true);
                break;
            case (PLAYER_DIRECTION.P_LEFT):
                animator.SetBool("left", true);
                break;
        }
        */
        animator.SetBool("boomerang_available", player_behaviour.p_boomerang_current == 0);
        if (prev_state != player_behaviour.p_state || prev_dir != player_behaviour.p_dir_state)
            StateChange();

        prev_state = player_behaviour.p_state;
        prev_dir = player_behaviour.p_dir_state;
	}

    void StateChange()
    {
        switch(player_behaviour.p_state)
        {
            case (PLAYER_STATE.P_IDLE):
                PlayAnimationStart("Idle");
                break;
            case (PLAYER_STATE.P_WALK):
                PlayAnimationStart("Walk");
                break;
            case (PLAYER_STATE.P_ATTACK):
                break;
            case (PLAYER_STATE.P_HURT):
                break;
        }
    }

    void PlayAnimationStart(string next_anim)
    {
        switch (player_behaviour.p_dir_state)
        {
            case (PLAYER_DIRECTION.P_UP):
                PlayAnimation(next_anim, "Back");
                break;
            case (PLAYER_DIRECTION.P_DOWN):
                PlayAnimation(next_anim, "Front");
                break;
            case (PLAYER_DIRECTION.P_RIGHT):
                if (facing_left)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    facing_left = false;
                }
                PlayAnimation(next_anim, "Side");
                break;
            case (PLAYER_DIRECTION.P_LEFT):
                if (!facing_left)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    facing_left = true;
                }
                PlayAnimation(next_anim, "Side");
                break;
        }
    }

    void PlayAnimation(string next_anim, string side)
    {
        if (animator.GetBool("boomerang_available"))
        {
            animator.Play(next_anim+side+"_B");
        }

        else
        {
            animator.Play(next_anim + side + "_NoB");
        }
    }
}
