using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Data;

namespace Functions
{
    class DrawGraph
    {
        private Graphics objGraphics; //Graphics ���ṩ��������Ƶ���ʾ�豸�ķ���
        private Bitmap objBitmap; //λͼ����
        Pen Bp = new Pen(Color.Black);//�����ɫ����
        Pen Rp = new Pen(Color.Red); //�����ɫ����
        Pen Sp = new Pen(Color.Silver);//��������ɫ���� 
        Font Bfont = new Font("Arial", 12, FontStyle.Bold);//������������ 
        Font font = new Font("Arial", 7);//����һ������
        Font Tfont = new Font("Arial", 9); //�����������
        private void InitializeGraph(DataTable myDt, int Yy, int Xx,int Ystart,int Xstart)
        {
            int count = myDt.Rows.Count;
            int fHeight = 80 + Yy * count;
            if (fHeight < 400) fHeight = 400;
            int fWidth = 80 + Xx * count;
            if (fWidth < 400) fWidth = 400;
            objBitmap = new Bitmap(fWidth, fHeight);
            objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.DrawRectangle(new Pen(Color.White, fHeight), Xstart, Ystart, fWidth, fHeight);//���Ƶ�ɫ
            for (int i = 0; i < count; i++) //������������ 
            {
                objGraphics.DrawLine(Sp, 40 + Xstart + Xx * i, 60 + Ystart, 40 + Xstart + Xx * i, Yy * count + Ystart);
            }
            objGraphics.DrawLine(Bp, 40 + Xstart, 50 + Ystart, 40 + Xstart, Yy * count + Ystart);//������������
            for (int i = 0; i < count; i++) //���ƺ������� 
            {
                objGraphics.DrawLine(Sp, 40 + Xstart, 60 + Ystart + Yy * i, 40 + Xstart + Xx * count, 60 + Ystart + Yy * i);
            }
            objGraphics.DrawLine(Bp, 40 + Xstart, 50 + Ystart, 40 + Xstart, Yy * count + Ystart);//���ƺ�������
        }
    }
}
