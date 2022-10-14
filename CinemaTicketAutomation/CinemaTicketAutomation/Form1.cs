﻿using CinemaTicketAutomation.Helpers;
using CinemaTicketAutomation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaTicketAutomation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Movie> movies;
        DateTime currentDate = DateTime.Now;
        DateTime useDate;
        Form2 form2;
        private void Form1_load_1(object sender, EventArgs e)
        {
            useDate = currentDate;
            lblDate.Text = useDate.ToShortDateString();
            movies = Helper.CreateMovies();
            FotolariVeSeanslariGetir();
            form2 = new Form2(movies, this);
            //btnPrev.Enabled = false;
        }
        private void FotolariVeSeanslariGetir()
        {
            Size pictureSize = new Size(300, 180);
            Size buttonSize = new Size(90, 40);
            int x = 50;
            int y = 100;
            int xIncrement = 400;
            int yIncrement = 300;
            for (int i = 0; i < movies.Count; i++)
            {
                PictureBox picture = new PictureBox();
                picture.Location = new Point(x, y);
                picture.Size = pictureSize;
                picture.Image = Image.FromFile(movies[i].picturePath);
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(picture);
                int buttonX = x;
                int buttonY = picture.Bottom + 10;
                for (int index = 0; index < 3; index++)
                {
                    Button button = new Button();
                    button.Text = movies[i].sessions[index].time;
                    button.Location = new Point(buttonX, buttonY);
                    button.Size = new Size(90, 40);
                    button.Tag = i;
                    button.Click += new EventHandler(button_Click);
                    this.Controls.Add(button);
                    buttonX += 100;
                }
                if (1200 > x + xIncrement + picture.Width)
                {
                    x += xIncrement;
                }
                else
                {
                    x = 50;
                    y += yIncrement;
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int movieIndex = Convert.ToInt32(button.Tag);
            string sessionTime = button.Text;
            string sessionDate = lblDate.Text;
            if (DateTime.Parse($"{sessionDate} {sessionTime}") < DateTime.Now)
            {
                MessageBox.Show("Seçilen seansı kaçırdınız. Başka seans seçebilirsiniz.");
                return;
            }
            this.Hide();
            form2.Show();
            form2.ListDetail(movieIndex, sessionTime, sessionDate);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            useDate = useDate.AddDays(1);
            lblDate.Text = useDate.ToShortDateString();
            btnPrev.Enabled = true;
            if(currentDate.AddDays(2) == useDate)
            {
                btnNext.Enabled = false;
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            useDate = useDate.AddDays(-1);
            lblDate.Text = useDate.ToShortDateString();
            if(currentDate == useDate)
            {
                btnPrev.Enabled = false;
            }
        }
    }
}
