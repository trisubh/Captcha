using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Captacha
{
    public partial class Captcha : System.Web.UI.Page
    {

        static Random rnd = new Random();
        public CaptchaValidate CaptchaValidate { get; set; } = new CaptchaValidate();
        string config = CaptcahaMode.mode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (config == "text")
            {
                Bitmap objBitmap = new Bitmap(130, 80);
                Graphics objGraphics = Graphics.FromImage(objBitmap);
                objGraphics.Clear(Color.White);
                Random objRandom = new Random();
                objGraphics.DrawLine(Pens.Black, objRandom.Next(0, 50), objRandom.Next(10, 30), objRandom.Next(0, 200), objRandom.Next(0, 50));
                objGraphics.DrawRectangle(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(0, 20), objRandom.Next(50, 80), objRandom.Next(0, 20));
                objGraphics.DrawLine(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(10, 50), objRandom.Next(100, 200), objRandom.Next(0, 80));
                Brush objBrush =
                    default(Brush);
                //create background style  
                HatchStyle[] aHatchStyles = new HatchStyle[]
                {
                HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
                    HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                    HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal
                };
                //create rectangular area  
                RectangleF oRectangleF = new RectangleF(0, 0, 300, 300);
                objBrush = new HatchBrush(aHatchStyles[objRandom.Next(aHatchStyles.Length - 3)], Color.FromArgb((objRandom.Next(100, 255)), (objRandom.Next(100, 255)), (objRandom.Next(100, 255))), Color.White);
                objGraphics.FillRectangle(objBrush, oRectangleF);
                //Generate the image for captcha  
                string captchaText = string.Format("{0:X}", objRandom.Next(1000000, 9999999));
                //add the captcha value in session  
                CaptchaValidate.StringToValidate = captchaText;
                if(CaptcahaMode.isstrict=="false" || CaptcahaMode.isstrict=="")
                Session["CaptchaVerify"] = captchaText.ToLower();
                else if (CaptcahaMode.isstrict == "true" )
                Session["CaptchaVerify"] = captchaText;

                else if (CaptcahaMode.isstrict == "false")
                Session["CaptchaVerify"] = captchaText.ToLower();
                Font objFont = new Font("Courier New", 15, FontStyle.Bold);
                //Draw the image for captcha  
                objGraphics.DrawString(captchaText, objFont, Brushes.Black, 20, 20);
                objBitmap.Save(Response.OutputStream, ImageFormat.Gif);
            }
            else if (config == "icon")
            {

                Bitmap objBitmap = new Bitmap(130, 80);
                Graphics objGraphics = Graphics.FromImage(objBitmap);
                objGraphics.Clear(Color.White);
                Random objRandom = new Random();
                objGraphics.DrawLine(Pens.Black, objRandom.Next(0, 50), objRandom.Next(10, 30), objRandom.Next(0, 200), objRandom.Next(0, 50));
                objGraphics.DrawRectangle(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(0, 20), objRandom.Next(50, 80), objRandom.Next(0, 20));
                objGraphics.DrawLine(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(10, 50), objRandom.Next(100, 200), objRandom.Next(0, 80));
                Brush objBrush =
                    default(Brush);
                //create background style  
                HatchStyle[] aHatchStyles = new HatchStyle[]
                {
                HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
                    HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                    HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal
                };
                //create rectangular area  
                RectangleF oRectangleF = new RectangleF(0, 0, 300, 300);
                objBrush = new HatchBrush(aHatchStyles[objRandom.Next(aHatchStyles.Length - 3)], Color.FromArgb((objRandom.Next(100, 255)), (objRandom.Next(100, 255)), (objRandom.Next(100, 255))), Color.White);
                objGraphics.FillRectangle(objBrush, oRectangleF);

                List<string> allcaptchaimages = new List<string>();

                foreach (string s in Directory.GetFiles(Server.MapPath(@"~\Captcha_img")).Select(Path.GetFileName))
                {
                    allcaptchaimages.Add(s);
                }
                int next = rnd.Next(allcaptchaimages.Count);

                string imagepath = Server.MapPath(Path.Combine(@"~\Captcha_img", allcaptchaimages[next]));
                System.Drawing.Image i = System.Drawing.Image.FromFile(imagepath);
                string filename = Path.GetFileNameWithoutExtension(imagepath);




                //add the captcha value in session  
                CaptchaValidate.StringToValidate = filename;
                Session["CaptchaVerify"] = filename.ToLower();

                //Draw the image for captcha  
                objGraphics.DrawImage(i, 10, 10, i.Width + 30, i.Height + 30);
                objBitmap.Save(Response.OutputStream, ImageFormat.Gif);
            }
            else if (config == "expression")
            {
                Bitmap objBitmap = new Bitmap(130, 80);
                Graphics objGraphics = Graphics.FromImage(objBitmap);
                objGraphics.Clear(Color.White);
                Random objRandom = new Random();
                objGraphics.DrawLine(Pens.Black, objRandom.Next(0, 50), objRandom.Next(10, 30), objRandom.Next(0, 200), objRandom.Next(0, 50));
                objGraphics.DrawRectangle(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(0, 20), objRandom.Next(50, 80), objRandom.Next(0, 20));
                objGraphics.DrawLine(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(10, 50), objRandom.Next(100, 200), objRandom.Next(0, 80));
                Brush objBrush =
                    default(Brush);
                //create background style  
                HatchStyle[] aHatchStyles = new HatchStyle[]
                {
                HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
                    HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                    HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal
                };
                //create rectangular area  
                RectangleF oRectangleF = new RectangleF(0, 0, 300, 300);
                objBrush = new HatchBrush(aHatchStyles[objRandom.Next(aHatchStyles.Length - 3)], Color.FromArgb((objRandom.Next(100, 255)), (objRandom.Next(100, 255)), (objRandom.Next(100, 255))), Color.White);
                objGraphics.FillRectangle(objBrush, oRectangleF);
                //Generate the image for captcha  
                int imin = objRandom.Next(1, 10);
                int imax = objRandom.Next(11, 20);
                char[] operatoraray = new char[] { '+', '*','-' };
                char _operator = operatoraray[rnd.Next(operatoraray.Length)];
                string captchaText = string.Format("{0:D}", imin) + " " + _operator + " " + string.Format("{0:D}", imax);
                string displayskin = string.Empty;
                //add the captcha value in session  
                // CaptchaValidate.StringToValidate = captchaText;
                switch (_operator)
                {
                    case '+':
                        displayskin = string.Format("{0:D}", imin + imax);
                        break;

                    case '*':
                        displayskin = string.Format("{0:D}", imin * imax);
                        break;
                    case '-':
                        displayskin = string.Format("{0:D}", imax / imin);
                        break;

                }
                Session["CaptchaVerify"] = displayskin;
                Font objFont = new Font("Courier New", 15, FontStyle.Bold);
                //Draw the image for captcha  
                objGraphics.DrawString(captchaText, objFont, Brushes.Black, 20, 20);
                objBitmap.Save(Response.OutputStream, ImageFormat.Gif);
            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string Text()
        {
            MemoryStream stream = new MemoryStream();
            Bitmap objBitmap = new Bitmap(130, 80);
            Graphics objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.Clear(Color.White);
            Random objRandom = new Random();
            objGraphics.DrawLine(Pens.Black, objRandom.Next(0, 50), objRandom.Next(10, 30), objRandom.Next(0, 200), objRandom.Next(0, 50));
            objGraphics.DrawRectangle(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(0, 20), objRandom.Next(50, 80), objRandom.Next(0, 20));
            objGraphics.DrawLine(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(10, 50), objRandom.Next(100, 200), objRandom.Next(0, 80));
            Brush objBrush =
                default(Brush);
            //create background style  
            HatchStyle[] aHatchStyles = new HatchStyle[]
            {
                HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
                    HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                    HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal
            };
            //create rectangular area  
            RectangleF oRectangleF = new RectangleF(0, 0, 300, 300);
            objBrush = new HatchBrush(aHatchStyles[objRandom.Next(aHatchStyles.Length - 3)], Color.FromArgb((objRandom.Next(100, 255)), (objRandom.Next(100, 255)), (objRandom.Next(100, 255))), Color.White);
            objGraphics.FillRectangle(objBrush, oRectangleF);
            //Generate the image for captcha  
            string captchaText = string.Format("{0:X}", objRandom.Next(1000000, 9999999));
            //add the captcha value in session  
            HttpContext.Current.Session["CaptchaVerify"] = captchaText.ToLower();
            Font objFont = new Font("Courier New", 15, FontStyle.Bold);
            //Draw the image for captcha  
            objGraphics.DrawString(captchaText, objFont, Brushes.Black, 20, 20);
            //return "I was called";
            objBitmap.Save(stream, ImageFormat.Gif);
            return Convert.ToBase64String(stream.GetBuffer());

        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string Icon()
        {
            MemoryStream stream = new MemoryStream();
            Bitmap objBitmap = new Bitmap(130, 80);
            Graphics objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.Clear(Color.White);
            Random objRandom = new Random();
            objGraphics.DrawLine(Pens.Black, objRandom.Next(0, 50), objRandom.Next(10, 30), objRandom.Next(0, 200), objRandom.Next(0, 50));
            objGraphics.DrawRectangle(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(0, 20), objRandom.Next(50, 80), objRandom.Next(0, 20));
            objGraphics.DrawLine(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(10, 50), objRandom.Next(100, 200), objRandom.Next(0, 80));
            Brush objBrush =
                default(Brush);
            //create background style  
            HatchStyle[] aHatchStyles = new HatchStyle[]
            {
                HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
                    HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                    HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal
            };
            //create rectangular area  
            RectangleF oRectangleF = new RectangleF(0, 0, 300, 300);
            objBrush = new HatchBrush(aHatchStyles[objRandom.Next(aHatchStyles.Length - 3)], Color.FromArgb((objRandom.Next(100, 255)), (objRandom.Next(100, 255)), (objRandom.Next(100, 255))), Color.White);
            objGraphics.FillRectangle(objBrush, oRectangleF);

            List<string> allcaptchaimages = new List<string>();

            foreach (string s in Directory.GetFiles(HttpContext.Current.Server.MapPath(@"~\Captcha_img")).Select(Path.GetFileName))
            {
                allcaptchaimages.Add(s);
            }
            int next = rnd.Next(allcaptchaimages.Count);

            string imagepath = HttpContext.Current.Server.MapPath(Path.Combine(@"~\Captcha_img", allcaptchaimages[next]));
            System.Drawing.Image i = System.Drawing.Image.FromFile(imagepath);
            string filename = Path.GetFileNameWithoutExtension(imagepath);




            //add the captcha value in session  
            //CaptchaValidate.StringToValidate = filename;
            HttpContext.Current.Session["CaptchaVerify"] = filename.ToLower();

            //Draw the image for captcha  
            objGraphics.DrawImage(i, 10, 10, i.Width + 30, i.Height + 30);
            objBitmap.Save(stream, ImageFormat.Gif);
            return Convert.ToBase64String(stream.GetBuffer());

        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string expression()
        {
            MemoryStream stream = new MemoryStream();
            Bitmap objBitmap = new Bitmap(130, 80);
            Graphics objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.Clear(Color.White);
            Random objRandom = new Random();
            objGraphics.DrawLine(Pens.Black, objRandom.Next(0, 50), objRandom.Next(10, 30), objRandom.Next(0, 200), objRandom.Next(0, 50));
            objGraphics.DrawRectangle(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(0, 20), objRandom.Next(50, 80), objRandom.Next(0, 20));
            objGraphics.DrawLine(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(10, 50), objRandom.Next(100, 200), objRandom.Next(0, 80));
            Brush objBrush =
                default(Brush);
            //create background style  
            HatchStyle[] aHatchStyles = new HatchStyle[]
            {
                HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
                    HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                    HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal
            };
            //create rectangular area  
            RectangleF oRectangleF = new RectangleF(0, 0, 300, 300);
            objBrush = new HatchBrush(aHatchStyles[objRandom.Next(aHatchStyles.Length - 3)], Color.FromArgb((objRandom.Next(100, 255)), (objRandom.Next(100, 255)), (objRandom.Next(100, 255))), Color.White);
            objGraphics.FillRectangle(objBrush, oRectangleF);
            //Generate the image for captcha  
            int imin = objRandom.Next(1, 20);
            int imax = objRandom.Next(1, 20);
            char[] operatoraray = new char[] { '+', '*' };
            char _operator = operatoraray[rnd.Next(operatoraray.Length)];
            string captchaText = string.Format("{0:D}", imin) + " " + _operator + " " + string.Format("{0:D}", imax);
            string displayskin = string.Empty;
            //add the captcha value in session  
            // CaptchaValidate.StringToValidate = captchaText;
            switch (_operator)
            {
                case '+':
                    displayskin = string.Format("{0:D}", imin + imax);
                    break;

                case '*':
                    displayskin = string.Format("{0:D}", imin * imax);
                    break;
                case '-':
                    displayskin = string.Format("{0:D}", imax * imin);
                    break;

            }
            HttpContext.Current.Session["CaptchaVerify"] = displayskin;
            Font objFont = new Font("Courier New", 15, FontStyle.Bold);
            //Draw the image for captcha  
            objGraphics.DrawString(captchaText, objFont, Brushes.Black, 20, 20);
            objBitmap.Save(stream, ImageFormat.Gif);
            return Convert.ToBase64String(stream.GetBuffer());
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string pronounce()
        {
            string word = HttpContext.Current.Session["CaptchaVerify"].ToString();
            Task.Run(() => { 
            ScriptCs.SpeakR.ScriptPack.SpeakR speak = new ScriptCs.SpeakR.ScriptPack.SpeakR();
            speak.Age(System.Speech.Synthesis.VoiceAge.Teen);
            speak.Gender(System.Speech.Synthesis.VoiceGender.Female);

                foreach (char a in word)
            {
                speak.Speak(a.ToString());
            }
            });
            return string.Empty;
        }

    }

    
}