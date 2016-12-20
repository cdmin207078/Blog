using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using System.Text;

namespace JIF.Core.Test
{
    [TestClass]
    public class HttpHelper_Test
    {
        [TestMethod]
        public void 上传文件()
        {
            WebRequest req = WebRequest.Create("http://localhost:60002/admin/fileupload");

            // 生成时间戳
            string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = Encoding.ASCII.GetBytes(string.Format("\r\n--{0}--\r\n", strBoundary));

            // 填报文类型
            req.Method = "Post";
            req.Timeout = 1000 * 120;
            req.ContentType = "multipart/form-data; boundary=" + strBoundary;

            // 封装HTTP报文头的流
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(strBoundary);
            sb.Append(Environment.NewLine);
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append("file");
            sb.Append("\"; filename=\"");
            sb.Append("undefined");
            sb.Append("\"");
            sb.Append(Environment.NewLine);
            sb.Append("Content-Type: ");
            sb.Append("multipart/form-data;");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sb.ToString());

            // 计算报文长度
            FileStream fileStream = new FileStream(@"C:\Users\Administrator\Desktop\IMG_0151.JPG", FileMode.Open, FileAccess.Read);
            long length = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
            req.ContentLength = length;
            //fileStream.Close();

            // 将报文头写入流
            Stream requestStream = req.GetRequestStream();
            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

            // 将上传文件内容写入流
            byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                requestStream.Write(buffer, 0, bytesRead);
            }

            // 将报文尾部写入流
            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);

            // 关闭流
            requestStream.Close();
            fileStream.Close();


            // 获得应答报文
            HttpWebResponse httpResponse = (HttpWebResponse)req.GetResponse();
            Stream responseStream = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
            string strResponse = reader.ReadToEnd();
            reader.Close();
            responseStream.Close();



            Console.WriteLine(strResponse);
        }
    }
}
