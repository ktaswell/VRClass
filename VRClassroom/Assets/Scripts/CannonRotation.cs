using UnityEngine;
using UnityEngine.UI;

public class CannonRotation : MonoBehaviour
{
  //public GameObject projectile;
  public Text velocityIn, angleIn;
  public float smooth = 2.0F;
  public float tiltAngle = 30.0F;
  public Rigidbody projectile;
  public Transform hit;
  public int ballCount = 0;

  const float angToRad = Mathf.PI / 360;
  private RaycastHit trajectory;
  private Vector3 projectilePosition;
  private GameObject[] projectileFired;
  private string velocity, angle;
  private int velocityVal = 0, angleVal = 0;
  private Vector3 angVelocity;
  // Use this for initialization

  void Start()
  {
    velocity = "0";
    angle = "0";
    velocityIn.text = velocity;
    angleIn.text = angle;

    projectileFired = new GameObject[6]; 
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
    
    Quaternion target = Quaternion.Euler(0, 180,  -1 * angleVal);
    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    projectilePosition = hit.position;
    // in order to calculate in 3 dimensions  where theta is angle from x to y and phi is angle from x to z
    // Vx = V*cos(theta)cos(phi)
    // Vy = V*sin(theta)
    // Vz = V*sin(phi)
    //to create projectile inside of cannon use some form of y=mx+b where m = slope of tangent to theta
    
    if (Input.GetButtonDown("Fire1") && ballCount < 6)
    {
      projectileFired[ballCount] = Instantiate(projectile, projectilePosition, projectile.transform.rotation) as GameObject;
      Rigidbody rb = projectileFired[ballCount].GetComponent<Rigidbody>();
      rb.velocity = transform.right * -velocityVal;
      ballCount++;
    }

    if (Input.GetKeyDown(KeyCode.R) || ballCount == 5)
    {
      for (ballCount = 5; ballCount > 0; ballCount--)
        Destroy(projectileFired[ballCount]);
    }

    velocity = velocityVal.ToString();
    angle = angleVal.ToString() + "";
    velocityIn.text = velocity;
    angleIn.text = angle;
  }
}
