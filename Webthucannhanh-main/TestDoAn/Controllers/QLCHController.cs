	using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestDoAn.Models;

namespace TestDoAn.Controllers
{
    public class QLCHController : Controller
    {
        private Models.QLBH db = new Models.QLBH();
		private static string tenFileHinhTam = "";
		// GET: Qlch
		public ActionResult Index()
        {
            return View();
        }
		

        public ActionResult IndexHD()
        {
			ViewBag.dscthd = db.ChiTietHoaDons.ToList();

			var link = db.HoaDons.OrderByDescending(s => s.mahd);
            return View(link.ToList());
        }
        public ActionResult FormCTHD(int id)
        {
				double tong = 0;
				List<Models.ChiTietHoaDon> ds = new List<ChiTietHoaDon>();
				foreach (var a in db.ChiTietHoaDons.Where(x => x.mahd.Equals(id)))
				{
					tong += (double)(a.soluong * a.dongia);
					ds.Add(a);
				}
			ViewBag.thanhtien = tong;
				return View(ds);

        }

		public ActionResult Search(string id)
		{
			int a = int.Parse(id);
			HoaDon hd = db.HoaDons.Find(a);
			if (hd != null)
				return PartialView(hd);
			return Content("Không Tìm Thấy");
		}
		public ActionResult IndexNL()
		{

			return View(db.NguyenLieux.ToList());
		}
		public ActionResult FormThemNL()
		{
			return View();
		}
		[HttpPost]
		public ActionResult themNL(Models.NguyenLieu a)
		{
			if (ModelState.IsValid)
			{
				NguyenLieu b = db.NguyenLieux.Find(a.manl);
				if(b==null)
                {
					db.NguyenLieux.Add(a);
					db.SaveChanges();
					return RedirectToAction("IndexNL");
				}
				ModelState.AddModelError("manl", "Mã Nguyên Liệu đã được sử dụng");
			}
			return View("FormThemNL");
		}
		public ActionResult FormXoaNL(string id)
		{
			bool Xoa = true;
			Models.NguyenLieu a = db.NguyenLieux.Find(id);
			foreach (Models.ChiTietSanPham t in db.ChiTietSanPhams.Where(x => x.manl == id).ToList())
			{					
				Xoa = false;
				break;
			}
			ViewBag.XoaNguyenLieu = Xoa;
			
			if (a != null)
			{
				return View(a);
			}
			return RedirectToAction("IndexNL");
		}

		public ActionResult xoaNL(string id)
		{
			Models.NguyenLieu a = db.NguyenLieux.Find(id);
			if (a != null)
			{
					db.NguyenLieux.Remove(a);
					db.SaveChanges();
			}
			return RedirectToAction("IndexNL");

		}
		public ActionResult FormSuaNL(string id)
		{
			Models.NguyenLieu a = db.NguyenLieux.Find(id);
			if (a != null)
				return View(a);
			return RedirectToAction("IndexNL");
		}
		[HttpPost]
		public ActionResult suaNL(Models.NguyenLieu b)
		{

			Models.NguyenLieu a = db.NguyenLieux.Find(b.manl);
			if (a != null)
			{
				a.tenhh = b.tenhh;
				a.soluong = b.soluong;
				a.maloai = b.maloai;
				a.hsd = b.hsd;
				a.nhacungcap = b.nhacungcap;
				a.dvt = b.dvt;
				a.dongia = b.dongia;
				db.SaveChanges();
				return RedirectToAction("IndexNL");
			}
			return View("FormSuaNL");
		}
		[HttpGet]
		public ActionResult xoahoadon(int id)
        {
			HoaDon a = db.HoaDons.Find(id);
			List<ChiTietHoaDon> ds = db.ChiTietHoaDons.Where(x => x.mahd == a.mahd).ToList();
			foreach(var b in ds)
            {
                if (b != null)
                {
					db.ChiTietHoaDons.Remove(b);
					db.SaveChanges();
				}
			}
			
				db.HoaDons.Remove(a);
				db.SaveChanges();
            
			return RedirectToAction("IndexHD");
        }

		public ActionResult IndexNH()
        {
			return View(db.NguyenLieux.ToList());
        }

		public ActionResult add(string id)
        {
			NguyenLieu a = db.NguyenLieux.Find(id);
			return View(a);
        }

		[HttpGet]
		public ActionResult addNL(string id)
        {
			NguyenLieu nl = db.NguyenLieux.Find(id);
			if(nl!=null)
            {
				int a = int.Parse(Request["slnhap"].ToString());
				if (a > 0)
				{
					nl.soluong = nl.soluong + a;
					db.NguyenLieux.AddOrUpdate(nl);
					db.SaveChanges();
					return RedirectToAction("IndexNH");
				}
				ModelState.AddModelError("slnhap", "Số lượng lớn hơn 0!!!");
			}
			return View("add");
        }

		public ActionResult indexsp()
        {
			return View(db.SanPhams.ToList());
        }
		public ActionResult formthemsp()
        {
			return View();
        }

		[HttpPost]
		public ActionResult themsp(SanPham a)
        {
			if (ModelState.IsValid)
			{
				SanPham b = db.SanPhams.Find(a.masp);
				if (b == null)
				{
					if (tenFileHinhTam == "")
					{
						a.hinh = "";
					}
					else
					{
						FileInfo fi = new FileInfo(tenFileHinhTam);
						a.hinh =	a.masp + fi.Extension;

						StringWriter sw = new StringWriter();
						sw.Write(Request.PhysicalApplicationPath);
						sw.Write(@"img\");
						sw.Write(a.hinh);

						StringWriter swt = new StringWriter();
						swt.Write(Request.PhysicalApplicationPath);
						swt.Write(@"Tam\");
						swt.Write(tenFileHinhTam);

						System.IO.File.Move(swt.ToString(), sw.ToString());
						//System.IO.File.Copy(swt.ToString(), sw.ToString());
						//System.IO.File.Delete(swt.ToString());
					}
					db.SanPhams.Add(a);
					db.SaveChanges();
					return RedirectToAction("indexsp");
				}
				ModelState.AddModelError("masp", "Mã đã được sử dụng");
			}
			return View("formthemsp");
		}

		[HttpPost]
		public ActionResult chonHinh(HttpPostedFileBase fileImg)
		{
			//fileTam = fileImg;
			if (fileImg != null)
			{
				StringWriter sw = new StringWriter();
				sw.Write(Request.PhysicalApplicationPath);
				sw.Write(@"Tam\");
				sw.Write(fileImg.FileName);

				fileImg.SaveAs(sw.ToString());

				tenFileHinhTam = fileImg.FileName;
			}
			else
			{
				tenFileHinhTam = "";
			}
			return RedirectToAction("formthemsp");
		}
	}
}