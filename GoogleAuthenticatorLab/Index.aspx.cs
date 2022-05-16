using OtpNet;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleAuthenticatorLab
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
                return;
            var model = GenerateGoogleAuthenticatorModel();
            var fileUrl = GenerateQRCode(model);
            this.imgBarcode.ImageUrl = fileUrl;
            this.lblSecretKey.Text = model.SecretKey;
        }

        private GoogleAuthenticatorModel GenerateGoogleAuthenticatorModel()
        {
            byte[] secretByte = KeyGeneration.GenerateRandomKey(20);
            string userName = "Kimxinfo";
            string secretKey = Base32Encoding.ToString(secretByte);
            string barcodeUrl = $"otpauth://totp/{userName}?secret={secretKey}&issuer=GoogleAuthenticatorLab";
            var model = new GoogleAuthenticatorModel
            {
                SecretKey = secretKey,
                BarcodeUrl = barcodeUrl
            };
            return model;
        }


        private string GenerateQRCode(GoogleAuthenticatorModel model)
        {
            string fileUrl = $"~/temp/{model.SecretKey}.jpg";
            string filePath = Server.MapPath(fileUrl);
            QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M;
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(model.BarcodeUrl, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        using (Bitmap image = qrCode.GetGraphic(10, Color.Black, Color.White, icon: null, iconSizePercent: 0))
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                                return fileUrl;
                            }

                        }
                    }
                }
            }

        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            byte[] secretKey = Base32Encoding.ToBytes(lblSecretKey.Text);

            long timeStepMatched = 0;//當目前代碼驗證成功後,會得一個流水號,第二次比對成功也是同樣號,可以用來比對同一代碼不能被用第2次
            var otp = new Totp(secretKey);
            if (otp.VerifyTotp(txtCode.Text.Trim(), out timeStepMatched, new VerificationWindow()))//STEP :30 預設值30秒產生一次,VerificationWindow 決定前後幾次可以接收,預設0,0,只接收當下
            {
                lblVerifyResult.Text = "Success";
            }
            else
                lblVerifyResult.Text = "The Code is not valid";
        }
    }
}