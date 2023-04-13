﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ship2
{
	public partial class FormShip : Form
	{
        private DrowingClassicShip ClassicShip;
        public DrowingClassicShip SelectedShip { get; private set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public FormShip()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Метод отрисовки корабля
        /// </summary>
        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBoxShip.Width, pictureBoxShip.Height);
            Graphics gr = Graphics.FromImage(bmp);
            ClassicShip.DrawShip(gr);
            pictureBoxShip.Image = bmp;
        }
        /// <summary>
        /// Метод  установки данных
        /// </summary>
        private void SetData()
        {
            Random rnd = new Random();
            ClassicShip.SetPosition(rnd.Next(10, 100), rnd.Next(10, 100), pictureBoxShip.Width, pictureBoxShip.Height);
            toolStripStatusLabelSpeed.Text = $"Скорость: {ClassicShip.Ship.MaxSpeed}";
            toolStripStatusLabelWeight.Text = $"Вес: {ClassicShip.Ship.Weight}";
            toolStripStatusLabelMainColor.Text = $"Цвет: {ClassicShip.Ship.MainColor.Name}";
        }
        /// <summary>
        /// Обработка нажатия кнопки "Создать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Color color = Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
            ColorDialog colorDialog = new ColorDialog();
            ColorDialog dialog = colorDialog;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                color = dialog.Color;
            }
            ClassicShip = new DrowingClassicShip(rnd.Next(100, 300), rnd.Next(1000, 2000), color);
            SetData();
            Draw();
        }
        /// <summary>
        /// Обработка нажатия кнопки "Модификация"
        /// </summary>
        private void ButtonCreateModify_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Color color = Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                color = dialog.Color;
            }
            Color dopColor = Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
            ColorDialog dialogDop = new ColorDialog();
            if (dialogDop.ShowDialog() == DialogResult.OK)
            {
                dopColor = dialogDop.Color;
            }
            ClassicShip = new DrowingBattleShip(rnd.Next(100, 300), rnd.Next(1000, 2000), color, dopColor, Convert.ToBoolean(rnd.Next(0, 2)), Convert.ToBoolean(rnd.Next(0, 2)));
            SetData();
            Draw();
        }
        /// <summary>
        /// Обработка нажатия кнопок управления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMove_Click(object sender, EventArgs e)
        {
            //получаем имя кнопки
            string name = ((Button)sender)?.Name ?? string.Empty;
            try
            {
                switch (name)
                {
                    case "buttonUp":
                        ClassicShip.MoveShip(Direction.Up);
                        break;
                    case "buttonDown":
                        ClassicShip.MoveShip(Direction.Down);
                        break;
                    case "buttonLeft":
                        ClassicShip.MoveShip(Direction.Left);
                        break;
                    case "buttonRight":
                        ClassicShip.MoveShip(Direction.Right);
                        break;
                }
                Draw();
            }
            catch (Exception)
            {
                MessageBox.Show("Создайте корабль!!!", "Ошибка взаимодействия!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            /// <summary>
            /// Изменение размеров формы
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        }
        private void PictureBoxShip_Resize(object sender, EventArgs e)
        {
            ClassicShip?.ChangeBorders(pictureBoxShip.Width, pictureBoxShip.Height);
            Draw();
        }

        private void ButtonSelectShip_Click(object sender, EventArgs e)
        {
            SelectedShip = ClassicShip;
            DialogResult = DialogResult.OK;
        }
    }
}
