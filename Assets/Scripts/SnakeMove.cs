using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeMove : MonoBehaviour {

	public static int score = 0;
	public static bool hard = false;
	public static List<Transform> tailList = new List<Transform>();

	public SpawnFood script;
	public AudioClip bite;                                                          // "Biting, Crunchy, B.wav" by InspectorJ of Freesound.org
	public AudioSource source;
    public GameObject tailPrefab;

	private float speed = .2f;
    private bool ate = false;
    private Vector2 dir = Vector2.right;

    // Use this for initialization
    void Start() {
		if (hard) {
			speed = .15f;
		}
        StartCoroutine(MoveCoroutine());
    }

    // Update is called once per frame
    void Update() {
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

		// Going up and press right
		if (Input.GetKeyDown(KeyCode.RightArrow) && (dir.ToString() == "(0.0, 1.0)"))
			dir = Vector2.right;
		// Going up and press left
		else if (Input.GetKeyDown(KeyCode.LeftArrow) && (dir.ToString() == "(0.0, 1.0)"))
			dir = Vector2.left;

		// Going down and press right
		else if (Input.GetKeyDown(KeyCode.RightArrow) && (dir.ToString() == "(0.0, -1.0)"))
			dir = Vector2.left;
		// Going down and press left
		else if (Input.GetKeyDown(KeyCode.LeftArrow) && (dir.ToString() == "(0.0, -1.0)"))
			dir = Vector2.right;

		// Going right and press right
		else if (Input.GetKeyDown(KeyCode.RightArrow) && (dir.ToString() == "(1.0, 0.0)"))
			dir = Vector2.down;
		// Going right and press left
		else if (Input.GetKeyDown(KeyCode.LeftArrow) && (dir.ToString() == "(1.0, 0.0)"))
			dir = Vector2.up;

		// Going left and press right
		else if (Input.GetKeyDown(KeyCode.RightArrow) && (dir.ToString() == "(-1.0, 0.0)"))
			dir = Vector2.up;
		// Going left and press left
		else if (Input.GetKeyDown(KeyCode.LeftArrow) && (dir.ToString() == "(-1.0, 0.0)"))
			dir = Vector2.down;

		else if (Input.GetMouseButtonDown(0)) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if (hit.collider.gameObject.name == "Left" && (dir.ToString() == "(0.0, 1.0)")) {
				dir = Vector2.left;
			}
			else if (hit.collider.gameObject.name == "Left" && (dir.ToString() == "(0.0, -1.0)")) {
				dir = Vector2.right;
			}
			else if (hit.collider.gameObject.name == "Left" && (dir.ToString() == "(1.0, 0.0)")) {
				dir = Vector2.up;
			}
			else if (hit.collider.gameObject.name == "Left" && (dir.ToString() == "(-1.0, 0.0)")) {
				dir = Vector2.down;
			}

			else if (hit.collider.gameObject.name == "Right" && (dir.ToString() == "(0.0, 1.0)")) {
				dir = Vector2.right;
			}
			else if (hit.collider.gameObject.name == "Right" && (dir.ToString() == "(0.0, -1.0)")) {
				dir = Vector2.left;
			}
			else if (hit.collider.gameObject.name == "Right" && (dir.ToString() == "(1.0, 0.0)")) {
				dir = Vector2.down;
			}
			else if (hit.collider.gameObject.name == "Right" && (dir.ToString() == "(-1.0, 0.0)")) {
				dir = Vector2.up;
			}

			else if (hit.collider.gameObject.name == "NormalGame") {
				SceneManager.LoadScene("Normal");
			}
			else if (hit.collider.gameObject.name == "HardGame") {
				hard = true;
				SceneManager.LoadScene("Normal");
			}
			else if (hit.collider.gameObject.name == "LevelSelect") {
				SceneManager.LoadScene("LevelSelect");
			}
			else if (hit.collider.gameObject.name == "ButtonA") {
				SceneManager.LoadScene("A");
			}
			else if (hit.collider.gameObject.name == "ButtonB") {
				SceneManager.LoadScene("B");
			}
			else if (hit.collider.gameObject.name == "ButtonC") {
				SceneManager.LoadScene("C");
			}
			else if (hit.collider.gameObject.name == "Return") {
				SceneManager.LoadScene("Menu");
			}
		}
		//// Check for arrow key input
		//if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
		//	dir = Vector2.right;
		//}
		//else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
		//	dir = Vector2.left;
		//}
		//else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
		//	dir = Vector2.up;
		//}
		//else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
		//	dir = Vector2.down;
		//}
	}

    IEnumerator MoveCoroutine() {
        while (true) {
            Move();
            yield return new WaitForSeconds(speed);
        }
    }

    void Move() {
        // Move head in new direction
        Vector2 headPos = transform.position;

        transform.Translate(dir);

        // If eaten food
        if (ate) {
            GameObject tailPiece = (GameObject)Instantiate(tailPrefab, headPos, Quaternion.identity);
            // Insert new tail into tail list
            tailList.Insert(0, tailPiece.transform);
            script.Spawn();
            score++;
            ate = false;

            if (score == 1) {
                speed -= .02f;
            }
            else if (score > 1 && score < 5) {
                speed -= .01f;
            }
            else if (score > 4 && score < 13) {
                speed -= .005f;
            }
            else if (speed >= .05){
                speed -= .001f;
            }
            Debug.Log(speed.ToString());
        }

        // If tail exists
        else if (tailList.Count > 0) {
            // Move last tail element to where head was
            tailList.Last().position = headPos;

            // Add to front of list, remove from back
            tailList.Insert(0, tailList.Last());
            tailList.RemoveAt(tailList.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        // Hit food item
        if (coll.name.StartsWith("food")) {
            ate = true;
            Destroy(coll.gameObject);
            source.PlayOneShot(bite);
        }
        else {
			Die();
        }
    }

    void Die() {
		tailList.Clear();
        score = 0;
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
