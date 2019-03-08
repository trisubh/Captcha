using BotDetect.Web;
using BotDetect.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web.Services;
using System.Web.Script.Services;
using System.Configuration;
using ScriptCs.SpeakR;



namespace Captacha
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
            
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CaptcahaMode.isstrict == "true" && (txtVerificationCode.Text == Session["CaptchaVerify"].ToString()))
            {
                Response.Redirect("About.aspx");
                lblCaptchaMessage.Text = "You have entered correct captcha !";
                lblCaptchaMessage.ForeColor = System.Drawing.Color.Green;
            }
            else if (CaptcahaMode.isstrict == "false" && (txtVerificationCode.Text.ToLower() == Session["CaptchaVerify"].ToString().ToLower()))
            {
                Response.Redirect("About.aspx");
                lblCaptchaMessage.Text = "You have entered correct captcha !";
                lblCaptchaMessage.ForeColor = System.Drawing.Color.Green;
            }
            else if (CaptcahaMode.isstrict == "" && (txtVerificationCode.Text.ToLower() == Session["CaptchaVerify"].ToString().ToLower()))
            {
                Response.Redirect("About.aspx");
                lblCaptchaMessage.Text = "You have entered correct captcha !";
                lblCaptchaMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblCaptchaMessage.Text = "Please enter correct captcha !";
                lblCaptchaMessage.ForeColor = System.Drawing.Color.Red;
            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string Test()
        {
            return "I was called";
        }

    }
}