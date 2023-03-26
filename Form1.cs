
    /* Auther
     * Samuel Kwibe
     * Class IT 145 Found Application Development
     * Professor : Barbara Bancroft
     * Date:03/09/2023
     */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;





namespace Zombi_shout_out_Game
{
    public partial class ZombiShouter : Form
    {
        bool goLeft,goRight, goUp, goDown, gameOver;
        string facing = "up";

        int playerHealth = 100; // this will be her to check the player health 
        int speed = 10; // the speed of the game
        int ammo = 10;
        int zombiespeed = 3;//Zombies speed
        Random randNum = new Random();
        int score; // the score that the user will get
        List<PictureBox> zombiesList = new List<PictureBox>();

        public ZombiShouter()
        {
            InitializeComponent();
            RestartGame();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if(playerHealth >1)
            {
                HealthBar.Value = playerHealth;                                                                         

            }
            else
            {
                gameOver= true;
                player.Image = Properties.Resources.dead;
                GameTimer.Stop();
            }
            txtAmmo.Text = "Ammo:" + ammo;
            txtScore.Text = "Kills:" + score;

            if(goLeft == true && player.Left > 0)
            {
                player.Left -= speed;

            }
            else if(goRight == true && player.Left+Width <this.ClientSize.Width)
            {
                player.Left += speed;
            }
            else if(goUp == true && player.Top > 45)
            {
                player.Top -= speed;
            }
            else if (goDown == true && player.Top + player.Height < this.ClientSize.Height)
            {
                player.Top += speed;
            }


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "ammo")
                {
                    if(player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        ammo += 5;
                    }
                }
                if (x is PictureBox && (string)x.Tag == "zombie")
                {

                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerHealth -= 1;
                    }


                    if (x.Left > player.Left)
                    {
                        x.Left -= zombiespeed;
                        ((PictureBox)x).Image = Properties.Resources.zleft;
                    }
                    if (x.Left < player.Left)
                    {
                        x.Left += zombiespeed;
                        ((PictureBox)x).Image = Properties.Resources.zright;
                    }
                    if (x.Top > player.Top)
                    {
                        x.Top -= zombiespeed;
                        ((PictureBox)x).Image = Properties.Resources.zup;
                    }
                    if (x.Top < player.Top)
                    {
                        x.Top += zombiespeed;
                        ((PictureBox)x).Image = Properties.Resources.zdown;
                    }

                }

                foreach (Control j in this.Controls)
                {
                    if (j is PictureBox && (string)j.Tag == "bullet" && x is PictureBox && (string)x.Tag == "zombie")
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;

                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            zombiesList.Remove(((PictureBox)x));
                            MakeZombies();
                        }
                    }
                }

            }


        }

        

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
             if(e.KeyCode== Keys.Left)
            {
                goLeft = true;
                facing = "left";
                player.Image = Properties.Resources.left;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                player.Image = Properties.Resources.right;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                player.Image = Properties.Resources.up;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                player.Image = Properties.Resources.down;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
                
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
                
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
                
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
                
            }

            if (e.KeyCode == Keys.Space) 
            {
                ShootButtet(facing);
            }



        }




        private void  ShootButtet(string direction)
        {
            Bullets ShootBullet = new Bullets();
            ShootBullet.direction = direction;
            ShootBullet.bulletLeft = player.Left + (player.Width / 2);
            ShootBullet.bulletTop = player.Top + (player.Height / 2);
            ShootBullet.MakeBullet(this);
        }

        private void MakeZombies()
        {
            PictureBox zombie= new PictureBox();
           zombie.Tag = "Zombie";
           zombie.Image = Properties.Resources.zdown;
           zombie.Left = randNum.Next(0, 900);
           zombie.Top = randNum.Next(0, 800);
           zombie.SizeMode = PictureBoxSizeMode.AutoSize;
           zombiesList.Add(zombie); 
            this.Controls.Add(zombie);
            player.BringToFront();

        }



        private void RestartGame()
        {

        }







    }
}
