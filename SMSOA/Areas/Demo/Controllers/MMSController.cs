﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace SMSOA.Areas.Demo.Controllers
{
    public class MMSController : Controller
    {
        //全局变量：唯一识别码
        
        // GET: Demo/MMS
        public ActionResult Index()
        {
            return View();
        }

        public ContentResult FileUp(/*HttpContext context*/)
        {

            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            string info;
            //判断是否存在文件
            if (files.Count > 0)
            {
                //1 获取文件集合中的第一个文件(每次只上传一个文件)
                HttpPostedFile file = files[0];
                //定义文件存放的目标路径
                string targetDir = System.Web.HttpContext.Current.Server.MapPath("~/FileUpLoad/Product");
                //创建目标路径
                Directory.CreateDirectory(targetDir);
                //Files.CreateDirectory(targetDir);
                //组合成文件的完整路径
                string path = System.IO.Path.Combine(targetDir, System.IO.Path.GetFileName(file.FileName));
                //保存上传的文件到指定路径中
                //2 将上传的文件读取为二进制数组
                //2.1将上传的图片转成二进制图片
                var file_stream = file.InputStream;
                BinaryReader binary_reader = new BinaryReader(file_stream);
                //2.2读取为二进制数组 此处需要使用异步读取吗？
               var content= binary_reader.ReadBytes((int)file_stream.Length);
                #region 给飞飞写的demo
                //给飞飞写的图片保存demo      
                //FileStream imagefile_stream = new FileStream("路径", FileMode.Create);
                //imagefile_stream.Write(你的二进制数组, 0, 长度);
                //FileInfo file_new = new FileInfo();
                
                #endregion

                //将二进制写入Redis中
                //file.SaveAs(path);
                //3 二进制写入至缓存中（写入hashtable中） 
                Common.Redis.HashRedisHelper hash_redis = new Common.Redis.HashRedisHelper();
                //3.1 判断缓存中是否已经存在指定hashId-key的值（不用判断了，新创建的fileName—唯一的—guid方式）
                Guid newfileName = new Guid();
                //hashId可以通过读取配置文件的方式读取
                hash_redis.Set<byte[]>("fastdfsfile", newfileName.ToString(), content);
                ViewBag.guid = newfileName.ToString();
                info = "上传成功";
            }
            else
            {
                info = "上传失败";
            }
            // string fjssmk = context.Request["fjssmk"];
            ////string userid = Utility.GetCurrentUser().zybh + "";
            //HttpFileCollection httpFileCollection = context.Request.Files;
            //HttpPostedFile file = null;
            // if (httpFileCollection.Count > 0)
            //      file = httpFileCollection[0];
            // FileInfo fileex = new FileInfo(file.FileName);
            return Content(info);
        }
        public ContentResult SendMMS()
        {
            string info;
            //1. 判断唯一识别码是否被创建(是否传入Redis)
            if (ViewBag.guid == null) { return Content(info = "请重新上传图片"); }
            //2. 从Redis中获取图片数据
            Common.Redis.HashRedisHelper hash_redis = new Common.Redis.HashRedisHelper();
            byte[] imgData = hash_redis.Get<byte[]>("fastdfsfile", ViewBag.guid);

            //3.封装进发送model-----------Demo中暂时不做，信息直接封装进XML，正式时再做

            #region 飞飞写的压缩程序，对图片进行压缩

            #endregion

            //4.发送
            #region 飞飞写的打zip包程序，组成Zip包并返回btye[]

            byte[] zipData = new byte[1];
            #endregion
            //4.1 将发送需要的content内容(zip包)转为字符串
            string content = Encoding.Default.GetString(zipData);
            #region 暂时不使用从本地读取文件的方式
            //string targetDir = System.Web.HttpContext.Current.Server.MapPath("~/FileUpLoad/Product");
            //暂时发送指定的zip文件            
            //String fileName = "u=605198921,4238220254&fm=21&gp=0.zip";

            //string path = System.IO.Path.Combine(targetDir, System.IO.Path.GetFileName(fileName));
            #endregion
            try
            {
                //4.2 发送彩信
                var res = SMSOA.Areas.Demo.SendMMS.MMSSend.test(content);

                //5.存入fastDFS,获取FileName
                string fileName = Common.FastDFS.fastDFSTestClient.test(zipData);
                info = "发送成功且成功存入fastDFS,fileName为" + fileName;
            }
            catch
            {
                info = "发送失败或存入FastDFS失败";
            }


            return Content(info);
        }

    }
}