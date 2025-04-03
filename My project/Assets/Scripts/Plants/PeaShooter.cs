using UnityEngine;

public class PeaShooter : Plants
{
    public float shootDuartion = 2;
    private float shootTimer = 0;
    public Transform PeaTransform;      //��λ�㶹�ӵ�
    public PeaBullet peaBulletPrefab;
    public float peaSpeed = 5;
    public int AttackValue = 8;
    protected override void EnableUpdate()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer > shootDuartion)
        {
            //TODO:��⽩ʬ�����ٷ���
            Shoot();
            shootTimer = 0;

        }

    }
    void Shoot()
    {
        PeaBullet pb = GameObject.Instantiate(peaBulletPrefab, PeaTransform.position,Quaternion.identity);
        pb.SetSpeed(peaSpeed);
        pb.SetAttackValue(AttackValue);
    }
}
