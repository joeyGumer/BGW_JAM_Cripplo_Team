using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM_RESULT
{
    ITEM_LIFE,
    ITEM_SLOW,
    ITEM_FAST,
    ITEM_MULTIPLE,
}

public struct buff
{
    buff(int a)
    {
        timer = 0;
    }

    float timer;
}

public class Item_behaviour : MonoBehaviour {

    public ITEM_RESULT type = ITEM_RESULT.ITEM_LIFE;
    bool no_more_effects = false;
    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (no_more_effects == false)
        {
            if (col.tag == "smilerang")
            {
                //Activate choose effect
                ChooseEffect();
                no_more_effects = true;
                Destroy(gameObject);
            }
        }  
    }

    void ChooseEffect()
    {
        //int result = Random.Range(0, 4);
        int result = (int)type;
        switch(result)
        {
            case 0:
                type = ITEM_RESULT.ITEM_LIFE;
                GameObject.Find("Player").GetComponent<Player_behaviour>().p_hp_current++;
                break;
            case 1:
                type = ITEM_RESULT.ITEM_SLOW;
                SmileSpeed(true);
                GameObject.Find("Player").GetComponent<Player_behaviour>().StartSmileTimer();
                break;
            case 2:
                type = ITEM_RESULT.ITEM_FAST;
                SmileSpeed(false);
                GameObject.Find("Player").GetComponent<Player_behaviour>().StartSmileTimer();
                break;
            case 3:
                type = ITEM_RESULT.ITEM_MULTIPLE;
                GameObject.Find("Player").GetComponent<Player_behaviour>().DoubleLimit();
                break;         
        }
    }

    void SmileSpeed(bool slow)
    {
       GameObject[] smiles =  GameObject.FindGameObjectsWithTag("smilerang");
        int size = smiles.GetLength(0);
       for(int i = 0; i < size; i++)
        {
            smiles[i].GetComponent<Smilerang_behaviour>().ChangeMaxSpeed(slow);
        }
    }

    public void SmileReset()
    {
        GameObject[] smiles = GameObject.FindGameObjectsWithTag("smilerang");
        int size = smiles.GetLength(0);
        for (int i = 0; i < size; i++)
        {
            smiles[i].GetComponent<Smilerang_behaviour>().s_max_velocity = smiles[i].GetComponent<Smilerang_behaviour>().s_base_max_velocity;
        }
    }
}
