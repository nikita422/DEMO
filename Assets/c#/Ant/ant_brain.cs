using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ant_brain : MonoBehaviour
{
    public float speed;
    bool stop_now=false;
  
    public Dig my_dig;// стройка к которой будет приписан мураш
    List<Vector3> way;// путь юнита
    Vector3  oldpos, newpos;
    bool now_move = false;
   
    bool is_work=false;

    bool goo = false;
    class Work
    {
        public string what_work;
        public Vector3 position;
        public Work(string name, Vector3 pos)
        {
            what_work = name; position = new Vector3(pos.x,pos.y,pos.z);
        }
     

    }

    List<Work> list_work;
   
   void Start()
   {
       list_work = new List<Work>();
       oldpos = transform.position;
       newpos = transform.position;
   }

    void Update()
    {
        if (!is_work)
        {
            if (list_work.Count != 0)
            {
                switch (list_work[list_work.Count - 1].what_work)
                {
                    case "go_to":
                        {
                            AB_alg ab = gameObject.AddComponent<AB_alg>();
                            ab.where_to_go(transform_vec(transform.position), list_work[list_work.Count - 1].position, GetComponent<ant_brain>());
                            is_work = true;
                            goo = true;
                            list_work.RemoveAt(list_work.Count - 1);
                        }
                        break;
                    case "take":
                        {
                            //тут спросить куда положить(Склад)
                            //добавить две работы, идти до склада и положить.
                        }
                        break;
                    case "put":
                        {

                        }
                        break;
                    case "go_to_dig"://разница из за разных компонентов AB 
                        {
                            AB_alg ab = gameObject.AddComponent<AB_alg>();
                            ab.where_to_go(transform_vec(transform.position), list_work[list_work.Count - 1].position, GetComponent<ant_brain>(),true);
                            is_work = true;
                            goo = true;
                            list_work.RemoveAt(list_work.Count - 1);
                        }
                        break;
                }
                                          
            }
            else
            {
                if (my_dig)
                {
                    //сделать запрос к стройке что брать и куда нести
                    //верет вектор3: что взять, куда положить.
                    Vector3 pos=new Vector3();
                    pos= my_dig.what_destroy_next();
                    list_work.Add(new Work("go_to_dig",pos));
                    list_work.Add(new Work("take", pos));
                }

           
            //if(my_job!=null){
                // запрос к менеджеру работ
            }
        }
    }
     
    public void set_way(List<Vector3>wayy)
    {
        way = new List<Vector3>(wayy);
        if (goo)
        {
            goo = false;
            StartCoroutine(go());
        }

    }
  
     
    public void go_to(Vector2 end_pos)
    {    
        list_work.Add(new Work("go_to", end_pos)); 
    }
    public void go_to_now(Vector2 end_pos)
    {
        stop_now = true;
        list_work.Clear();
        list_work.Add(new Work("go_to", end_pos));
        stop_now = false;
        is_work = false;
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
            if (stop_now)
            {
                is_work = false;
                way.Clear();
                yield break;
            }
        }
        is_work = false;
        way.Clear();               

    }
}





