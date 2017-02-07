using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour {

    public ControllableUnit SelectedUnit;
    public static ArrayList CurrentlySelectedUnits = new ArrayList();
    public static ArrayList UnitsOnScreen = new ArrayList();
    public static ArrayList UnitsInDrag = new ArrayList();
    public bool FinishedDragOnThisFrame;
    public bool UserIsDragging;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //print(CurrentlySelectedUnits.Count);
        if (Input.GetMouseButtonDown(0))
        {
            CurrentlySelectedUnits.Clear();
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Unit"))
            {
                hit.transform.GetComponent<ControllableUnit>().IsSelected = true;
                CurrentlySelectedUnits.Add(hit.transform.gameObject);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Ground") && CurrentlySelectedUnits.Count > 0)
            {
                for(int i = 0; i < CurrentlySelectedUnits.Count; i++)
                {
                    GameObject UnitObj = CurrentlySelectedUnits[i] as GameObject;
                    ControllableUnit UnitScript = UnitObj.GetComponent<ControllableUnit>();
                    UnitScript.MoveTo(hit.point);
                }
                //SelectedUnit.MoveTo(hit.point);
            }
        }
    }

    private void LateUpdate()
    {
        UnitsInDrag.Clear();


        if ((UserIsDragging || FinishedDragOnThisFrame) && UnitsOnScreen.Count > 0){
            for (int i = 0; i < UnitsOnScreen.Count; i++)
            {
                GameObject UnitObj = UnitsOnScreen[i] as GameObject;
                ControllableUnit UnitScript = UnitObj.GetComponent<ControllableUnit>();

                if (!UnitAlreadyInDragUnits(UnitObj))
                {
                    if (UnitInsideDrag(UnitScript.ScreenPos))
                    {
                        UnitsInDrag.Add(UnitObj);
                        UnitScript.IsSelected = true;
                    }
                    else
                    {
                        if (!UnitAlreadyInCurrentlySelectedUnits(UnitObj))
                            UnitScript.IsSelected = false;
                    }
                }
            }
        }

        if (FinishedDragOnThisFrame)
        {
            FinishedDragOnThisFrame = false;
            PutDraggedUnitsToSelected();
        }
    }

    public static bool UnitWithinScreenSpace(Vector2 UnitScreenPosition)
    {
        if (UnitScreenPosition.x < Screen.width && UnitScreenPosition.y < Screen.height
            && UnitScreenPosition.x > 0 && UnitScreenPosition.y > 0)
            return true;
        else
            return false;
    }

    public static void RemoveFromOnScreenUnits(GameObject Unit)
    {
        for (int i = 0; i < UnitsOnScreen.Count; i++)
        {
            GameObject UnitObj = UnitsOnScreen[i] as GameObject;
            if(Unit == UnitObj)
            {
                UnitsOnScreen.RemoveAt(i);
                UnitObj.GetComponent<ControllableUnit>().IsOnScreen = false;
                return;
            }
        }
    }

    public static void DeselectGameObjectsIfSelected()
    {
        if(CurrentlySelectedUnits.Count > 0)
        {
            for (int i = 0; i < CurrentlySelectedUnits.Count; i++)
            {
                GameObject ArrayListUnit = CurrentlySelectedUnits[i] as GameObject;
                ArrayListUnit.GetComponent<ControllableUnit>().IsSelected = false;
            }
        }
    }

    public static bool UnitInsideDrag(Vector2 UnitInScreenPos)
    {
        if (UnitInScreenPos.x > GUISelectorBox.BoxStart.x && UnitInScreenPos.y < GUISelectorBox.BoxStart.y
            && UnitInScreenPos.x < GUISelectorBox.BoxFinish.x && UnitInScreenPos.y > GUISelectorBox.BoxFinish.y)
            return true;
        else return false;

    }

    public static bool UnitAlreadyInCurrentlySelectedUnits(GameObject Unit)
    {
        if (CurrentlySelectedUnits.Count > 0)
        {
            for (int i = 0; i < CurrentlySelectedUnits.Count; i++)
            {
                GameObject ArrayListUnit = CurrentlySelectedUnits[i] as GameObject;
                if (ArrayListUnit == Unit)
                    return true;
            }
            return false;
        }
        else
            return false;
    }

    public static bool UnitAlreadyInDragUnits(GameObject Unit)
    {
        if (UnitsInDrag.Count > 0)
        {
            for (int i = 0; i < UnitsInDrag.Count; i++)
            {
                GameObject ArrayListUnit = UnitsInDrag[i] as GameObject;
                if (ArrayListUnit == Unit)
                    return true;
            }
            return false;
        }
        else
            return false;
    }

    public static void PutDraggedUnitsToSelected()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
            DeselectGameObjectsIfSelected();
        if(UnitsInDrag.Count > 0)
        {
            for(int i = 0; i < UnitsInDrag.Count; i++)
            {
                GameObject UnitObj = UnitsInDrag[i] as GameObject;

                if (!UnitAlreadyInCurrentlySelectedUnits(UnitObj))
                {
                    CurrentlySelectedUnits.Add(UnitObj);
                    UnitObj.GetComponent<ControllableUnit>().IsSelected = true;
                }
            }
            UnitsInDrag.Clear();
        }
    }
}
