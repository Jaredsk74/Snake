using UnityEngine;
using System.Collections;

public class SpawnFood : MonoBehaviour {

    public GameObject foodPrefab;
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderRight;
    public Transform borderLeft;

    // Use this for initialization
    void Start() {
		// Spawn the first piece of food; Future calls will be done through SnakeMove.cs
		Invoke("Spawn", 2);
    }

    public void Spawn() {
		Vector2 location;
		RaycastHit2D hit;

		do {
			location = getFoodSpawnLocation();
			hit = Physics2D.Raycast(location, Vector2.zero);
		} while (hit.collider != null);

		Instantiate(foodPrefab, location, Quaternion.identity);
    }

    private Vector2 getFoodSpawnLocation() {
        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);
        int y = (int)Random.Range(borderTop.position.y, borderBottom.position.y);
        return new Vector2(x, y);
    }
}
