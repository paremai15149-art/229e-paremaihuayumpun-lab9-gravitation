using System.Collections.Generic;
using UnityEngine;

public class Gravitation : MonoBehaviour
{
    public static List<Gravitation> otherObjects;
    private Rigidbody rb;
    const float G = 0.006673f;
    //set speed for orbiting
    [Serializefield] bool planet = false;
    [Serializefield] int orbitSpeed = 1000;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherObjects == null)  // เช็คว่าวัตถุมี class Gravitation หรือไม่
        { 
            otherObjects = new List<Gravitation>(); // สร้าง List ใหม่เพื่อเก็บ Gravitation
        }

        otherObjects.Add(this); // ใส่วัตถุที่มี Gravitation เข้าไปใน List รายชื่อ

        //orbiting
        if (!planet)
        { rb.AddForce(Vector3.left = orbitSpeed);

    }
    void FixedUpdate()
    {
        foreach (Gravitation obj in otherObjects)
        {
            if (obj != this) // เช็คว่าต้องไม่ใช่วัตถุตนเอง เพื่อไม่ให้เกิดแรงดึงดูดตนเอง
            {
                AttractionForce(obj); // เรียก Method เพื่อใส่แรงดึงดูดหลังคำนวณ
            }
        }
    }
    void AttractionForce(Gravitation other)
    {
        Rigidbody otherRb = other.rb; // ดึง Rigidbody ของอีกวัตถุเพื่อใช้ค่า m2
        Vector3 direction = rb.position - otherRb.position; // หาทิศทางว่าอีกวัตถุอยู่ทิศทางไหน

        float distance = direction.magnitude; // หาระยะห่างระหว่างวัตถุจาก Vector Direction ( ค่า r )
        if (distance == 0f) return; // หากวัตถุอยู่ในตำแหน่งที่ซ้อนกัน ไม่ต้องมีแรงดึงดูดอีก

        // สูตรแรงดึงดูด G = (m1 * m2) / r^2
        float forceMagnitude = G * ((rb.mass * otherRb.mass) / Mathf.Pow(distance, 2));
        Vector3 gravitionalForce = forceMagnitude * direction.normalized; // รวมแรงและทิศทางเพื่อขยับวัตถุตามแรงดึงดูด
        otherRb.AddForce(gravitionalForce); // ใส่แรงดึงดูดให้กับวัตถุอื่น
    }
}
