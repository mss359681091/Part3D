/******************************************************************
** Copyright (c) 2012 -2050 成瑞软件技术部
** 创建人: 李赛赛
** 创建日期:2013-06-01
** 描 述: .ZIP文件解压、压缩类
** 版 本:1.0
**-----------------------------------------------------------------
********************************************************************/
using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Part3D.models
{
    public class UnZipClass
    {

        public string CreateDirectory(string paramPath)
        {
            string strDir = string.Empty;
            string[] arrPath = paramPath.Split(new string[] { "\\" }, StringSplitOptions.None);//拆分文件路径

            string _path = string.Empty;

            int temp = 0;//查找模版文件路径

            for (int i = 0; i < arrPath.Length; i++)
            {
                if (arrPath.Length - 1 == i && arrPath[i].IndexOf(".") > 0)
                {
                    break;
                }

                if (i == 0)
                {
                    _path = arrPath[i] + "\\";
                }
                else if (i == 1)
                {
                    _path = _path + arrPath[i];
                }
                else
                {
                    _path = _path + "\\" + arrPath[i];
                }
                if (i > 0)
                {
                    if (!Directory.Exists(_path))
                    {
                        Directory.CreateDirectory(_path);
                    }
                    if (arrPath[i] == "MasterplateManager")
                    {
                        temp = i;
                    }
                }

            }
            if (arrPath.Length > temp + 1)
            {
                if (arrPath[temp + 1] == "temp")
                {
                    if (arrPath.Length > temp + 2)
                    {
                        strDir = arrPath[temp + 2];
                    }
                    else
                    {
                        strDir = arrPath[temp + 1];
                    }
                }
                else
                {
                    strDir = arrPath[temp + 1];
                }
            }
            return strDir;
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="ZipPath">被解压的文件路径</param>
        /// <param name="Path">解压后文件的路径</param>
        public string UnZip(string ZipPath, string Path)
        {
            string returnValue = string.Empty;
            ZipInputStream s = new ZipInputStream(File.OpenRead(ZipPath));
            ZipEntry theEntry;
            try
            {
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string fileName = System.IO.Path.GetFileName(theEntry.Name);
                    //生成解压目录
                    returnValue = CreateDirectory(Path + theEntry.Name);
                    if (fileName != String.Empty)
                    {
                        //解压文件
                        FileStream streamWriter = File.Create(Path + theEntry.Name);

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                streamWriter.Close();
                                streamWriter.Dispose();
                                break;
                            }
                        }
                        streamWriter.Close();
                        streamWriter.Dispose();
                    }
                }
            }
            catch (Exception)
            {
               
            }
            finally
            {
                s.Close();
                s.Dispose();
            }
            return returnValue;
        }
    }
}
