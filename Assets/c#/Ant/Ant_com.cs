using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Ant_com : MonoBehaviour {

    
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
 
    public void spawn_ant()
    {

    }

    //public void move_ant(List<GameObject> selected_units, Vector3 way_end)
    //{

    //    if (selected_units != null && way_end != null)
    //    {
    //        for (int i = 0; i < selected_units.Count; i++)
    //        {
    //            AB_alg ab = selected_units[i].AddComponent<AB_alg>();
    //            ab.where_to_go(transform_vec(selected_units[i].transform.position), way_end, GetComponent<map>().get_hight_map(), selected_units[i].GetComponent<ant_brain>());
    //        }
    //    }
    //}
    public Vector3 transform_vec(Vector3 camera_vec)
    {
        Vector3 int_vec = new Vector3();
        int_vec.x = 14 - 1 - (int)camera_vec.y;
        int_vec.y = (int)camera_vec.x;
        return int_vec;
    }

    //IEnumerator move(List<GameObject> selected_units, Vector3 way_end)
    //{
    //    if (selected_units != null && way_end != null)
    //    {
    //        for (int i = 0; i < selected_units.Count; i++)
    //        {
    //            AB_alg ab = selected_units[i].AddComponent<AB_alg>();
    //            ab.where_to_go(transform_vec(selected_units[i].transform.position), way_end, GetComponent<map>().get_hight_map(), selected_units[i].GetComponent<ant_brain>());
    //        }
    //    }
    //}



}
