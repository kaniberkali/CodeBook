using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeBook
{
    public partial class Form1 : Form
    {
        public void Console(string text)
        {
            ListConsole.Items.Insert(0,DateTime.Now + " "+ text);
        }
        public Form1(string file)
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            string[] Directories = file.Split('\\');
            string path = "";
                for (int i=0;i<Directories.Length-1;i++)
            {
                path += Directories[i] + "\\";
            }
            string lang = File.ReadAllText(path + "Language.CodeBook");
            if (lang.Contains("Custom") == true)
                comboBox1.SelectedIndex = 0;
            if (lang.Contains("CSharp") == true)
                comboBox1.SelectedIndex = 1;
            if (lang.Contains("VB") == true)
                comboBox1.SelectedIndex = 2;
            if (lang.Contains("HTML") == true)
                comboBox1.SelectedIndex = 3;
            if (lang.Contains("XML") == true)
                comboBox1.SelectedIndex = 4;
            if (lang.Contains("SQL") == true)
                comboBox1.SelectedIndex = 5;
            if (lang.Contains("PHP") == true)
                comboBox1.SelectedIndex = 6;
            if (lang.Contains("JS") == true)
                comboBox1.SelectedIndex = 7;
            if (lang.Contains("Lua") == true)
                comboBox1.SelectedIndex = 8;
            textBox1.Text = File.ReadAllText(path+ "Title.CodeBook");
            richTextBox6.Text = File.ReadAllText(path + "Detail.CodeBook");
            fastColoredTextBox6.Text = File.ReadAllText(path + "Code.CodeBook");
            selecteddate = File.ReadAllText(path + "Date.CodeBook");
            Console("Path :"+ path);
        }
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        ListView datas = new ListView();

        private void cleardatas()
        {
            groupBox1.Text = "Title";
            groupBox2.Text = "Title";
            groupBox3.Text = "Title";
            groupBox4.Text = "Title";
            groupBox5.Text = "Title";

            label2.Text = "Date : 00.00.0000 00:00";
            label3.Text = "Date : 00.00.0000 00:00";
            label4.Text = "Date : 00.00.0000 00:00"; 
            label5.Text = "Date : 00.00.0000 00:00";
            label6.Text = "Date : 00.00.0000 00:00";

            label11.Text = "Null";
            label12.Text = "Null";
            label13.Text = "Null";
            label14.Text = "Null";
            label15.Text = "Null";

            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            richTextBox4.Text = "";
            richTextBox5.Text = "";
        }
        private void updatedatas()
        {
            cleardatas();
            datas.Clear();
            foreach (var d in Directory.GetDirectories(Application.StartupPath + "\\" + "Datas"))
            {
                var dirName = new DirectoryInfo(d).Name;
                try
                {
                    ListViewItem item = new ListViewItem();
                    string Title = File.ReadAllText(Application.StartupPath + "\\" + "Datas\\" + dirName + "\\Title.CodeBook");
                    string Language = File.ReadAllText(Application.StartupPath + "\\" + "Datas\\" + dirName + "\\Language.CodeBook");
                    string Date = File.ReadAllText(Application.StartupPath + "\\" + "Datas\\" + dirName + "\\Date.CodeBook");
                    string Detail = File.ReadAllText(Application.StartupPath + "\\" + "Datas\\" + dirName + "\\Detail.CodeBook");
                    string Code = File.ReadAllText(Application.StartupPath + "\\" + "Datas\\" + dirName + "\\Code.CodeBook");
                    item.Text = Title;
                    item.SubItems.Add(Date);
                    item.SubItems.Add(Language);
                    item.SubItems.Add(Detail);
                    item.SubItems.Add(Code);
                    datas.Items.Add(item);
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show(ex.ToString(), "@kodzamani.tk");
                }
            }
            vScrollBar1.Value = 0;
            vScrollBar1.Maximum = datas.Items.Count*10;
            setdatas();
            Console("Update Datas");
        }
        string selecteddate = "";
        private void setdatas()
        {
            try
            {
                if (datas.Items.Count > 0)
                {
                    string lang = datas.Items[vScrollBar1.Value / 10].SubItems[2].Text;
                    if (lang.Contains("Custom")==true)
                    comboBox1.SelectedIndex = 0;
                    if (lang.Contains("CSharp") == true)
                        comboBox1.SelectedIndex = 1;
                    if (lang.Contains("VB") == true)
                        comboBox1.SelectedIndex = 2;
                    if (lang.Contains("HTML") == true)
                        comboBox1.SelectedIndex = 3;
                    if (lang.Contains("XML") == true)
                        comboBox1.SelectedIndex = 4;
                    if (lang.Contains("SQL") == true)
                        comboBox1.SelectedIndex = 5;
                    if (lang.Contains("PHP") == true)
                        comboBox1.SelectedIndex = 6;
                    if (lang.Contains("JS") == true)
                        comboBox1.SelectedIndex = 7;
                    if (lang.Contains("Lua") == true)
                        comboBox1.SelectedIndex = 8;
                    textBox1.Text = datas.Items[vScrollBar1.Value / 10].SubItems[0].Text;
                    richTextBox6.Text = datas.Items[vScrollBar1.Value / 10].SubItems[3].Text;
                    fastColoredTextBox6.Text = datas.Items[vScrollBar1.Value / 10].SubItems[4].Text;
                    selecteddate = datas.Items[vScrollBar1.Value / 10].SubItems[1].Text;
                }
                    if (vScrollBar1.Value < vScrollBar1.Maximum)
                {
                    groupBox1.Text = datas.Items[vScrollBar1.Value/10+1].SubItems[0].Text;
                    label2.Text = "Date : " + datas.Items[vScrollBar1.Value/10+1].SubItems[1].Text;
                    label11.Text = datas.Items[vScrollBar1.Value/10+1].SubItems[2].Text;
                    richTextBox1.Text = datas.Items[vScrollBar1.Value/10+1].SubItems[3].Text;
                }
                if (vScrollBar1.Value < vScrollBar1.Maximum-1)
                {
                    groupBox2.Text = datas.Items[vScrollBar1.Value/10 + 2].SubItems[0].Text;
                    label3.Text = "Date : "+ datas.Items[vScrollBar1.Value/10 + 2].SubItems[1].Text;
                    label12.Text = datas.Items[vScrollBar1.Value/10 + 2].SubItems[2].Text;
                    richTextBox2.Text = datas.Items[vScrollBar1.Value/10 + 2].SubItems[3].Text;
                }
                if (vScrollBar1.Value < vScrollBar1.Maximum - 2)
                {
                    groupBox3.Text = datas.Items[vScrollBar1.Value/10 + 3].SubItems[0].Text;
                    label4.Text = "Date : " + datas.Items[vScrollBar1.Value/10 + 3].SubItems[1].Text;
                    label13.Text = datas.Items[vScrollBar1.Value/10 + 3].SubItems[2].Text;
                    richTextBox3.Text = datas.Items[vScrollBar1.Value/10 + 3].SubItems[3].Text;
                }
                if (vScrollBar1.Value < vScrollBar1.Maximum - 3)
                {
                    groupBox4.Text = datas.Items[vScrollBar1.Value/10 + 4].SubItems[0].Text;
                    label5.Text = "Date : " + datas.Items[vScrollBar1.Value/10 + 4].SubItems[1].Text;
                    label14.Text = datas.Items[vScrollBar1.Value/10 + 4].SubItems[2].Text;
                    richTextBox4.Text = datas.Items[vScrollBar1.Value/10 + 4].SubItems[3].Text;
                }
                if (vScrollBar1.Value < vScrollBar1.Maximum - 4)
                {
                    groupBox5.Text = datas.Items[vScrollBar1.Value/10 + 5].SubItems[0].Text;
                    label6.Text = "Date : " + datas.Items[vScrollBar1.Value/10 + 5].SubItems[1].Text;
                    label15.Text = datas.Items[vScrollBar1.Value/10 + 5].SubItems[2].Text;
                    richTextBox5.Text = datas.Items[vScrollBar1.Value/10 + 5].SubItems[3].Text;
                }
            }
            catch { }
            if (newtext != comboBox1.Text+textBox1.Text+richTextBox6.Text+ fastColoredTextBox6.Text)
            {
                Console("Set Datas");
                newtext = comboBox1.Text + textBox1.Text + richTextBox6.Text + fastColoredTextBox6.Text;
            }
        }
        string newtext = "";
        
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fastColoredTextBox6.Language = (FastColoredTextBoxNS.Language)comboBox1.SelectedIndex;
            Console("Selected Language : "+comboBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (!(new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator))
                    label7.Visible = true;
                RegistryKey uygulama = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                uygulama.SetValue("CodeBook", "\"" + Application.ExecutablePath + "\"");
            }
            catch { }
            if (Directory.Exists(Application.StartupPath + "\\" + "Logs") == false)
                Directory.CreateDirectory(Application.StartupPath + "\\" + "Logs");
            if (Directory.Exists(Application.StartupPath + "\\"+"Datas") == false)
                Directory.CreateDirectory(Application.StartupPath + "\\" + "Datas");
            updatedatas();
            Thread th = new Thread(visibleState); th.Start();
            comboBox1.SelectedIndex = 0;
            Console("Form is loaded");
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                Console("this Normaled");
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                Console("this Maximized");
            }
        }
        [DllImport("User32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
        private void visibleState()
        {
            for (; ; )
            {
                if (this.Visible == false)
                {
                    Thread.Sleep(100);
                    int ctrl = GetAsyncKeyState(16);
                    int shift = GetAsyncKeyState(17);
                    int c = GetAsyncKeyState(67);
                    if (ctrl != 0 && shift != 0 && c != 0)
                    {
                        this.Show();
                        Console("this Showed");
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Console("this Hided");
        }

        private string time()
        {
            return DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year + " " + DateTime.Now.Hour + "." + DateTime.Now.Minute+"."+DateTime.Now.Second;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            cleardatas();
            setdatas();
        }
        private void writetext(string title, string text)
        {
            FileStream fs = new FileStream(title + ".CodeBook", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(text);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (richTextBox6.Text != "" && textBox1.Text != "" && fastColoredTextBox6.Text != "")
                {
                    if (Directory.Exists(Application.StartupPath + "\\" + "Datas") == false)
                        Directory.CreateDirectory(Application.StartupPath + "\\" + "Datas");
                    if (listBox1.Text == "Save")
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\" + "Datas\\" + textBox1.Text + " " + time());
                        writetext(Application.StartupPath + "\\" + "Datas\\" + textBox1.Text + " " + time()+ "\\Title", textBox1.Text);
                        writetext(Application.StartupPath + "\\" + "Datas\\" + textBox1.Text + " " + time() + "\\Language", comboBox1.Text);
                        writetext(Application.StartupPath + "\\" + "Datas\\" + textBox1.Text + " " + time() + "\\Date", time());
                        writetext(Application.StartupPath + "\\" + "Datas\\" + textBox1.Text + " " + time() + "\\Detail", richTextBox6.Text);
                        writetext(Application.StartupPath + "\\" + "Datas\\" + textBox1.Text + " " + time() + "\\Code", fastColoredTextBox6.Text);
                        Console("Saved");
                    }
                    if (listBox1.Text == "Update")
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\" + "Datas\\" + textBox1.Text + " " + selecteddate);
                        writetext(Application.StartupPath + "\\" + "Datas\\" + textBox1.Text + " " + selecteddate + "\\Title", textBox1.Text);
                        writetext(Application.StartupPath + "\\" + "Datas\\" + textBox1.Text + " " + selecteddate + "\\Language", comboBox1.Text);
                        writetext(Application.StartupPath + "\\" + "Datas\\" + textBox1.Text + " " + selecteddate + "\\Date", time());
                        writetext(Application.StartupPath + "\\" + "Datas\\" + textBox1.Text + " " + selecteddate + "\\Detail", richTextBox6.Text);
                        writetext(Application.StartupPath + "\\" + "Datas\\" + textBox1.Text + " " + selecteddate + "\\Code", fastColoredTextBox6.Text);
                        Console("Updated");
                    }
                    if (listBox1.Text == "Delete")
                    {
                        try
                        {
                            foreach (var d in Directory.GetFiles(Application.StartupPath + "\\" + "Datas\\" + textBox1.Text + " " + selecteddate))
                            {
                                var dirName = new DirectoryInfo(d).Name;
                                try
                                {
                                    File.Delete(Application.StartupPath + "\\" + "Datas\\" + textBox1.Text + " " + selecteddate + "\\" + dirName);
                                    Console(dirName + " Deleted");
                                }
                                catch (FileNotFoundException ex)
                                {
                                    MessageBox.Show(ex.ToString(), "@kodzamani.tk");
                                }
                            }
                            Directory.Delete(Application.StartupPath + "\\" + "Datas\\" + textBox1.Text + " " + selecteddate);
                            Console(textBox1.Text + " " + selecteddate + " Deleted");
                        }
                        catch { }
                    }
                        updatedatas();
                }
                else
                {
                    MessageBox.Show("No field can be left blank.", "@kodzamani.tk");
                }
            }
            listBox1.SelectedIndex = -1;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Console("this Closed");
            Application.Exit();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + "\\" + "Logs") == false)
                Directory.CreateDirectory(Application.StartupPath + "\\" + "Logs");

            string dosya_yolu = Application.StartupPath + "\\" + "Logs\\"+time()+".log";
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < ListConsole.Items.Count; i++)
                sw.WriteLine(ListConsole.Items[i]);
            sw.Flush();
            sw.Close();
            fs.Close();
            Process[] runingProcess = Process.GetProcesses();
            for (int i = 0; i < runingProcess.Length; i++)
            {
                if (runingProcess[i].ProcessName == "CodeBook")
                    runingProcess[i].Kill();
            }
        }
    }
}
