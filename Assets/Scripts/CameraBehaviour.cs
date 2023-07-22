using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 offset;
    private GameObject player;
    private Camera thisCam;
    private Vector3 oldPlayerGlobalPos;
    private Vector3 playerGlobalPos;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            oldPlayerGlobalPos = playerGlobalPos;
        }

        if (!thisCam)
        {
            thisCam = gameObject.GetComponent<Camera>();
        }

        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        playerGlobalPos = player.transform.position;

        // Handled in Update as no physics to break.
        MoveCamera();
        RotateCamera();

        oldPlayerGlobalPos = playerGlobalPos;
    }

    /// <summary>
    /// A method to move the camera to follow the player.
    /// TODO: Add camera rotation on the y axis to allow the player to look around.
    /// </summary>
    private void MoveCamera()
    {
        if (oldPlayerGlobalPos != playerGlobalPos)
        {
            var amountToMove = playerGlobalPos - oldPlayerGlobalPos;

            this.transform.position += amountToMove;
        }
    }

    private void RotateCamera()
    {
        var mouseX = Input.GetAxis("Mouse X");
        var rotatePoint = new Vector3(player.transform.position.x, player.transform.position.y + offset.y, player.transform.position.z);

        transform.RotateAround(rotatePoint, Vector3.up, mouseX);
    }
}
