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

namespace XyLyChuoi
{
    public class BienDoiChuoiGen
    {
        //list chứa vị trí các dấu gạch chân, mặc định dấu đầu tiên ở vị trí -1
        static List<int> theIndexOfTheSpace = new List<int>();

        //Trả về chuỗi gen được biến đổi theo gem mã hóa
        static public string ChuyenHoaGen(string Gen)
        {
            int IndexOfTheSpace = -1;
            
            //Tìm vị trí các dấu gạch chân xong bỏ vô list
            do
            {
                theIndexOfTheSpace.Add(IndexOfTheSpace);
                IndexOfTheSpace = Gen.IndexOf('_', IndexOfTheSpace + 1);
            } while (IndexOfTheSpace != -1);

            StringBuilder InputToCut = new StringBuilder(Gen);

            int count = 0;
            for (; count < theIndexOfTheSpace.Count; count++)
            {
                for (int i = 2; i < Gen.Length; i++)
                {
                    if (theIndexOfTheSpace.Contains(i - 1) || theIndexOfTheSpace.Contains(i - 2))
                        continue;

                    if (InputToCut[i] == InputToCut[theIndexOfTheSpace[count] + 1])
                        InputToCut[i] = InputToCut[theIndexOfTheSpace[count] + 2];
                }
            }

            return InputToCut.ToString();
        }

        //Trả về list các đoạn gen đã cắt và lượt phần đầu
        static public List<string> CatGen(string Gen)
        {
            //for (int i = 0; i < theIndexOfTheSpace.Count; i++)
            //    Gen.Remove(theIndexOfTheSpace[i] + 1, 2);

            string[] CutDone = Gen.Split('_');

            for (int i = 0; i < CutDone.Length ; i++)
            {
                CutDone[i] = CutDone[i].Remove(0, 2);
            }

            return CutDone.OfType<string>().ToList();
        }
    }
}

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string Input = XulyFile.Nhapxuat.Nhap("F:\\GitHub\\first-project\\BienDoiChuoiPart1\\test.txt");

            List<string> CutDone = XyLyChuoi.BienDoiChuoiGen.CatGen(XyLyChuoi.BienDoiChuoiGen.ChuyenHoaGen(Input));
            
            //Kiểm tra chuỗi đã cắt
            for (int i = 0; i < CutDone.Count; i++ )
            {
                Console.WriteLine(CutDone[i]);
            }

            Console.ReadKey();
        }
    }
}
