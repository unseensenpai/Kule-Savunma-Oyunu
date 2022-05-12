using UnityEngine;

public class CameraController : MonoBehaviour
{
   // public bool doMove = true;
    public float panSpeed = 30f; // Panaromik görüntünün hýzý
    public float panBorder = 20f; // Kameranýn hareket etmesi için trigger uzunluðu
    public float scrollSpeed = 5f; // Scroll hýzý
    public float minY = 10f; // minimum scrollanabilir nokta
    public float maxY = 80f; // maximum scrollanabilir nokta
    public float minX = -5f; 
    public float maxX = 75f; 
    public float minZ = -90f;
    public float maxZ = 0f; 


    public void Update()
    {
        if (GameManagers.GameIsOver)
        {
            this.enabled = false;
            return;
        }

      /*  if (Input.GetKeyDown(KeyCode.Escape)) // ESCye basýldýðýnda kamerayý kilitle
            doMove = !doMove;

        if (!doMove)
            return; */

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorder) // Mouse pozisyonu y ekseninde ekranýn uzunluðunun 10 piksel yakýnýna girdiyse kamerayý hareket ettir.
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World); // Space.World komutunun amacý kameranýn deðil dünyanýn ileri gitme açýsýný almak.
            // W tuþuna basýldýðýnda kamera objesini panaromik hýzda zaman içinde ileriye hareket ettir.
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorder)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World); 
            // S tuþuna basýldýðýnda kamera objesini panaromik hýzda zaman içinde geriye hareket ettir.
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorder)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            // D tuþuna basýldýðýnda kamera objesini panaromik hýzda zaman içinde saða hareket ettir.
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorder)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            // A tuþuna basýldýðýnda kamera objesini panaromik hýzda zaman içinde sola hareket ettir.
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // Mouse ScrollWheel'ýn takma adýný tutuyoruz.
        Vector3 pos = transform.position; // Kameranýn pozisyonunu tutuyoruz.
        pos.y = Mathf.Clamp(pos.y, minY, maxY); // Clamp minimum ve maximum range belirlemede kullanýlýyor. Uzaklýk Ekseni.
        pos.x = Mathf.Clamp(pos.x, minX, maxX); // Yatay Eksen
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ); // Dikey Eksen
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime; // Kameranýn uzaklýðýný scroll hareketiyle zaman içinde sabit hýzla azaltýp artýrýyoruz.
        transform.position = pos;

    }
}
