using UnityEngine;
using UnityEngine.AI;


public class Door : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float range = 5f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private int requiredKeyID = 0;

    private static NavMeshSurface navMesh;
    private static GameObject player;

    private Vector3 openPosition;
    private bool hasKey;

    // Start is called before the first frame update
    void Start()
    {
        if (navMesh == null)
            navMesh = GameObject.Find("NavMesh").GetComponent<NavMeshSurface>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        openPosition = transform.position + new Vector3(0, 5, 0);
        hasKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasKey)
            hasKey = CheckKey();
        else
            DoorOpen();
    }

    private bool CheckKey()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            KeyInventory keyInv = player.GetComponent<KeyInventory>();
            if (keyInv != null && keyInv.hasKey(requiredKeyID))
                return true;
        }
        return false;
    }

    private void DoorOpen()
    {
        if (transform.position.y < openPosition.y)
        {
            transform.position += Vector3.up * (speed / 10);
        }
        else
        {
            Destroy(gameObject);
            navMesh.BuildNavMesh();
        }
    }
}
