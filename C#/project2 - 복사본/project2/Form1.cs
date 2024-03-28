using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus.Device;

namespace project2
{
    public partial class Form1 : Form
    {
        TcpClient tc = new TcpClient();
        ModbusIpMaster mim;
        Label[] lb = new Label[6];
        public Form1()
        {
            InitializeComponent();
            //출력할 레블을 배열로 정의 한다
            lb[0] = label2;
            lb[1] = label3;
            lb[2] = label4;
            lb[3] = label5;
            lb[4] = label6;
            lb[5] = label7;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //접속 버튼
            tc.Connect(textBox1.Text, 502);
            mim = ModbusIpMaster.CreateIp(tc);
            
            mim.Transport.ReadTimeout = 100;
            mim.Transport.WriteTimeout = 100;
            mim.Transport.Retries = 0;

            if(tc.Connected)
            {
                MessageBox.Show("연결완료!");
            }
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                mim.WriteSingleCoil(0, false);
                mim.WriteSingleCoil(0, true);
                mim.WriteSingleCoil(0, false);
            }
            catch
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //0.1초 마다 타이머 함수 실행
            try
            {
                //읽기코일의 3번지부터 6개를 읽어 온다
                bool[] data =mim.ReadInputs(3, 6);
                //P03, P04, P05, P06, P07, P08 순으로 배열에 들어 간다

                //설정값
                ushort[] data1 = mim.ReadHoldingRegisters(0, 1);
                ushort[] data2 = mim.ReadInputRegisters(0, 1);

                label10.Text = data1[0].ToString();
                label11.Text = data2[0].ToString();
                //받아온 값 6개에 따라 배경 색을 변경한다
                for (int i = 0; i < 6; i++)
                {
                    if (data[i] == true)
                        lb[i].BackColor = Color.Green;
                    else
                        lb[i].BackColor = Color.Red;
                }
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //PLC의 M00101에 true 신호를 전송하여 
                //PLC의 가상의 레지스터에 값을 보내 준다
                mim.WriteSingleCoil(1, false);
                mim.WriteSingleCoil(1, true);
                mim.WriteSingleCoil(1, false);
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ushort data = ushort.Parse(textBox2.Text);
                mim.WriteSingleRegister(0, data);
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                mim.WriteSingleCoil(2, false);
                mim.WriteSingleCoil(2, true);
                mim.WriteSingleCoil(2, false);
            }
            catch
            {

            }
        }
    }
}
