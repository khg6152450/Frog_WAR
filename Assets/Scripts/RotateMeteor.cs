using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RotateMeteor : MonoBehaviour
{
    // 메테오 회전 속도
    public float turnSpeed = 0.1f;

    Transform target;

    public GameObject magic_ring;
    public GameObject frog;
    public GameObject frog2;
    public GameObject me;

    public GameObject button;
    public GameObject title;

    void Update()
    {
        // 메테오 회전 z-axis(축)
        transform.Rotate(new Vector3(0, -1, 0) * 1 * Time.deltaTime * turnSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        // "Ground" 태그인 오브젝트와 만나면 충돌 후 삭제
        if (other.gameObject.tag == "Ground")
        {
            Debug.Log("Collision");
            //Destroy(this.gameObject);

            magic_ring.SetActive(true);
            //frog.SetActive(true);

            //button.gameObject.SetActive(true);
            //StartCoroutine(waitTime());

            //frog2.SetActive(true);

            //title.gameObject.SetActive(true);

            me.SetActive(false);
            //Destroy(this.gameObject);
        }
    }

    IEnumerator waitTime()
    {
        frog.SetActive(true);
        yield return new WaitForSeconds(1f);
        frog2.SetActive(true);
    }
}
