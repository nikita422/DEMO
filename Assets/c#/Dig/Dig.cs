using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Dig : MonoBehaviour {
    //delta 60
    List<ant_brain> worker;
    List<Vector3> desrtoy_stack;
    GameObject[,] go_zone;
    int count = -1;
    int worker_now = 0;
    public float vert = 230F;
    public float old_vert=230F;
    public float speed=20F;
    bool up = false;
    public  void set_primary(List<Vector3> desrtoy_stackk, GameObject[,] go_zonee)
    {
        go_zone = new GameObject[14, 40];
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                go_zone[i, j] = go_zonee[i,j];
            }
        }
        print("set_primary");
        desrtoy_stack = new List<Vector3>(desrtoy_stackk);
        
    }
    void Update()
    {
        if (up)
        {
            speed = 40;
            vert -= speed * Time.deltaTime;
            if (old_vert - 60 > vert)
            {
                up = false;
                old_vert = vert;
            }
        }
    }
    public Vector3 what_destroy_next()
    {
        print("what_desrtoy_next");
        count++;  
        //просто возращаем местположение блока, который надо разрушить
        //мураш сам найдет путь, как дойдет, возмет блок и будет считать путь до склада.
        if (desrtoy_stack.Count < count)
        {
            //desrtoy this dig
        }
        return desrtoy_stack[count];
    }

    void OnGUI()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            up = true;
        }
        if (GUI.Button(new Rect(10, vert, 50, 50), worker_now.ToString()))
        {
            print("press digging"); 
            worker = new List<ant_brain>(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UnitSelectionComponent>().get_selected_units());
            if (worker != null)
            {
                print("add "+ worker.Count);
            }
            for (int i = 0; i < worker.Count; i++)
            {
                worker[i].set_dig(this);
            }
            worker_now = worker.Count;
        }
    }
   


}
