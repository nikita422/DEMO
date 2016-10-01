using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Dig_c : MonoBehaviour {

  
    public GameObject pref_dig;
    

    Select_zone select_zone;
    List<GameObject> list_dig;
    
	void Start () {
        print("start_dig_commader");
        list_dig = new List<GameObject>();
        select_zone = GetComponent<Select_zone>();
        select_zone.enabled = false;
	}



    public void create_new_dig(int[,] select_map, GameObject[,] go_zonee)
    {
        print("create_new_dig");
        Wave_Alg w_a = GetComponent<Wave_Alg>();
        w_a.set_primary(select_map);
        w_a.build_plan();

        GameObject dig = Instantiate(pref_dig, new Vector3(-1,-1,0), Quaternion.identity) as GameObject;
        dig.GetComponent<Dig>().set_primary(w_a.get_what_destroy(),go_zonee);
        //все другие диги поднять вверх
        list_dig.Add(dig);
      
    }
 



    void OnGUI()
    {
       
            if(GUI.Button(new Rect(10, 70, 100, 20), "STORE"))
            {
                select_zone.enabled = true;
                select_zone.one_start(false);
            }
            if (GUI.Button(new Rect(120, 70, 100, 20), "DIGGING"))
            {
                select_zone.enabled = true;
                select_zone.one_start(true);
            }

        }
        
    }

    


