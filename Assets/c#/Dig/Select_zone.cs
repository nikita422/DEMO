using UnityEngine;
using System.Collections;

public class Select_zone : MonoBehaviour {

   public GameObject pref_green_zone;
   public GameObject pref_red_zone;
   public GameObject pref_selec_zone;
   GameObject[,] go_dirt;
   GameObject select_zone;//бегает за мышкой
   GameObject[,] go_zone;

   int[,] select_map;
   
   public bool active = false; //используется для update
   bool already_start = false;//для конструктора
   bool begin_plan = false;// for btn OK and CANCEL 
   bool mouse_in_button = true;
	 
    void one_start()
    {


        select_map = new int[14, 40];
        go_zone = new GameObject[14, 40];
        if (GetComponent<map>().get_hight_map() != null)
        {
            go_dirt = (GameObject.FindWithTag("MainCamera")).GetComponent<map>().get_hight_map();
        }

        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                if (go_dirt[i, j] == null)
                {
                    select_map[i, j] = 0;
                }
                else
                {
                    select_map[i, j] = 1;
                }
            }
        }
        select_zone = Instantiate(pref_selec_zone, new Vector3(0, 0, 2), Quaternion.identity) as GameObject;
    }
	void Update () {
        if(active){
            
            if (Input.mousePosition.x < 260 && Input.mousePosition.y > 180)
            {
                mouse_in_button = true;
            }
            else
            {

                mouse_in_button = false;
            }



            if (!already_start)
            {
                one_start();//КОНСТРУКТОР
                already_start = true;
            }


            select_zone.transform.position = transform_vec(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (Input.GetMouseButtonDown(0) && !mouse_in_button)//create
            {
                begin_plan = true;
                Vector3 vec = camera_to_arr(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (go_zone[(int)vec.x, (int)vec.y] == null && go_dirt[(int)vec.x, (int)vec.y]!=null)
                {
                    if (is_near_zero((int)vec.x, (int)vec.y))
                    {
                        select_map[(int)vec.x, (int)vec.y] = 666;
                    }
                    else
                    {
                        select_map[(int)vec.x, (int)vec.y] = 2;
                    }               
                    go_zone[(int)vec.x, (int)vec.y] = Instantiate(pref_green_zone, transform_vec(Camera.main.ScreenToWorldPoint(Input.mousePosition)), Quaternion.identity) as GameObject;
                }
            }
            if (Input.GetMouseButtonDown(1) && !mouse_in_button)//delete
            {
                
                Vector3 vec = camera_to_arr(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (go_dirt[(int)vec.x, (int)vec.y] == null)
                {
                    select_map[(int)vec.x, (int)vec.y] = 0;
                }
                else
                {
                    select_map[(int)vec.x, (int)vec.y] = 1;
                }
                Destroy(go_zone[(int)vec.x, (int)vec.y]);
                 
            }
        }
	}

     
    void OnGUI()
    {
        if (begin_plan)
        {
            if (GUI.Button(new Rect(10, 100, 70, 30), "OK"))      //ЕСЛИ ПРИНИМААЕМ, ТО ОТПРАВЛЯЕМ СЕЛЕКТ МЭП НАВЕРХ, 
            {
                active = false;
                GetComponent<Dig_c>().create_new_dig(select_map);// можно еще отправить сюда геймобжекты чтобы выделялся склад
                //все чистим
                begin_plan = false;
                active= false;
                Destroy(select_zone);
            }
            if (GUI.Button(new Rect(86, 100, 70, 30), "CANCEL"))
            {
                active = false;
                //все чистим
                begin_plan = false;
                Destroy(select_zone);
                
            }
             
          
        }
    }

    public void set_active(bool activee){
        active=activee;
        if (activee == false)
        {
            already_start = false;
            
        }

    }
    public Vector3 transform_vec(Vector3 camera_vec)
    {
        Vector3 int_vec = new Vector3();
        int_vec.x = (int)camera_vec.x+0.5F;
        int_vec.y = (int)camera_vec.y + 0.5F;
        int_vec.z = -2;
        return int_vec;
    }
    public Vector3 camera_to_arr(Vector3 camera_vec)
    {
        Vector3 int_vec = new Vector3();
        int_vec.x = 14 - 1 - (int)camera_vec.y;//13-(int..
        int_vec.y = (int)camera_vec.x;
        return int_vec;
    }

    bool is_near_zero(int x, int y)
    {
        if (select_map[x + 1, y] == 0) return true;
        if (select_map[x, y+1] == 0) return true;
        if (select_map[x - 1, y] == 0) return true;
        if (select_map[x, y-1] == 0) return true;

        return false;
    }
}
