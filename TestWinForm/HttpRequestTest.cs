using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWinForm
{
    public partial class HttpRequestTest : Form
    {
        public HttpRequestTest()
        {
            InitializeComponent();
        }

        private void HttpRequestTest_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        ///// <summary>
        ///// 获取IP
        ///// </summary>
        //private void button10_Click(object sender, EventArgs e)
        //{
        //    Thread thread = new Thread(delegate ()
        //    {
        //        try
        //        {
        //            String url = "http://2017.ip138.com/ic.asp";
        //            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
        //            request.Method = "GET";
        //            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
        //            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        //            request.ContentType = "text/html; charset=gb2312";
        //            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
        //            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
        //            HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
        //            Stream responseStream = response.GetResponseStream();
        //            StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("gb2312"));
        //            string html = streamReader.ReadToEnd();
        //            int index = html.IndexOf("您的IP是：[") + 7;
        //            int end = html.IndexOf("</center></body></html>");
        //            String ip = html.Substring(index, end - index);
        //            ip = ip.Replace("]", " ");
        //            CallBackHandle d = () =>
        //            {
        //                this.label5.Text = ip;
        //            };
        //            Invoke(d, new object[] { });
        //        }
        //        catch
        //        {
        //            CallBackHandle d = () =>
        //            {
        //                this.label5.Text = "IP地址获取错误";
        //            };
        //            Invoke(d, new object[] { });
        //        }
        //    });
        //    thread.Start();
        //}
        ///// <summary>
        ///// 登录
        ///// </summary>
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    UserModel user = this.getUser(true);
        //    if (user == null) return;
        //    this.writeMessage("账号登录...");
        //    HttpRequest.loginchiji(user.Cookies, user.userName, user.userPwd, json =>
        //    {
        //        CallBackHandle c = () =>
        //        {
        //            if (Convert.ToInt32(json["status"]) == 0)
        //            {
        //                this.writeMessage("账号" + user.userName + "登录成功");
        //                //this.getUdou(user);
        //                RefreshState = 0;
        //            }
        //            else
        //            {
        //                this.writeMessage(json["msg"].ToString());
        //                this.writeMessage("");
        //            }
        //        };
        //        Invoke(c, new object[] { });
        //    }, error =>
        //    {
        //        CallBackHandle c = () =>
        //        {
        //            this.writeMessage(error);
        //        };
        //        Invoke(c, new object[] { });
        //    });
        //}
        ///// <summary>
        ///// 获取U豆
        ///// </summary>
        //private void getUdou(UserModel user)
        //{
        //    HttpRequest.userInfo(user.Cookies, json =>
        //    {
        //        CallBackHandle c = () =>
        //        {
        //            this.writeMessage(json["msg"].ToString());
        //            if (Convert.ToInt32(json["status"]) == 0)
        //            {

        //                user.userid = json["userid"].ToString();
        //                user.udou = json["user_udou"].ToString();
        //                if (RefreshState == 0)
        //                    _bll.UpdateUserIdAndUdou(user);
        //                else if (RefreshState == 999)
        //                    _bll.UpdateUdouAfter(user);
        //                else
        //                    _bll.UpdateUdouBefore(user);
        //                this.dataGridView1.Rows[user.index - 1].Cells[4].Value = user.udou;
        //                this.getYesterdayProfitAndLoss(user);
        //            }
        //        };
        //        Invoke(c, new object[] { });
        //    }, error =>
        //    {
        //        CallBackHandle c = () =>
        //        {
        //            this.writeMessage(error);
        //        };
        //        Invoke(c, new object[] { });
        //    });
        //}
    }
}
