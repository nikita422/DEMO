using UnityEngine;
using System.Collections;

public class Store_alg : MonoBehaviour
{

    int left, right;
    int first_j, end_j, middle;
    Vector2 firts_down;
    // Update is called once per frame
    // coord first_up; bool first_yes = false;
    //class coord
    //{
    //    public int x, y;
    //    public coord(int xx, int yy) { x = xx; y = yy; }
    //}
    public Vector2 where_start_wave(int[,] select_map)// c 0,1,2
    {
        for (int j = 0; j < 40; j++)
        {
            for (int i = 0; i < 14; i++)
            {
                if (select_map[i, j] == 2)
                {
                    first_j = j;
                    goto next;
                }
            }
        }
    next:
        for (int j = 40; j > 0; j--)
        {
            for (int i = 0; i < 14; i++)
            {
                if (select_map[i, j] == 2)
                {
                    end_j = j;
                    goto nextt;
                }
            }
        }
    nextt:
        middle = (int)((first_j + end_j) / 2);//

        for (int i = 1; i < 13; i++)
        {
            for (int j = 1; j < 39; j++)
            {
                if (select_map[i + 1, j] == 0 || select_map[i, j + 1] == 0 || select_map[i - 1, j] == 0 || select_map[i, j - 1] == 0)
                {
                    if (j < middle)
                    {
                        left++;
                    }
                    if (j > middle)
                    {
                        right++;
                    }
                }
            }
        }
        //find first down_pight

        if (right > left)
        {
            for (int i = 13; i > 0; i--)
            {
                for (int j = 39; j > 0; j--)
                {
                    if (select_map[i, j] == 2)
                    {
                        if (j > middle)
                        {
                            firts_down = new Vector2(i, j);
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 14; i > 0; i--)
            {
                for (int j = 0; j < 39; j++)
                {
                    if (select_map[i, j] == 2)
                    {
                        if (j < middle)
                        {
                            firts_down = new Vector2(i, j);
                        }
                    }
                }
            }
        }



        return firts_down;


    }
}
