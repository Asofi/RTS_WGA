using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControllableUnit : BaseUnit {

    public enum UnitStates
    {
        IDLE,
        WALKING
    }

    public UnitStates UnitState = UnitStates.IDLE;

    public Transform Target;

    public NavMeshAgent mAgent;
    public NavMeshObstacle mObstacle;

    Renderer mRenderer;

    // Use this for initialization
    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        mObstacle = GetComponent<NavMeshObstacle>();
        mRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (UnitState)
        {
            case UnitStates.WALKING:
                mObstacle.enabled = false;
                mAgent.enabled = true;
                break;

            case UnitStates.IDLE:
                mAgent.enabled = false;
                mObstacle.enabled = true;
                break;
        }

        //print(gameObject.name + "       " + CharactersController.UnitInsideDrag(ScreenPos));
        if (!IsSelected)
        {
            mRenderer.material.color = Color.white;
            ScreenPos = Camera.main.WorldToScreenPoint(transform.position);

            if (CharactersController.UnitWithinScreenSpace(ScreenPos))
            {
                if(!IsOnScreen)
                {
                    CharactersController.UnitsOnScreen.Add(gameObject);
                    IsOnScreen = true;
                }
            } else
            {
                if (IsOnScreen)
                    CharactersController.RemoveFromOnScreenUnits(gameObject);
            }
        } else
            mRenderer.material.color = Color.red;

        if (mAgent.pathStatus == NavMeshPathStatus.PathComplete && UnitState == UnitStates.WALKING)
        {
            UnitState = UnitStates.IDLE;

        }
    }

    public void MoveTo(Vector3 position)
    {
        UnitState = UnitStates.WALKING;
        mObstacle.enabled = false;
        mAgent.enabled = true;
        Target.position = position;
        mAgent.SetDestination(Target.position);
    }
}
