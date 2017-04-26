namespace VRTK.Examples
{
    using UnityEngine;

    public class Gun : VRTK_InteractableObject
    {
        private GameObject bullet1;
        private GameObject bullet2;
        //private GameObject bullet3;

        public bool bulletOne = true;
        public bool bulletTwo = false;
        //public bool bulletThree = false;

        public float bulletSpeed = 1000f;
        private float bulletLife = 5f;


        private ParticleSystem particles1;
        private ParticleSystem particles2;
        //private ParticleSystem particles3;




        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);
            FireBullet();
        }

        public void ChooseBulletOne()
        {
            bulletOne = true;
            bulletTwo = false;
            // bulletThree = false;
        }

        public void ChooseBulletTwo()
        {
            bulletTwo = true;
            bulletOne = false;
            //bulletThree = false;

        }
        /*
        public void ChooseBulletThree()
        {
            bulletThree = true;
            bulletTwo = false;
            bulletOne = false;
        }
        */
        protected void Start()
        {
            bullet1 = transform.Find("Bullet1").gameObject;
            bullet2 = transform.Find("Bullet2").gameObject;
            //  bullet3 = transform.Find("Bullet3").gameObject;
            bullet1.SetActive(false);
            bullet2.SetActive(false);
            //  bullet3.SetActive(false);

            particles1 = bullet1.GetComponent<ParticleSystem>();
            particles1.Stop();
            particles2 = bullet2.GetComponent<ParticleSystem>();
            particles2.Stop();
            //  particles3 = bullet3.GetComponent<ParticleSystem>();
            // particles3.Stop();
        }

        private void FireBullet()
        {
            if (bulletOne == true)
            {
                GameObject bulletClone = Instantiate(bullet1, bullet1.transform.position, bullet1.transform.rotation) as GameObject;
                bulletClone.SetActive(true);
                if (particles1.isPaused || particles1.isStopped)
                {
                    particles1.Play();
                }
                Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
                rb.AddForce(-bullet1.transform.forward * bulletSpeed);


                Destroy(bulletClone, bulletLife);
                particles1.Stop();
            }
            else if (bulletTwo == true)
            {
                GameObject bulletClone = Instantiate(bullet2, bullet2.transform.position, bullet2.transform.rotation) as GameObject;
                bulletClone.SetActive(true);
                if (particles2.isPaused || particles2.isStopped)
                {
                    particles2.Play();
                }
                Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
                rb.AddForce(-bullet2.transform.forward * bulletSpeed);


                Destroy(bulletClone, bulletLife);
                particles2.Stop();
            }/*
            else if (bulletThree == true)
            {
                GameObject bulletClone = Instantiate(bullet3, bullet3.transform.position, bullet3.transform.rotation) as GameObject;
                bulletClone.SetActive(true);
                if (particles3.isPaused || particles3.isStopped)
                {
                    particles3.Play();
                }
                Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
                rb.AddForce(-bullet3.transform.forward * bulletSpeed);


                Destroy(bulletClone, bulletLife);
                particles3.Stop();
            }*/
             /*
                         GameObject bulletClone = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation) as GameObject;
                         bulletClone.SetActive(true);
                         if (particles.isPaused || particles.isStopped)
                         {
                             particles.Play();
                         }
                         Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
                         rb.AddForce(-bullet.transform.forward * bulletSpeed);


                         Destroy(bulletClone, bulletLife);
                         particles.Stop();
               */
        }
    }
}
