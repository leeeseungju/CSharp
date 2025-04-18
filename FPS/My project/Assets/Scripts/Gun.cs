using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSJ
{
    public class Gun : MonoBehaviour
    {
        public enum State
        {
            Ready,
            Empty,
            Reloading
        }
        public State state { get; private set; }

        public Transform fireTransform;

        //public ParticleSystem muzzleFlashEffect;
        //public ParticleSystem shellEffect;

        private LineRenderer bulletLineRenderer;

        //private AudioSource gunAudioSource;

        public GunData gunData;

        public float fireDistance = 50f;

        public int ammoRemain = 100;
        public int magAmmo;

        private float lastFireTime;

        public float timeBetFire;


        private void Awake()
        {
            //gunAudioSource = GetComponent<AudioSource>();
            bulletLineRenderer = GetComponent<LineRenderer>();
            bulletLineRenderer.positionCount = 2;
            bulletLineRenderer.enabled = false;
        }

        private void OnEnable()
        {
            ammoRemain = gunData.startAmmoRemain;
            magAmmo = gunData.magCapacity;
            state = State.Ready;
            lastFireTime = 0;
        }

        public void Fire()
        {
            if (state == State.Ready && Time.time >= lastFireTime + gunData.timeBetFire)
            {
                lastFireTime = Time.time;
                Shot();
            }
            /*            if(Time.time >= lastFireTime + timeBetFire) 
                        {
                            lastFireTime = Time.time;
                            Shot();
                        }
            */
        }

        private void Shot()
        {
            RaycastHit hit;
            Vector3 hitPosition = Vector3.zero;
            if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
            {
                //IDamageable target = hit.collider.GetComponent<IDamageable>();
/*                if (target != null)
                {
                    target.OnDamage(10f, hit.point, hit.normal);
                }
*/                hitPosition = hit.point;
            }
            else
            {
                hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
            }
            StartCoroutine(ShotEffect(hitPosition));

            magAmmo--;
            if (magAmmo <= 0)
            {
                state = State.Empty;
            }
        }

        IEnumerator ShotEffect(Vector3 hitPosition)
        {
            //muzzleFlashEffect.Play();
            //shellEffect.Play();

            //gunAudioSource.PlayOneShot(gunData.shotClip);

            bulletLineRenderer.SetPosition(0, fireTransform.position);
            bulletLineRenderer.SetPosition(1, hitPosition);
            bulletLineRenderer.enabled = true;

            yield return new WaitForSeconds(0.03f);
            bulletLineRenderer.enabled = false;
        }

        public bool Reload()
        {
            if (state == State.Reloading | ammoRemain <= 0 | magAmmo >= gunData.magCapacity)
            {
                return false;
            }

            StartCoroutine(ReloadRoutine());
            return true;
        }

        private IEnumerator ReloadRoutine()
        {
            state = State.Reloading;

            yield return new WaitForSeconds(gunData.reloadTime);

            int ammoToFill = gunData.magCapacity - magAmmo;

            if (ammoRemain < ammoToFill)
            {
                ammoToFill = ammoRemain;
            }

            magAmmo += ammoToFill;
            ammoRemain -= ammoToFill;

            state = State.Ready;
        }
    }
}