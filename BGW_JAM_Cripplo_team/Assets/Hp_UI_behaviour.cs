using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_UI_behaviour : MonoBehaviour {

    public GameObject player;
    Player_behaviour pl_behaviour;

    public Image hp_1, hp_2, hp_3;
    public Sprite hp_on = null;
    public Sprite hp_of = null;

    public uint hp;
	// Use this for initialization
	void Start () {
        pl_behaviour = player.gameObject.GetComponent<Player_behaviour>();
        hp = pl_behaviour.p_hp_max;
    }
	
	// Update is called once per frame
	void Update () {

        uint tmp_hp = hp;

        hp = pl_behaviour.p_hp_current;

        if(hp != tmp_hp)
        {
            if(hp < tmp_hp)
            {
                ChooseImageToChange(tmp_hp, true);
            }
            else
            {
                ChooseImageToChange(hp, false);
            }

        }
		
	}

    void ChooseImageToChange(uint h, bool lose)
    {
        switch(h)
        {
            case 1:
                ChangeImage(hp_1,lose);
                break;
            case 2:
                ChangeImage(hp_2, lose);
                break;
            case 3:
                ChangeImage(hp_3, lose);
                break;
        }
    }

    void ChangeImage(Image tmp, bool lose)
    {
        if(lose)
        {
            tmp.sprite = hp_of;
        }
        else
        {
            tmp.sprite = hp_on;
        }
    }
}
