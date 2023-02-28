using IMAOCMS.Desktop.Models;
using System;
using System.IO.Ports;
namespace IMAOCMS.Desktop
{
    public partial class Form1 : Form
    {
        public bool isLoad = false;
        public int devAddr = 1;
        public bool isRecordLog = true;
        public int outCount;
        public int logCount;
        public double laohuaintval = 1.0;
        public double xunhuaintval = 1.0;
        public double readinterval = 1.0;
        private Thread thread;
        SerialPort port = new SerialPort();
        public Form1()
        {
            InitializeComponent();


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var serialPort in SerialPort.GetPortNames())
            {
                cb_com.Items.Add(serialPort);
            }
            //try
            //{
            //    if (this.isLoad)
            //        return;
            //    string[] portNames = SerialPort.GetPortNames();
            //    if (portNames != null && portNames.Length != 0)
            //    {
            //        foreach (string str in portNames)
            //        {
            //            //ComboBox newItem = new ComboBox();
            //            //newItem.Content = (object)str;
            //            cb_com.Items.Add(port);
            //        }
            //    }
            //    this.isLoad = true;
            //}
            //catch (Exception ex)
            //{
            //    int num = (int)MessageBox.Show("Bir hata oluştu\r\n：" + ex.Message + " on TestDevice.Com48Control.Com48Control_Loaded");
            //}

        }
        public enum Parity
        {
            //
            // Summary:
            //     No parity check occurs.
            None,
            //
            // Summary:
            //     Sets the parity bit so that the count of bits set is an odd number.
            Odd,
            //
            // Summary:
            //     Sets the parity bit so that the count of bits set is an even number.
            Even,
            //
            // Summary:
            //     Leaves the parity bit set to 1.
            Mark,
            //
            // Summary:
            //     Leaves the parity bit set to 0.
            Space
        }

