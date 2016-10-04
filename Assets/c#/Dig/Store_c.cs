using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Store_c : MonoBehaviour {

    public GameObject pref_store;
    Store_alg st_alg;
    Wave_Alg wave_alg;

    Select_zone select_zone;
    List<GameObject> list_store;
	void Start () {
      //  print("start_store_commader");
      //  list_store = new List<GameObject>();
       // select_zone = GetComponent<Select_zone>();
     //   select_zone.enabled = false;
        this.enabled = false;
	}
    public void create_new_store(int[,] select_map, GameObject[,] go_zonee)
    {
        //for (int i = 0; i < list_dig.Count; i++)
        //{
        //    list_dig[i].GetComponent<Dig>().up_gui();
        //}
         print("create_new_store");
         st_alg = GetComponent<Store_alg>();
         wave_alg = GetComponent<Wave_Alg>();
           wave_alg.set_primary(select_map, st_alg.where_start_wave(select_map));
           wave_alg.build_plan();

           GameObject store = Instantiate(pref_store, new Vector3(-3, -1, 0), Quaternion.identity) as GameObject;
           store.GetComponent<Store>().set_primary(wave_alg.get_what_destroy(), go_zonee);
           list_store.Add(store);
         

    }
	// Update is called once per frame
	void Update () {
	
	}
}
