using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Enter : MonoBehaviour {


    bool is_press_ok = false;
    List<Vector3> store_stack;
    map map;
    Vector3 enter;
    int count = -1;
    void Start () {
        map = GameObject.FindWithTag("MainCamera").GetComponent<map>();
    }

	
	// Update is called once per frame
	void Update () {
        if (!is_press_ok)
        {
            transform.position =new Vector2((transform_vec(Camera.main.ScreenToWorldPoint(Input.mousePosition))).x,21.5F);
            if (Input.GetMouseButtonDown(0))
            {
                is_press_ok = true;
                create_enter();
            }
        }
      

    }


      void create_enter()
    {
        Vector2 pos = camera_to_arr(transform.position);
        
        //print("create_enter");
        map.delete_block((int)pos.x+2,(int)pos.y);
        map.delete_block((int)pos.x + 3, (int)pos.y);
        enter = new Vector3((int)pos.x + 2, (int)pos.y);
        create_Stack(pos);
    }

    public Vector3 get_enter_pos()
    {
        return enter;
    }

    public Vector3 where_put_block()
    {
        count++;
        if (count > 22)
        {
            return enter;
        }
        else
        {
            return store_stack[count];
        }
    }

    public void put_block(Vector3 pos)
    {
        if (pos.z==-1)
        {
            map.create_back((int)pos.x, (int)pos.y);
        }
       else
        {
            map.create_dirt((int)pos.x, (int)pos.y);   
        }
    }


    //void OnGUI()
    //{

    //    if (GUI.Button(new Rect(10, 100, 70, 30), "OK"))      
    //    {
    //        is_press_ok = true;
    //    }
    //    if (GUI.Button(new Rect(86, 100, 70, 30), "CANCEL"))
    //    {
                
    //    }

    //}
    public Vector3 transform_vec(Vector3 camera_vec)
    {
        Vector3 int_vec = new Vector3();
        int_vec.x = (int)camera_vec.x + 0.5F;
        int_vec.y = (int)camera_vec.y + 0.5F;
        int_vec.z = -2;
        return int_vec;
    }
    public Vector3 camera_to_arr(Vector3 camera_vec)
    {
        Vector3 int_vec = new Vector3();
        int_vec.x = 25 - 1 - (int)camera_vec.y;//13-(int..
        int_vec.y = (int)camera_vec.x;
        return int_vec;
    }




    void create_Stack(Vector2 pos)
    {
        store_stack = new List<Vector3>(22);



        store_stack.Add(new Vector3( pos.x+1 , 0 ));//

        store_stack.Add(new Vector3( pos.x+1  ,pos.y-1 ,-1  ));//

        store_stack.Add(new Vector2(  pos.x+1 , pos.y+1  ));
        
        store_stack.Add(new Vector3(pos.x+1   ,pos.y-2   ,-1));//

        store_stack.Add(new Vector2(pos.x+1   ,pos.y+2   ));


        store_stack.Add(new Vector2(   pos.x+1,pos.y- 3   ));

        store_stack.Add(new Vector3(  pos.x , pos.y-2 ,-1 ));//

        store_stack.Add(new Vector3( pos.x  ,  pos.y+1,-1 ));//

        store_stack.Add(new Vector3( pos.x  , pos.y+2, -1));//

        store_stack.Add(new Vector3( pos.x  ,pos.y, -1));//10//


        store_stack.Add(new Vector2( pos.x  ,pos.y-1   ));

        store_stack.Add(new Vector2(  pos.x+1 ,  pos.y+3 ));
        
        store_stack.Add(new Vector3(  pos.x ,pos.y+3, -1));//

        store_stack.Add(new Vector2( pos.x -1 , pos.y+0  ));

        store_stack.Add(new Vector2(  pos.x-1 ,pos.y-1   ));          //15


        store_stack.Add(new Vector2( pos.x -1 ,pos.y+1   ));

        store_stack.Add(new Vector2(pos.x - 1, pos.y + 2));

        store_stack.Add(new Vector2(pos.x - 1, pos.y -2));

        store_stack.Add(new Vector2(pos.x -2, pos.y -1));//19

        store_stack.Add(new Vector2(pos.x - 2, pos.y));


        store_stack.Add(new Vector2(pos.x - 2, pos.y + 1));

        store_stack.Add(new Vector2(pos.x - 3, pos.y));





    }

}
