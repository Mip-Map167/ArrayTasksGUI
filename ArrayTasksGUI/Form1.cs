using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ArrayTasksGUI
{
    public partial class Form1 : Form
    {
        // Элементы управления
        private ComboBox cmbTasks;
        private Button btnExecute;
        private TextBox txtInput;
        private ListBox lstResult;
        private Label lblInstruction;
        private Label lblResult;
        private Panel panelMain;

        // Переменные для хранения данных
        private int[] array;
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            // Настройка формы
            this.Text = "Задачи с массивами";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 250);

            // Создание элементов управления
            panelMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.Transparent
            };

            // Заголовок
            Label lblTitle = new Label
            {
                Text = "Работа с массивами в C#",
                Font = new Font("Arial", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 150),
                Location = new Point(20, 20),
                Size = new Size(400, 40)
            };

            // Выбор задачи
            Label lblSelectTask = new Label
            {
                Text = "Выберите задачу:",
                Font = new Font("Arial", 12),
                Location = new Point(20, 80),
                Size = new Size(120, 25)
            };

            cmbTasks = new ComboBox
            {
                Location = new Point(150, 80),
                Size = new Size(300, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 11),
                BackColor = Color.White
            };

            // Добавление задач в выпадающий список
            cmbTasks.Items.AddRange(new object[]
            {
                "1. Сумма элементов массива",
                "2. Поиск максимума и минимума",
                "3. Подсчет четных и нечетных",
                "4. Реверс массива",
                "5. Сдвиг элементов",
                "6. Подсчет дубликатов"
            });
            cmbTasks.SelectedIndex = 0;

            // Кнопка выполнения
            btnExecute = new Button
            {
                Text = "Выполнить задачу",
                Location = new Point(470, 80),
                Size = new Size(150, 35),
                Font = new Font("Arial", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(100, 150, 200),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnExecute.FlatAppearance.BorderSize = 0;
            btnExecute.Click += BtnExecute_Click;

            // Инструкция
            lblInstruction = new Label
            {
                Text = "Введите числа (через пробел):",
                Font = new Font("Arial", 11),
                Location = new Point(20, 130),
                Size = new Size(200, 25)
            };

            // Поле ввода
            txtInput = new TextBox
            {
                Location = new Point(220, 130),
                Size = new Size(400, 30),
                Font = new Font("Arial", 11),
                BackColor = Color.White
            };

            // Метка для результатов
            lblResult = new Label
            {
                Text = "Результат:",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(20, 180),
                Size = new Size(100, 25)
            };

            // Список для результатов
            lstResult = new ListBox
            {
                Location = new Point(20, 210),
                Size = new Size(740, 320),
                Font = new Font("Consolas", 11),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Кнопка для генерации случайных чисел
            Button btnRandom = new Button
            {
                Text = "Случайные числа",
                Location = new Point(630, 130),
                Size = new Size(130, 30),
                Font = new Font("Arial", 10),
                BackColor = Color.FromArgb(150, 200, 150),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRandom.FlatAppearance.BorderSize = 0;
            btnRandom.Click += BtnRandom_Click;

            // Кнопка очистки
            Button btnClear = new Button
            {
                Text = "Очистить",
                Location = new Point(630, 170),
                Size = new Size(130, 30),
                Font = new Font("Arial", 10),
                BackColor = Color.FromArgb(200, 150, 150),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += (s, e) =>
            {
                txtInput.Clear();
                lstResult.Items.Clear();
            };

            // Добавление элементов на панель
            panelMain.Controls.AddRange(new Control[]
            {
                lblTitle, lblSelectTask, cmbTasks, btnExecute,
                lblInstruction, txtInput, lblResult, lstResult,
                btnRandom, btnClear
            });

            // Добавление панели на форму
            this.Controls.Add(panelMain);
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                lstResult.Items.Clear();
                lstResult.Items.Add("▶ Выполняется задача...");
                lstResult.Items.Add("");

                string input = txtInput.Text.Trim();

                // Если поле пустое, создаем массив по умолчанию
                if (string.IsNullOrEmpty(input))
                {
                    array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                    lstResult.Items.Add("Используются числа по умолчанию: 1-10");
                }
                else
                {
                    // Разбираем введенные числа
                    string[] parts = input.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    array = new int[parts.Length];

                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (int.TryParse(parts[i], out int num))
                        {
                            array[i] = num;
                        }
                        else
                        {
                            MessageBox.Show($"Ошибка в числе '{parts[i]}'! Пожалуйста, введите целые числа.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                lstResult.Items.Add($"Исходный массив: [{string.Join(", ", array)}]");
                lstResult.Items.Add("");

                // Выполнение выбранной задачи
                switch (cmbTasks.SelectedIndex)
                {
                    case 0:
                        Task1_SumOfArray();
                        break;
                    case 1:
                        Task2_MinMax();
                        break;
                    case 2:
                        Task3_EvenOdd();
                        break;
                    case 3:
                        Task4_ReverseArray();
                        break;
                    case 4:
                        Task5_ShiftArray();
                        break;
                    case 5:
                        Task6_CountDuplicates();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRandom_Click(object sender, EventArgs e)
        {
            int size = random.Next(5, 11); // Случайный размер от 5 до 10
            int[] randomArray = new int[size];

            for (int i = 0; i < size; i++)
            {
                randomArray[i] = random.Next(1, 51); // Числа от 1 до 50
            }

            txtInput.Text = string.Join(" ", randomArray);
        }

        // Задача 1: Сумма элементов массива
        private void Task1_SumOfArray()
        {
            int sum = 0;
            foreach (int num in array)
            {
                sum += num;
            }

            lstResult.Items.Add("=== Сумма элементов массива ===");
            lstResult.Items.Add($"Сумма всех элементов: {sum}");
            lstResult.Items.Add($"Среднее арифметическое: {(double)sum / array.Length:F2}");
        }

        // Задача 2: Поиск максимума и минимума
        private void Task2_MinMax()
        {
            int min = array[0];
            int max = array[0];
            int minIndex = 0;
            int maxIndex = 0;

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                    minIndex = i;
                }
                if (array[i] > max)
                {
                    max = array[i];
                    maxIndex = i;
                }
            }

            lstResult.Items.Add("=== Поиск максимума и минимума ===");
            lstResult.Items.Add($"Минимальный элемент: {min} (индекс {minIndex})");
            lstResult.Items.Add($"Максимальный элемент: {max} (индекс {maxIndex})");
        }

        // Задача 3: Подсчет четных и нечетных
        private void Task3_EvenOdd()
        {
            int evenCount = 0;
            int oddCount = 0;

            foreach (int num in array)
            {
                if (num % 2 == 0)
                    evenCount++;
                else
                    oddCount++;
            }

            lstResult.Items.Add("=== Подсчет четных и нечетных ===");
            lstResult.Items.Add($"Четных чисел: {evenCount}");
            lstResult.Items.Add($"Нечетных чисел: {oddCount}");

            // Добавим дополнительную информацию
            lstResult.Items.Add($"Процент четных: {evenCount * 100.0 / array.Length:F1}%");
            lstResult.Items.Add($"Процент нечетных: {oddCount * 100.0 / array.Length:F1}%");
        }

        // Задача 4: Реверс массива
        private void Task4_ReverseArray()
        {
            // Создаем копию массива для демонстрации
            int[] reversedArray = new int[array.Length];
            Array.Copy(array, reversedArray, array.Length);

            // Реверсируем
            Array.Reverse(reversedArray);

            lstResult.Items.Add("=== Реверс массива ===");
            lstResult.Items.Add($"Исходный массив: [{string.Join(", ", array)}]");
            lstResult.Items.Add($"Реверсированный: [{string.Join(", ", reversedArray)}]");

            // Дополнительно: реверс на месте
            lstResult.Items.Add("");
            lstResult.Items.Add("--- Реверс на месте (без создания нового массива) ---");

            int[] tempArray = new int[array.Length];
            Array.Copy(array, tempArray, array.Length);

            for (int i = 0; i < tempArray.Length / 2; i++)
            {
                int temp = tempArray[i];
                tempArray[i] = tempArray[tempArray.Length - 1 - i];
                tempArray[tempArray.Length - 1 - i] = temp;
            }

            lstResult.Items.Add($"Результат: [{string.Join(", ", tempArray)}]");
        }

        // Задача 5: Сдвиг элементов
        private void Task5_ShiftArray()
        {
            if (array.Length == 0) return;

            int[] shiftedArray = new int[array.Length];
            Array.Copy(array, shiftedArray, array.Length);

            // Циклический сдвиг вправо
            int lastElement = shiftedArray[shiftedArray.Length - 1];

            for (int i = shiftedArray.Length - 1; i > 0; i--)
            {
                shiftedArray[i] = shiftedArray[i - 1];
            }

            shiftedArray[0] = lastElement;

            lstResult.Items.Add("=== Сдвиг элементов вправо ===");
            lstResult.Items.Add($"Исходный массив:  [{string.Join(", ", array)}]");
            lstResult.Items.Add($"После сдвига:    [{string.Join(", ", shiftedArray)}]");

            // Дополнительно: сдвиг влево
            int[] leftShifted = new int[array.Length];
            Array.Copy(array, leftShifted, array.Length);

            int firstElement = leftShifted[0];

            for (int i = 0; i < leftShifted.Length - 1; i++)
            {
                leftShifted[i] = leftShifted[i + 1];
            }

            leftShifted[leftShifted.Length - 1] = firstElement;

            lstResult.Items.Add("");
            lstResult.Items.Add("--- Сдвиг влево для сравнения ---");
            lstResult.Items.Add($"После сдвига влево: [{string.Join(", ", leftShifted)}]");
        }

        // Задача 6: Подсчет дубликатов
        private void Task6_CountDuplicates()
        {
            lstResult.Items.Add("=== Подсчет дубликатов ===");

            Dictionary<int, int> counts = new Dictionary<int, int>();

            foreach (int num in array)
            {
                if (counts.ContainsKey(num))
                    counts[num]++;
                else
                    counts[num] = 1;
            }

            // Вывод результатов
            foreach (var pair in counts)
            {
                string word = pair.Value == 1 ? "раз" :
                             (pair.Value >= 2 && pair.Value <= 4) ? "раза" : "раз";
                lstResult.Items.Add($"Число {pair.Key} встречается {pair.Value} {word}");
            }

            // Дополнительная статистика
            lstResult.Items.Add("");
            lstResult.Items.Add("--- Статистика ---");
            lstResult.Items.Add($"Всего уникальных чисел: {counts.Count}");
            lstResult.Items.Add($"Всего элементов: {array.Length}");

            // Поиск самого частого числа
            int mostFrequent = 0;
            int maxCount = 0;
            foreach (var pair in counts)
            {
                if (pair.Value > maxCount)
                {
                    maxCount = pair.Value;
                    mostFrequent = pair.Key;
                }
            }

            if (maxCount > 1)
            {
                lstResult.Items.Add($"Самое частое число: {mostFrequent} (встречается {maxCount} раз)");
            }
            else
            {
                lstResult.Items.Add("Все числа уникальны (нет повторений)");
            }
        }
    }
}