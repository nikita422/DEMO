using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Store : MonoBehaviour {

    List<ant_brain> worker;
    List<Vector3> shelf_stack;
    GameObject[,] go_zone;
    int count = -1;
    int worker_now = 0;
    public float vert = 266F;
    public float old_vert = 266F;
    public float speed = 20F;
    bool up = false;
    /*
      
     при выборе работы, просто все склады подсвечиваются и ты выбираешь куда именно тоскать еду, появляется иконка и на нее уже назначаешь рабочих
      
     */
    public void set_primary(List<Vector3> shelf_stackk, GameObject[,] go_zonee)
    {
        go_zone = new GameObject[14, 40];
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                go_zone[i, j] = go_zonee[i, j];
            }
        }
        print("set_primary_store");
        shelf_stack = new List<Vector3>(shelf_stackk);
    }
    public Vector3 what_shelf_next()
    {
        print("what_shelf_next");
        count++;
         
        if (shelf_stack.Count < count)
        {
            Destroy(this);
        }
        return shelf_stack[count];
    }






}
