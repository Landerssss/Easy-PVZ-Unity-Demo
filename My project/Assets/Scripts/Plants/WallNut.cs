using UnityEngine;

public class WallNut : Plants
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            GetComponent<Animator>().enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            GetComponent<Animator>().enabled = true;
        }
    }

}
