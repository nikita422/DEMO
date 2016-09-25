using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Dig : MonoBehaviour {

    List<GameObject> desrtoy_stack;
    int count = 0;

    public Vector3 what_destroy_next()
    {



        //просто возращаем местположение блока, который надо разрушить
        //мураш сам найдет путь, как дойдет, возмет блок и будет считать путь до склада.

        return new Vector3(14,26,0);
    }




}
