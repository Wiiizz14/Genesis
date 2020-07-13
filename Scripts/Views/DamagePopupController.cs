using TMPro;
using UnityEngine;

public class DamagePopupController : MonoBehaviour
{
    public Transform damagePopupPrefab;
    public float disappearTimer; // 0.5
    public float disappearSpeed; // 50

    private TextMeshPro textMeshPro;
    private Color textColor;


    void Awake()
    {
        textMeshPro = transform.GetComponent<TextMeshPro>();
    }


    void Setup(int damageAmount)
    {
        textMeshPro.SetText(damageAmount.ToString());
        textColor = textMeshPro.color;
    }


    private void Update()
    {
        float moveYspeed = 10f;

        transform.position += new Vector3(0, moveYspeed) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;

        if (disappearTimer < 0)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMeshPro.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }


    public static DamagePopupController Create(Vector3 position, int damageAmount, Transform damagePopupPrefab)
    {
        Transform damagePopupTrandform = Instantiate(damagePopupPrefab, position, Quaternion.identity);
        DamagePopupController damagePopup = damagePopupTrandform.GetComponent<DamagePopupController>();
        damagePopup.Setup(damageAmount);

        // Align to camera view
        damagePopup.transform.rotation = Camera.main.transform.rotation;
        return damagePopup;
    }
}
