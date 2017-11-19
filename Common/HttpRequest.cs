using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using HtmlAgilityPack;
using Common.JsonHandle;

namespace Common
{
    class HttpRequest
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="success"></param>
        /// <param name="error"></param>
        public static void login(Dictionary<String, String> cookies,
            String userName,
            String userPwd,
            Action<Dictionary<String, Object>> success,
            Action<String> error)
        {
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(1000);
                BaseHttpRequest baseRequest = new BaseHttpRequest();
                baseRequest.Post("http://www.ji7.com/member/login")
                    .SetHeader("http://www.ji7.com/", "http://www.ji7.com", "application/json, text/javascript, */*; q=0.01", "application/x-www-form-urlencoded; charset=UTF-8")
                    .setCookies(cookies)
                    .Form(new
                    {
                        username = userName,
                        password = userPwd,
                        vcode = ""
                    })
                .OnSuccess(result =>
                {
                    var zipStream = new System.IO.Compression.GZipStream(result, System.IO.Compression.CompressionMode.Decompress);
                    StreamReader streamReader = new StreamReader(zipStream, Encoding.UTF8);
                    string html = streamReader.ReadToEnd();
                    Dictionary<String, Object> dic = JsonToObject.jsonToDictionary(html);
                    success(dic);
                    baseRequest.close();
                })
                .OnFail(errorText =>
                {
                    error(errorText);
                }).Go();
            });
            thread.Start();
        }

        public static void loginchiji(Dictionary<String, String> cookies,
           String userName,
           String userPwd,
           Action<Dictionary<String, Object>> success,
           Action<String> error)
        {
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(1000);
                BaseHttpRequest baseRequest = new BaseHttpRequest();
                baseRequest.Post("http://m.ji7.com/member/login")
                    .SetHeader("http://m.ji7.com/", "http://m.ji7.com", "application/json, text/javascript, */*; q=0.01", "application/x-www-form-urlencoded; charset=UTF-8")
                    .setCookies(cookies)
                    .Form(new
                    {
                        username = userName,
                        password = userPwd,
                        vcode = ""
                    })
                .OnSuccess(result =>
                {
                    var zipStream = new System.IO.Compression.GZipStream(result, System.IO.Compression.CompressionMode.Decompress);
                    StreamReader streamReader = new StreamReader(zipStream, Encoding.UTF8);
                    string html = streamReader.ReadToEnd();
                    Dictionary<String, Object> dic = JsonToObject.jsonToDictionary(html);
                    success(dic);
                    baseRequest.close();
                })
                .OnFail(errorText =>
                {
                    error(errorText);
                }).Go();
            });
            thread.Start();
        }
        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="success"></param>
        /// <param name="error"></param>
        public static void checkIn(Dictionary<String, String> cookies,
            String userid,
            Action<Dictionary<String, Object>> success,
            Action<String> error)
        {
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(1000);

                BaseHttpRequest baseRequest = new BaseHttpRequest();

                baseRequest.Post("http://www.ji7.com/sign/sign_in").
                    SetHeader("http://www.ji7.com/sign",
                    "http://www.ji7.com",
                    "application/json, text/javascript, */*; q=0.01",
                    "application/x-www-form-urlencoded; charset=UTF-8").
                    setCookies(cookies).
                    Form(new
                    {

                        userid = userid
                    }).OnSuccess(result =>
                    {
                        var zipStream = new System.IO.Compression.GZipStream(result, System.IO.Compression.CompressionMode.Decompress);
                        StreamReader streamReader = new StreamReader(zipStream, Encoding.UTF8);
                        string html = streamReader.ReadToEnd();
                        Dictionary<String, Object> dic = JsonToObject.jsonToDictionary(html);
                        success(dic);
                        baseRequest.close();
                    }).OnFail(errorText =>
                    {
                        error(errorText);
                    }).Go();
            });
            thread.Start();
        }

        /// <summary>
        /// 获取用户信息 -- U豆
        /// </summary>
        public static void userInfo(Dictionary<String, String> cookies,
            Action<Dictionary<String, Object>> success,
            Action<String> error)
        {
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(1000);

                BaseHttpRequest baseRequest = new BaseHttpRequest();

                baseRequest.Get("http://www.ji7.com/")
                    .SetHeader("http://www.ji7.com/", null, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8", null)
                    .setCookies(cookies)
                    .OnSuccess(result =>
                    {
                        var zipStream = new System.IO.Compression.GZipStream(result, System.IO.Compression.CompressionMode.Decompress);
                        StreamReader streamReader = new StreamReader(zipStream, Encoding.UTF8);
                        string html = streamReader.ReadToEnd();
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(html);

                        HtmlNode node = doc.DocumentNode.SelectSingleNode("//input[@id='userid']");
                        String userid = node.Attributes[3].Value;
                        HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//i[@class='fl acc_price']");
                        HtmlNode ubiNode = collection[0];
                        HtmlNode udouNode = collection[1];

                        Dictionary<String, Object> dic = new Dictionary<String, Object>();
                        dic.Add("userid", userid);
                        dic.Add("user_udou", udouNode.InnerText);
                        dic.Add("user_ubi", ubiNode.InnerText);
                        dic.Add("msg", "用户数据获取成功!");
                        dic.Add("status", 0);
                        success(dic);
                        baseRequest.close();
                    })
                    .OnFail(errorText =>
                    {
                        error(errorText);
                    }).Go();
            });
            thread.Start();
        }
        /// <summary>
        /// 获取自动竞猜模式
        /// </summary>
        public static void getAutoSetParameter(Dictionary<String, String> cookies,
            Action<List<String>> success,
            Action<String> error)
        {
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(1000);

                BaseHttpRequest baseRequest = new BaseHttpRequest();

                baseRequest.Get("http://www.ji7.com/pc/autoSet")
                    .SetHeader("http://www.ji7.com/pc", null, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8", null)
                    .setCookies(cookies)
                    .OnSuccess(result =>
                    {
                        var zipStream = new System.IO.Compression.GZipStream(result, System.IO.Compression.CompressionMode.Decompress);
                        StreamReader streamReader = new StreamReader(zipStream, Encoding.UTF8);
                        string html = streamReader.ReadToEnd();
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(html);

                        List<String> list = new List<string>();
                        HtmlNode node = doc.DocumentNode.SelectSingleNode("//select[@id='tbID']");
                        foreach (HtmlNode item in node.ChildNodes)
                        {
                            if (item.Name.Contains("option"))
                            {
                                list.Add(item.Attributes[0].Value);
                            }
                        }

                        HtmlNode htmlNode = doc.DocumentNode.SelectSingleNode("//input[@id='startNO']");
                        list.Add(htmlNode.Attributes[4].Value);
                        success(list);
                        baseRequest.close();
                    })
                    .OnFail(errorText =>
                    {
                        error(errorText);
                    }).Go();
            });
            thread.Start();
        }
        /// <summary>
        /// 开启自动竞猜
        /// </summary>
        public static void autoSet(Dictionary<String, String> cookies,
            Dictionary<String, Object> dictionary,
            Action<Dictionary<String, Object>> success,
            Action<String> error)
        {
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(1000);
                BaseHttpRequest baseRequest = new BaseHttpRequest();
                baseRequest.Post("http://www.ji7.com/pc/autoSet")
                    .SetHeader("http://www.ji7.com/pc/autoSet", "http://www.ji7.com", "application/json, text/javascript, */*; q=0.01", "application/x-www-form-urlencoded; charset=UTF-8")
                    .setCookies(cookies)
                    .FormDic(dictionary)
                    .OnSuccess(result =>
                    {
                        var zipStream = new System.IO.Compression.GZipStream(result, System.IO.Compression.CompressionMode.Decompress);
                        StreamReader streamReader = new StreamReader(zipStream, Encoding.UTF8);
                        string html = streamReader.ReadToEnd();
                        Dictionary<String, Object> dic = JsonToObject.jsonToDictionary(html);
                        success(dic);
                        baseRequest.close();
                    })
                    .OnFail(errorText =>
                    {
                        error(errorText);
                    }).Go();
            });
            thread.Start();
        }
        /// <summary>
        /// 获取到开奖信息
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="success"></param>
        /// <param name="error"></param>
        public static void open(Dictionary<String, String> cookies,
            Action<Dictionary<String, Object>> success,
            Action<String> error)
        {
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(1000);

                BaseHttpRequest baseRequest = new BaseHttpRequest();

                baseRequest.Get("http://www.ji7.com/pc/open")
                    .SetHeader("http://www.ji7.com/pc", null, "application/json, text/javascript, */*; q=0.01", null)
                    .setCookies(cookies)
                    .OnSuccess(result =>
                    {
                        var zipStream = new System.IO.Compression.GZipStream(result, System.IO.Compression.CompressionMode.Decompress);
                        StreamReader streamReader = new StreamReader(zipStream, Encoding.UTF8);
                        string html = streamReader.ReadToEnd();
                        Dictionary<String, Object> dic = JsonToObject.jsonToDictionary(html);
                        success(dic);
                        baseRequest.close();
                    })
                    .OnFail(errorText =>
                    {
                        error(errorText);
                    }).Go();
            });
            thread.Start();
        }
        /// <summary>
        /// 投注
        /// </summary>
        public static void bet(Dictionary<String, String> cookies,
            Dictionary<String, object> dictionary,
            String betNO,
            Action<Dictionary<String, Object>> success,
            Action<String> error)
        {
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(1000);
                BaseHttpRequest baseRequest = new BaseHttpRequest();
                String url = String.Format("http://www.ji7.com/pc/insert/{0}", betNO);
                baseRequest.Post(url)
                    .SetHeader(url, "http://www.ji7.com", "application/json, text/javascript, */*; q=0.01", "application/x-www-form-urlencoded; charset=UTF-8")
                    .setCookies(cookies)
                    .FormDic(dictionary)
                    .OnSuccess(result =>
                    {
                        var zipStream = new System.IO.Compression.GZipStream(result, System.IO.Compression.CompressionMode.Decompress);
                        StreamReader streamReader = new StreamReader(zipStream, Encoding.UTF8);
                        string html = streamReader.ReadToEnd();
                        Dictionary<String, Object> dic = JsonToObject.jsonToDictionary(html);
                        success(dic);
                        baseRequest.close();
                    })
                    .OnFail(errorText =>
                    {
                        error(errorText);
                    }).Go();
            });
            thread.Start();
        }
        /// <summary>
        /// 停止自动投注
        /// </summary>
        public static void stopAutoSet(Dictionary<String, String> cookies,
            Action<Dictionary<String, Object>> success,
            Action<String> error)
        {
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(1000);
                BaseHttpRequest baseRequest = new BaseHttpRequest();
                baseRequest.Post("http://www.ji7.com/pc/autoSet")
                    .SetHeader("http://www.ji7.com/pc/autoSet", "http://www.ji7.com", "application/json, text/javascript, */*; q=0.01", "application/x-www-form-urlencoded; charset=UTF-8")
                    .setCookies(cookies)
                    .Form(new
                    {
                        act = "stop"
                    })
                    .OnSuccess(result =>
                    {
                        var zipStream = new System.IO.Compression.GZipStream(result, System.IO.Compression.CompressionMode.Decompress);
                        StreamReader streamReader = new StreamReader(zipStream, Encoding.UTF8);
                        string html = streamReader.ReadToEnd();
                        Dictionary<String, Object> dic = JsonToObject.jsonToDictionary(html);
                        success(dic);
                        baseRequest.close();
                    })
                    .OnFail(errorText =>
                    {
                        error(errorText);
                    }).Go();
            });
            thread.Start();
        }
        /// <summary>
        /// 领取VIP工资
        /// </summary>
        public static void getRewardSalary(Dictionary<String, String> cookies,
            Action<Dictionary<String, Object>> success,
            Action<String> error)
        {
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(1000);
                BaseHttpRequest baseRequest = new BaseHttpRequest();
                baseRequest.Post("http://www.ji7.com/vip/get_reward_salary")
                    .SetHeader("http://www.ji7.com/vip", "http://www.ji7.com", "application/json, text/javascript, */*; q=0.01", "application/x-www-form-urlencoded; charset=UTF-8")
                    .setCookies(cookies)
                    .Form(new
                    {
                        action = "getgetgetgetgetgetgetgetgetget"
                    })
                    .OnSuccess(result =>
                    {
                        var zipStream = new System.IO.Compression.GZipStream(result, System.IO.Compression.CompressionMode.Decompress);
                        StreamReader streamReader = new StreamReader(zipStream, Encoding.UTF8);
                        string html = streamReader.ReadToEnd();
                        Dictionary<String, Object> dic = JsonToObject.jsonToDictionary(html);
                        success(dic);
                        baseRequest.close();
                    })
                    .OnFail(errorText =>
                    {
                        error(errorText);
                    }).Go();
            });
            thread.Start();
        }
        /// <summary>
        /// 获取昨日盈亏
        /// </summary>
        public static void getYesterdayProfitAndLoss(Dictionary<String, String> cookies,
            Action<List<String>> success,
            Action<String> error)
        {
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(1000);
                BaseHttpRequest baseRequest = new BaseHttpRequest();
                baseRequest.Get("http://www.ji7.com/pc/myList")
                    .SetHeader("http://www.ji7.com/pc", null, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8", null)
                    .setCookies(cookies)
                    .OnSuccess(result =>
                    {
                        var zipStream = new System.IO.Compression.GZipStream(result, System.IO.Compression.CompressionMode.Decompress);
                        StreamReader streamReader = new StreamReader(zipStream, Encoding.UTF8);
                        string html = streamReader.ReadToEnd();
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(html);
                        HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@class='fr chart_all']");
                        String toDay = node.ChildNodes[1].ChildNodes[1].ChildNodes[1].InnerText;
                        String money = node.ChildNodes[1].ChildNodes[3].ChildNodes[1].InnerText;

                        HtmlNode htmlNode = doc.DocumentNode.SelectSingleNode("//table[@class='compe_table1']");
                        HtmlNodeCollection nodeCollection = htmlNode.ChildNodes;
                        String msg = "";
                        foreach (HtmlNode n in nodeCollection)
                        {
                            if (n.InnerText.Contains("已开奖"))
                            {
                                String no = n.ChildNodes[1].InnerText;
                                String proportion = n.ChildNodes[13].InnerText;

                                msg = String.Format("第{0}期的盈亏比例为:{1}", no, proportion);
                                break;
                            }
                        }
                        success(new List<string>() { toDay, money, msg });
                        baseRequest.close();
                    })
                    .OnFail(errorText =>
                    {
                        error(errorText);
                    }).Go();
            });
            thread.Start();
        }
        /// <summary>
        /// 领取竞猜工资
        /// </summary>
        public static void get_jingcai_gongzi(Dictionary<String, String> cookies,
            Action<Dictionary<String, Object>> success,
            Action<String> error)
        {
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(1000);
                BaseHttpRequest baseRequest = new BaseHttpRequest();
                baseRequest.Post("http://www.ji7.com/vip/get_jingcai_gongzi")
                    .SetHeader("http://www.ji7.com/pc", "http://www.ji7.com", "application/json, text/javascript, */*; q=0.01", "application/x-www-form-urlencoded; charset=UTF-8")
                    .setCookies(cookies)
                    .Form(new
                    {
                        action = "getgetgetgetgetgetgetgetgetget"
                    })
                    .OnSuccess(result =>
                    {
                        var zipStream = new System.IO.Compression.GZipStream(result, System.IO.Compression.CompressionMode.Decompress);
                        StreamReader streamReader = new StreamReader(zipStream, Encoding.UTF8);
                        string html = streamReader.ReadToEnd();
                        Dictionary<String, Object> dic = JsonToObject.jsonToDictionary(html);
                        success(dic);
                        baseRequest.close();
                    })
                    .OnFail(errorText =>
                    {
                        error(errorText);
                    }).Go();
            });
            thread.Start();
        }
        /// <summary>
        /// 获取竞猜工资
        /// </summary>
        public static void getJingcaiMoney(Dictionary<String, String> cookies,
            Action<String> success,
            Action<String> error)
        {
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(1000);
                BaseHttpRequest baseRequest = new BaseHttpRequest();
                baseRequest.Get("http://www.ji7.com/vip/get_jingcai_gongzi")
                    .SetHeader("http://www.ji7.com/pc", null, "text/html, */*; q=0.01", null)
                    .setCookies(cookies)
                    .OnSuccess(result =>
                    {
                        var zipStream = new System.IO.Compression.GZipStream(result, System.IO.Compression.CompressionMode.Decompress);
                        StreamReader streamReader = new StreamReader(zipStream, Encoding.UTF8);
                        string html = streamReader.ReadToEnd();
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(html);
                        HtmlNode node = doc.DocumentNode.SelectSingleNode("//span[@class='red1']");
                        String money = node.InnerText;
                        success(money);
                        baseRequest.close();
                    })
                    .OnFail(errorText =>
                    {
                        error(errorText);
                    }).Go();
            });
            thread.Start();
        }

        /// <summary>
        /// 提卡
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="success"></param>
        /// <param name="error"></param>
        public static void tika(Dictionary<String, String> cookies,
            String goods_id,
            string cat_id,
             Action<Dictionary<String, Object>> success,
             Action<String> error)
        {
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(1000);
                BaseHttpRequest baseRequest = new BaseHttpRequest();
                baseRequest.Post("http://www.ji7.com/dshop/virfuli_exchange")
                    .SetHeader("http://www.ji7.com/pc", "http://www.ji7.com", "application/json, text/javascript, */*; q=0.01", "application/x-www-form-urlencoded; charset=UTF-8")
                    .setCookies(cookies)
                    .Form(new
                    {
                        //            goods_id:goods_id,
                        //cat_id:cat_id,
                        //re_num:re_num,
                        //pay_passwd:pay_passwd
                        // action = "getgetgetgetgetgetgetgetgetget"
                    })
                    .OnSuccess(result =>
                    {
                        var zipStream = new System.IO.Compression.GZipStream(result, System.IO.Compression.CompressionMode.Decompress);
                        StreamReader streamReader = new StreamReader(zipStream, Encoding.UTF8);
                        string html = streamReader.ReadToEnd();
                        Dictionary<String, Object> dic = JsonToObject.jsonToDictionary(html);
                        success(dic);
                        baseRequest.close();
                    })
                    .OnFail(errorText =>
                    {
                        error(errorText);
                    }).Go();
            });
            thread.Start();
        }

        public static void cjdr(Dictionary<String, String> cookies,
           string cjid,
           string joinid,
           string selectNum,
           string currentNum,
            Action<Dictionary<String, Object>> success,
            Action<String> error)
        {
            string url = "http://m.ji7.com/cltx/guess?id=" + cjid;
            Thread thread = new Thread(delegate ()
            {
                Thread.Sleep(2000);
                BaseHttpRequest baseRequest = new BaseHttpRequest();
                baseRequest.Post(url)
                    .SetHeader(
                    url,
                    "http://m.ji7.com",
                    "application/json, text/javascript, */*; q=0.01",
                    "application/x-www-form-urlencoded; charset=UTF-8")
                    .setCookies(cookies)
                    .Form(new
                    {
                        joinid = joinid,
                        pknum = selectNum,
                        round = currentNum
                        //            goods_id:goods_id,
                        //cat_id:cat_id,
                        //re_num:re_num,
                        //pay_passwd:pay_passwd
                        // action = "post"
                    })
                    .OnSuccess(result =>
                    {
                        var zipStream = new System.IO.Compression.GZipStream(result, System.IO.Compression.CompressionMode.Decompress);
                        StreamReader streamReader = new StreamReader(zipStream, Encoding.UTF8);
                        string html = streamReader.ReadToEnd();
                        Dictionary<String, Object> dic = JsonToObject.jsonToDictionary(html);
                        success(dic);
                        baseRequest.close();
                    })
                    .OnFail(errorText =>
                    {
                        error(errorText);
                    }).Go();
            });
            thread.Start();
        }
    }
}
