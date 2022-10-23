using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace PingPong2
{
    public partial class Form1 : Form
    {
        public static bool START = true;
        bool up, left;
        public string KEY;
        Sprite[] sprites = new Sprite[10];
        int ballSpeed = 3;
        int enemySpeed = 3;
        int counter = 0;
        public void LoadSprite(string file, int num, int x, int y) 
        {
            sprites[num] = new Sprite(file, x, y);
        }

        public void LoadSprite(string file, int num, int x, int y, int w, int h)
        {
            sprites[num] = new Sprite(file, x, y, w, h);
        }
        public void SetupGame() 
        {
            left = true;
            up = true;
            LoadSprite("paddle.png", 1, 0, 120);
            LoadSprite("paddle.png", 2, 518, 120);
            LoadSprite("ball.png", 3, 250, 140, 20, 20);

        }
        public Form1()
        {
            InitializeComponent();
            // Полноэкранный режим:
            /*this.WindowState = FormWindowState.Maximized;*/
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void GamePlay()
        {
            Invoke((MethodInvoker)(() =>
            {


                if (START)
                {
                    this.Refresh();
                    label1.Hide();
                }

                if (KeyPressed(Keys.Q) && SpriteY(1) > 8)
                    MoveSprite(1, SpriteX(1), SpriteY(1) - 5);
                if (KeyPressed(Keys.A) && SpriteY(1) < 245)
                    MoveSprite(1, SpriteX(1), SpriteY(1) + 5);

                if (left)
                    MoveSprite(3, SpriteX(3) - ballSpeed, SpriteY(3));
                if (!left)
                    MoveSprite(3, SpriteX(3) + ballSpeed, SpriteY(3));
                if (up)
                    MoveSprite(3, SpriteX(3), SpriteY(3) - ballSpeed);
                if (!up)
                    MoveSprite(3, SpriteX(3), SpriteY(3) + ballSpeed);

                if (up && SpriteY(3) < 11)
                    up = false;
                if (!up && SpriteY(3) > 300)
                    up = true;

                if (sprites[3].SpriteCollision(sprites[1]) && left)
                {
                    left = false;
                    counter++;
                    ballSpeed++;
                    if (counter % 2 == 0 || counter % 5 == 0)
                    {
                        enemySpeed++;
                    }
                }
                if (sprites[3].SpriteCollision(sprites[2]) && !left)
                    left = true;

                if (SpriteY(3) < SpriteY(2) + sprites[2]._height / 2 && SpriteY(2) > 8 && left == false)
                    MoveSprite(2, SpriteX(2), SpriteY(2) - enemySpeed);
                if (SpriteY(3) > SpriteY(2) + sprites[2]._height / 2 && SpriteY(2) < 245 && left == false)
                    MoveSprite(2, SpriteX(2), SpriteY(2) + enemySpeed);

                if (SpriteX(3) < -5)
                {
                    lblEnd.Show();
                    lblEnd.Text = "Победил игрок справа!\nНажмите r чтобы играть заново\nEsc чтобы выйти";
                    START = false;
                }

                if (SpriteX(3) > 515)
                {
                    lblEnd.Show();
                    lblEnd.Text = "Победил игрок слева!\nНажмите r чтобы играть заново\nEsc чтобы выйти";
                    START = false;
                }

                if (!START && KeyPressed(Keys.R))
                {
                    label1.Hide();
                    lblEnd.Hide();
                    START = true;
                    counter = 0;
                    ballSpeed = 3;
                    SetupGame();
                }

                if (!START && KeyPressed(Keys.Escape))
                {
                    Application.Exit();
                }
            }));
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            Thread thread = new Thread(GamePlay);
            thread.Start();
        }

        private void lblStart_Click(object sender, EventArgs e)
        {
            tmrRefresh.Start();
            START = true;
            lblExit.Hide();
            lblStart.Hide();
            SetupGame();
        }

        public bool KeyPressed(Keys k) 
        {
            if (KEY == k.ToString())
            {
                return true;
            }
            else return false;
        }

        public int SpriteY(int num) { return sprites[num]._y; }
        public int SpriteX(int num) { return sprites[num]._x; }

        public void MoveSprite(int num, int x, int y) 
        {
            sprites[num]._x = x;
            sprites[num]._y = y;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            KEY = e.KeyCode.ToString();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            KEY = "";
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            this.DoubleBuffered = true;
            Graphics g = e.Graphics;
            foreach (Sprite s in sprites) 
            {
                if (s != null) 
                {
                    if (s.Show == true) 
                    {
                        g.DrawImage(s.CurrentSprite, new Rectangle(s._x, s._y, s._width, s._height));
                    }
                }
            }
        }
    }
}
