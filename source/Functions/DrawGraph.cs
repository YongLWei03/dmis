using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Data;

namespace Functions
{
    class DrawGraph
    {
        private Graphics objGraphics; //Graphics 类提供将对象绘制到显示设备的方法
        private Bitmap objBitmap; //位图对象
        Pen Bp = new Pen(Color.Black);//定义黑色画笔
        Pen Rp = new Pen(Color.Red); //定义红色画笔
        Pen Sp = new Pen(Color.Silver);//定义银灰色画笔 
        Font Bfont = new Font("Arial", 12, FontStyle.Bold);//定义大标题字体 
        Font font = new Font("Arial", 7);//定义一般字体
        Font Tfont = new Font("Arial", 9); //定义大点的字体
        private void InitializeGraph(DataTable myDt, int Yy, int Xx,int Ystart,int Xstart)
        {
            int count = myDt.Rows.Count;
            int fHeight = 80 + Yy * count;
            if (fHeight < 400) fHeight = 400;
            int fWidth = 80 + Xx * count;
            if (fWidth < 400) fWidth = 400;
            objBitmap = new Bitmap(fWidth, fHeight);
            objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.DrawRectangle(new Pen(Color.White, fHeight), Xstart, Ystart, fWidth, fHeight);//绘制底色
            for (int i = 0; i < count; i++) //绘制竖坐标线 
            {
                objGraphics.DrawLine(Sp, 40 + Xstart + Xx * i, 60 + Ystart, 40 + Xstart + Xx * i, Yy * count + Ystart);
            }
            objGraphics.DrawLine(Bp, 40 + Xstart, 50 + Ystart, 40 + Xstart, Yy * count + Ystart);//绘制竖坐标轴
            for (int i = 0; i < count; i++) //绘制横坐标线 
            {
                objGraphics.DrawLine(Sp, 40 + Xstart, 60 + Ystart + Yy * i, 40 + Xstart + Xx * count, 60 + Ystart + Yy * i);
            }
            objGraphics.DrawLine(Bp, 40 + Xstart, 50 + Ystart, 40 + Xstart, Yy * count + Ystart);//绘制横坐标轴
        }
    }
}
