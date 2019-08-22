using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComUtil {
    public partial class util : Form {
        public util() {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// 时间转时间戳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e) {
            textBox1.Text = DateTimeToStamp(dateTimePicker1.Value);
        }

        // 时间转时间戳
        public string DateTimeToStamp(DateTime now) {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(now - startTime).TotalMilliseconds; // 相差毫秒数
            return timeStamp.ToString();
        }

        private void button2_Click(object sender, EventArgs e) {
            dateTimePicker1.Value = StampToDateTime(textBox1.Text);
        }

        public DateTime StampToDateTime(string timeStamp) {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long mTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(mTime);
            return startTime.Add(toNow);
        }

        /// <summary>
        /// 打开图片文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e) {
            OpenFileDialog opndlg = new OpenFileDialog();
            opndlg.Filter = "所有图像文件|*.bmp;*.pcx;*.png;*.jpg;*.gif";
            opndlg.Title = "打开图像文件";
            if (opndlg.ShowDialog() == DialogResult.OK) {
                pictureBox1.Load(opndlg.FileName);
            }
        }

        /// <summary>
        /// 图片转换base64
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e) {
            if (pictureBox1.Image != null) {
                
                    using (MemoryStream ms1 = new MemoryStream()) {
                    try {
                        pictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] buffer = ms1.GetBuffer();
                        richTextBox1.Text = Convert.ToBase64String(buffer, 0, (int)ms1.Length);
                        ms1.Close();

                    } catch (System.Runtime.InteropServices.ExternalException err) {
                    MessageBox.Show("转换失败");
                    } finally {
                        ms1.Close();
                    }
            }
                
                
            }
            
        }

        /// <summary>
        /// base64转换图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e) {
            if (richTextBox1.Text != null && !richTextBox1.Text.Equals("")) {
                
                Image image = null;
                using(MemoryStream stream = new MemoryStream()) {
                    try {
                        byte[] imageData = Convert.FromBase64String(richTextBox1.Text);
                        stream.Write(imageData, 0, imageData.Length);
                        image = Image.FromStream(stream);
                        stream.Close();
                    } catch (System.FormatException err) {
                        MessageBox.Show("base64格式错误");
                    } catch (System.ArgumentException err2) {
                        MessageBox.Show("base64格式错误");
                    } finally {
                        stream.Close();
                    }
                    

                }

                if (image != null) {
                    pictureBox1.Image = image;
                    pictureBox1.Refresh();
                }

            }
        }
    }
}
