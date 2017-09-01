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
        static bool checkip(string ip)
        {
            // Chuỗi có khoảng trắng
            if (ip.Contains(' ') == true) return false;
            //Chiều dài chuỗi không hợp lệ
            if (ip.Length == 0 || ip.Length > 1000) return false;
            //Chuỗi có chứa số
            for (var i = '0'; i <= '9'; i++)
            {
                if (ip.Contains(i) == true) return false;

            }
            //Chuỗi có đoạn gen trống hoặc 2 kí tự đầu không là kí tự thường hoặc chiều dài 1 đoạn gen <3
            int index = ip.IndexOf('_');
            while (index != -1)
            {
                if (ip[index - 1] == '_' || ip[index + 1] == '_') return false;
                if (ip[index + 1] < 'a' || ip[index + 1] > 'z' || ip[index + 2] < 'a' || ip[index + 2] > 'z') return false;
                int tmp = index;
                index = ip.IndexOf('_', index + 1);
                if (index - tmp <= 3 && index!=-1) return false;
            }
            return true;
        }
        static public string Nhap(string fPath)
        {
            string[] input = File.ReadAllLines(fPath);
            if (Nhapxuat.checkip(input[0]) == true)
                return input[0];
            else
                return "NULL";
        }
    }
}

namespace XyLyChuoi
{
    public class BienDoiChuoiGen
    {
        public List<string> TenPokemom = new List<string>{"Articuno","Cobalion","Dialga","Entei","Giratina","Groudon","HoOh","Keldeo","Kyogre","Landorus","Lugia","Moltres","Palkia","Raikou","Rayquaza","Suicune","Terrakion","Thundurus","Tornadus","Virizion","Xerneas","Yveltal","Zapdos" };
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

        //Tra ve chuoi la ten cua pokemon, nếu không có tên hay nhiều tên thì trả về chuỗi ""
        static public string KiemTraGen(List<string> Gen, List<string> TenPokemon)
        {
            string result = "";

            int i = 0;
            for(; i < Gen.Count ; i++)
            {
                if (TenPokemon.Contains(Gen[i]))
                {
                    result = Gen[i];
                }
            }

            for (; i < Gen.Count; i++)
            {
                if (TenPokemon.Contains(Gen[i]))
                {
                    if (result == Gen[i])
                        continue;
                    else
                        return "";
                }
            }

            return result;
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
