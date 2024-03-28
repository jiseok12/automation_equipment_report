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
//네임 스페이스

namespace project4
{
    public partial class Form1 : Form
    {
        TcpClient tc = new TcpClient();
        ModbusIpMaster mim;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //접속 버튼 함수
            tc.Connect(textBox1.Text, 502);
            mim = ModbusIpMaster.CreateIp(tc);

            //modbus설정
            mim.Transport.WriteTimeout = 100; //0.5초
            mim.Transport.ReadTimeout = 100;
            mim.Transport.Retries = 0;

            if(tc.Connected)
            {
                timer1.Enabled = true;
                MessageBox.Show("연결이되었습니다");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //공정시작버튼 함수
            try
            {
                //M00100 사각 펄스 전송
                //M00100 쓰기코일 0번지로 지정되어있다
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
            //0.1초 타이머 함수
            try
            {
                //읽기코일에 0번지부터 4기 읽어온다
                bool[] data = mim.ReadInputs(0, 4);
                if (data[0])
                {
                    //M2_S1감지
                    label1.BackColor = Color.Green;
                }
                else
                {
                    label1.BackColor = Color.Red;
                }
                if (data[1])
                {
                    //M2_S1감지
                    label2.BackColor = Color.Green;
                }
                else
                {
                    label2.BackColor = Color.Red;
                }
                if (data[2])
                {
                    //M2_S1감지
                    label3.BackColor = Color.Green;
                }
                else
                {
                    label3.BackColor = Color.Red;
                }
                if (data[3])
                {
                    //M2_S1감지
                    label4.BackColor = Color.Green;
                }
                else
                {
                    label4.BackColor = Color.Red;
                }

                //input register에서 0번지와 1번지값은 읽는다
                //0번지부터 2개의 데이터를 가져온다
                ushort[] data2 = mim.ReadInputRegisters(0, 2);
                //data2[0] : 0000
                textBox4.Text = data2[0].ToString();
                textBox5.Text = data2[1].ToString();
                //holding register에서 02nsj

                ushort[] data3 = mim.ReadHoldingRegisters(0, 2);
                //data2[0] : 0000
                textBox8.Text = data3[0].ToString();
                textBox9.Text = data3[1].ToString();
            }
            catch
            {

            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //D0000설정하기 버튼
            //쓰기레지스터 0번에 textbox값을 숫자로 바꿔서 입력
            try
            {
                ushort num = ushort.Parse(textBox6.Text);
                mim.WriteSingleRegister(0, num);
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ushort num = ushort.Parse(textBox7.Text);
                mim.WriteSingleRegister(1, num);
            }
            catch
            {

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //M00100 사각 펄스 전송
                //M00100 쓰기코일 0번지로 지정되어있다
                mim.WriteSingleCoil(1, false);
                mim.WriteSingleCoil(1, true);
                mim.WriteSingleCoil(1, false);
            }
            catch
            {

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                //M00100 사각 펄스 전송
                //M00100 쓰기코일 0번지로 지정되어있다
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
