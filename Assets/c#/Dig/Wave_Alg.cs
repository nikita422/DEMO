using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Wave_Alg : MonoBehaviour {


    int[,] dig_plan;
    List<Vector3> now_block;
    List<Vector3> what_desrtoy;

    int x, y;

    public void set_primary(int[,] plan)
    {
        x = 14; y = 40;
        now_block = new List<Vector3>();
        what_desrtoy = new List<Vector3>();
       
        dig_plan = new int[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                dig_plan[i, j] = plan[i, j];
                if (plan[i, j] == 666)
                {
                    now_block.Add(new Vector3(i, j));
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
           int nomer = 2;
           bool end = false;

           do
           {
               List<Vector3> temp = new List<Vector3>();

               for (int count = 0; count < now_block.Count; count++)
               {
                   int x = (int)now_block[count].x;
                   int y = (int)now_block[count].y;



                   for (int i = 0; i < 4; i++)
                   {
                       switch (i)
                       {
                           case 0://8

                               if (dig_plan[x - 1, y] == 2)
                               {
                                   dig_plan[x - 1, y] = nomer; nomer++;
                                   temp.Add(new Vector3(x - 1, y));
                               }

                               break;
                           case 1://6
                               if (dig_plan[x, y + 1] == 2)
                               {
                                   dig_plan[x, y + 1] = nomer; nomer++;
                                   temp.Add(new Vector3(x, y + 1));
                               }

                               break;
                           case 2://2
                               if (dig_plan[x + 1, y] == 2)
                               {
                                   dig_plan[x + 1, y] = nomer; nomer++;
                                   temp.Add(new Vector3(x + 1, y));
                               }
                               break;
                           case 3://4
                               if (dig_plan[x, y - 1] == 2)
                               {
                                   dig_plan[x, y - 1] = nomer; nomer++;
                                   temp.Add(new Vector3(x, y - 1));
                               }
                               break;
                       }
                   }
               }
               now_block = temp;
               if (temp.Count == 0)
               {
                   end = true;
               }
               for (int k = 0; k < temp.Count; k++)
               {
                   what_desrtoy.Add(temp[k]);
               }
           } while (!end);
       }

    
	
	 
	

 
}
