using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private MainUi mainUi;
    [SerializeField]
    private Score score;
    [SerializeField]
    private float angularSpeed;
    [SerializeField]
    private float rotationRadius;

    private bool isCanMove = false;
    private float posX, posY, angle = 4.7f;
    private int direction = 1;

    private void Awake()
    {
        StartCoroutine(TimerToStart());
    }

    private void Update()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            collision.GetComponent<Edge>().Switch();
            score.AddScore();
        }
        else if (collision.gameObject.layer == 7)
        {
            score.ShowFinalScore();
            mainUi.OpenLosePanel();
            Time.timeScale = 0;
        }
    }

    private void Movement()
    {
        if (!isCanMove)
            return;

        posX = Mathf.Cos(angle) * rotationRadius;
        posY = Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector2(posX, posY);
        angle = angle + angularSpeed * Time.deltaTime * direction;

        if (angle >= 360)
            angle = 0;
    }

    private IEnumerator TimerToStart()
    {
        GetComponent<Animator>().SetTrigger("Appear");
        yield return new WaitForSeconds(1.0f);
        isCanMove = true;
    }

    public void ChangeDirection()
    {
        direction *= -1;
    }
}
