﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ForgetPassword
{
    public class ForgotPasswordModel
    {
        public string Email {  get; set; }
        public long UserId {  get; set; }
        
       public string Token {  get; set; }
    }
}
