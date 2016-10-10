using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class UnitSelectionComponent : MonoBehaviour
{

    public static class Utils
    {
        static Texture2D _whiteTexture;
        public static Texture2D WhiteTexture
        {
            get
            {
                if (_whiteTexture == null)
                {
                    _whiteTexture = new Texture2D(1, 1);
                    _whiteTexture.SetPixel(0, 0, Color.white);
                    _whiteTexture.Apply();
                }

                return _whiteTexture;
            }
        }

        public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
        {
            // Move origin from bottom left to top left
            screenPosition1.y = Screen.height - screenPosition1.y;
            screenPosition2.y = Screen.height - screenPosition2.y;
            // Calculate corners
            var topLeft = Vector3.Min(screenPosition1, screenPosition2);
            var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
            // Create Rect
            return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }



        public static void DrawScreenRect(Rect rect, Color color)
        {
            GUI.color = color;
            GUI.DrawTexture(rect, WhiteTexture);
            GUI.color = Color.white;
        }

        public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
        {
            // Top
            Utils.DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
            // Left
            Utils.DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
            // Right
            Utils.DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
            // Bottom
            Utils.DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
        }
    }
    List<GameObject> selected_units;
    List<GameObject> all_units;
    List<ant_brain> selected_units_brain;
    bool is_active_select_mode = false;
    void Start()
    {
       // selected_units=new List<GameObject>();
    }

    bool isSelecting = false;
    Vector3 mousePosition1;// для рисования
    public Vector3 mousePos1;//
    public GameObject selectionCirclePrefab;
    public List<ant_brain> get_selected_units()
    {
        selected_units_brain = new List<ant_brain>();
        for (int i = 0; i < selected_units.Count; i++)
        {
            selected_units_brain.Add(selected_units[i].GetComponent<ant_brain>());
        }

        return selected_units_brain;
    }

  

    void Update()
    {

         
            all_units = GameObject.FindGameObjectsWithTag("Player").ToList<GameObject>();
            //Debug.Log("choose_mode_activve_now");
            // Если нажать левую кнопку мыши, начните выбор и запомните расположение мыши
            if (Input.GetMouseButtonDown(0))
            {
                selected_units = new List<GameObject>();
                isSelecting = true;
                mousePosition1 = Input.mousePosition;
                mousePos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            }
            // Если мы отпускаем левую кнопку мыши, выбор конца
            if (Input.GetMouseButtonUp(0))
            {
                isSelecting = false;
            }

            //  Выделить все объекты в окне выбора
            if (isSelecting)
            {
                for (int i = 0; i < all_units.Count(); i++)
                {
                    if (is_enter(i))
                    {
                       
                        if (!selected_units.Contains(all_units[i]))//если в какой нибудь из выбранных юниты не входит нынешний юнит
                        {
                            print(all_units[i]);
                            selected_units.Add(all_units[i]);
                        }
                    }


                }
            }
        
    }
   

    void OnGUI()
    {
        if (isSelecting)
        {
            // Создаем прямоугольник с обеих позиций мыши
            var rect = Utils.GetScreenRect(mousePosition1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    bool is_enter(int i)
    {

        if (all_units[i].transform.position.x > mousePos1.x)
        {//1,3
            if (all_units[i].transform.position.x < Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            {//два случая

                if (all_units[i].transform.position.y > mousePos1.y)
                {//1
                    if (all_units[i].transform.position.y < Camera.main.ScreenToWorldPoint(Input.mousePosition).y)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {//3
                    if (all_units[i].transform.position.y > Camera.main.ScreenToWorldPoint(Input.mousePosition).y)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            { 
                return false;
            }
        }//2,4
        else
        {
            if (all_units[i].transform.position.x > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            {
                if (all_units[i].transform.position.y < mousePos1.y)
                {
                    if (all_units[i].transform.position.y > Camera.main.ScreenToWorldPoint(Input.mousePosition).y)
                    {
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (all_units[i].transform.position.y < Camera.main.ScreenToWorldPoint(Input.mousePosition).y)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            else
            {

                return false;
            }


        }
    }

}