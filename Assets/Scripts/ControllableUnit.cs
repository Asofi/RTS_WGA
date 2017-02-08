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
    public bool isCameraLockedOn = false;

    Renderer mRenderer;
    HighlightsFX highlight;

    // Use this for initialization
    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        mObstacle = GetComponent<NavMeshObstacle>();
        mRenderer = GetComponent<Renderer>();
        highlight = Camera.main.GetComponent<HighlightsFX>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (UnitState)
        {
            case UnitStates.WALKING:
                //mObstacle.enabled = false;
                //mAgent.enabled = true;
                break;

            case UnitStates.IDLE:
                //mAgent.enabled = false;
                //mObstacle.enabled = true;
                break;
        }

        //print(gameObject.name + "       " + CharactersController.UnitInsideDrag(ScreenPos));
        if (!IsSelected)
        {
            highlight.enabled = false;
        } else
            highlight.enabled = true;

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
