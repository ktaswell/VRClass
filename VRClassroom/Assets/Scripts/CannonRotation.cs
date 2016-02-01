using UnityEngine;
using UnityEngine.UI;

public class CannonRotation : MonoBehaviour
{
  //public GameObject projectile;
  public Text velocityIn, angleIn;
  public float smooth = 2.0F;
  public float tiltAngle = 30.0F;
  public Rigidbody projectile;
  public Vector3 raycastDirection;
  public Transform hit;
  public float x, y;

  const float angToRad = Mathf.PI / 360;
  private RaycastHit trajectory;
  private Vector3 projectilePosition;
  private Transform cannon;
  private string velocity, angle;
  private int velocityVal = 0, angleVal = 0;
  private Vector3 angVelocity;
  // Use this for initialization

  void Start()
  {
    cannon = GetComponent<Transform>();

    velocity = "0";
    angle = "0";
    velocityIn.text = velocity;
    angleIn.text = angle;
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
    
    Quaternion target = Quaternion.Euler(0, 0,  -1 * angleVal);
    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    projectilePosition = hit.position;
    // in order to calculate in 3 dimensions  where theta is angle from x to y and phi is angle from x to z
    // Vx = V*cos(theta)cos(phi)
    // Vy = V*sin(theta)
    // Vz = V*sin(phi)
    //to create projectile inside of cannon use some form of y=mx+b where m = slope of tangent to theta
    x = -velocityVal * Mathf.Cos((360 - transform.rotation.eulerAngles.z) * angToRad);
    y = velocityVal * Mathf.Sin((360 - transform.rotation.eulerAngles.z) * angToRad);
    angVelocity = new Vector3(x, y, 0F);
    
    if (Input.GetButtonDown("Fire1"))
    {
      Rigidbody projectileFired = Instantiate(projectile, projectilePosition, projectile.transform.rotation) as Rigidbody;
      projectileFired.velocity = angVelocity; 
    }

    velocity = velocityVal.ToString();
    angle = angleVal.ToString();
    velocityIn.text = velocity;
    angleIn.text = angle;
  }
}
