using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace КрестикиНолики
{
    public partial class main : Form
    {
        /// <summary>
        /// Флаг, указывающий на то, 
        /// кому принадлежит следующий ход
        /// </summary>
        private bool sym;
        /// <summary>
        /// Флаг завершения игры
        /// </summary>
        private bool fin;
        /// <summary>
        /// Результат побед крестиков
        /// </summary>
        private int xResultVal;
        /// <summary>
        /// Результат побед ноликов
        /// </summary>
        private int oResultVal;
        public main()
        {
            InitializeComponent();
            init();
        }
        /// <summary>
        /// Инициализатор класса
        /// </summary>
        private void init()
        {
            //Ставим результаты побед в 0
            xResultVal = oResultVal = 0;
            //Указываем, что игра ещё идёт
            fin = false;
            //По умолчанию первым будет ходить 'крестик'
            sym = false;
            //Инициализируем события для кнопок игрового поля
            initButtonsEvents();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            xResultVal = oResultVal = 0;
            clearGameField();
            fin = false;
            sym = false;
            updateVinResults();
        }
        /// <summary>
        /// Инициализируем события для кнопок игрового поля
        /// </summary>
        private void initButtonsEvents()
        {
            //Проходимся по всем контроллам, на панели с кнопками
            //Т.к. мы точно знаем, что у нас там только кнопки
            //то сразу ставим тип Button. В противном случае, 
            //нужно было бы ставиить тип object, и при каждой итерации
            //проверять его, на соответствие button. Я специально подробно
            //расписал такой вариант ниже, в методе clearGameFieldUniversal
            foreach (Button but in gameFieldPanel.Controls)
                //Добавляем обработчик события клика по кнопке
                but.Click += But_Click;
        }
        /// <summary>
        /// Обработчик события клика по кнопке на игровом поле
        /// </summary>
        private void But_Click(object sender, EventArgs e)
        {
            //Если игра всё ещё идёт
            if (!fin)
            {
                //При стандартном соыбтии, нам всегда 
                //передаётся контролл, для которого оно произошло.
                //Просто приводим его к нужному нам типу.
                Button but = (Button)sender;
                //Проставляем на кнопке соответствующее значение
                setButtonValue(but);
                //Проверка завершения игры на этом ходе
                checkFinal();
            }
        }
        /// <summary>
        /// Клик по кнопке запуска игры против бота
        /// </summary>
        private void startVsPlayer_Click(object sender, EventArgs e)
        {
            //Очищаем игровое поле
            clearGameField();
            //Указываем, что игра началась
            fin = false;
            //По умолчанию первым будет ходить 'крестик'
            sym = false;
        }
        /// <summary>
        /// Очищаем игровое поле
        /// </summary>
        private void clearGameField()
        {
            //Проходимся по всем контроллам, на панели с кнопками
            //Т.к. мы точно знаем, что у нас там только кнопки
            //то сразу ставим тип Button. В противном случае, 
            //нужно было бы ставиить тип object, и при каждой итерации
            //проверять его, на соответствие button. Я специально подробно
            //расписал такой вариант ниже, в методе clearGameFieldUniversal
            foreach (Button but in gameFieldPanel.Controls)
            {
                //Сбрасываем текст
                but.Text = "";
                //Сбрасываем цвет
                but.ForeColor = Color.Black;
            }
        }
        /// <summary>
        /// Проставляем значение на кнопке
        /// </summary>
        /// <param name="but">Кнопка, для обработки</param>
        private void setButtonValue(Button but)
        {
            //Если на этой кнопке уже есть значение
            if (but.Text.Length > 0)
                //Выводим сообщение об этом
                MessageBox.Show("Сюда поставить нельзя!");
            //Если кнопка "пустая"
            else
            {
                //Если "Крестик"
                if (sym)
                {
                    //Ставим крестик на кнопке
                    but.Text = "X";
                    //Красим текст на ней в синий цвет (чисто для красоты)
                    but.ForeColor = Color.Blue;
                }
                //Если "Нолик"
                else
                {
                    //Ставим нолик на кнопке
                    but.Text = "0";
                    //Красим текст на ней в красный цвет (чисто для красоты)
                    but.ForeColor = Color.Red;
                }

                //Меняем значение на противоположное
                sym = !sym;
            }
        }
        /// <summary>
        /// Проверка завершения игры на этом ходе
        /// </summary>
        private void checkFinal()
        {

            /*
             Корчое говоря, решение тут - предельно тупое.
             Но, мне влом в 3 часа ночи придумывать что-либо изаящнее.

            Короче говоря, в чём суть. Названия кнопок на игровом поле
            созданы по одному принципу: слово field_ и координаты кнопки
            в сетке 3х3. Например - field_02, это кнопка из первого столбца
            третьего ряда.

            Так вот, мы собираем все координаты выбранных ноликов и 
            крестиков, и просто сравниваем их со списком уже известных решений.
            */

            //Инициализируем массив выигрышных комбинаций
            string[][] winResults = new string[][]
            {
                new string[] {"00", "01", "02"},
                new string[] {"10", "11", "12"},
                new string[] {"20", "21", "22"},
                new string[] {"00", "10", "20"},
                new string[] {"01", "11", "21"},
                new string[] {"02", "12", "22"},
                new string[] {"00", "11", "22"},
                new string[] {"02", "11", "20"}
            };

            //Результаты нажатий
            List<string> xValues = new List<string>();
            List<string> OValues = new List<string>();

            //Проходимся по всем контроллам, на панели с кнопками
            //Т.к. мы точно знаем, что у нас там только кнопки
            //то сразу ставим тип Button. В противном случае, 
            //нужно было бы ставиить тип object, и при каждой итерации
            //проверять его, на соответствие button. Я специально подробно
            //расписал такой вариант ниже, в методе clearGameFieldUniversal
            foreach (Button but in gameFieldPanel.Controls)
            {
                //Если на кнопке крестик
                if (but.Text == "X")
                    //Добавляем координаты из названия в списк крестика
                    xValues.Add(but.Name.Replace("field_", ""));
                //Если на кнопке нолик
                else if (but.Text == "0")
                    //Добавляем координаты из названия в списк нолика
                    OValues.Add(but.Name.Replace("field_", ""));
            }

            //Если победили крестики
            if (checkWin(winResults, xValues))
            {
                //Игра завершена
                fin = true;
                //Выводим сообщение о победе
                MessageBox.Show("Победили крестики!");
                //Увеличиваем количество побед крестиков
                xResultVal++;
                //Обновляем значения результатов побед
                updateVinResults();
            }
            //Если победили нолики
            else if (checkWin(winResults, OValues))
            {
                //Игра завершена
                fin = true;
                //Выводим сообщение о победе
                MessageBox.Show("Победили нолики!");
                //Увеличиваем количество побед ноликов
                oResultVal++;
                //Обновляем значения результатов побед
                updateVinResults();
            }
            //Если наставлено уже 9 знаков
            else if (OValues.Count + xValues.Count == 9)
            {
                //Игра завершена
                fin = true;
                //Выводим сообщение о победе
                MessageBox.Show("Ничья!");
            }
            //В остальных случаях - игра идёт дальеше
        }
        /// <summary>
        /// Обновляем значения результатов побед
        /// </summary>
        private void updateVinResults()
        {
            //Обновляем значения результатов побед
            xResult.Text = xResultVal.ToString();
            oResult.Text = oResultVal.ToString();
        }

        /// <summary>
        /// Проверяем существующие комбинации с выигрышными
        /// </summary>
        /// <param name="winResults">Выигрышные комбинации</param>
        /// <param name="values">Набранные комбинации</param>
        /// <returns>True - победа</returns>
        private bool checkWin(string[][] winResults, List<string> values)
        {
            bool ex = true;

            //Проходимся по списку выигрышных комбинаций
            for (int i = 0; i < winResults.Length; i++)
            {
                //Ставим флаг в true
                ex = true;
                //Проходимся по всем цифрам комбинации
                for (int j = 0; j < winResults[i].Length; j++)
                {
                    //Проверяем нахождение комбинации, 
                    //при помощи логического сложения
                    ex = ex && values.Contains(winResults[i][j]);
                }
                //Если хоть одной цифры из комбинации не найдётся среди 
                //отмеченных, это будет считаться проигрышем

                //Если все цифры комбинации мы нашли в списке
                if (ex)
                    //Выходим из массива
                    break;
            }

            return ex;
        }

    }
}
