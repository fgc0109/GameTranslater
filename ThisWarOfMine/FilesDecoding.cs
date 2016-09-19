using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace ThisWarOfMine
{
    public class FilesDecoding
    {
        private static MemoryStream mStreamIDX = null;
        private static MemoryStream mStreamDAT = null;
        private static MemoryStream[] mStreamZip = null;
        private static MemoryStream[] mStreamUZip = null;

        private static DeflateStream[] mStreamDeflate = null;
        private static GZipStream[] mStreamGZip = null;

        private static byte[] mIdxHeader = new byte[3];
        private static byte[] mIdxCounts = new byte[4];
        private static byte[] mIdxUnknow = new byte[4];

        private static int mFileCount = 0;
        private static int[] mLengthHash = null;
        private static int[] mLengthBefore = null;
        private static int[] mLengthAfters = null;
        private static int[] mLengthDeviat = null;

        private static List<byte[]> mIdxHash = new List<byte[]>();
        private static List<byte[]> mIdxBefore = new List<byte[]>();
        private static List<byte[]> mIdxAfters = new List<byte[]>();
        private static List<byte[]> mIdxDeviat = new List<byte[]>();
        private static List<byte[]> mIdxEnding = new List<byte[]>();

        /// <summary>
        /// 读取打包文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="error">错误信息</param>
        /// <returns>文件读取状态</returns>
        public static bool FileLoad(string path, out string error)
        {
            error = String.Empty;
            try
            {
                byte[] idxData = File.ReadAllBytes(path + ".idx");
                mStreamIDX = new MemoryStream(idxData);

                byte[] datData = File.ReadAllBytes(path + ".dat");
                mStreamDAT = new MemoryStream(datData);
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 文件解包
        /// </summary>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public static bool DataUnpacking(out string error)
        {
            error = String.Empty;
            try
            {
                BinaryReader idxReader = new BinaryReader(mStreamIDX);

                mIdxHeader = idxReader.ReadBytes(3);
                mIdxCounts = idxReader.ReadBytes(4);
                mIdxUnknow = idxReader.ReadBytes(4);

                mFileCount = (mIdxCounts[0]) + (mIdxCounts[1] << 8) + (mIdxCounts[2] << 16) + (mIdxCounts[3] << 24);

                mLengthHash = new int[mFileCount];
                mLengthBefore = new int[mFileCount];
                mLengthAfters = new int[mFileCount];
                mLengthDeviat = new int[mFileCount];

                for (int i = 0; i < mFileCount; i++)
                {
                    mIdxHash.Add(idxReader.ReadBytes(4));
                    mIdxBefore.Add(idxReader.ReadBytes(4));
                    mIdxAfters.Add(idxReader.ReadBytes(4));
                    mIdxDeviat.Add(idxReader.ReadBytes(4));
                    mIdxEnding.Add(idxReader.ReadBytes(1));
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }

            try
            {
                BinaryReader datReader = new BinaryReader(mStreamDAT);
                mStreamZip = new MemoryStream[mFileCount];

                for (int i = 0; i < mFileCount; i++)
                {
                    byte[] datBefore = mIdxBefore[i];
                    mLengthBefore[i] = (datBefore[0]) + (datBefore[1] << 8) + (datBefore[2] << 16) + (datBefore[3] << 24);

                    byte[] datDeviat = mIdxDeviat[i];
                    mLengthDeviat[i] = (datDeviat[0]) + (datDeviat[1] << 8) + (datDeviat[2] << 16) + (datDeviat[3] << 24);

                    mStreamDAT.Seek(0, SeekOrigin.Begin);
                    mStreamDAT.Position = mLengthDeviat[i];

                    mStreamZip[i] = new MemoryStream(datReader.ReadBytes(mLengthBefore[i]));
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 文件解压缩并读取至相应的内存流
        /// </summary>
        /// <param name="mainForm"></param>
        public static void DataUncompress()
        {
            mStreamDeflate = new DeflateStream[mFileCount];
            mStreamUZip = new MemoryStream[mFileCount];

            BinaryReader[] zipReader = new BinaryReader[mFileCount];


            for (int i = 0; i < mFileCount; i++)
            {
                byte[] datHash = mIdxHash[i];
                mLengthHash[i] = (datHash[0]) + (datHash[1] << 8) + (datHash[2] << 16) + (datHash[3] << 24);

                byte[] datAfter = mIdxAfters[i];
                mLengthAfters[i] = (datAfter[0]) + (datAfter[1] << 8) + (datAfter[2] << 16) + (datAfter[3] << 24);

                //准备处理内存流数据
                zipReader[i] = new BinaryReader(mStreamZip[i]);
                //移动至压缩文件内容部分
                zipReader[i].ReadBytes(10);
                //去除压缩文件结尾
                mStreamZip[i].SetLength(mStreamZip[i].Length - 4);

                mStreamDeflate[i] = new DeflateStream(mStreamZip[i], CompressionMode.Decompress,true);

                mStreamUZip[i] = new MemoryStream();
                mStreamDeflate[i].CopyTo(mStreamUZip[i]);
            }
        }

        /// <summary>
        /// 信息显示和更新
        /// </summary>
        /// <param name="mainForm"></param>
        //public static void InfoPrint(ThisWarTranslaterMain mainForm)
        //{
        //    mainForm.textDebug.Text = mainForm.textDebug.Text + "\r\n[信息]获取到的文件数量为：" + m_fileCount;

        //    //数据更新UI暂时挂起
        //    mainForm.infoList.BeginUpdate();

        //    mainForm.infoList.Items.Clear();
        //    for (int i = 0; i < m_fileCount; i++)
        //    {
        //        ListViewItem infoListItem = new ListViewItem();

        //        infoListItem.Text = mLengthHash[i].ToString("X8");
        //        infoListItem.SubItems.Add(mLengthDeviat[i].ToString());
        //        infoListItem.SubItems.Add(mLengthBefore[i].ToString());
        //        infoListItem.SubItems.Add(mLengthAfters[i].ToString());

        //        mainForm.infoList.Items.Add(infoListItem);
        //    }
        //    mainForm.infoList.EndUpdate();
        //}

        public static string exportFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            for (int i = 0; i < mFileCount; i++)
            {
                FileStream outputFile = new FileStream(path + mLengthHash[i].ToString("X8"), FileMode.Create);

                mStreamUZip[i].Seek(0, SeekOrigin.Begin);
                outputFile.Write(mStreamUZip[i].ToArray(), 0, (int)mStreamUZip[i].Length);

                mStreamUZip[i].Seek(0, SeekOrigin.Begin);
                outputFile.Close();
            }
                return "文件已经保存";
        }
    }
}
