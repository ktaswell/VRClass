using UnityEngine;
using UnityEngine.UI;

public class CannonRotation : MonoBehaviour
{
  //public GameObject projectile;
  public Text velocityIn, angleIn, distOut;
  public float smooth = 2.0F;
  public float tiltAngle = 30.0F;
  public Rigidbody projectile;
  public Transform hit;
  public int ballCount = 0;

  private Vector3 projectilePosition;
  private GameObject projFiredObj;
  public Rigidbody projectileFired;
  private string velocity, angle, dist;
  private float distVal = 0;
  private int velocityVal = 0, angleVal = 0;
  private bool test;
  // Use this for initialization

  void Start()
  {
    velocity = "0";
    angle = "0";
    dist = "N/A";
    velocityIn.text = velocity;
    angleIn.text = angle;

    projectileFired = projectile;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonDown("Up") && velocityVal < 90)
      velocityVal += 5;
    if (Input.GetButtonDown("Down") && velocityVal > 0)
      velocityVal -= 5;
    if (Input.GetButtonDown("Right") && angleVal < 90)
      angleVal += 5;
    if (Input.GetButtonDown("Left") && angleVal > 0)
      angleVal -= 5;

    Quaternion target = Quaternion.Euler(0, 180, -1 * angleVal);
    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    projectilePosition = hit.position;
    // in order to calculate in 3 dimensions  where theta is angle from x to y and phi is angle from x to z
    // Vx = V*cos(theta)cos(phi)
    // Vy = V*sin(theta)
    // Vz = V*sin(phi)
    //to create projectile inside of cannon use some form of y=mx+b where m = slope of tangent to theta

    if (Input.GetButtonDown("Fire1") && ballCount < 1)
    {
      projectileFired = Instantiate(projectile, projectilePosition, projectile.transform.rotation) as Rigidbody;
      projectileFired.velocity = transform.right * -velocityVal;
      projFiredObj = projectileFired.gameObject;
      ballCount++;
      test = true;
    }

    if (projectileFired && test)
    {
      if (projectileFired.velocity == Vector3.zero)
      {
        projFiredObj = projectileFired.gameObject;
        distVal = Mathf.Abs(transform.position.x + projFiredObj.transform.position.x)/2;
        dist = distVal.ToString();
      }

      if (Input.GetKeyDown(KeyCode.R))
      {
        projFiredObj = projectileFired.gameObject;
        Destroy(projFiredObj);
        dist = "N/A";
        ballCount = 0;
        test = false;
      }
    }
    velocity = velocityVal.ToString();
    angle = angleVal.ToString() + "";
    velocityIn.text = velocity;
    angleIn.text = angle;
    distOut.text = dist;
  }
}
