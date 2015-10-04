using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader ReadFile = new StreamReader("in.txt",Encoding.Default);
            string Input = null;
            int n = 0;
            bool end = false;
	        //Читаем из файла информацию
            Input = ReadFile.ReadToEnd();
            String[] parts = Input.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            n = Convert.ToInt32(parts[0]);
            using (StreamWriter WriteFile = new StreamWriter("out.txt"))
            {
                for (int i = 1; i < parts.Length; i++)
                {
                    WriteFile.WriteLine(parts[i]);
                    Console.WriteLine(parts[i]);
                }
            }

            ///////////////////////////////////////////////////////////////////////////////
            int[] matr = new int[parts.Length - 1];
            // заполняем матрицу размерами слов
            for (int i = 0; i < matr.Length; i++)
            {
                matr[i] = parts[i + 1].Length;
                // если слово длиннее чем строка
                if (matr[i] > n)
                {
                    Console.WriteLine("Не корректные значения!");
                    return;
                }
            }
            ////////////////////////////////////////////////////////////////////////////////
            int[] CountInStr = new int[matr.Length];
            // заполняем матрицу 0
            for (int i = 0; i < CountInStr.Length; i++)
            {
                CountInStr[i] = 0;
            }
            int CountOfWords = matr.Length;
            int k = 0;
            // заполняем матрицу количсетвом слов в строке
            while (end != true)
            {
                int temp = n;
                for (int i = matr.Length - CountOfWords; i < matr.Length; i++)
                {
                    if (i == matr.Length - 1)
                    {
                        temp -= matr[i] + 1;    // сколько осталось свободного места
                        --CountOfWords;         // уменьшаем количетво доступных слов
                        CountInStr[k] += 1;     // увеличиваем количество для вмещения
                    }
                    else
                    {
                        if (temp - matr[i] >= matr[i + 1] + 1)// если есть место для слова + пробел
                        {
                            temp -= matr[i] + 1;
                            --CountOfWords;
                            CountInStr[k] += 1;
                        }
                        else // если только для слова
                        {
                            temp -= matr[i];
                            --CountOfWords;
                            CountInStr[k] += 1;
                            k += 1;
                            break;
                        }
                    }
                }// если слова кончились
                if (CountOfWords == 0)
                {
                    end = true;
                }

            }
            int CountOfNewStr = 0;// к-ство новый строк
            foreach (int i in CountInStr)
            {
                if (i == 0) // если в строке 0 слов 
                {
                    continue;
                }
                Console.WriteLine(i);
                CountOfNewStr += 1;
            }
            ////////////////////////////////////
            int[] CountOfSpaces = new int[CountOfNewStr];
            // сколько пробелов нужно для разделения 
            int tempi = 1;
            for (int i = 0; i < CountOfSpaces.Length; i++)
            {
                int length = n;
                for (int z = 0; z < CountInStr[i]; z++)
                {
                    length -= parts[tempi].Length; // от длины строк отнимаем длины слов
                    tempi++;
                }
                CountOfSpaces[i] = length;         // свободное место на пробелы
                Console.WriteLine("Count of spaces {0}",CountOfSpaces[i]);
            }
            ////////////////////////////////строки пробелов
            int tempz = 1;
            // запись новой строки
            using (StreamWriter Write = new StreamWriter("out.txt"))
            {
                for (int i = 0; i < CountOfNewStr; i++)
                {
                    if (CountInStr[i] == 1)         // если в строке только 1 слово
                    {
                        Write.Write(parts[tempz]);
                        tempz++;
                    }
                    else if (CountInStr[i] > 1)     // больше 1 слова
                    {
                        int tempRaz = CountInStr[i] - 1;    // временная переменная к-ства мест между словами
                        int tempS = CountOfSpaces[i];       // временная переменная к-ства пробелов на данной строке
                        if (tempS % tempRaz == 0 && tempRaz != 1)// если можно поровну разставить пробелы между словами и мест между словами не 1
                        {
                            for (int l = 0; l < CountInStr[i]; l++)
                            {
                                Write.Write(parts[tempz]);
                                tempz++;
                                Write.Write(" ");
                            }

                        }
                        else
                        {
                            for (int l = 0; l < CountInStr[i]; l++)
                            {
                               
                                    if (tempRaz == 1)// если осталось 1 место для разстановки
                                    {
                                        Write.Write(parts[tempz]);
                                        tempz++;
                                        for (int v = 0; v < tempS; v++)
                                        {
                                            Write.Write(" ");
                                        }
                                        Write.Write(parts[tempz]);
                                        tempz++;
                                        break;
                                    }
                                    else // если больше мест
                                    {
                                        Write.Write(parts[tempz]);
                                        tempz++;
                                        for (int v = 0; v < (tempS / tempRaz) - 1; v++)
                                        {
                                            Write.Write(" ");
                                        }
                                        tempS -= (tempS / tempRaz) - 1;// уменьшаем к-ство оставшихся пробелов
                                        tempRaz--;                     // уменьшаем к-ство отсавшихся мест для разстановки
                                    }
                                
                            }
                        }
                    }
                    Write.WriteLine();// закончить строку
                }
            }
            Console.ReadLine();
        }
    }
}

