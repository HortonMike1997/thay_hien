using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using Tesseract;
using System.Threading.Tasks;

namespace ConnectToMaple
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public static float bien_a = 0, bien_b = 0, bien_c = 0;
        private void btOCR_Click(object sender, EventArgs e)
        {

            //bien_a = 0; bien_b = 0; bien_c = 0;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                var img = new Bitmap(openFileDialog.FileName);
                var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                var page = ocr.Process(img);
                string equation = page.GetText();
                var builder = new StringBuilder();
                List<string> vetrai = new List<string>();
                List<string> vephai = new List<string>();
                int flagvetrai = 1;
                string output = string.Empty;
                output += equation + "\r\n";

                equation = equation.Substring(equation.IndexOf(":") + 1);
                //xoa khoang trang
                equation = equation.Replace(" ", string.Empty);
                equation = equation.Replace('—', '-');
                equation = equation.Replace('I', '1');
                equation = equation.Replace('Z', '2');
                equation = equation.Replace('S', '5');

                //equation = equation.Replace("\n", string.Empty);

                output += equation + "\r\n";
                for (int i = 0; i < equation.Length; i++)
                {
                    if (equation[i] == '+' || equation[i] == '-' || equation[i] == '—' || equation[i] == '=' || i + 1 == equation.Length)
                    {
                        if (builder.ToString() != "" && flagvetrai == 1)
                        {
                            vetrai.Add(builder.ToString());
                        }
                        if (builder.ToString() != "" && flagvetrai == 0)
                        {
                            vephai.Add(builder.ToString());
                        }
                        builder.Clear();
                        if (equation[i] == '=')
                        {
                            flagvetrai = 0;
                            continue;
                        }

                    }
                    builder.Append(equation[i]);
                }
                builder.Clear();
                string out2 = String.Empty;
                float bien2 = 0, bienY = 0, bien1 = 0, bien0 = 0;
                int bientam = 0, dkX = 0;
                //Ve trai get bien
                for (int i = 0; i < vetrai.Count; i++)
                {
                    bientam = 0;
                    dkX = 0;
                    for (int j = 0; j < vetrai[i].Length; j++)
                    {

                        if (vetrai[i].ElementAt(j) == 'Y' || vetrai[i].ElementAt(j) == 'y')
                        {
                            dkX = 1;
                            if (builder.ToString() == "" || builder.ToString() == "-" || builder.ToString() == "+" || builder.ToString() == "\n")
                                builder.Append(1);
                            Int32.TryParse(builder.ToString(), out bientam);
                            bienY += bientam;
                            break;
                        }
                        if (vetrai[i].ElementAt(j) == 'X' || vetrai[i].ElementAt(j) == 'x')
                        {
                            dkX = 1;
                            if (builder.ToString() == "" || builder.ToString() == "-" || builder.ToString() == "+" || builder.ToString() == "\n")
                                builder.Append(1);
                            if (j + 1 < vetrai[i].Length)
                            {

                                if (vetrai[i].ElementAt(j + 1) == '2' || vetrai[i].ElementAt(j + 1) == '²')
                                {
                                    Int32.TryParse(builder.ToString(), out bientam);
                                    bien2 -= bientam;
                                    break;
                                }
                            }

                            Int32.TryParse(builder.ToString(), out bientam);
                            bien1 -= bientam;
                            break;

                        }
                        builder.Append(vetrai[i].ElementAt(j));
                    }
                    if (dkX == 0)
                    {
                        Int32.TryParse(builder.ToString(), out bientam);
                        bien0 -= bientam;
                    }
                    builder.Clear();
                }
                //Ve phai get bien
                for (int i = 0; i < vephai.Count; i++)
                {
                    bientam = 0;
                    dkX = 0;
                    for (int j = 0; j < vephai[i].Length; j++)
                    {

                        if (vephai[i].ElementAt(j) == 'Y' || vephai[i].ElementAt(j) == 'y')
                        {
                            dkX = 1;
                            if (builder.ToString() == "" || builder.ToString() == "-" || builder.ToString() == "+" || builder.ToString() == "\n")
                                builder.Append(1);
                            Int32.TryParse(builder.ToString(), out bientam);
                            bienY -= bientam;
                            break;
                        }
                        if (vephai[i].ElementAt(j) == 'X' || vephai[i].ElementAt(j) == 'x')
                        {
                            dkX = 1;
                            if (builder.ToString() == "" || builder.ToString() == "-" || builder.ToString() == "+" || builder.ToString() == "\n")
                                builder.Append(1);
                            if (j + 1 < vephai[i].Length)
                            {

                                if (vephai[i].ElementAt(j + 1) == '2' || vephai[i].ElementAt(j + 1) == '²')
                                {
                                    Int32.TryParse(builder.ToString(), out bientam);
                                    bien2 += bientam;
                                    break;
                                }
                            }

                            Int32.TryParse(builder.ToString(), out bientam);
                            bien1 += bientam;
                            break;

                        }
                        builder.Append(vephai[i].ElementAt(j));
                    }
                    if (dkX == 0)
                    {
                        Int32.TryParse(builder.ToString(), out bientam);
                        bien0 += bientam;
                    }
                    builder.Clear();
                }
                if (bienY != 1)
                {
                    bien0 = bien0 / bienY;
                    bien1 = bien1 / bienY;
                    bien2 = bien2 / bienY;

                }
                //Xuat du lieu test thu
                int sizeOfList = vetrai.Count;
                output += "Ve trai :\r\n";
                for (int i = 0; i < vetrai.Count; i++)
                {
                    output += vetrai[i] + "\r\n";
                }
                output += "Ve phai :\r\n";
                for (int i = 0; i < vephai.Count; i++)
                {
                    output += vephai[i] + "\r\n";
                }
                output += "\r\nBien0: " + bien0 + "\r\nBien1: " + bien1 + "\r\nBien2: " + bien2 + "\r\nBienY: " + bienY;
                //txtResult.Text = output;
                bien_a = bien2;  bien_b = bien1; bien_c = bien0;
                using (Form1 f2 = new Form1())
                {
                    f2.ShowDialog(this);
                }
                this.Close();

            }

        }

        private void btswitch_Click(object sender, EventArgs e)
        {
            //using (Form1 f2 = new Form1())
            //{
            //    f2.ShowDialog(this);
            //}
        }
    }
}
