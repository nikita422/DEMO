using UnityEngine;
using System.Collections;

public class Select_zone : MonoBehaviour {

   public GameObject pref_green_zone;
   public GameObject pref_selec_zone;
   GameObject[,] go_dirt;
   GameObject select_zone;//бегает за мышкой
   GameObject[,] go_zone;
   bool is_dig; // это выделение зоны для стройки? еслли нет то для склада
   int[,] select_map;
   bool now_press;
  
 
   bool begin_plan = false;// true когда начато планирование
   bool mouse_in_button = true;
	 
    public void one_start(bool is_digg)
    {
        is_dig = is_digg;
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

            is_mouse_in_button();

               
            select_zone.transform.position = transform_vec(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (Input.GetMouseButtonDown(0) && !mouse_in_button)//create
            {
                begin_plan = true;
                Vector3 vec = camera_to_arr(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                if (is_dig)
                {
                    if (go_zone[(int)vec.x, (int)vec.y] == null && go_dirt[(int)vec.x, (int)vec.y] != null)
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
                else ////////////STOREEEEE
                {
                    if (go_zone[(int)vec.x, (int)vec.y] == null && go_dirt[(int)vec.x, (int)vec.y] == null)
                    {    
                        select_map[(int)vec.x, (int)vec.y] = 2; 
                        go_zone[(int)vec.x, (int)vec.y] = Instantiate(pref_green_zone, transform_vec(Camera.main.ScreenToWorldPoint(Input.mousePosition)), Quaternion.identity) as GameObject;
                    }
                }
            }
            if (Input.GetMouseButtonDown(1) && !mouse_in_button)//delete
            {              
                Vector3 vec = camera_to_arr(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (is_dig)
                {
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
                else//////////STOOOOOOORE
                {
                    if (go_dirt[(int)vec.x, (int)vec.y] == null)
                    {
                        select_map[(int)vec.x, (int)vec.y] = 0;//?????????????????????????????????
                    }
                    else
                    {
                        select_map[(int)vec.x, (int)vec.y] = 1;//????????????????????????????
                    }
                    Destroy(go_zone[(int)vec.x, (int)vec.y]);
                }
            }
            //if (now_press)
            //{
            //    Vector3 vec = camera_to_arr(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //    map_dirt.delete_block((int)vec.x, (int)vec.y);
            //}
        }
	
   
     
    void OnGUI()
    {
        if (begin_plan)
        {
            if (GUI.Button(new Rect(10, 100, 70, 30), "OK"))      //ЕСЛИ ПРИНИМААЕМ, ТО ОТПРАВЛЯЕМ СЕЛЕКТ МЭП НАВЕРХ, 
            {
                if (is_dig) {
                                GetComponent<Dig_c>().create_new_dig(select_map,go_zone);
                }
                else
                {
                    //create_store
                }
                //все чистим
                begin_plan = false;             
                Destroy(select_zone);
                this.enabled = false;
                desrtoy_select_go();
              
            }
            if (GUI.Button(new Rect(86, 100, 70, 30), "CANCEL"))
            {  
                //все чистим
                begin_plan = false;
                Destroy(select_zone);
                this.enabled = false;
                desrtoy_select_go();
               
            }    
        }
    }

    public void desrtoy_select_go()
    {
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                if (go_zone[i, j] != null)
                {
                    Destroy(go_zone[i,j]);
                }
                
            }
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

    void is_mouse_in_button()
    {
        if (Input.mousePosition.x < 260 && Input.mousePosition.y > 180)
        {
            mouse_in_button = true;
        }
        else
        {
            mouse_in_button = false;
        }
    }
}
