using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Drawing;
namespace PlatForm.CustomControlLib
{
    [ToolboxData("<{0}:showPictrue runat=server></{0}:showPictrue>")]
    public class showPictrue : WebControl, INamingContainer
    {
        public showPictrue()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        private Graphics objGraphics; //Graphics ���ṩ��������Ƶ���ʾ�豸�ķ���
        private Bitmap objBitmap; //λͼ����

        private int m_Width = 900; //ͼ����
        private int m_Height = 500; //ͼ��߶�
        private float m_XSlice = 50; //X��̶ȿ��
        private float m_YSlice = 50; //Y��̶ȿ��
        private float m_YSliceValue = 20; //Y��̶ȵ���ֵ���
        private float m_YSliceBegin = 0; //Y��̶ȿ�ʼֵ
        private float m_Tension = 0.5f;
        private string m_Title = "������˾�����������ͼ"; //Title
        private string m_Unit = "��Ԫ"; //��λ
        private string m_XAxisText = "�·�"; //X��˵������
        private string m_YAxisText = "��Ԫ"; //Y��˵������
        private string[] m_Keys = new string[] { "һ��", "����", "����", "����", "����", "����", "����", "����", "����", "ʮ��", "ʮһ��", "ʮ����" }; //��
        private float[] m_Values = new float[] { 20.0f, 30.0f, 50.0f, 55.4f, 21.6f, 12.8f, 99.5f, 36.4f, 78.2f, 56.4f, 45.8f, 66.5f }; //ֵ
        private Color m_BgColor = Color.White; //����
        private Color m_TextColor = Color.Black; //������ɫ
        private Color m_BorderColor = Color.Black; //����߿���ɫ
        private Color m_AxisColor = Color.Black; //������ɫ
        private Color m_AxisTextColor = Color.Black; //��˵��������ɫ
        private Color m_SliceTextColor = Color.Black; //�̶�������ɫ
        private Color m_SliceColor = Color.Black; //�̶���ɫ
        private Color m_CurveColor = Color.Red; //������ɫ

        //����ͼ�񲢷���bmpͼ�����
        public Bitmap CreateImage()
        {
            InitializeGraph();

            DrawContent(ref objGraphics);

            return objBitmap;
        }

        //��ʼ�������ͼ�����򣬻����߿򣬳�ʼ����
        private void InitializeGraph()
        {

            //���ݸ����ĸ߶ȺͿ�ȴ���һ��λͼͼ��
            objBitmap = new Bitmap(Width, Height);

            //��ָ���� objBitmap ���󴴽� objGraphics ���� (����objBitmap�����л�ͼ)
            objGraphics = Graphics.FromImage(objBitmap);

            //���ݸ�����ɫ(LightGray)���ͼ��ľ������� (����)
            objGraphics.DrawRectangle(new Pen(BorderColor, 1), 0, 0, Width, Height);
            objGraphics.FillRectangle(new SolidBrush(BgColor), 1, 1, Width - 2, Height - 2);

            //��X��,pen,x1,y1,x2,y2 ע��ͼ���ԭʼX���Y������������Ͻ�Ϊԭ�㣬���Һ����¼����
            objGraphics.DrawLine(new Pen(new SolidBrush(AxisColor), 1), 100, Height - 100, Width - 75, Height - 100);

            //��Y��,pen,x1,y1,x2,y2
            objGraphics.DrawLine(new Pen(new SolidBrush(AxisColor), 1), 100, Height - 100, 100, 75);

            //��ʼ������˵������
            SetAxisText(ref objGraphics);

            //��ʼ��X���ϵĿ̶Ⱥ�����
            SetXAxis(ref objGraphics);

            //��ʼ��Y���ϵĿ̶Ⱥ�����
            SetYAxis(ref objGraphics);

            //��ʼ������
            CreateTitle(ref objGraphics);
        }

        private void SetAxisText(ref Graphics objGraphics)
        {
            objGraphics.DrawString(XAxisText, new Font("����", 10), new SolidBrush(AxisTextColor), Width / 2 - 50, Height - 50);

            int X = 30;
            int Y = (Height / 2) - 50;
            for (int i = 0; i < YAxisText.Length; i++)
            {
                objGraphics.DrawString(YAxisText[i].ToString(), new Font("����", 10), new SolidBrush(AxisTextColor), X, Y);
                Y += 15;
            }
        }

        private void SetXAxis(ref Graphics objGraphics)
        {
            int x1 = 100;
            int y1 = Height - 110;
            int x2 = 100;
            int y2 = Height - 90;
            int iCount = 0;
            int iSliceCount = 1;
            float Scale = 0;
            int iWidth = (int)((Width - 200) * (50 / XSlice));

            objGraphics.DrawString(Keys[0].ToString(), new Font("����", 10), new SolidBrush(SliceTextColor), 85, Height - 90);

            for (int i = 0; i <= iWidth; i += 10)
            {
                Scale = i * (XSlice / 50);

                if (iCount == 5)
                {
                    objGraphics.DrawLine(new Pen(new SolidBrush(AxisColor)), x1 + Scale, y1, x2 + Scale, y2);
                    //The Point!������ʾX��̶�
                    if (iSliceCount <= Keys.Length - 1)
                    {
                        objGraphics.DrawString(Keys[iSliceCount].ToString(), new Font("����", 10), new SolidBrush(SliceTextColor), x1 + Scale - 15, y2);
                    }
                    else
                    {
                        //������Χ�������κο̶�����
                    }
                    iCount = 0;
                    iSliceCount++;
                    if (x1 + Scale > Width - 100)
                    {
                        break;
                    }
                }
                else
                {
                    objGraphics.DrawLine(new Pen(new SolidBrush(SliceColor)), x1 + Scale, y1 + 5, x2 + Scale, y2 - 5);
                }
                iCount++;
            }
        }

