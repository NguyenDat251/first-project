using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace XulyFile
{
    public class Nhapxuat
    {
        static public string Nhap(string fPath)
        {
            string[] input = File.ReadAllLines(fPath);
            return input[0];
        }
    }
}
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string Input = XulyFile.Nhapxuat.Nhap("test.txt");
            int IndexOfTheSpace = -1;
            Console.WriteLine(Input);
            //list chứa vị trí các dấu gạch chân, mặc định dấu đầu tiên ở vị trí -1
            List<int> theIndexOfTheSpace = new List<int>();
            //Tìm vị trí các dấu gạch chân xong bỏ vô list
            do
            {
                theIndexOfTheSpace.Add(IndexOfTheSpace);
                IndexOfTheSpace = Input.IndexOf('_', IndexOfTheSpace + 1);
            } while (IndexOfTheSpace != -1);

            StringBuilder InputToCut = new StringBuilder(Input);

            int count = 0;
            for (; count < theIndexOfTheSpace.Count; count++)
            {
                for (int i = 2; i < Input.Length; i++)
                {
                    if (theIndexOfTheSpace.Contains(i - 1) || theIndexOfTheSpace.Contains(i - 2))
                        continue;

                    if (InputToCut[i] == InputToCut[theIndexOfTheSpace[count] + 1])
                        InputToCut[i] = InputToCut[theIndexOfTheSpace[count] + 2];
                }
            }

            
            //đây là phần t thay đổi coi m có thấy không 

            Console.WriteLine(InputToCut);
            //Console.WriteLine("Dzot xau trai vc");
            //Console.WriteLine("test push lan 2");

            Console.ReadKey();
        }
    }
}
