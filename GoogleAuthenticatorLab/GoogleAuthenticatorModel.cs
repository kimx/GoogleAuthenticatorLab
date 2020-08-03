using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleAuthenticatorLab
{
    public class GoogleAuthenticatorModel
    {
        public string SecretKey { get; set; }
        public string BarcodeUrl { get; set; }
        public string Code { get; set; }

    }
}