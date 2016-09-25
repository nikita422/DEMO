using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Dig_c : MonoBehaviour {

     //все стройки
    bool active = false;
    public int toolbar_type_plan;
    public string[] toolbartp = new string[] {"STORE" ,"DIGGING"};
    Select_zone select_zone;
    List<Dig> list_dig;
    Dig dig;
	void Start () {
        List<Dig> list_dig=new List<Dig>();
	}



    public void create_new_dig(int[,] select_map)
    {
        Wave_Alg w_a = GetComponent<Wave_Alg>();
        w_a.set_primary(select_map);
        w_a.build_plan();
        Dig new_dig = GetComponent<Dig>();
        new_dig.set_primary(w_a.get_what_destroy());
        list_dig.Add(new_dig);
    }

	 
	void Update () {
        if (active)
        {
            select_zone = GetComponent<Select_zone>();
            switch (toolbar_type_plan)
            {
                case 0:///store
                    {
                       
                    }
                    break;
                case 1:///digging
                    {
                        select_zone.set_active(true);
                    }
                    break;
            }


        }
	}




    void OnGUI()
    {
        if (active)
        {
            toolbar_type_plan = GUI.Toolbar(new Rect(10, 70, 150, 20), toolbar_type_plan, toolbartp);
        }
        //if(есть хоть один блок в плане) вывести галочку принять или отменить, если принять,возвращаем инт,делаем карту и кидаем все в 
        // в отдельный диг, и создаем новый гуи иконку склада, хотя это можно и на сам диг отдать.хх
    }

   public void set_active(bool activee)
    {
        active = activee;
    }

}
