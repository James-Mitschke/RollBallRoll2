using Assets.Scripts.Platforms;
using Assets.Scripts.Player;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;

    public PlayerModel playerModel;
    public float movementSpeed;
    public float jumpSpeed;
    private Rigidbody playerRB;
    private bool isGrounded = true;
    private int floorLayer;

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        floorLayer = LayerMask.NameToLayer("Floor");
        playerModel = new PlayerModel(movementSpeed, jumpSpeed);

        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    void FixedUpdate()
    {
        // Why FixedUpdate for this? Because Time.deltaTime gives consistent and smoove movement in FixedUpdate.
        MovePlayer();
    }

    /// <summary>
    /// A method to handle the movement of the player.
    /// This will be reworked at a later point to use player assigned keys instead of WASD Space.
    /// TODO: Rework to use player assignable keys instead of hardcoded WASD Space.
    /// </summary>
    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W) && playerRB.velocity.z < playerModel.MovementSpeed)
        {
            // Apply forward force based on camera's current forward transform
            var force = new Vector3(mainCamera.transform.forward.x, 
                mainCamera.transform.forward.y, 
                mainCamera.transform.forward.z * playerModel.MovementSpeed * Time.deltaTime);

            playerRB.AddForce(force);
        }
        if (Input.GetKey(KeyCode.A) && playerRB.velocity.x > -playerModel.MovementSpeed)
        {
            // Apply left force based on camera's current negative right transform
            var force = new Vector3(-mainCamera.transform.right.x * playerModel.MovementSpeed * Time.deltaTime,
                -mainCamera.transform.right.y,
                -mainCamera.transform.right.z);

            playerRB.AddForce(force);
        }
        if (Input.GetKey(KeyCode.D) && playerRB.velocity.x < playerModel.MovementSpeed)
        {
            // Apply right force based on camera's current right transform
            var force = new Vector3(mainCamera.transform.right.x * playerModel.MovementSpeed * Time.deltaTime,
                mainCamera.transform.right.y,
                mainCamera.transform.right.z);

            playerRB.AddForce(force);
        }
        if (Input.GetKey(KeyCode.S) && playerRB.velocity.x > -playerModel.MovementSpeed)
        {
            // Apply backward force based on camera's current negative forward transform
            var force = new Vector3(-mainCamera.transform.forward.x,
                -mainCamera.transform.forward.y,
                -mainCamera.transform.forward.z * playerModel.MovementSpeed * Time.deltaTime);

            playerRB.AddForce(force);
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            playerRB.AddForce(0, 1 * playerModel.JumpSpeed * Time.deltaTime, 0);
        }

        // If the player isn't intentionally moving then slow down their movement. This won't actually stop the player which isn't ideal.
        // TODO: Improve and make this stop the player when velocity gets below a small arbitrary boundary.
        if (!Input.anyKey)
        {
            playerRB.AddForce(-(playerRB.velocity.x / 4), 0, -(playerRB.velocity.z / 4));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player is touching an object on the floor layer, enable jumping
        if (!isGrounded && collision.gameObject.layer == floorLayer)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Placeholder for stay attached to platform functionality
        switch (collision.gameObject.tag)
        {
            case "Elevator":
                var elevatorScript = collision.gameObject.GetComponent<ElevatingPlatform>();
                MoveWithPlatform(elevatorScript.GetPlatformPositions());
                break;

            case "Mover":
                var moverScript = collision.gameObject.GetComponent<MovingPlatform>();
                MoveWithPlatform(moverScript.GetPlatformPositions());
                break;

            case "Rotator":
                var rotatorScript = collision.gameObject.GetComponent<RotatingPlatform>();
                MoveWithPlatform(rotatorScript.GetPlatformPositions());
                break;

            default:
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // If the player isn't touching an object on the floor layer, disable jumping
        if (isGrounded && collision.gameObject.layer == floorLayer)
        {
            isGrounded = false;
        }
    }

    public PlayerModel GetPlayerModel()
    {
        return this.playerModel;
    }

    private void MoveWithPlatform(PlatformModel platformPositions)
    {
        var amountToMove = platformPositions.CurrentPosition - platformPositions.OldPosition;

        this.transform.position += amountToMove;
    }
}
