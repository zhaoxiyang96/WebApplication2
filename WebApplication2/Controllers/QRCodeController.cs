using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class QRCodeController : Controller
    {
        // GET: QRCode
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 显示二维码
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowQRCode()
        {
            Common.QRCodeHelper qrCode = new Common.QRCodeHelper();
            string msg = "https://zhaoxiyang96.github.io/zhaoxiyang/QRCode/InforMation";
            var qrImg = qrCode.GenerateQRCode(msg, "请扫描二维码拨打挪车电话");
            var base64 = "data:image/jpeg;base64," + Convert.ToBase64String(qrImg);
            ViewBag.CodeImg = base64;
            return View();
        }

        public ActionResult InforMation()
        {
            return View();
        }
    }
}