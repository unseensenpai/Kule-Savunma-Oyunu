using UnityEngine;

public class CameraController : MonoBehaviour
{
   // public bool doMove = true;
    public float panSpeed = 30f; // Panaromik g�r�nt�n�n h�z�
    public float panBorder = 20f; // Kameran�n hareket etmesi i�in trigger uzunlu�u
    public float scrollSpeed = 5f; // Scroll h�z�
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

      /*  if (Input.GetKeyDown(KeyCode.Escape)) // ESCye bas�ld���nda kameray� kilitle
            doMove = !doMove;

        if (!doMove)
            return; */

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorder) // Mouse pozisyonu y ekseninde ekran�n uzunlu�unun 10 piksel yak�n�na girdiyse kameray� hareket ettir.
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World); // Space.World komutunun amac� kameran�n de�il d�nyan�n ileri gitme a��s�n� almak.
            // W tu�una bas�ld���nda kamera objesini panaromik h�zda zaman i�inde ileriye hareket ettir.
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorder)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World); 
            // S tu�una bas�ld���nda kamera objesini panaromik h�zda zaman i�inde geriye hareket ettir.
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorder)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            // D tu�una bas�ld���nda kamera objesini panaromik h�zda zaman i�inde sa�a hareket ettir.
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorder)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            // A tu�una bas�ld���nda kamera objesini panaromik h�zda zaman i�inde sola hareket ettir.
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // Mouse ScrollWheel'�n takma ad�n� tutuyoruz.
        Vector3 pos = transform.position; // Kameran�n pozisyonunu tutuyoruz.
        pos.y = Mathf.Clamp(pos.y, minY, maxY); // Clamp minimum ve maximum range belirlemede kullan�l�yor. Uzakl�k Ekseni.
        pos.x = Mathf.Clamp(pos.x, minX, maxX); // Yatay Eksen
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ); // Dikey Eksen
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime; // Kameran�n uzakl���n� scroll hareketiyle zaman i�inde sabit h�zla azalt�p art�r�yoruz.
        transform.position = pos;

    }
}
