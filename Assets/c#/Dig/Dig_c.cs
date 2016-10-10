using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Dig_c : MonoBehaviour {

  
    public GameObject pref_dig;
    
    List<GameObject> list_dig;
    
	void Start () {
      
        list_dig = new List<GameObject>();
   
	}

    
    public void create_new_dig(int[,] select_map, GameObject[,] go_zonee)
    {
        for (int i = 0; i < list_dig.Count; i++)
        {
            list_dig[i].GetComponent<Dig>().up_gui();
        }

         
        //string s = "";
        //for (int i = 0; i < 25; i++)
        //{
        //    for (int j = 0; j < 40; j++)
        //    {
        //        s += select_map[i, j].ToString(); s += " ";
        //    } s += '\n';
        //}
        //print(s);


 
        Wave_Alg w_a = GetComponent<Wave_Alg>();
        w_a.set_primary(select_map);
        w_a.build_plan();
      
        w_a.print_what_desrtoy();
        w_a.print_where_desrtoy();
        print("Dig_enter: "+ w_a.get_enter_dig().ToString());
        GameObject dig = Instantiate(pref_dig, new Vector3(-1,-1,0), Quaternion.identity) as GameObject;
        
        dig.GetComponent<Dig>().set_primary(w_a.get_what_destroy(),go_zonee,w_a.get_enter_dig(),w_a.get_where_destroy());
         
        
        list_dig.Add(dig);
        
    }
 
    }

    


