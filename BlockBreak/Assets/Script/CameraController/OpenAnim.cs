using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class OpenAnim : MonoBehaviour
{
    public GameObject black0;
    public GameObject black1;

    public float openSpeed;
    public string idName;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(open());
    }

    private IEnumerator open()
    {
        black0.SetActive(true);
        black1.SetActive(true);
        //print(black0.transform.position.y);
        while (black0.transform.position.y < 20)
        {
            black0.transform.Translate(0, openSpeed, 0);
            black1.transform.Translate(0, -openSpeed, 0);
            yield return new WaitForFixedUpdate();
        }
        while (black0.transform.position.y > 8)
        {
            black0.transform.Translate(0, -openSpeed, 0);
            black1.transform.Translate(0, openSpeed, 0);
            yield return new WaitForFixedUpdate();
        }
        while (black0.transform.position.y < 20)
        {
            black0.transform.Translate(0, openSpeed, 0);
            black1.transform.Translate(0, -openSpeed, 0);
            yield return new WaitForFixedUpdate();
        }
        UiStatic.TalkKickIssue(idName);
        black0.SetActive(false);
        black1.SetActive(false);
    }
}
