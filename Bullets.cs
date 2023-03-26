using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;







namespace Zombi_shout_out_Game
{
    internal class Bullets
    {
        public string direction; // the direction of the bullets 
        public int bulletLeft; // this will be going left
        public int bulletTop; // the bullet will be going up / top 



        private int speed = 20;// the speed of bullets  will be seat up to 20
        private PictureBox bullet = new PictureBox();
        private Timer bulletTimer = new Timer();

        public void MakeBullet(Form form) // this will allow me to add the bullets on the other form that i have created before the form 1
        {
            bullet.BackColor = Color.White;
            bullet.Size = new Size(5, 5);
            bullet.Tag = bulletLeft;
            bullet.Tag = bulletTop;
            bullet.BringToFront();

            form.Controls.Add(bullet);

            bulletTimer.Interval = speed;
            bulletTimer.Tick += new EventHandler(BulletTimerEvent);

            bulletTimer.Start();
        }
          private void BulletTimerEvent(object sender, EventArgs e) 
        {
            if(direction == "left")
            {
                bullet.Left -= speed;
            }
             if(direction == "right")
            {
                bullet.Left += speed;
            }

            if (direction == "up")
            {
                bullet.Top += speed;
            }
            if (direction == "down")
            {
                bullet.Top += speed;
            }


            if (bullet.Left < 10 || bullet.Left > 860 || bullet.Top < 10 || bullet.Top > 600)
            {
                bulletTimer.Stop(); // thia stop the bullet to come out 
                bulletTimer.Dispose();
                bullet.Dispose();
                bulletTimer = null;
                bullet = null;
            }



        }
    }
}
