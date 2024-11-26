using UnityEngine;

public class RecoilController : MonoBehaviour
{

    [Header("Recoil_Transform")]
    public Transform recoilPositionTransform;
    public Transform recoilRotationTransform;

    [Header("Recoil_Settings")]
    public float positionDampTime;
    public float rotationDampTime;

    public float recoil1;
    public float recoil2;
    public float recoil3;
    public float recoil4;

    public Vector3 recoilRotation;
    public Vector3 recoilKickBack;
   

    private Vector3 currentRecoil1;
    private Vector3 currentRecoil3;
    private Vector3 rotationOutput;

    

    void Update()
    {
        // Recoil değerlerini zamanla sıfırlıyoruz
        currentRecoil1 = Vector3.Lerp(currentRecoil1, Vector3.zero, recoil1 * Time.deltaTime);
        currentRecoil3 = Vector3.Lerp(currentRecoil3, Vector3.zero, recoil3 * Time.deltaTime);

        // Transform'lara recoil uygulaması
        recoilPositionTransform.localPosition = Vector3.Slerp(recoilPositionTransform.localPosition, currentRecoil3, positionDampTime * Time.deltaTime);
        rotationOutput = Vector3.Slerp(rotationOutput, currentRecoil1, rotationDampTime * Time.deltaTime);
        recoilRotationTransform.localRotation = Quaternion.Euler(rotationOutput);

       
    }

    public void Recoil()
    {
        currentRecoil1 += new Vector3(recoilRotation.x, Random.Range(-recoilRotation.y, recoilRotation.y), Random.Range(-recoilRotation.z, recoilRotation.z)) * 10f; // Katsayı ekleyin
        currentRecoil3 += new Vector3(Random.Range(-recoilKickBack.x, recoilKickBack.x), Random.Range(-recoilKickBack.y, recoilKickBack.y), recoilKickBack.z);
       
    }




    /*
    [Header("Recoil_Transform")]
    public Transform RecoilPositionTranform;
    public Transform RecoilRotationTranform;
    [Space(10)]
    [Header("Recoil_Settings")]
    public float PositionDampTime;
    public float RotationDampTime;
    [Space(10)]
    public float Recoil1;
    public float Recoil2;
    public float Recoil3;
    public float Recoil4;
    [Space(10)]
    public Vector3 RecoilRotation;
    public Vector3 RecoilKickBack;

    public Vector3 RecoilRotation_Aim;
    public Vector3 RecoilKickBack_Aim;
    [Space(10)]
    public Vector3 CurrentRecoil1;
    public Vector3 CurrentRecoil2;
    public Vector3 CurrentRecoil3;
    public Vector3 CurrentRecoil4;
    [Space(10)]
    public Vector3 RotationOutput;

    public bool aim;

    void FixedUpdate()
    {
        CurrentRecoil1 = Vector3.Lerp(CurrentRecoil1, Vector3.zero, Recoil1 * Time.deltaTime);
        CurrentRecoil2 = Vector3.Lerp(CurrentRecoil2, CurrentRecoil1, Recoil2 * Time.deltaTime);
        CurrentRecoil3 = Vector3.Lerp(CurrentRecoil3, Vector3.zero, Recoil3 * Time.deltaTime);
        CurrentRecoil4 = Vector3.Lerp(CurrentRecoil4, CurrentRecoil3, Recoil4 * Time.deltaTime);

        RecoilPositionTranform.localPosition = Vector3.Slerp(RecoilPositionTranform.localPosition, CurrentRecoil3, PositionDampTime * Time.fixedDeltaTime);
        RotationOutput = Vector3.Slerp(RotationOutput, CurrentRecoil1, RotationDampTime * Time.fixedDeltaTime);
        RecoilRotationTranform.localRotation = Quaternion.Euler(RotationOutput);
    }
    public void Fire()
    {
        if (aim == true)
        {
            CurrentRecoil1 += new Vector3(RecoilRotation_Aim.x, Random.Range(-RecoilRotation_Aim.y, RecoilRotation_Aim.y), Random.Range(-RecoilRotation_Aim.z, RecoilRotation_Aim.z));
            CurrentRecoil3 += new Vector3(Random.Range(-RecoilKickBack_Aim.x, RecoilKickBack_Aim.x), Random.Range(-RecoilKickBack_Aim.y, RecoilKickBack_Aim.y), RecoilKickBack_Aim.z);
        }
        if (aim == false)
        {
            CurrentRecoil1 += new Vector3(RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
            CurrentRecoil3 += new Vector3(Random.Range(-RecoilKickBack.x, RecoilKickBack.x), Random.Range(-RecoilKickBack.y, RecoilKickBack.y), RecoilKickBack.z);
        }
    }*/
}