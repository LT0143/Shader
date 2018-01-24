using UnityEngine;
using System.Collections;
using System.IO;
//using BestHTTP;
using System;

public class UpLoadTexture : MonoBehaviour
{
    public Texture2D UpTexture2D;
    public Texture2D DownTexture2D;
    private const string postUrl = "http://192.168.213.80:8020/uploader/head";
    private const string getUrlHead = "http://192.168.213.80:8020/headPhotos/url?=";
    private ulong UserID = 2;

    // 读取path指定资源，加载成图片 path下
    IEnumerator Openfile(string fileName, string textureName)
    {
        //string path = Application.streamingAssetsPath + "/file/2/";
        //string fullPath = path + textureName;
        //byte[] texture = File.ReadAllBytes(fileName); //读取图片字节流
        //File.WriteAllBytes(fullPath, texture); //保存在该项目目录下，www读取项目目录外的文件报错？？
        WWW wwwTexture = new WWW("file://" + fileName); //读取文件
        yield return wwwTexture;
        if (wwwTexture.error != null)
        {
            Debug.Log(wwwTexture.error);
        }
        else
        {
            UpTexture2D = wwwTexture.texture;
            //GetComponent<MeshRenderer>().sharedMaterial.mainTexture = wwwTexture.texture;
        }
        //yield return new WaitForSeconds(2);
        //Rect rect = new Rect(0, 500, 200, 200);
        StartCoroutine(TransmitPicture());
    }

    IEnumerator TransmitPicture()
    {
        yield return new WaitForEndOfFrame();
       

        //FileStream fs = File.OpenRead(path);
        //int len = (int) fs.Length;
        //Debug.Log("len  "+ len);
        //将texture转成png格式图片
        byte[] pictureBytes = UpTexture2D.EncodeToPNG();// new byte[len]; 
        //Destroy(UpTexture2D);
        //fs.Read(bs, 0, len);
        //生成表单数据 ，然后www类就可以将表单数据post到web服务器上
        WWWForm form = new WWWForm();
        form.AddField("playerID", UserID + ".png");
        form.AddBinaryData("picture", pictureBytes);

        // HTTPRequest request = new HTTPRequest(new Uri("http://192.168.213.80:8020/uploader/head"), HTTPMethods.Post, OnRequest);
        //// HTTPRequest request = new HTTPRequest(new Uri("http://192.168.213.80:8020/0.jpg"), OnRequest);
        // //request.AddField("fileName", "a.png"); 
        // request.AddBinaryData("files", bs);
        // request.Send();

        //添加二进制文件到表单，该函数可上传文件或者图片到web服务器
        //第一个参数key用于得到文件流，第二个参数告诉服务器保存上传的文件要用什么文件名,jpg用/jpg ,png用/png。
        //form.AddBinaryData("fileUpload", bs,"testHead.png", "image/png");
        //发送一个post请求,form是要发送的表单数据
        WWW wwwTexture = new WWW(postUrl, form);

        //WWW wwwTexture = new WWW("http://192.168.213.80:8020/0.jpg");
        yield return wwwTexture;
        if (wwwTexture.error != null)
        {
            Debug.Log(wwwTexture.error);
        }
        else
        {

            DownTexture2D = wwwTexture.texture;
            GetComponent<MeshRenderer>().sharedMaterial.mainTexture = DownTexture2D;
        }
        //此时数据流也开始自动的下载web的服务器的数据，可通过www访问下载到的数据
    }
/*
    private void OnRequestFinished(HTTPRequest originalRequest, HTTPResponse response)
    {
        //if (response.Message == "OK")
            Debug.Log("下载好  " + response.DataAsText);
        if(string.IsNullOrEmpty(response.DataAsText))
            return;
        HTTPRequest request = new HTTPRequest(new Uri(response.DataAsText), OnRequest);
        request.Send();
        
        //Texture2D tex = new Texture2D(100,100);
        //tex.LoadImage(response.Data);
        // DownTexture2D  = tex;
    }

    void OnRequest(HTTPRequest originalRequest, HTTPResponse response)
    {
        DownTexture2D = response.DataAsTexture2D;
        if(DownTexture2D ==null)
            Debug.LogError("DownTexture2D ==null");
        GetComponent<MeshRenderer>().sharedMaterial.mainTexture = DownTexture2D;

    }
    */

    // Use this for initialization
    void Start ()
    {
        string path = Application.streamingAssetsPath + "/file/head.jpg";
        StartCoroutine(Openfile(path, "/head2.jpg"));
    }
}
