using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbitrary
{
    class Program
    {
        static int select_n()
        {
            int n;
            while (true)
            {
                Console.WriteLine("insert your parameter (integer)");
                if (int.TryParse(Console.ReadLine(), out n))
                    break;
                else
                    Console.WriteLine("your input is not viable. please select an integer.");
            }
            Console.WriteLine("how many functions do you want to add to your calculator? insert a number. it must greater than 0");
            return n;
        }
        static int select_f()
        {
            int f;
            while (true)
            {
                Console.WriteLine("insert your number (integer)");
                if (int.TryParse(Console.ReadLine(), out f))
                {
                    if (f <= 0)
                    {
                        Console.WriteLine("your input is not viable. please select an integer.");
                        continue;
                    }
                    break;
                }
                else
                    Console.WriteLine("your input is not viable. please select an integer.");
            }
            return f;
        }
        static int select_type()
        {
            Console.WriteLine("what kind of methods do you want to use: ");
            Console.WriteLine("1: strings (add_one, subtract_two, multiply_by_three, divide_by_four");
            Console.WriteLine("2: arithmetic (x + 1, x - 2, x * 3, x / 4)");
            int type;
            while (true)
            {
                Console.WriteLine("insert your number (integer)");
                if (int.TryParse(Console.ReadLine(), out type))
                {
                    if (type <= 0 || type >= 3)
                    {
                        Console.WriteLine("your input is not viable. please select an integer. it must be either 1 or 2");
                        continue;
                    }
                    break;
                }
                else
                    Console.WriteLine("your input is not viable. please select an integer.");
            }
            return type;
        }
        static readonly string[] numbers = new[] { "zero", "one", "two", "three","four", "five", "six", "seven", "eight", "nine" };
        static bool parseEnglishNumber(string num, out int number)
        {
            number = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i].Equals(num))
                {
                    number = i;
                    return true;
                }
            }
            Console.WriteLine("the english number you have entered cannot be identified. please use one of these: zero, one, two, three, four, five, six, seven, eight, nine ");
            return false;
        }
        static Func<int, int> parse_string_method()
        {
            while (true)
            {
                string method = Console.ReadLine().ToLowerInvariant();
                string[] data = method.Split('_');
                int number;
                if (!parseEnglishNumber(data[data.Length - 1], out number))
                    continue;
                if (data[0] == "add") return (p) => { return p + number; };
                else if (data[0] == "subtract") return (p) => { return p - number; };
                else if (data[0] == "multiply") return (p) => { return p * number; };
                else if (data[0] == "divide") return (p) => { return p / number; };
                else
                {
                    Console.WriteLine("the method you have input cannot be identified. please use one of these: add, subtract, multiply or divide");
                    continue;
                }
            }
        }
        static Func<int, int> parse_arithmetic_method()
        {
            while (true)
            {
                string method = Console.ReadLine().ToLowerInvariant();
                string[] data = method.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (data.Length != 3)
                {
                    Console.WriteLine("the method you have entered is not valid. it must follow the pattern: x [operator] [integer]; example x + 5");
                    continue;
                }
                int number;
                if (!int.TryParse(data[2], out number))
                {
                    Console.WriteLine("the number you have entered cannot be parsed. please input an integer.");
                    continue;
                }
                if (data[1] == "+") return (p) => { return p + number; };
                else if (data[1] == "-") return (p) => { return p - number; };
                else if (data[1] == "*") return (p) => { return p * number; };
                else if (data[1] == "/") return (p) => { return p / number; };
                else
                {
                    Console.WriteLine("the arithmetic operator you have input cannot be identified. use one of these: +, -, * or /");
                    continue;
                }
            }
        }
        static void call(Func<int, int> fn, int param)
        {
            try { Console.Write(fn(param)); }
            catch (Exception e) { Console.Write(e.Message); }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("welcome to arbitrary calculator!");
            int n = select_n();
            int f = select_f();
            int type = select_type();
            List<Func<int, int>> fns = new List<Func<int, int>>();
            for (int i = 0; i < f; i++)
            {
                if (type == 1)
                    fns.Add(parse_string_method());
                else
                    fns.Add(parse_arithmetic_method());
            }
            Console.Write("results:[");
            if (f > 0)
            {
                call(fns[0], n);
                for (int i = 1; i < fns.Count; i++)
                {
                    Console.Write(",");
                    call(fns[i], n);
                }
            }
            Console.WriteLine("]");
        }
    }
}