        private void btn_conn_Click(object sender, EventArgs e)
        {

            //port.PortName = cb_com.Text;
            //port.BaudRate = 9600;
            //if (this.port != null)
            //    this.port.Close();
            //this.port = new SerialPort();
            //this.port.PortName = (this.cb_com.SelectedItem as ComboBox).ToString();
            //this.port.BaudRate = int.Parse((this.cb_bau.SelectedItem as ComboBox).ToString());
            //this.port.DataBits = int.Parse((this.cb_data.SelectedItem as ComboBox).ToString());
            //double num = (double)int.Parse((this.cb_stop.SelectedItem as ComboBox).ToString());
            try
            {

                port.PortName = cb_com.Text;
                port.BaudRate = 9600;

                //double num = (double)int.Parse((this.cb_stop.SelectedItem as ComboBox).ToString());
                //if (num <= 1.0)
                //{
                //    if (num != 0.0)
                //    {
                //        if (num == 1.0)
                //            this.port.StopBits = StopBits.One;
                //    }
                //    else
                //        this.port.StopBits = StopBits.None;
                //}
                //else if (num != 1.5)
                //{
                //    if (num == 2.0)
                //        this.port.StopBits = StopBits.Two;
                //}
                //else
                //    this.port.StopBits = StopBits.OnePointFive;
                switch ((this.cb_checked as ComboBox).ToString())
                {
                    case "None":
                        this.port.Parity = (System.IO.Ports.Parity)Parity.None;
                        break;
                    case "Odd":
                        this.port.Parity = (System.IO.Ports.Parity)Parity.Odd;
                        break;
                    case "Even":
                        this.port.Parity = (System.IO.Ports.Parity)Parity.Even;
                        break;
                    case "Mark":
                        this.port.Parity = (System.IO.Ports.Parity)Parity.Mark;
                        break;
                    case "Space":
                        this.port.Parity = (System.IO.Ports.Parity)Parity.Space;
                        break;
                }
                this.port.Open();
                if (!this.port.IsOpen)
                    return;

                else
                {
                    if (this.port != null)
                        this.port.Close();
                    if (this.thread != null)
                    {
                        try
                        {
                            this.thread.Abort();
                        }
                        catch
                        {
                        }
                        this.thread = (Thread)null;
                    }
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("发生错误：" + ex.Message + " on TestDevice.Com48Control.btn_conn_Click");
            }

        }
        private void AnalyzeOutData(Dictionary<int, int> dic)
        {
            try
            {
                this.pictureBox1.Invoke((Action)(() =>
                {
                    if (dic.ContainsKey(1))
                        //this.SetImage(dic[1], false);
                        return;
                }));
            }
            catch (Exception ex)
            {
            }
        }
        private void SetImage(Image img, int status, bool isIn)
        {
        }
        private void GetDeviceAddr()
        {
            try
            {
                string sources = this.devAddr.ToString("X2") + "03" + 50.ToString("X4") + "0001";
                string hexString = sources + GlobalClass.CRC16(sources);
                byte[] toHexByte = GlobalClass.strToToHexByte(hexString);
                this.port.Write(toHexByte, 0, toHexByte.Length);
                this.RecordLog("send：" + hexString + "    time：" + DateTime.Now.ToString("HH:mm:ss"));
                Thread.Sleep(150);
                string str = "";
                if (this.port.BytesToRead > 0)
                {
                    byte[] numArray = new byte[this.port.BytesToRead];
                    this.port.Read(numArray, 0, numArray.Length);
                    str = GlobalClass.byteToHexStr(numArray);
                    if ((int)toHexByte[0] == (int)numArray[0] && (int)toHexByte[1] == (int)numArray[1])
                    {
                        int num = (int)numArray[2];
                        this.txt_addr.Text = (((int)numArray[3] << 8) + (int)numArray[4]).ToString();
                    }
                }
                this.RecordLog("receive：" + str + "    time：" + DateTime.Now.ToString("HH:mm:ss"));
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("发生错误：" + ex.Message + " on TestDevice.Com48Control.GetDeviceAddr");
            }
        }
        public void RecordLog(string log)
        {
            if (!this.isRecordLog)
                return;
            //this.Dispatcher.Invoke((Action)(() =>
            //{
            //    try
            //    {
            //        if (this.logCount >= 500)
            //        {
            //            this.txt_log.Text = "";
            //            this.logCount = 0;
            //        }
            //        this.txt_log.Text += (log += "\r\n");
            //        txt_log.ScrollBars = ScrollBars.Both;
            //        ++this.logCount;
            //    }
            //    catch (Exception ex)
            //    {
            //        int num = (int)MessageBox.Show("发生错误：" + ex.Message + " on TestDevice.Com48Control.RecordLog");
            //    }
            //}));
        }
        private void TestLaoHua()
        {
            try
            {
                string str1 = this.devAddr.ToString("X2") + "0F0000" + this.outCount.ToString("X4");
                int num = this.outCount / 8;
                if (this.outCount % 8 > 0)
                    ++num;
                string sources1 = str1 + num.ToString("X2");
                string sources2 = sources1;
                for (int index = 0; index < num; ++index)
                {
                    sources1 += "00";
                    sources2 += "FF";
                }
                string str2 = sources1 + GlobalClass.CRC16(sources1);
                string str3 = sources2 + GlobalClass.CRC16(sources2);
                Queue<string> stringQueue = new Queue<string>();
                stringQueue.Enqueue(str3);
                stringQueue.Enqueue(str2);
                while (true)
                {
                    try
                    {
                        if (this.port.IsOpen)
                        {
                            string hexString = stringQueue.Dequeue();
                            stringQueue.Enqueue(hexString);
                            byte[] toHexByte = GlobalClass.strToToHexByte(hexString);
                            this.port.Write(toHexByte, 0, toHexByte.Length);
                            this.RecordLog("send：" + hexString + "    time：" + DateTime.Now.ToString("HH:mm:ss"));
                            Thread.Sleep(150);
                            string str4 = "";
                            if (this.port.BytesToRead > 0)
                            {
                                byte[] numArray = new byte[this.port.BytesToRead];
                                this.port.Read(numArray, 0, numArray.Length);
                                str4 = GlobalClass.byteToHexStr(numArray);
                                this.AnalyzeLaohua(toHexByte, numArray);
                            }
                            this.RecordLog("receive：" + str4 + "    time：" + DateTime.Now.ToString("HH:mm:ss"));
                        }
                    }
                    catch
                    {
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(this.laohuaintval));
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void AnalyzeLaohua(byte[] sendBuff, byte[] recBuff)
        {
            try
            {
                Dictionary<int, int> dic = new Dictionary<int, int>();
                if ((int)sendBuff[0] == (int)recBuff[0] && (int)sendBuff[1] == (int)recBuff[1] && (int)sendBuff[2] == (int)recBuff[2] && (int)sendBuff[3] == (int)recBuff[3] && (int)sendBuff[4] == (int)recBuff[4] && (int)sendBuff[5] == (int)recBuff[5])
                {
                    int num = (int)sendBuff[8];
                    for (int key = 1; key <= this.outCount; ++key)
                        dic.Add(key, num);
                }
                if (dic.Count <= 0)
                    return;
                //this.AnalyzeOutData(dic);
            }
            catch (Exception ex)
            {
            }
        }

        private void btn_laohua_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.btn_laohua.ToString() == "Aging test")
                {
                    if (this.port == null)
                    {
                        int num1 = (int)MessageBox.Show("请先连接设备");
                    }
                    else
                    {
                        try
                        {
                            try
                            {
                                this.outCount = int.Parse(this.txt_outcount.Text.Trim());
                            }
                            catch
                            {
                                int num2 = (int)MessageBox.Show("Please enter the number of relay outputs");
                                return;
                            }
                            this.laohuaintval = double.Parse(this.txt_laohuaterval.Text.Trim());
                            this.devAddr = int.Parse(this.txt_addr.Text.Trim());
                            this.thread = new Thread(new ThreadStart(this.TestLaoHua));
                            this.thread.Start();
                            //this.thread.IsBackground = true;
                            //this.btn_laohua.Content = (object)"Stop aging test";
                            //this.btn_xunhuan.IsEnabled = false;
                            //this.btn_read.IsEnabled = false;
                            //this.btn_outclose.IsEnabled = false;
                            //this.btn_outopen.IsEnabled = false;
                            //this.btn_outset.IsEnabled = false;
                            //this.btn_setpara.IsEnabled = false;
                        }
                        catch
                        {
                            int num3 = (int)MessageBox.Show("Please enter aging interval");
                        }
                    }
                }
                else
                {
                    if (this.thread != null)
                    {
                        try
                        {
                            this.thread.Abort();
                        }
                        catch
                        {
                        }
                        this.thread = (Thread)null;
                    }
                    //btn_laohua.Content = (object)"Aging test";
                    //btn_xunhuan.IsEnabled = true;
                    //btn_read.IsEnabled = true;
                    //btn_outclose.IsEnabled = true;
                    //btn_outopen.IsEnabled = true;
                    //btn_outset.IsEnabled = true;
                    //btn_setpara.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("发生错误：" + ex.Message + " on TestDevice.Com48Control.cb_laohua_Click");
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txt_xunhuanterval_TextChanged(object sender, EventArgs e)
        {

        }
    }
}