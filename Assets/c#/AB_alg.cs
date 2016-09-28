using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AB_alg : MonoBehaviour{


    //Начальные координаты в воздухе, за это отвечает кликер, а не алг.
    class Ghost
    {

        public int now_x, now_y;
        public List<Vector3> way;
        public Ghost(int xx, int yy)
        {
            way = new List<Vector3>();
            now_x = xx; now_y = yy;
        }
        public Ghost(int xx, int yy, List<Vector3> wayy, Vector3 next_way)
        {
            way = new List<Vector3>(wayy);
            way.Add(next_way);
            now_x = xx; now_y = yy;
        }
        public List<Vector3> get_way()
        {
            return way;
        }
    }

   
    int[,] hight_map;
    bool find=false;
    List<Ghost> list_ghost;
    List<Vector3> wayy;
 
  


    bool go;
    int x1, y1, x2, y2;

    public void where_to_go(Vector3 start_vec, Vector3 end_vec, ant_brain ant)
    {
        StartCoroutine(find_way(start_vec, end_vec, ant));
    }

    
    IEnumerator find_way(Vector3 start_vec, Vector3 end_vec, ant_brain ant)
    {
        GameObject[,] go_dirt = (GameObject.FindWithTag("MainCamera")).GetComponent<map>().get_hight_map();
            go = true;
            hight_map = new int[14, 40];
            list_ghost = new List<Ghost>();

            x1 = (int)start_vec.x;
            y1 = (int)start_vec.y;
            x2 = (int)end_vec.x;
            y2 = (int)end_vec.y;


            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    if (go_dirt[i, j] == null)
                    {
                        hight_map[i, j] = 0;
                    }
                    else
                    {
                        hight_map[i, j] = 1;
                    }
                }
            }
             yield return null;

            list_ghost.Add(new Ghost(x1, y1));
            do
            {
                yield return null;

                for (int count = 0; count < list_ghost.Count; count++)
                {

                    yield return null;
                    if (list_ghost[count].now_x == x2 && list_ghost[count].now_y == y2)
                    {
                        ant.set_way(list_ghost[count].get_way());
                        yield break;
                    }

                    int x = list_ghost[count].now_x;
                    int y = list_ghost[count].now_y;
                    if (x < 1 || x > 13 || y < 1 || y > 39)
                    {
                        list_ghost.RemoveAt(count);
                        continue;
                    }


                    //Debug.Log("i: " + count + " X:" + x + " Y:" + y);
                    hight_map[x, y] = 5;
                    int next_x = 0, next_y = 0;
                    Vector3 temp_next_way = new Vector3(0, 0, 0);
                    bool already_go = false;

                    if (hight_map[x - 1, y] == 0 && (hight_map[x - 1, y - 1] == 1 || hight_map[x - 1, y + 1] == 1))
                    {
                       // Debug.Log("8");
                        next_x = x - 1;
                        next_y = y;
                        temp_next_way = new Vector3(0, 1);
                        already_go = true;
                        hight_map[x - 1, y] = 5;

                    }

                    if (hight_map[x - 1, y + 1] == 0 && ((hight_map[x - 1, y] == 1 || hight_map[x, y + 1] == 1) && (hight_map[x - 1, y] != hight_map[x, y + 1])))
                    {//9

                        if (already_go)
                        {

                            list_ghost.Add(new Ghost(x - 1, y + 1, list_ghost[count].get_way(), new Vector3(1, 1)));

                        }
                        else
                        {

                            next_x = x - 1;
                            next_y = y + 1;

                            temp_next_way = new Vector3(1, 1);

                            already_go = true;
                        }
                        hight_map[x - 1, y + 1] = 5;
                    }

                    if (hight_map[x, y + 1] == 0 && (hight_map[x + 1, y + 1] == 1 || hight_map[x - 1, y + 1] == 1))
                    {
                       // Debug.Log("6");
                        if (already_go)
                        {

                            list_ghost.Add(new Ghost(x, y + 1, list_ghost[count].get_way(), new Vector3(1, 0)));

                        }
                        else
                        {
                            next_x = x;
                            next_y = y + 1;

                            temp_next_way = new Vector3(1, 0);

                            already_go = true;
                        }

                        hight_map[x, y + 1] = 5;
                    }


                    if (hight_map[x + 1, y + 1] == 0 && (hight_map[x + 1, y] == 1 || hight_map[x, y + 1] == 1) && (hight_map[x + 1, y] != hight_map[x, y + 1]))
                    {
                       // Debug.Log("3");
                        if (already_go)
                        {
                            list_ghost.Add(new Ghost(x + 1, y + 1, list_ghost[count].get_way(), new Vector3(1, -1)));
                        }
                        else
                        {
                            next_x = x + 1;
                            next_y = y + 1;
                            temp_next_way = new Vector3(1, -1);
                            already_go = true;
                        }
                        hight_map[x + 1, y + 1] = 5;
                    }


                    if (hight_map[x + 1, y] == 0 && (hight_map[x + 1, y + 1] == 1 || hight_map[x + 1, y - 1] == 1))
                    {//2
                        if (already_go)
                        {

                            list_ghost.Add(new Ghost(x + 1, y, list_ghost[count].get_way(), new Vector3(0, -1)));

                        }
                        else
                        {
                            next_x = x + 1;
                            next_y = y;
                            temp_next_way = new Vector3(0, -1);
                            already_go = true;
                        }
                        hight_map[x + 1, y] = 5;
                    }

                    if (hight_map[x + 1, y - 1] == 0 && (hight_map[x, y - 1] == 1 || hight_map[x + 1, y] == 1) && (hight_map[x, y - 1] != hight_map[x + 1, y]))
                    {//1
                        if (already_go)
                        {

                            list_ghost.Add(new Ghost(x + 1, y - 1, list_ghost[count].get_way(), new Vector3(-1, -1)));

                        }
                        else
                        {
                            next_x = x + 1;
                            next_y = y - 1;
                            temp_next_way = new Vector3(-1, -1);
                            already_go = true;
                        }
                        hight_map[x + 1, y - 1] = 5;
                    }

                    if (hight_map[x, y - 1] == 0 && (hight_map[x + 1, y - 1] == 1 || hight_map[x - 1, y - 1] == 1))
                    {//4

                        if (already_go)
                        {


                            list_ghost.Add(new Ghost(x, y - 1, list_ghost[count].get_way(), new Vector3(-1, 0)));

                        }
                        else
                        {
                            next_x = x;
                            next_y = y - 1;
                            temp_next_way = new Vector3(-1, 0);
                            already_go = true;
                        }
                        hight_map[x, y - 1] = 5;
                    }

                    if (hight_map[x - 1, y - 1] == 0 && (hight_map[x, y - 1] == 1 || hight_map[x - 1, y] == 1) && (hight_map[x, y - 1] != hight_map[x - 1, y]))
                    {//7

                        if (already_go)
                        {

                            list_ghost.Add(new Ghost(x - 1, y - 1, list_ghost[count].get_way(), new Vector3(-1, 1)));

                        }
                        else
                        {
                            next_x = x - 1;
                            next_y = y - 1;

                            temp_next_way = new Vector3(-1, 1);
                            already_go = true;
                        }
                        hight_map[x - 1, y - 1] = 5;
                    }

                    if (!already_go)
                    {
                        //desrtoy ghost
                        list_ghost.RemoveAt(count);
                        continue;
                    }
                    list_ghost[count].way.Add(temp_next_way);
                    list_ghost[count].now_x = next_x;
                    list_ghost[count].now_y = next_y;
                }
                if (list_ghost.Count == 0)
                {
                    go = false;
                }
                
            } while (go);
            //not find

            List<Vector3> list = new List<Vector3>(1);
            list.Add(new Vector3(0, 0, 0));


            ant.set_way(list);
            yield break;
           
        }

    public void test()
    {
        print("get_comp<ab>_OK!");
    }

    }






