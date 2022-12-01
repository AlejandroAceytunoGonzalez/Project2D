using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletController : MonoBehaviour
{
    public List<GameObject> bulletsNormal = new List<GameObject>();
    public GameObject bulletPrefab;

    public List<GameObject> bulletsCharged = new List<GameObject>();
    public GameObject chargedBulletPrefab;

    public List<GameObject> enemyTurretBullets = new List<GameObject>();
    public GameObject enemyTurretBullet;

    public List<GameObject> hits = new List<GameObject>();
    public GameObject GimmeBulletNormal(Vector3 position, Vector3 localScale)
    {
        return GimmeBullet(bulletPrefab, bulletsNormal, position, localScale);

    }

    public GameObject GimmeBulletCharged(Vector3 position, Vector3 localScale)
    {
        return GimmeBullet(chargedBulletPrefab, bulletsCharged, position, localScale);
    }

    public GameObject GimmeEnemyTurretBullet(Vector3 position, Vector3 localScale)
    {
        return GimmeBullet(enemyTurretBullet, enemyTurretBullets, position, localScale);
    }

    public GameObject GimmeHit(Bullet bullet, Vector3 position)
    {
        foreach (GameObject hit in hits)
        {
            if (!hit.gameObject.activeInHierarchy && bullet.bulletHit.name + "(Clone)" == hit.name)
            {
                hit.transform.position = position;
                hit.SetActive(true);
                return hit;
            }
        }
        GameObject newHit = Instantiate(bullet.bulletHit, position, new Quaternion(), transform);
        hits.Add(newHit);
        newHit.SetActive(true);
        return newHit;
    }



    private GameObject GimmeBullet(GameObject prefab, List<GameObject> prefabList,Vector3 position, Vector3 localScale)
    {
        foreach (GameObject bullet in prefabList)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.transform.position = position;
                bullet.transform.localScale = localScale;
                bullet.SetActive(true);
                return bullet;
            }
        }
        GameObject newBullet = Instantiate(prefab, position, new Quaternion(), transform);
        newBullet.transform.localScale = localScale;
        prefabList.Add(newBullet);
        newBullet.SetActive(true);
        return newBullet;
    }
}
