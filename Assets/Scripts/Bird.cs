using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour {

    public UIDialog uiDialog;
    private Vector3 _initialPosition;
    private float _launchPower = 500;
    private float _minXDrag = -9f;
    private float _maxXDrag = 3f;
    private float _minYDrag = -6f;
    private float _maxYDrag = 0.75f;
    private bool _birdWasLaunched = false;
    private float amountOfTimeIdling;


    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        RenderArrows();

        if (transform.position.y > 10 ||
            transform.position.x > 40 ||
            transform.position.x < -20 ||
            transform.position.y < -15
            )
        {
            //Implement Overall Level Clamping
        }

        if (_birdWasLaunched &&
            GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1
           )
            amountOfTimeIdling += Time.deltaTime;


        //Instantiate a reset button
        if (amountOfTimeIdling > 3 && _birdWasLaunched)
        {
            uiDialog.ActivateResetDialog();
        }
    }

    private void OnMouseDown () {
        if(!_birdWasLaunched)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            GetComponent<LineRenderer>().enabled = true;
        }
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<LineRenderer>().enabled = false ;
        _birdWasLaunched = true;
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
        transform.position = new Vector3(
            x: Mathf.Clamp(newPosition.x, _minXDrag, _maxXDrag),
            y: Mathf.Clamp(newPosition.y, _minYDrag, _maxYDrag)
        );
    }

    private void RenderArrows()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
    }

}