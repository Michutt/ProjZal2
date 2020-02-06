using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Game
{
    public partial class Form1 : Form
    {
        //pola
        Random random = new Random();

        bool flaga = false;
        int angle;
        string ini = "";

        long result = 0;
        long res1, res2, res3 = 0;

        Stopwatch stopWatch = new Stopwatch();

        List<Bitmap> trash1 = new List<Bitmap>()
        {
        Game.Properties.Resources.p1, Game.Properties.Resources.p2, Game.Properties.Resources.i1, Game.Properties.Resources.i2,
        Game.Properties.Resources.s1, Game.Properties.Resources.s2, Game.Properties.Resources.pl1, Game.Properties.Resources.pl2
        };

        List<Bitmap> trash2 = new List<Bitmap>()
        {
        Game.Properties.Resources.p1, Game.Properties.Resources.p2, Game.Properties.Resources.i1, Game.Properties.Resources.i2,
        Game.Properties.Resources.s1, Game.Properties.Resources.s2, Game.Properties.Resources.pl1, Game.Properties.Resources.pl2
        };

        List<Bitmap> bins = new List<Bitmap>()
        {
        Game.Properties.Resources.papier, Game.Properties.Resources.inne, Game.Properties.Resources.szklo,
        Game.Properties.Resources.plastik
        };

        List<string> flags = new List<string>()
        {
            "papier", "papier", "inne", "inne", "szklo", "szklo", "plastik", "plastik"
        };

        List<string> flags1 = new List<string>()
        {
            "papier", "papier", "inne", "inne", "szklo", "szklo", "plastik", "plastik"
        };

        // konstruktor
        public Form1(int angle)
        {
            this.angle = angle;
            InitializeComponent();
        }

        //przycisk START
        private void menu_start_click(object sender, EventArgs e)
        {
            Label[] labele = { label3, label4, label5, label6, label7, label8, label9, label10 };

            Label clickedLabel = sender as Label;

            label11.Location = new Point(310, 260);

            if (clickedLabel != null)
            {
                if (tableLayoutPanel1.Visible == false)
                    return;

                tableLayoutPanel1.Visible = false;
                tableLayoutPanel4.Visible = true;
                tableLayoutPanel5.Visible = true;
                label11.Visible = true;
                result = 0;
                label13.Text = "Twój wynik to: ";
                flaga = false;


                foreach (Label label in labele)
                {
                    label.Visible = true;
                    if (label != null)
                    {
                        label.Image = null;
                    }
                }

                Image i = new Bitmap(Game.Properties.Resources.start);
                label11.Image = i;

                timer1.Start();
            }
        }

        //metoda przypisujaca losowe obrazki do miejsc
        private void AssignIconsToSquares()
        {
            Label[] labele = { label3, label4, label5, label6, label7, label8, label9, label10 };
            foreach (Label label in labele)
            {
               try
                {
                    if (label != null)
                    {
                        int randomNumber = random.Next(trash1.Count);
                        Image i = new Bitmap(trash1[randomNumber]);
                        label.Image = i;
                        label.Text = flags[randomNumber];
                        trash1.RemoveAt(randomNumber);
                        flags.RemoveAt(randomNumber);
                    }
                }
                catch (System.ArgumentOutOfRangeException e2) { }
                
            }
            foreach (Bitmap trash in trash2)
            {
                trash1.Add(trash);
            }

            foreach (string flag in flags1)
            {
                flags.Add(flag);
            }
        }

        //metoda losujaca pojemnik
        private void AssignIconsToMidSquare()
        {
            int randomNumber = random.Next(bins.Count);
            Image i = new Bitmap(bins[randomNumber]);
            label11.Image = i;
            ini = flags[randomNumber * 2];
        }

        //metoda wykonujaca czynnosci po zlym kliknieciu
        private void Decision_false(Label label)
        {
            Label[] labele = { label3, label4, label5, label6, label7, label8, label9, label10 };
            foreach (Label labela in labele)
            {
                if (labela != null)
                {
                    Image i = new Bitmap(Game.Properties.Resources.x);
                    labela.Image = i;
                }
            }

            long duration = stopWatch.ElapsedMilliseconds;
            stopWatch.Stop();
            stopWatch.Reset();
        }
        
        //metoda wykonujaca czynnosci po dobrym kliknieciu
        private void Decision_true(Label label)
        {
            Label[] labele = { label3, label4, label5, label6, label7, label8, label9, label10 };
            foreach(Label labela in labele)
            {
                if (labela != null)
                {
                    Image i = new Bitmap(Game.Properties.Resources.y);
                    labela.Image = i;
                }
            }

            long duration = stopWatch.ElapsedMilliseconds;

            if (duration <= 5000)
            {
                result += 5000 - duration;
            }

            label13.Text = "Twój wynik to: " + result.ToString();

            stopWatch.Stop();
            stopWatch.Reset();
        }

        //metoda sprawdzajaca poprawnosc klikniecia
        private void Decision_check(Label label)
        {
            if (label.Text == ini)
            {
                Decision_true(label);
            }
            else
            {
                Decision_false(label);
            }
        }

        //metoda wykonujaca czynnosci po kliknieciu w pojemnik
        private void Label11_Click(object sender, EventArgs e)
        {
            Label[] labele = { label3, label4, label5, label6, label7, label8, label9, label10 };
            if (flaga == true)
            {
                return;
            }
            else
            {
                foreach (Label label in labele)
                {
                    if (label != null)
                    {
                        label.Image = null;
                    }
                }
                timer2.Start();
                stopWatch.Start();
                AssignIconsToSquares();
                AssignIconsToMidSquare();
                flaga = true;
            }
        }

        //metoda wykonujaca czynnosci po kliknieciu w obszary
        private void Label4_Click(object sender, EventArgs e)
        {
            Label iconLabel = sender as Label;
            if (flaga == true)
            {
                flaga = false;
                Decision_check(iconLabel);
                timer2.Stop();
            }
        }

        //przycisk wyjscie do menu
        private void Label12_Click(object sender, EventArgs e)
        {
            Label[] labele = { label3, label4, label5, label6, label7, label8, label9, label10 };
            tableLayoutPanel1.Visible = true;
            tableLayoutPanel4.Visible = false;
            tableLayoutPanel5.Visible = false;
            label11.Visible = false;
            timer1.Stop();

            foreach (Label label in labele)
            {
                label.Visible = false;
                if (label != null)
                {
                    label.Text = null;
                }
            }
        }

        //2 obslugi zdarzen dotyczacych najezdzania myszka na napisy
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            Label iconLabel = sender as Label;
            iconLabel.Font = new Font("Microsoft Sans Serif", 50); //Microsoft Sans Serif; 36pt; style=Bold
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            Label iconLabel = sender as Label;
            iconLabel.Font = new Font("Microsoft Sans Serif", 36);
        }

        //timer dotyczacy obracania sie obszarow
        private void timer2_Tick(object sender, EventArgs e)
        {
            Label[] labele = { label3, label4, label5, label6, label7, label8, label9, label10 };
            int radius = 250;
            for (int i = 0; i < labele.Length; i++)
            {
                double x = label11.Location.X + Math.Cos(Math.PI * (angle + i * 45) / 180.0) * radius;
                double y = label11.Location.Y + Math.Sin(Math.PI * (angle + i * 45) / 180.0) * radius;
                int x1 = Convert.ToInt32(x);
                int y1 = Convert.ToInt32(y);
                labele[i].Location = new Point(x1, y1);
            }
            timer2.Stop();
            timer2.Start();
            angle++;
        }

        //timer dotyczacy czasu pojedynczej rozgrywki
        private void Timer1_Tick(object sender, EventArgs e)
        {
            Label[] labele = { label3, label4, label5, label6, label7, label8, label9, label10 };
            timer1.Stop();
            MessageBox.Show("Dobra robota, twój wynik to: " + result , "Koniec czasu");

            if (result >= res1)
            {
                res3 = res2;
                res2 = res1;
                res1 = result;
            }
            else if (result >= res2)
            {
                res3 = res2;
                res2 = result;
            }
            else if (result >= res3)
            {
                res3 = result;
            }


            tableLayoutPanel1.Visible = true;
            tableLayoutPanel4.Visible = false;
            tableLayoutPanel5.Visible = false;
            label11.Visible = false;

            foreach (Label label in labele)
            {
                label.Visible = false;
                if (label != null)
                {
                    label.Text = null;
                }
            }
        }

        //obsluga przycisku RANKING
        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1: " + res1 +"\n2: " + res2 + "\n3: " + res3, "Ranking bieżącej sesji");
        }
    }
}
