using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ant_brain : MonoBehaviour
{
    public float speed;
    //bool stop_now=false;
     
    GameObject dirt_to_store;
    public Dig my_dig;// стройка к которой будет приписан мураш
    List<Vector3> way;// путь юнита
    Vector3  oldpos, newpos;
    map map;
    bool now_move = false;
    Vector3 dig_enter;
    Vector3 enter_store_pos;
    public Enter enter_store;

    //public float angle = 0;
    //public float radius = 0.6f;
    //public bool isCircle = false;


    public bool is_work=false;
    bool wait = false;
    public bool goo = false;
   public bool loaded_dirt = false;
    AB_alg ab;
    class Work
    {
        public string what_work;
        public Vector3 position;
        public int x, y;
        public Work(string name, Vector3 pos)
        {
            what_work = name; position = new Vector3(pos.x,pos.y,pos.z);
            x = (int)pos.x;
            y = (int)pos.y;
        }
        public Work(string name)
        {
            what_work = name;     
        }

    }


    public bool see_dirt = false;
   
    Queue<Work> qlist_work;
   void Start()
   {
        map = (GameObject.FindWithTag("MainCamera")).GetComponent<map>();
        dirt_to_store = Instantiate(GameObject.FindGameObjectWithTag("Dirt_enter"), transform.position, Quaternion.identity) as GameObject;
        dirt_to_store.transform.parent = this.gameObject.transform;
        dirt_to_store.transform.position +=new Vector3(0, 0, -2);
        dirt_to_store.SetActive(false);

       ab = gameObject.AddComponent<AB_alg>();
       qlist_work = new Queue<Work>();
       oldpos = transform.position;
       newpos = transform.position;
       enter_store = GameObject.FindWithTag("Enter").GetComponent<Enter>();
       enter_store_pos = enter_store.get_enter_pos();
    }

    void Update()
    {

        
          
        

        if (!is_work)
        {   
            if (qlist_work.Count != 0)
            {
                switch (qlist_work.Peek().what_work)
                {               
                    case "go_to":
                        { 
                            ab.where_to_go(transform_vec(transform.position), qlist_work.Peek().position, GetComponent<ant_brain>());
                            is_work = true;
                            goo = true;
                        }
                        break;
                    case "take":
                        {
                            loaded_dirt = true;
                            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<map>().delete_block(qlist_work.Peek().x, qlist_work.Dequeue().y);
                            dirt_to_store.SetActive(true);
                        }
                        break;
                    case "put":
                        {

                            
                        }
                        break;
                    case "put_enter":
                        {
                            loaded_dirt = false;
                            print("WORK:put_enter");   
                            dirt_to_store.SetActive(false);
                           
                            enter_store.put_block(qlist_work.Peek().position);
                            qlist_work.Dequeue();
                        }
                        break;

                }
                                          
            }
            else
            {
                if (my_dig&& !loaded_dirt)
                {
                    if ((transform_vec(transform.position).x == (int)dig_enter.x) && (transform_vec(transform.position).y == (int)dig_enter.y))
                    {
                        Vector3 pos_block = new Vector3();    pos_block = my_dig.what_destroy_next();
                        Vector3 pos_desrtoy = new Vector3();  pos_desrtoy = my_dig.where_desrtoy();
          
                        if (transform_vec(transform.position)==pos_desrtoy)
                        {
                            qlist_work.Enqueue(new Work("take", pos_block));
                        }
                        else
                        {
                            qlist_work.Enqueue(new Work("go_to", pos_desrtoy));
                            qlist_work.Enqueue(new Work("take", pos_block));  
                        }
                    }
                    else
                    {
                        qlist_work.Enqueue(new Work("go_to", dig_enter));  
                    }          
                }
                if (loaded_dirt)
                {
                    if (transform_vec(transform.position) != enter_store_pos)
                    {
                        qlist_work.Enqueue(new Work("go_to", enter_store_pos));  
                    }
                    else
                    {
                        Vector3 pos_block = new Vector3();
                        pos_block = enter_store.where_put_block();

                        Vector2 pos_desrtoy = map.where_can(pos_block);// FIND WITH FUNC
                        if(pos_desrtoy==new Vector2(0, 0))
                        {
                            print("NOT_FIND_WHERE_CAN");
                        }
                        qlist_work.Enqueue(new Work("go_to", pos_desrtoy));
                        qlist_work.Enqueue(new Work("put", pos_block));
                    }
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
        qlist_work.Enqueue(new Work("go_to", end_pos)); 
    }
    //public void go_to_now(Vector2 end_pos)
    //{
    //    stop_now = true;
    //    list_work.Clear();
    //    list_work.Add(new Work("go_to", end_pos));
    //    stop_now = false;
    //    is_work = false;
    //}
    public Vector3 transform_vec(Vector3 camera_vec)
    {
        Vector3 int_vec = new Vector3();
        int_vec.x = 25 - 1 - (int)camera_vec.y;
        int_vec.y = (int)camera_vec.x;
        return int_vec;
    }

    public void set_dig(Dig digg)
    {
        my_dig = digg;
        if (my_dig)
        {
            dig_enter = my_dig.get_enter_dig();
        }
    }

  
    IEnumerator go()
    {
        for (int i = 0; i < way.Count; i++)
        {
            do
            {

                //if (way[i] == new Vector3(1, 1, 0))
                //{
                //    angle += Time.deltaTime;

                //    var x =Mathf.Cos(angle) * radius+transform.position.x;
                //    var y =Mathf.Sin(angle ) * radius + transform.position.y;
                //    transform.position = new Vector3(x, y);
                //}
                //else
                {
                    if (way[i] == new Vector3(0, 0, 0))
                    {
                        //Debug.Log("stay");
                        break;
                    }

                    else
                    {
                        wait = false;
                    }

                    transform.Translate(way[i] * speed * Time.deltaTime);
                    newpos = transform.position;
                }
                yield return null;
                 
            } while (Vector3.Distance(oldpos, newpos) < 1);
            
            transform.position = oldpos + way[i];
            oldpos = transform.position;
            //if (stop_now)
            //{
            //    is_work = false;
            //    way.Clear();
            //    yield break;
            //}
        }
        qlist_work.Dequeue();
        is_work = false;    
        way.Clear();               

    }
}





