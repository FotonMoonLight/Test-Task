using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task_Compression.Classes
{
    public static class Comoresion_logic
    {
        public static string Compression(this string str)
        {
            char _Flag = str[0]; //Устанавливаем первый элемент
            int _CountChars = 1;
            string _Answer = "";
            for (int i = 1; i < str.Length; i++) //Начинаем с 1 так как не нужно проверять 0-ой элемент с собой 
            {
                

                if(str[i] == _Flag)
                {
                    _CountChars++;
                }
                else
                {
                    _Answer += _Flag;//Присваеваем предыдущий элемент так как str[i] не равна ему
                    if(_CountChars > 1)//Если букв больше чем 1, то присваиваем число
                        _Answer += _CountChars;
                    _Flag = str[i];
                    _CountChars = 1;
                    
                }
            }
            _Answer += _Flag;//Присваиваем последний элемент
            if (_CountChars > 1)
            {
                _Answer += _CountChars;
            }

            return _Answer;
        }
        public static string Decompressions(this string str) 
        {
            string _Answer = "";
            char currentChar = str[0];//Из-за компрессии 0-ой элемент всегда буква
            for (int i = 0; i < str.Length-1; i++) //Идем по строке без учета последнего символа
            {
                if (char.IsDigit(str[i]))//Если число заполняем символами
                {
                    for(int j = 48;j < str[i]-1; j++)//Char переводит символы в код ASCII, где 0 это 48
                    {
                        _Answer += currentChar;//Заполняем символом который запомнил 
                        Console.WriteLine(Convert.ToInt32(str[i]));
                    }
                    currentChar = str[i+1];//Устанавливаем следующую букву как текущую
                }
                else//Если идет не число то просто прибовляем к ответу текущий символ
                {   
                    _Answer += str[i];
                    currentChar = str[i];
                }
            }
            currentChar = str[str.Length - 1];
            if (char.IsDigit(currentChar) == false)//Проверяем последний символ
                _Answer += currentChar;//Если не число просто прибавляем к ответу
            else
            {
                for (int j = 48;j < currentChar-1; j++)//Если число, то заполняем ответ циклом
                {
                    _Answer += str[str.Length - 2];
                }
            }
            return _Answer;
        }

    }
}
