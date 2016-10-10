using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Wave_Alg : MonoBehaviour {


    int[,] dig_plan;
    List<Vector3> now_block;
    List<Vector3> what_desrtoy;
    List<Vector3> where_desrtoy;
    int x, y;

    Vector3 dig_enter;
    bool alreay_enter = false;

    public void set_primary(int[,] plan)
    {
        x = 25; y = 40;
        now_block = new List<Vector3>();
        what_desrtoy = new List<Vector3>();
        where_desrtoy = new List<Vector3>();
        dig_plan = new int[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                dig_plan[i, j] = plan[i, j];
                if (plan[i, j] == 666)
                {
         
                    if (!alreay_enter)
                    {
                        alreay_enter = true;
                        
                        where_enter(plan, i, j);
                    }

                    now_block.Add(new Vector3(i, j));
                    dig_plan[i, j] = 0;///////////
                }
            }
        }
        for (int k = 0; k < now_block.Count; k++)
        {
            what_desrtoy.Add(now_block[k]);
        }
    }
       public List<Vector3> get_what_destroy()
        {
            return what_desrtoy;
        }
       public List<Vector3> get_where_destroy()
       {
           return where_desrtoy;
       }
       public  Vector3  get_enter_dig()
       {
           return dig_enter;
       }


       public void set_primary(int[,] plan, Vector2 start_block)
       {
           x = 14; y = 40;
           now_block = new List<Vector3>();
           what_desrtoy = new List<Vector3>();

           dig_plan = new int[x, y];
           now_block.Add(start_block);
           what_desrtoy.Add(start_block);
       }
       public void build_plan()
       {
           int nomer = 3;
           bool end = false;
           do
           {
               List<Vector3> temp = new List<Vector3>();

               for (int count = 0; count < now_block.Count; count++)
               {
                   int x = (int)now_block[count].x;
                   int y = (int)now_block[count].y;

               

                               if (dig_plan[x - 1, y] == 2)//8
                               {
                                   dig_plan[x - 1, y] = nomer; nomer++;
                                   temp.Add(new Vector3(x - 1, y));
                                   what_desrtoy.Add(new Vector3(x - 1, y));
                               }

                               if (dig_plan[x, y + 1] == 2)//6
                               {
                                   dig_plan[x, y + 1] = nomer; nomer++;
                                   temp.Add(new Vector3(x, y + 1));
                                    what_desrtoy.Add(new Vector3(x, y + 1));
                               }

                             
                               if (dig_plan[x + 1, y] == 2)//2
                               {
                                   dig_plan[x + 1, y] = nomer; nomer++;
                                   temp.Add(new Vector3(x + 1, y));
                                    what_desrtoy.Add(new Vector3(x + 1, y));
                               }
                              
                               if (dig_plan[x, y - 1] == 2)//4
                               {
                                   dig_plan[x, y - 1] = nomer; nomer++;
                                   temp.Add(new Vector3(x, y - 1));
                                   what_desrtoy.Add(new Vector3(x, y - 1));
                               }
               }
               now_block = temp;
               if (temp.Count == 0)
               {
                   end = true;
                   where_desrtoy.Add(dig_enter);
                   for (int i = 0; i < what_desrtoy.Count; i++)
                   {
                       where_desrtoy.Add(what_desrtoy[i]);
                   }
               }
              
           } while (!end);
       }

       public void print_map()
       {
           print("print_map");
           string s="";
           for (int i = 0; i < 14; i++)
           {
               for (int j = 0; j < 40; j++)
               {
                   s+= dig_plan[i, j].ToString(); s += " ";
               } s += '\n';
           }
           print(s);
       }
       public void print_what_desrtoy()
       {
           string s = "Desrtoy_block: ";
           for (int i = 0; i < what_desrtoy.Count; i++)
           {
               s += what_desrtoy[i].x + ", " + what_desrtoy[i].y; s += "    ";
           }
           print(s);
       }


    public void print_where_desrtoy()
    {
        string s = "Where_desrtoy: ";
        for (int i = 0; i < where_desrtoy.Count; i++)
        {
            s += where_desrtoy[i].x + ", " + where_desrtoy[i].y; s += "    ";
        }
        print(s);

    }
       void where_enter(int[,] plan,int x,int y)
       {
 
           if (plan[x - 1, y] == 0)
           {
               if (plan[x - 1, y - 1] == 1 || plan[x - 1, y + 1] == 1) dig_enter=new Vector3(x - 1, y);  
           }
           if (plan[x, y + 1] == 0)
           {
               if (plan[x - 1, y + 1] == 1 || plan[x + 1, y + 1] == 1) dig_enter= new Vector3(x, y + 1);
           }
           if (plan[x + 1, y] == 0)
           {
               if (plan[x + 1, y - 1] == 1 || plan[x + 1, y + 1] == 1) dig_enter= new Vector3(x + 1, y);
           }
           if (plan[x, y - 1] == 0)
           {
               if (plan[x - 1, y - 1] == 1 || plan[x + 1, y - 1] == 1) dig_enter= new Vector3(x, y - 1);
           }

    }
	

 
}