        private void SetYAxis(ref Graphics objGraphics)
        {
            int x1 = 95;
            int y1 = (int)(Height - 100 - 10 * (YSlice / 50));
            int x2 = 105;
            int y2 = (int)(Height - 100 - 10 * (YSlice / 50));
            int iCount = 1;
            float Scale = 0;
            int iSliceCount = 1;

            int iHeight = (int)((Height - 200) * (50 / YSlice));

            objGraphics.DrawString(YSliceBegin.ToString(), new Font("����", 10), new SolidBrush(SliceTextColor), 60, Height - 110);

            for (int i = 0; i < iHeight; i += 10)
            {
                Scale = i * (YSlice / 50);

                if (iCount == 5)
                {
                    objGraphics.DrawLine(new Pen(new SolidBrush(AxisColor)), x1 - 5, y1 - Scale, x2 + 5, y2 - Scale);
                    //The Point!������ʾY��̶�
                    objGraphics.DrawString(Convert.ToString(YSliceValue * iSliceCount + YSliceBegin), new Font("����", 10), new SolidBrush(SliceTextColor), 60, y1 - Scale);

                    iCount = 0;
                    iSliceCount++;
                }
                else
                {
                    objGraphics.DrawLine(new Pen(new SolidBrush(SliceColor)), x1, y1 - Scale, x2, y2 - Scale);
                }
                iCount++;
            }
        }

        private void DrawContent(ref Graphics objGraphics)
        {
            if (Keys.Length == Values.Length)
            {
                Pen CurvePen = new Pen(CurveColor, 1);
                PointF[] CurvePointF = new PointF[Keys.Length];
                float keys = 0;
                float values = 0;
                float Offset1 = (Height - 100) + YSliceBegin;
                float Offset2 = (YSlice / 50) * (50 / YSliceValue);

                for (int i = 0; i < Keys.Length; i++)
                {
                    keys = XSlice * i + 100;
                    values = Offset1 - Values[i] * Offset2;
                    CurvePointF[i] = new PointF(keys, values);
                }
                objGraphics.DrawCurve(CurvePen, CurvePointF, Tension);
            }
            else
            {
                objGraphics.DrawString("Error!The length of Keys and Values must be same!", new Font("����", 16), new SolidBrush(TextColor), new Point(150, Height / 2));
            }
        }

        //��ʼ������
        private void CreateTitle(ref Graphics objGraphics)
        {
            objGraphics.DrawString(Title, new Font("����", 16), new SolidBrush(TextColor), new Point(5, 5));
        }

        public int Width
        {
            set
            {
                if (value < 300)
                {
                    m_Width = 300;
                }
                else
                {
                    m_Width = value;
                }
            }
            get
            {
                return m_Width;
            }
        }

        public int Height
        {
            set
            {
                if (value < 300)
                {
                    m_Height = 300;
                }
                else
                {
                    m_Height = value;
                }
            }
            get
            {
                return m_Height;
            }
        }

        public float XSlice
        {
            set
            {
                m_XSlice = value;
            }
            get
            {
                return m_XSlice;
            }
        }

        public float YSlice
        {
            set
            {
                m_YSlice = value;
            }
            get
            {
                return m_YSlice;
            }
        }

        public float YSliceValue
        {
            set
            {
                m_YSliceValue = value;
            }
            get
            {
                return m_YSliceValue;
            }
        }

        public float YSliceBegin
        {
            set
            {
                m_YSliceBegin = value;
            }
            get
            {
                return m_YSliceBegin;
            }
        }

        public float Tension
        {
            set
            {
                if (value < 0.0f && value > 1.0f)
                {
                    m_Tension = 0.5f;
                }
                else
                {
                    m_Tension = value;
                }
            }
            get
            {
                return m_Tension;
            }
        }

        public string Title
        {
            set
            {
                m_Title = value;
            }
            get
            {
                return m_Title;
            }
        }

        public string Unit
        {
            set
            {
                m_Unit = value;
            }
            get
            {
                return m_Unit;
            }
        }

        public string[] Keys
        {
            set
            {
                m_Keys = value;
            }
            get
            {
                return m_Keys;
            }
        }

        public float[] Values
        {
            set
            {
                m_Values = value;
            }
            get
            {
                return m_Values;
            }
        }

        public Color BgColor
        {
            set
            {
                m_BgColor = value;
            }
            get
            {
                return m_BgColor;
            }
        }

        public Color TextColor
        {
            set
            {
                m_TextColor = value;
            }
            get
            {
                return m_TextColor;
            }
        }

        public Color BorderColor
        {
            set
            {
                m_BorderColor = value;
            }
            get
            {
                return m_BorderColor;
            }
        }

        public Color AxisColor
        {
            set
            {
                m_AxisColor = value;
            }
            get
            {
                return m_AxisColor;
            }
        }

        public string XAxisText
        {
            set
            {
                m_XAxisText = value;
            }
            get
            {
                return m_XAxisText;
            }
        }

        public string YAxisText
        {
            set
            {
                m_YAxisText = value;
            }
            get
            {
                return m_YAxisText;
            }
        }

        public Color AxisTextColor
        {
            set
            {
                m_AxisTextColor = value;
            }
            get
            {
                return m_AxisTextColor;
            }
        }

        public Color SliceTextColor
        {
            set
            {
                m_SliceTextColor = value;
            }
            get
            {
                return m_SliceTextColor;
            }
        }

        public Color SliceColor
        {
            set
            {
                m_SliceColor = value;
            }
            get
            {
                return m_SliceColor;
            }
        }

        public Color CurveColor
        {
            set
            {
                m_CurveColor = value;
            }
            get
            {
                return m_CurveColor;
            }
        }
    }


}
