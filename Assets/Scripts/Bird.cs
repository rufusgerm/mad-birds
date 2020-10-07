using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour {

    private Vector3 _initialPosition;
    [SerializeField] private float _launchPower = 500;
    private bool _birdWasLaunched = false;
    private float amountOfTimeIdling;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);

        if (_birdWasLaunched &&
            GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1
           )
            amountOfTimeIdling += Time.deltaTime;

        if (transform.position.y > 10 ||
            transform.position.x > 40 ||
            transform.position.x < -20||
            transform.position.y < -15 ||
            (amountOfTimeIdling > 4 && _birdWasLaunched))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnMouseDown () {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;
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
    }

}