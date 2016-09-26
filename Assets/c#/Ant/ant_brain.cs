using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ant_brain : MonoBehaviour
{
    public float speed;
    public Vector3 what_desrtoy;
    public Dig my_dig;// стройка к которой будет приписан мураш
    

    List<Vector3> way;// путь юнита
    Vector3  oldpos, newpos;
    bool now_move;
    bool b_if_way = false;
    bool work;
    bool is_digging=false;
   void Start()
   {
       oldpos = transform.position;
       newpos = transform.position;
       now_move = false;
   }

    void Update()
    {
        if (b_if_way && !now_move)
        {
            now_move = true;
            StartCoroutine(go());
        }
        if (my_dig != null && !work)
        {
            b_if_way = true;
            way = new List<Vector3>();
            way.Add(my_dig.what_destroy_next());//пойдет к блоку на стройке
            is_digging = true;
        }
        if (my_dig != null && is_digging  && !now_move)
        {
            //разрушить блок
            //найти склад куда нести
        }

    }
     
    public void set_way(List<Vector3>wayy)
    {
        b_if_way = true;

        is_digging = false;//если вдруг выделили его и "сбили с пути"
        my_dig = null;

       // Debug.Log("ant_brain_set_way:"+ wayy.Count);
        way = new List<Vector3>(wayy);
    }
     
    public void go_to(Vector2 end_pos)
    {
        AB_alg ab = gameObject.AddComponent<AB_alg>();
        ab.where_to_go(transform_vec(transform.position), end_pos, GetComponent<ant_brain>());
    }
    public Vector3 transform_vec(Vector3 camera_vec)
    {
        Vector3 int_vec = new Vector3();
        int_vec.x = 14 - 1 - (int)camera_vec.y;
        int_vec.y = (int)camera_vec.x;
        return int_vec;
    }

    public void set_dig(Dig digg)
    {
        my_dig = digg;
    }


    IEnumerator go()
    {
        for (int i = 0; i < way.Count; i++)
        {
            do
            {
                if (way[i] == new Vector3(0, 0, 0))
                {
                    Debug.Log("stay");
                    break;
                }
                transform.Translate(way[i] * speed * Time.deltaTime);
                newpos = transform.position;
                yield return null;
                
            } while (Vector3.Distance(oldpos, newpos) < 1);
            transform.position = oldpos + way[i];
            oldpos = transform.position;
        }
        now_move = false;
        b_if_way = false;
        way.Clear();
    }
}





