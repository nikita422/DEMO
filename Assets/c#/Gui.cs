using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour {
    public string textfield;
    public float testlslider;
    public float testscroll;
    public Vector2 scrollpos = Vector2.zero;
    public Rect window = new Rect(200, 20, 150, 50);

    public int toolbar = 0;
    public string[] toolbarset = new string[] {"1","2","tree","","4342"};

    public bool b = false;

    public GUIStyle stylebtn;


	// Use this for initialization
	void Start () {
	
	}

    void OnGUI()
    {

       
        
        GUI.Box(new Rect(10, 100, 250, 150), "BOX");//
        GUI.BeginGroup(new Rect(10, 100, 250, 150), "");
        GUI.Label(new Rect(10, 10, 150, 20), "РУССКИЙ");//
        textfield = GUI.TextField(new Rect(10, 70, 150, 20), textfield);//
        if (GUI.Button(new Rect(10, 30, 150, 20), "button"))//
        {
            print("this work!");

        }
        GUI.EndGroup();
        testlslider = GUI.HorizontalSlider(new Rect(10,10,150,20),testlslider,1.0F,15.0F);//
        testscroll = GUI.HorizontalScrollbar(new Rect(10, 30, 150, 20), testscroll, 1.0f, 0.0F, 10.0F);//

        //window = GUI.Window(0, window, dowindow, "text window");
        toolbar = GUI.Toolbar(new Rect(250, 70, 150, 20), toolbar, toolbarset);
        b = GUI.Toggle(new Rect(250, 110, 150, 20), b, "true or false");
    }

    private void dowindow(int windowID)
    {
        GUI.Button(new Rect(10, 10, 150, 20), "TEST");
        print("HELLO WORLD_w");
        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        
    }


	// Update is called once per frame
	void Update () {
	
	}
}
