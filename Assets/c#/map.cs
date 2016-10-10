using UnityEngine;
using System.Collections;

public class map : MonoBehaviour {

    
    public static GameObject[,] go_dirt;
    public static int x=25, y=40;
    public float Size;
    public GameObject Pref_Dirt;
    public GameObject Pref_Ant;
    public GameObject Pref_back;
    public string text;



    void Start () {
        go_dirt = new GameObject[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                go_dirt[i, j] = null;
            }
        }
        create_map(25, 40);//не создавать первые пять рядов  
    }
    //void Update()
    //{

    //    if (Input.GetButtonUp("Jump"))
    //    {

    //        string s = "";
    //        for (int i = 0; i < x; i++)
    //        {
    //            for (int j = 0; j < y; j++)
    //            {
    //                if (go_dirt[i, j] == null)
    //                {
    //                    s += 0; s += " ";
    //                }
    //                else
    //                {
    //                    s += 1 ; s += " ";
    //                }

    //            }
    //            s += '\n';
    //        }

    //        Debug.Log(s);
    //    }
    //}
    public  GameObject[,] get_hight_map()
    {
        return go_dirt;
    }
    public void create_map(int hight, int lenght)
    {

        for (int i = 5; i < hight; i++)//первые 5 рядов - поверхность
        {
            for (int j = 0; j < lenght; j++)
            {
                create_dirt(i, j);
            }
        }

    }
    public void create_dirt(int arr_x,int arr_y)
    {
        if (go_dirt[arr_x, arr_y] == null)
        {   
            Vector2 pos = new Vector2(Size * arr_to_camera(arr_x, arr_y).x, Size * arr_to_camera(arr_x, arr_y).y);
            GameObject ground = Instantiate(Pref_Dirt, pos, Quaternion.identity) as GameObject;
            //  ground.transform.parent =GameObject.Find("back").transform;
           // ground.GetComponent<TextMesh>().text = "123123";
            go_dirt[arr_x, arr_y] = ground;   

        }
    }

    public void create_back(int x,int y)
    {
        if (go_dirt[x, y] == null)
        {
            Vector2 pos = new Vector2(x,y);
            GameObject back=Instantiate(Pref_back, pos, Quaternion.identity) as GameObject;
        }
    }




    public void create_ant(int arr_x,int arr_y)
    {
        if (go_dirt[arr_x, arr_y] == null)
        {
            Vector2 pos = new Vector2(Size * arr_to_camera(arr_x, arr_y).x, Size * arr_to_camera(arr_x, arr_y).y);
            //Pref_Ant.transform.localScale = new Vector3(Size, Size, Size);
            GameObject ant = Instantiate(Pref_Ant, pos, Quaternion.identity) as GameObject; 
        }
    }
    public void delete_block(int arr_x,int arr_y)
    {
       // print("delete_block");
        if (go_dirt[arr_x, arr_y] != null)
        {
            Destroy(go_dirt[arr_x, arr_y]);
        }
    }
    public Vector3 arr_to_camera(int arr_x,int arr_y){

        Vector3 cam_vec = new Vector3();
        cam_vec.x = arr_y+Size/2;
        cam_vec.y = x - arr_x - 1 + Size / 2;
        return cam_vec;

    }

   
}
