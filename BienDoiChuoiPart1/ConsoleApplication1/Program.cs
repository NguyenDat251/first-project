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

        static public void Xuat(string fPath, string[] content)
        {
            File.WriteAllLines(fPath, content);
        }
    }
}

namespace XuLyChuoi
{
    public class BienDoiChuoiGen
    {
        //Trả về chuỗi gen được biến đổi theo gem mã hóa
        static public string ChuyenHoa(string Gen)
        {
            //list chứa vị trí các dấu gạch chân, mặc định dấu đầu tiên ở vị trí -1
            List<int> theIndexOfTheSpace = new List<int>();
            
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

        static public  string MaHoaGen(string Gen)
        {
            int index = Gen.IndexOf("zz");

            //có gen thông tin zz
            if(index != -1 && Gen[index - 1] == '_')
            {
                string[] ListGen = new string[3];
                ListGen[0] = ChuyenHoa(Gen);

                int i = 1;
                while(true)
                {
                    ListGen[i] = ChuyenHoa(ListGen[i - 1]);

                    if (ListGen[i] == ListGen[i - 1])
                        return ListGen[i];
                    else if (ListGen[i] == Gen)
                        return null;
                    i++;
                }
            }

            //Không có gen thông tin zz
            return ChuyenHoa(Gen);
        }

        //Trả về list các đoạn gen đã cắt và lượt phần đầu
        static public List<string> CatGen(string Gen)
        {
            string[] CutDone = Gen.Split('_');

            for (int i = 0; i < CutDone.Length ; i++)
            {
                CutDone[i] = CutDone[i].Remove(0, 2);
            }

            return CutDone.OfType<string>().ToList();
        }

        //Tra ve chuoi la ten cua pokemon, nếu không có tên hay nhiều tên thì trả về chuỗi ""
        static public string findPokemon(List<string> Gen, List<string> TenPokemon)
        {
            string Pokemon = "";

            int i = 0;
            //tìm ra con pokemon đầu tiên
            for(; i < Gen.Count ; i++)
            {
                if (TenPokemon.Contains(Gen[i]))
                {
                    Pokemon = Gen[i];
                    break;
                }
            }

            //kiểm tra xem có pokemon nào khác con Pokemon đã tìm thấy ở trên không
            for (i++; i < Gen.Count; i++)
            {
                if (TenPokemon.Contains(Gen[i]))
                {
                    if (Pokemon == Gen[i])
                        continue;
                    else
                        return null;
                }
            }

            return Pokemon;
        }

        static public string createSkill(List<string> Gen, string Pokemon)
        {
            string skill = Pokemon;
            for (int i = 0; i < Gen.Count; ++i)
                if (Gen[i] == Pokemon)  Gen.Remove(Gen[i--]);

            IEnumerable<string> SortedGen = from gen in Gen
                                        orderby gen.Length, gen.Substring(0, 1), gen.Substring(1, 1)
                                        select gen;

            foreach (string gen in SortedGen)  
                skill += gen;

                return skill;
        }
    }
}

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> TenPokemon = new List<string>{"Articuno","Cobalion","Dialga","Entei","Giratina","Groudon","HoOh","Keldeo","Kyogre","Landorus","Lugia","Moltres","Palkia","Raikou","Rayquaza","Suicune","Terrakion","Thundurus","Tornadus","Virizion","Xerneas","Yveltal","Zapdos" };
            
            //-------------------------------------------------------Input
            string Input = XulyFile.Nhapxuat.Nhap("test.txt");

            //----------------------------------------------------------Ma hoa
            string GenMaHoa = XuLyChuoi.BienDoiChuoiGen.MaHoaGen(Input);
            string[] output = {"NULL", "NULL"};

            if(GenMaHoa != null)
            {
                output[0] = GenMaHoa;
                List<string> CutDone = XuLyChuoi.BienDoiChuoiGen.CatGen(GenMaHoa);

                string poke = XuLyChuoi.BienDoiChuoiGen.findPokemon(CutDone, TenPokemon);

                if (poke != null)
                {
                    string Skill = XuLyChuoi.BienDoiChuoiGen.createSkill(CutDone, poke);

                    if (Skill != null) output[1] = Skill;
                }
            }

            //------------------------------------------Output
            XulyFile.Nhapxuat.Xuat("Output.txt", output);

            
        }
    }
}
