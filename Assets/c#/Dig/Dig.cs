using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Dig : MonoBehaviour {
    //delta 60
    /*
     добавить появление плана при наведение мышкой на иконку
     */
    List<ant_brain> worker;
    List<Vector3> desrtoy_stack;
    List<Vector3> where_desrtoy_stack;
    Vector3 dig_enter;
    GameObject[,] go_zone;

    int count = -1;
    int worker_now = 0;
    public float vert = 266F;
    public float old_vert = 266F;
    public float speed=20F;
    bool up = false;
    public bool toggle_visible_plan;

    public  void set_primary(List<Vector3> desrtoy_stackk, GameObject[,] go_zonee, Vector3 dig_enterr, List<Vector3> where_desrtoyy)
    {
        go_zone = new GameObject[25, 40];
        for (int i = 0; i < 25; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                go_zone[i, j] = go_zonee[i,j];
            }
        }
        //print("set_primary");
        desrtoy_stack = new List<Vector3>(desrtoy_stackk);
        where_desrtoy_stack = new List<Vector3>(where_desrtoyy);
        dig_enter = dig_enterr;
    }
   
    public Vector3 what_destroy_next()
    {
        //print("what_desrtoy_next");
        count++;  
         
        if (desrtoy_stack.Count < count)
        {
            Destroy(this);
        }
        print("desrtoy_next_block:" + desrtoy_stack[count].x + " " + desrtoy_stack[count].y);
        return desrtoy_stack[count];
    }
    public Vector3  where_desrtoy()
    {         
        if (where_desrtoy_stack.Count < count)
        {
            Destroy(this);
        }
        return where_desrtoy_stack[count];
    }

    public void up_gui()
    {
        up = true;
    }
    void OnGUI()
    {
        if (up)
        {
            speed = 8;
            vert -= speed;
            if (old_vert - 60 > vert)
            {
                up = false;
                old_vert = vert;
            }
        }

        if (GUI.Button(new Rect(10, vert, 50, 50), worker_now.ToString()))
        {
            print(dig_enter.ToString());
            print("press digging"); 
            worker = new List<ant_brain>(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UnitSelectionComponent>().get_selected_units());
            if (worker != null)
            {
                print("add "+ worker.Count);
            }
            for (int i = 0; i < worker.Count; i++)
            {
                worker[i].set_dig(this,dig_enter);
            }
            worker_now = worker.Count;
        }
        //toggle_visible_plan = GUI.Toggle(new Rect(60, vert, 50, 50), toggle_visible_plan, "");
        //if (toggle_visible_plan)
        //{
        //    print("fggfg");
        //    for (int i = 0; i < 14; i++)
        //    {
        //        for (int j = 0; j < 40; j++)
        //        {
        //            if (go_zone[i, j] != null)
        //        {
        //            go_zone[i, j].SetActive(toggle_visible_plan);
        //        }   
        //        }
        //    }
        //}
    }
 
}
