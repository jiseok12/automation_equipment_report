using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Modbus.Device; //라이브러리
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace projectgogogo
{
    public partial class Form1 : Form
    {
        //C# 전역변수
        TcpClient tc = new TcpClient();
        ModbusIpMaster min;
        int check_auto;

        //label클래스를 저장할 배열 16개를 생성한다
        Label[] mylabel = new Label[16];

        public Form1()
        {
            InitializeComponent();
            mylabel[0] = label1;
            mylabel[1] = label4;
            mylabel[2] = label5;
            mylabel[3] = label6;
            mylabel[4] = label7;
            mylabel[5] = label8;
            mylabel[6] = label11;
            mylabel[7] = label10;
            mylabel[8] = label9;
            mylabel[9] = label12;
            mylabel[10] = label13;
            mylabel[11] = label14;
            mylabel[12] = label15;
            mylabel[13] = label16;
            mylabel[14] = label17;
            mylabel[15] = label18;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //접속 버튼
            tc.Connect(textBox1.Text, 502);
            //ip 조소의 502번 포트에 접속한다
            min = ModbusIpMaster.CreateIp(tc);

            min.Transport.WriteTimeout = 100;
            min.Transport.ReadTimeout = 100;
            min.Transport.Retries = 0;

            if (tc.Connected)
            {
                MessageBox.Show("접속완료");
            }
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //전진버튼클릭
            try
            {
                min.WriteSingleCoil(0, true);
                // (코일번호, on/off)

            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //후진 버튼클릭
            try
            {
                min.WriteSingleCoil(0, false);
                // (코일번호, on/off)

            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //상태 확인 버튼
            try
            {
                bool[] data = min.ReadInputs(0, 3);
                //0번지 부터 3개의 배열을 읽는다
                //bool[] data = min.ReadInputs(0, 1); 
                // 0번지에서 값을 1개 읽어야 한다
                //전진
                if (data[0])
                {
                    label1.BackColor = Color.Red;
                    //label1.Text = "M1후진감지=감지";
                }
                else
                {
                    label1.BackColor = Color.Green;
                    //label1.Text = "M1후진감지=미감지";
                }
                if (data[1])
                {
                    label4.BackColor = Color.Red;
                   // label4.Text = "M1전진감지=감지";
                }
                else
                {
                    label4.BackColor = Color.Green;
                    //label4.Text = "M1전진감지=미감지";
                }
                if (data[2])
                {
                    label5.BackColor = Color.Red;
                    //label5.Text = "M1자제감지=감지";
                }
                else
                {
                    label5.BackColor = Color.Green;
                    //label5.Text = "M1자제감지=미감지";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("상태 확인 버튼 오류");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //m2벨트 작동
            try
            {
                min.WriteSingleCoil(1, true);
                // (코일번호, on/off)
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //m2벨트 정지
            try
            {
                min.WriteSingleCoil(1, false);
                // (코일번호, on/off)

            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //m1_s1 상태확인
            try { 
                bool[] data = min.ReadInputs(2, 1);

                if (data[0])
                {
                    label3.Text = "자재있음";
                }
                else
                {
                    label3.Text = "자재없음";
                }
            }
            catch
            {
                MessageBox.Show("실패");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //노란색 on
            try
            {
                min.WriteSingleCoil(9, true);
                // (코일번호, on/off)

            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //노란색 off
            try
            {
                min.WriteSingleCoil(9, false);
                // (코일번호, on/off)
                //min.WriteMultipleCoils
                //시작 코일 번호를 입력하면 배열 만큼 코일을 수만큼 제어가능

                //min.ReadInputRegisters
                //plc 값을 변수를 읽어 올수 있다
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //빨강색 on
            try
            {
                min.WriteSingleCoil(10, true);
                // (코일번호, on/off)
                //min.WriteMultipleCoils
                //시작 코일 번호를 입력하면 배열 만큼 코일을 수만큼 제어가능

                //min.ReadInputRegisters
                //plc 값을 변수를 읽어 올수 있다
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //빨강색 off
            try
            {
                min.WriteSingleCoil(10, false);
                // (코일번호, on/off)
                //min.WriteMultipleCoils
                //시작 코일 번호를 입력하면 배열 만큼 코일을 수만큼 제어가능

                //min.ReadInputRegisters
                //plc 값을 변수를 읽어 올수 있다
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //타이머가 작동 중일 경우 0.1초 간격으로 실행 한다
            //상태 확인

            try
            {
                bool[] data = min.ReadInputs(0, 16);
                //0번지 부터 3개의 배열을 읽는다
                //bool[] data = min.ReadInputs(0, 1); 
                // 0번지에서 값을 1개 읽어야 한다

                //읽기 레지스터
                ushort[] data2 = min.ReadInputRegisters(0, 1);
                label19.Text = data2[0].ToString();
                //레지스터 값을 읽어온다

                //쓰기레지스터(제한값)
                ushort[] data3 = min.ReadHoldingRegisters(0, 1);
                label20.Text = data3[0].ToString();

                for (int i = 0; i<16; i++)
                {
                    if (data[i])
                        mylabel[i].BackColor = Color.Green;
                    else
                        mylabel[i].BackColor = Color.Red;
                }


                //전진
                /*
                if (data[0])
                {
                    label1.BackColor = Color.Green;
                    //label1.Text = "M1후진감지=감지";
                }
                else
                {
                    label1.BackColor = Color.Red;
                    //label1.Text = "M1후진감지=미감지";
                }

                if (data[1])
                {
                    label4.BackColor = Color.Green;
                    // label4.Text = "M1전진감지=감지";
                }
                else
                {
                    label4.BackColor = Color.Red;
                    //label4.Text = "M1전진감지=미감지";
                }

                if (data[2])
                {
                    label5.BackColor = Color.Green;
                    //label5.Text = "M1자제감지=감지";
                }
                else
                {
                    label5.BackColor = Color.Red;
                    //label5.Text = "M1자제감지=미감지";
                }

                label6.Text = data[3].ToString();

                if (data[3])
                {
                    label6.BackColor = Color.Red;
                    //label5.Text = "M1자제감지=감지";
                }
                else
                {
                    label6.BackColor = Color.Green;
                    //label5.Text = "M1자제감지=미감지";
                }

                if (data[4])
                {
                    label7.BackColor = Color.Red;
                    //label5.Text = "M1자제감지=감지";
                }
                else
                {
                    label7.BackColor = Color.Green;
                    //label5.Text = "M1자제감지=미감지";
                }
                if (data[5])
                {
                    label8.BackColor = Color.Red;
                    //label5.Text = "M1자제감지=감지";
                }
                else
                {
                    label8.BackColor = Color.Green;
                    //label5.Text = "M1자제감지=미감지";
                }
                if (data[6])
                {
                    label11.BackColor = Color.Red;
                    //label5.Text = "M1자제감지=감지";
                }
                else
                {
                    label11.BackColor = Color.Green;
                    //label5.Text = "M1자제감지=미감지";
                }
                if (data[7])
                {
                    label10.BackColor = Color.Red;
                    //label5.Text = "M1자제감지=감지";
                }
                else
                {
                    label10.BackColor = Color.Green;
                    //label5.Text = "M1자제감지=미감지";
                }
                if (data[8])
                {
                    label9.BackColor = Color.Red;
                    //label5.Text = "M1자제감지=감지";
                }
                else
                {
                    label9.BackColor = Color.Green;
                    //label5.Text = "M1자제감지=미감지";
                }
                */
            }
            catch (Exception ex)
            {
                //MessageBox.Show("상태 확인 버튼 오류");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                min.WriteSingleCoil(2, true);
                // (코일번호, on/off)
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                min.WriteSingleCoil(2, false);
                // (코일번호, on/off)
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                min.WriteSingleCoil(3, true);
                // (코일번호, on/off)
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                min.WriteSingleCoil(3, false);
                // (코일번호, on/off)
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                min.WriteSingleCoil(4, true);
                // (코일번호, on/off)
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                min.WriteSingleCoil(5,true);
                // (코일번호, on/off)
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                min.WriteSingleCoil(6, true);
                // (코일번호, on/off)
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                min.WriteSingleCoil(4, false);
                // (코일번호, on/off)
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                min.WriteSingleCoil(5, false);
                // (코일번호, on/off)
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                min.WriteSingleCoil(6, false);
                // (코일번호, on/off)
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                min.WriteSingleCoil(7, true);
                // (코일번호, on/off)
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            try
            {
                min.WriteSingleCoil(7, false);
                // (코일번호, on/off)
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            try
            {
                min.WriteSingleCoil(8, true);
                // (코일번호, on/off)
                //min.WriteMultipleCoils
                //시작 코일 번호를 입력하면 배열 만큼 코일을 수만큼 제어가능

                //min.ReadInputRegisters
                //plc 값을 변수를 읽어 올수 있다
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            try
            {
                min.WriteSingleCoil(8, false);
                // (코일번호, on/off)
                //min.WriteMultipleCoils
                //시작 코일 번호를 입력하면 배열 만큼 코일을 수만큼 제어가능

                //min.ReadInputRegisters
                //plc 값을 변수를 읽어 올수 있다
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            try
            {
                //min.WriteSingleCoil(8, true);
                bool[] data = { true, true, true };
                min.WriteMultipleCoils(8, data);
                // (코일번호, on/off)
                //min.WriteMultipleCoils
                //시작 코일 번호를 입력하면 배열 만큼 코일을 수만큼 제어가능

                //min.ReadInputRegisters
                //plc 값을 변수를 읽어 올수 있다
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            try
            {
                //min.WriteSingleCoil(8, true);
                bool[] data = { false, false, false};
                min.WriteMultipleCoils(8, data);
                // (코일번호, on/off)
                //min.WriteMultipleCoils
                //시작 코일 번호를 입력하면 배열 만큼 코일을 수만큼 제어가능

                //min.ReadInputRegisters
                //plc 값을 변수를 읽어 올수 있다
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패");
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            try
            {
                ushort num = ushort.Parse(textBox2.Text);
                //num값을 PLC의 holding register의 작업을 한다
                //쓰기 레지스터 0번지에 16비트 정수를 사용한다
                min.WriteSingleRegister(0, num);
            }
            catch
            {

            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            try
            {
                ushort[] data = min.ReadHoldingRegisters(0, 1);
                label20.Text = data[0].ToString();
            }
            catch
            {

            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            try
            {
                ushort[] data = min.ReadInputRegisters(0, 1);
                label19.Text = data[0].ToString();
            }
            catch
            {

            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            try
            {
                //M00101에 쓰기작업을 시작한다
                min.WriteSingleCoil(0, false);
                min.WriteSingleCoil(0, true);
                min.WriteSingleCoil(0, false);
            }
            catch
            {

            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            try
            {
                //M00101에 쓰기작업을 시작한다
                min.WriteSingleCoil(1, false);
                min.WriteSingleCoil(1, true);
                min.WriteSingleCoil(1, false);
            }
            catch
            {

            }
        }
    }
}
