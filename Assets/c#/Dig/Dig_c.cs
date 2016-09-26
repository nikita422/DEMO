using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Dig_c : MonoBehaviour {

     //все стройки
    public GameObject pref_dig;
    bool active = false;

   
  

    Select_zone select_zone;
    List<GameObject> list_dig;
    
	void Start () {
        list_dig = new List<GameObject>();
        select_zone = GetComponent<Select_zone>();
	}



    public void create_new_dig(int[,] select_map)
    {
        print("create_new_dig");
        Wave_Alg w_a = GetComponent<Wave_Alg>();
        w_a.set_primary(select_map);
        w_a.build_plan();

        GameObject dig = Instantiate(pref_dig, new Vector3(-1,-1,0), Quaternion.identity) as GameObject;
        dig.GetComponent<Dig>().set_primary(w_a.get_what_destroy());
        list_dig.Add(dig);
        //if (w_a.get_what_destroy() != null)
        //{
        //    List<Vector3> w_d = new List<Vector3>(w_a.get_what_destroy());
        //    for (int i = 0; i < w_d.Count; i++)
        //    {
        //        print(i+") "+w_d[i].x + " " + w_d[i].y);
        //    }
        //}
        //new_dig.set_primary(w_a.get_what_destroy()) != null
      // list_dig.Add(new_dig);
    }

	 
	void Update () {
        if (active)
        {
           
        }      
	}




    void OnGUI()
    {
        
        if (active)
        {

            if(GUI.Button(new Rect(10, 70, 100, 20), "STORE"))
            {

            }
            if (GUI.Button(new Rect(120, 70, 100, 20), "DIGGING"))
            {
                select_zone.set_active(true);
            }

            

        }
        //if(есть хоть один блок в плане) вывести галочку принять или отменить, если принять,возвращаем инт,делаем карту и кидаем все в 
        // в отдельный диг, и создаем новый гуи иконку склада, хотя это можно и на сам диг отдать.хх


        //if (GUI.Button(new Rect(10, 400, 30, 30), "digging"))
        //{
        //    print("press digging");
        //    //worker = new List<ant_brain>(GetComponent<UnitSelectionComponent>().get_selected_units());
        //   // if (worker != null)
        //    {
        //        print("add");
        //    }

        //}
    }

   public void set_active(bool activee)
    {
        active = activee;
        if (activee == false)
        {
            select_zone.set_active(false);
        }
    }

}
