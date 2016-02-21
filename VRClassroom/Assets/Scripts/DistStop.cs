using UnityEngine;
using System.Collections;

public class DistStop : MonoBehaviour
{

  private Rigidbody rb;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("Floor"))
    {
      rb.velocity = Vector3.zero;
    }
  }

}
