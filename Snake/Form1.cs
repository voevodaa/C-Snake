using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private int SnakeX, SnakeY;
        private int SnakeDirectionX = 1, SnakeDirectionY = 1;
        private Point FoodPosition = new Point(5,5);
        private const int CellSize = 20; // размер змейки
        private Point[] Tail = new Point[3];
        private bool IsStepLocked;
        private Random random = new Random();

        private void gameUpdate_Tick(object sender, EventArgs e)
        {
            PushInTail(new Point(SnakeX, SnakeY));  

            SnakeX += SnakeDirectionX;
            SnakeY += SnakeDirectionY;
            if(SnakeX > 15) // шаги змейки
            {
                SnakeX = 0;
            }
            if (SnakeX < 0)
            {
                SnakeX = 15;
            }
            if(SnakeY > 15) // шаги змейки
            {
                SnakeY = 0;
            }
            if(SnakeY < 0)
            {
                SnakeY = 15;
            }
            IsStepLocked = false;

            if(SnakeX == FoodPosition.X && SnakeY == FoodPosition.Y)
            {
                Array.Resize(ref Tail, Tail.Length + 1);
                CreatFood();
            }

            Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.A:
                    SetDirection(-1, 0);
                    break;
                case Keys.D:
                    SetDirection(1, 0);
                    break;
                case Keys.S:
                    SetDirection(0, 1);
                    break;
                case Keys.W:
                    SetDirection(0, -1);
                    break;
            }
        }

        private void SetDirection(int x, int y)
        {
            if ((SnakeDirectionX * -1 == x && SnakeDirectionY * -1 == y) || IsStepLocked)
            {
                return;
            }
            SnakeDirectionX = x;
            SnakeDirectionY = y;

            IsStepLocked = true;
        }

        public Form1()
        {
            InitializeComponent();
            CreatFood();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           Graphics graphics = e.Graphics;
           Brush pen = Brushes.Green;
           Brush tailBrush = Brushes.Red;
           Brush food = Brushes.Blue;

           graphics.FillRectangle(pen, new Rectangle(SnakeX * CellSize, SnakeY * CellSize, CellSize, CellSize));

            for (int i = 0; i < 3; i++)
            {
                graphics.FillRectangle(food, new Rectangle(FoodPosition.X * CellSize, FoodPosition.Y * CellSize, CellSize, CellSize));
            }
           
            
            for (int i = 0; i < Tail.Length; i++)
            {
                graphics.FillRectangle(tailBrush, new Rectangle(Tail[i].X * CellSize, Tail[i].Y * CellSize, CellSize, CellSize));
            }
        }

        private void CreatFood()
        {
            
            FoodPosition.X = random.Next(0, 10);
            FoodPosition.Y = random.Next(0, 10);
        }

        private void PushInTail(Point point)
        {
            for (int i = Tail.Length - 1; i > 0; i--)
            {
                Tail[i] = Tail[i - 1];   
            }
            Tail[0] = point;
        }
    }
}