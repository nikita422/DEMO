using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Dig : MonoBehaviour {

    List<Vector3> desrtoy_stack;
    int count = -1;

    public  void set_primary(List<Vector3> desrtoy_stackk)
    {
        desrtoy_stack = new List<Vector3>(desrtoy_stackk);
    }
    public Vector3 what_destroy_next()
    {
        count++;  
        //просто возращаем местположение блока, который надо разрушить
        //мураш сам найдет путь, как дойдет, возмет блок и будет считать путь до склада.
        return desrtoy_stack[count];
    }

    void OnGUI()
    {

    }


}
