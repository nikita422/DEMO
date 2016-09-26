using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Dig : MonoBehaviour {

    List<ant_brain> worker;
    List<Vector3> desrtoy_stack;
    int count = -1;
    int worker_now = 0;//count of wokrer on this dig
    
    public  void set_primary(List<Vector3> desrtoy_stackk)
    {
        print("set_primary");
        desrtoy_stack = new List<Vector3>(desrtoy_stackk);
    }
    public Vector3 what_destroy_next()
    {
        print("what_desrtoy_next");
        count++;  
        //просто возращаем местположение блока, который надо разрушить
        //мураш сам найдет путь, как дойдет, возмет блок и будет считать путь до склада.
        if (desrtoy_stack.Count < count)
        {
            //desrtoy this dig
        }
        return desrtoy_stack[count];
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 200, 50, 50), worker_now.ToString()))
        {
            print("press digging"); 
            worker = new List<ant_brain>(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UnitSelectionComponent>().get_selected_units());
            if (worker != null)
            {
                print("add "+ worker.Count);
            }
            for (int i = 0; i < worker.Count; i++)
            {
                worker[i].set_dig(this);
            }
        }
    }


}
