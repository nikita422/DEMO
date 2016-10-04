using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Dig_c : MonoBehaviour {

  
    public GameObject pref_dig;
    
    List<GameObject> list_dig;
    
	void Start () {
        print("start_dig_commader");
        list_dig = new List<GameObject>();
   
	}

    
    public void create_new_dig(int[,] select_map, GameObject[,] go_zonee)
    {
        for (int i = 0; i < list_dig.Count; i++)
        {
            list_dig[i].GetComponent<Dig>().up_gui();
        }
        print("create_new_dig");
        Wave_Alg w_a = GetComponent<Wave_Alg>();
        w_a.set_primary(select_map);
        w_a.build_plan();

        GameObject dig = Instantiate(pref_dig, new Vector3(-1,-1,0), Quaternion.identity) as GameObject;
        dig.GetComponent<Dig>().set_primary(w_a.get_what_destroy(),go_zonee);
         
        
        list_dig.Add(dig);
        
    }
 
    }

    


