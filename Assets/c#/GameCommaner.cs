using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameCommaner : MonoBehaviour {
    public GameObject pref_enter;
    public int toolbar_mode = 0;
    public int toolbar_block = 0;
    public string[] toolbarblock = new string[] {"DIRT", "ANT"};
    public string[] toolbarmode = new string[] { "debug", "CHOOSE","CREATE"};

    Dig_c dig_c;
    Store_c store_c;
    public bool now_press = false;
    public bool mouse_in_button;

    Vector3 mousep;
    Vector3 int_mousep;

    UnitSelectionComponent unitselecomp;
    Select_zone select_zone;

    GameObject Enter;
    bool already_enter = false;
     /*
      
      уходи от тулбаров и переходи на кнопки
      * 
      */
   map map_dirt;
    int x=25;
    int y=40;//40
  	void Start () {
        map_dirt = GetComponent<map>();
        unitselecomp = GetComponent<UnitSelectionComponent>();
        toolbar_mode = 1;
        select_zone = GetComponent<Select_zone>();
	}
	
	
	void Update () {
        //mousep = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //int_mousep = camera_to_arr(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        is_mouse_in_button();
        


        if (toolbar_mode==0)//CREATE
        {
            unitselecomp.enabled = false;
            
            if (Input.GetMouseButtonDown(0) && !mouse_in_button)
            {
                now_press = true;
            }
            if (Input.GetMouseButtonDown(1) && !mouse_in_button)//CREATE
            {
               
                if (toolbar_block == 1)//ANT
                {
                    Vector3 vec = camera_to_arr(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    map_dirt.create_ant((int)vec.x, (int)vec.y);
                }

                if (toolbar_block == 0)//DIRT
                {
                    Vector3 vec = camera_to_arr(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    map_dirt.create_dirt((int)vec.x, (int)vec.y);
                }
            }
            if (Input.GetMouseButtonUp(0) && !mouse_in_button)
            {
                now_press = false;
            }
        }
        if (toolbar_mode==1)//CHOOSE
        {
            unitselecomp.enabled = true;
            if (Input.GetMouseButtonDown(1) && !mouse_in_button)//R
            {
              Vector3 end_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
              List<ant_brain> selec_unit_brain = unitselecomp.get_selected_units();
              for (int i = 0; i < selec_unit_brain.Count; i++)
              {
                  
                  selec_unit_brain[i].go_to(camera_to_arr(end_pos));
              }
            }
        }

        if (now_press)
        {
            Vector3 vec = camera_to_arr(Camera.main.ScreenToWorldPoint(Input.mousePosition));      
            map_dirt.delete_block((int)vec.x, (int)vec.y);
        }



	}

    void OnGUI()
    {
       
    toolbar_mode = GUI.Toolbar(new Rect(10, 20, 240, 40), toolbar_mode, toolbarmode);

        if (toolbar_mode == 0)
        {
            toolbar_block = GUI.Toolbar(new Rect(10, 70, 150, 20), toolbar_block, toolbarblock);
        }
        if (toolbar_mode == 2)
        {
            if (GUI.Button(new Rect(10, 70, 100, 20), "STORE"))
            {
                select_zone.one_start("Store");
            }
            if (GUI.Button(new Rect(120, 70, 100, 20), "DIGGING"))
            {
                select_zone.one_start("Dig");
            }
            if (GUI.Button(new Rect(230, 70, 100, 20), "ENTER"))
            {
                
                if (!already_enter)
                {
                    already_enter = true;
                     
                    Enter = Instantiate(pref_enter, new Vector3(-1, -1, 4), Quaternion.identity) as GameObject;
                }
            }
        }

        //GUI.TextArea(new Rect(100, 100, 100, 100), mousep.ToString());
        //GUI.TextArea(new Rect(0, 100, 100, 100), int_mousep.ToString());
    }

 
    public Vector3 camera_to_arr(Vector3 camera_vec)
    {
        Vector3 int_vec = new Vector3();
        int_vec.x = 25 - 1 - (int)camera_vec.y;//13-(int..
        int_vec.y = (int)camera_vec.x;
        return int_vec;
    }

    void is_mouse_in_button()
    {
        if ((Input.mousePosition.x < 260 && Input.mousePosition.y > 230) || (Input.mousePosition.x < 60 && Input.mousePosition.y < 300))
        {
            mouse_in_button = true;
        }
        else
        {
            mouse_in_button = false;
        }


    }
}
