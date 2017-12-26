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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
            //load Maple
            MapleEngine.MapleCallbacks cb;
            byte[] err = new byte[2048];
            IntPtr kv;
            String[] argv = new String[2];
            argv[0] = "maple";
            argv[1] = "-A2";
            cb.textCallBack = cbText;
          
            cb.errorCallBack = cbError;
            cb.statusCallBack = cbStatus;
            cb.readlineCallBack = null;
            cb.redirectCallBack = null;
            cb.streamCallBack = null;
            cb.queryInterrupt = null;
            cb.callbackCallBack = null;

            try
            {
                kv = MapleEngine.StartMaple(2, argv, ref cb, IntPtr.Zero, IntPtr.Zero, err);
            }
            catch (DllNotFoundException)
            {
                return;
            }
            catch (EntryPointNotFoundException)
            {
                return;
            }

            if (kv.ToInt64() == 0)
            {
                MessageBox.Show("Fatal Error, could not start Maple: "
                        + System.Text.Encoding.ASCII.GetString(err, 0, Array.IndexOf(err, (byte)0)), "Lỗi", MessageBoxButtons.OK);
                return;
            }
            //Load package mình vừa viết, tên là TEST
            try
            {
                MapleEngine.EvalMapleStatement(kv, Encoding.ASCII.GetBytes("read(\"D:/TAM/TEST2.mpl\");with(TEST);"));
                // MapleEngine.EvalMapleStatement(kv, Encoding.ASCII.GetBytes("with(TEST):"));
                Rich.Text = "";
            }
            catch (Exception)
            {
            }
        }
        string str;
        public void cbText(IntPtr data, int tag, IntPtr output)
        {
            //Xuất kết quả ra ngoài
            str = str + Marshal.PtrToStringAnsi(output) + "\\\\";
            Rich.Text = Rich.Text + "\n" + Marshal.PtrToStringAnsi(output);
            
            
        }

        public static void cbError(IntPtr data, IntPtr offset, IntPtr msg)
        {
            string s = Marshal.PtrToStringAnsi(msg);
        }

        public static void cbStatus(IntPtr data, IntPtr used, IntPtr alloc, double time)
        {

        }
        int bien_a = 0, bien_b = 0, bien_c = 0;
        
        private void Tinh_Click_1(object sender, System.EventArgs e)
        {

            MapleEngine.MapleCallbacks cb;
            byte[] err = new byte[2048];
            IntPtr kv;

            String[] argv = new String[2];
            argv[0] = "maple";
            argv[1] = "-A2";

            cb.textCallBack = cbText;
            cb.errorCallBack = cbError;
            cb.statusCallBack = cbStatus;
            cb.readlineCallBack = null;
            cb.redirectCallBack = null;
            cb.streamCallBack = null;
            cb.queryInterrupt = null;
            cb.callbackCallBack = null;
            Rich.Text = "";
            str = "";


            float a = Form2.bien_a, b =Form2.bien_b, c = Form2.bien_c;

            try
            {

                kv = MapleEngine.StartMaple(2, argv, ref cb, IntPtr.Zero, IntPtr.Zero, err);

                try
                {
                    //sử dụng hàm Tong trong package TEST
                    String expr = "Tong(";

                    //expr += txtInput.Text;
                    expr += a + "," + b + "," + c;
                    expr += ");";

                    IntPtr val = MapleEngine.EvalMapleStatement(kv, Encoding.ASCII.GetBytes(expr));
                }
                catch (Exception)
                {
                    MessageBox.Show("Không thể load Maple", "Lỗi", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {
            }
            string tmp = "D:\\TAM\\";
            // txtOutput.Text = Path.Combine(tmp, "Eq2ImgWinForms.gif");
            txtOutput.Text = str;
            NativeMethods.CreateGifFromEq(str, Path.Combine(tmp, "Eq2ImgWinForms.gif"));
            pictureBox1.Image = Image.FromFile(Path.Combine(tmp, "Eq2ImgWinForms.gif"));
            pictureBox2.Image = Image.FromFile(Path.Combine(tmp, "plot.jpg"));
            
        }

        private void txtOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
        //private static MapleEngine.TextCallBack callbackDelegate;
        private void btOCR_Click(object sender, EventArgs e)
        {
             if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                var img = new Bitmap(openFileDialog.FileName);
                var ocr = new TesseractEngine("./tessdata", "vie", EngineMode.Default);
                var page = ocr.Process(img);
                string equation = page.GetText();
                var builder = new StringBuilder();
                List<string> vetrai = new List<string>() ;
                List<string> vephai = new List<string>();
                int flag = 0, flagvetrai = 1;
                string output = string.Empty;
                output += equation + "\r\n";
                if (!(equation.Contains("khảo sát") || equation.Contains("Khảo sát") || equation.Contains("khao sat") || equation.Contains("khảosát") || equation.Contains("khaosat") || equation.Contains("Khaosat") || equation.Contains("Khao sat")))
                {
                    output += "Bài toán khảo sát hàm số: ";
                    txtResult.Text = output.ToString();
                   // return;
                }
                    
                equation = equation.Substring(equation.IndexOf(":")+1);
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
                    if(equation[i]=='+'|| equation[i] == '-' || equation[i] == '—' || equation[i] == '=' || i+1 == equation.Length)
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
                int dkX = 0 ,bien3 = 0;
                int bientam = 0;
                //Ve trai get bien
                for (int i = 0; i < vetrai.Count; i++)
                {
                    bientam = 0;
                    dkX = 0;
                    for (int j = 0; j < vetrai[i].Length; j++)
                    {
                        out2 += vetrai[i].ElementAt(j).ToString();
                        if (vetrai[i].ElementAt(j)=='X' || vetrai[i].ElementAt(j) == 'x' )
                        {
                            dkX = 1;
                            if (builder.ToString() == ""|| builder.ToString() == "-" || builder.ToString() == "\n")
                                builder.Append(1);
                            if(j+1< vetrai[i].Length)
                            {
                                
                                if (vetrai[i].ElementAt(j + 1) == '2' || vetrai[i].ElementAt(j + 1) =='²')
                                {
                                    Int32.TryParse(builder.ToString(), out bientam);
                                    bien_a += bientam;
                                    break;
                                }
                                if (vetrai[i].ElementAt(j + 1) == '3')
                                {
                                    Int32.TryParse(builder.ToString(), out bientam);
                                    bien3 += bientam;
                                    break;
                                }
                            }
                            
                            Int32.TryParse(builder.ToString(), out bientam);
                            bien_b += bientam;
                            break;

                        }
                        builder.Append(vetrai[i].ElementAt(j));
                    }
                    if (dkX == 0)
                    {
                        Int32.TryParse(builder.ToString(), out bientam);
                        bien_c += bientam;
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
                        out2 += vephai[i].ElementAt(j).ToString();
                        if (vephai[i].ElementAt(j) == 'X' || vephai[i].ElementAt(j) == 'x')
                        {
                            dkX = 1;
                            if (builder.ToString() == "" || builder.ToString() == "-" || builder.ToString() == "\n")
                                builder.Append(1);
                            if (j + 1 < vephai[i].Length)
                            {

                                if (vephai[i].ElementAt(j + 1) == '2'|| vephai[i].ElementAt(j + 1) == '²')
                                {
                                    Int32.TryParse(builder.ToString(), out bientam);
                                    bien_a -= bientam;
                                    break;
                                }
                                if (vephai[i].ElementAt(j + 1) == '3')
                                {
                                    Int32.TryParse(builder.ToString(), out bientam);
                                    bien3 -= bientam;
                                    break;
                                }
                            }

                            Int32.TryParse(builder.ToString(), out bientam);
                            bien_b -= bientam;
                            break;

                        }
                        builder.Append(vephai[i].ElementAt(j));
                    }
                    if (dkX == 0)
                    {
                        Int32.TryParse(builder.ToString(), out bientam);
                        bien_c -= bientam;
                    }
                    builder.Clear();
                }

                //Xuat du lieu test thu
                int sizeOfList = vetrai.Count;
                output += "Ve trai :\r\n";
                for (int i = 0; i < vetrai.Count; i++)
                {
                    output += vetrai[i]+ "\r\n";
                }
                output += "Ve phai :\r\n";
                for (int i = 0; i < vephai.Count; i++)
                {
                    output += vephai[i] + "\r\n";
                }
                output += "\r\nbien_c: " +bien_c+ "\r\nbien_b: " + bien_b+ "\r\nbien_a: " + bien_a+ "\r\nBien3: " + bien3;
                txtResult.Text = output;
                txtInput.Text = bien_a + "," + bien_b + "," + bien_c;


                //MapleEngine.MapleCallbacks cb;
                //byte[] err = new byte[2048];
                //IntPtr kv;

                //String[] argv = new String[2];
                //argv[0] = "maple";
                //argv[1] = "-A2";

                //cb.textCallBack = cbText;
                //cb.errorCallBack = cbError;
                //cb.statusCallBack = cbStatus;
                //cb.readlineCallBack = null;
                //cb.redirectCallBack = null;
                //cb.streamCallBack = null;
                //cb.queryInterrupt = null;
                //cb.callbackCallBack = null;
                //Rich.Text = "";
                //str = "";


                //int a = 2, b = 4, c = 6;

                //try
                //{

                //    kv = MapleEngine.StartMaple(2, argv, ref cb, IntPtr.Zero, IntPtr.Zero, err);

                //    try
                //    {
                //        //sử dụng hàm Tong trong package TEST
                //        String expr = "Tong(";

                //        expr += txtInput.Text;
                //        //expr += a + "," + b + "," + c;
                //        expr += ");";

                       

                //       // if (callbackDelegate != null) throw new InvalidOperationException("Can't hook more than once");
                //        //IntPtr hInstance = LoadLibrary("User32"); its: kv
                //        //callbackDelegate = new MapleEngine.TextCallBack(cbText);
                //        //hhook = SetWindowsHookEx(WH_KEYBOARD_LL, callbackDelegate, hInstance, 0);
                //        IntPtr val = MapleEngine.EvalMapleStatement(kv, Encoding.ASCII.GetBytes(expr));
                //        //if (val == IntPtr.Zero) throw new Win32Exception();
                //    }
                //    catch (Exception)
                //    {
                //        MessageBox.Show("Không thể load Maple", "Lỗi", MessageBoxButtons.OK);
                //    }
                //}
                //catch (Exception)
                //{
                //}
                //string tmp = "D:\\TAM\\";
                //// txtOutput.Text = Path.Combine(tmp, "Eq2ImgWinForms.gif");
                //txtOutput.Text = str;
                //NativeMethods.CreateGifFromEq(str, Path.Combine(tmp, "Eq2ImgWinForms.gif"));
                //pictureBox1.Image = Image.FromFile(Path.Combine(tmp, "Eq2ImgWinForms.gif"));
                //pictureBox2.Image = Image.FromFile(Path.Combine(tmp, "plot.jpg"));


            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    
    }

}

       
       



  
