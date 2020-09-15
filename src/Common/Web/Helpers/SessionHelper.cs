using Common.Extensions;
using Common.Models;
using System;

namespace Web.Helpers
{
    public static class SessionHelper
    {
        public const string AccessTokenFormat = "{username}|{logindate}|{expireddate}";
        public const string AccessTokenHashKey = "hashkey";

        public static AccessTokenModel CreateAccessToken(string userName, bool isRememberMe = false)
        {
            DateTime loginDate = DateTime.Now;
            DateTime expriedDate = isRememberMe ? DateTime.Now.AddDays(7) : DateTime.Now.AddDays(1);

            return new AccessTokenModel()
            {
                AccessToken = AccessTokenFormat.Replace("username", userName)
                                                           .Replace("logindate", loginDate.ToString("yyyy-MM-dd HH:mm-ss.fff"))
                                                           .Replace("exprieddate", expriedDate.ToString("yyyy-MM-dd HH:mm-ss.fff"))
                                                           .TripleDESEncrypt(AccessTokenHashKey),
                LoginDate = loginDate,
                ExpiredDate = expriedDate
            };
        }
    }
}