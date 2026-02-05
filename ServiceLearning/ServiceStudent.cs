using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml; // EPPlus
using System.IO;
using System.Data.Entity; // Ensure you have this for EF
using System.Drawing;

namespace ServiceLearning
{
    public class ServiceStudent
    {
        public class SVviewModel
        {
            public int Stt { get; set; }
            public string MSSV { get; set; }
            public string HoTen { get; set; }
            public string Khoa { get; set; }
            public string VaiTro { get; set; }
            public string ThamGia { get; set; }
        }

        //build query
        private IQueryable<SINH_VIEN> BuildQuery_sv(Context db, string MSSV, string Makhoa, string loai, DateTime? start, DateTime? end)
        {
            var query = db.SINH_VIEN.AsNoTracking().Where(x => x.Hide == false);

            //filter
            if (!string.IsNullOrEmpty(MSSV))
            {
                query = query.Where(x => x.MSSV.Contains(MSSV));
            }

            if (!string.IsNullOrEmpty(Makhoa))
            {
                query = query.Where(x => x.Khoa == Makhoa);
            }

            if (!string.IsNullOrEmpty(loai) || start.HasValue || end.HasValue)
            {
                query = query.Where(s => s.HD_SINHVIEN.Any(h =>
                    h.HOAT_DONG.Hide == false &&
                    (string.IsNullOrEmpty(loai) || h.HOAT_DONG.Loai == loai) &&
                    (!start.HasValue || h.HOAT_DONG.NgayBatDau >= start) &&
                    (!end.HasValue || h.HOAT_DONG.NgayKetThuc <= end)
                ));
            }

            return query;
        }

        //Pagination for Grid view
        public List<SVviewModel> GetPagedData(int pageIndex, int pageSize, string mssv, string maKhoa, string loai, DateTime? start, DateTime? end, out int totalRecords)
        {
            using (var db = new Context())
            {
                var query = BuildQuery_sv(db, mssv, maKhoa, loai, start, end);
                totalRecords = query.Count();
                //fetch data
                var rawData = query
                    .OrderBy(s => s.MSSV)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(s => new
                    {
                        s.MSSV,
                        s.HoTen,
                        KhoaName = s.KHOA1.TenKhoa,
                        Activities = s.HD_SINHVIEN
                            .Where(h => h.HOAT_DONG.Hide == false &&
                                    (string.IsNullOrEmpty(loai) || h.HOAT_DONG.Loai == loai) &&
                                    (!start.HasValue || h.HOAT_DONG.NgayBatDau >= start) &&
                                    (!end.HasValue || h.HOAT_DONG.NgayKetThuc <= end))
                            .Select(h => new { h.VaiTro, h.HOAT_DONG.TenHoatDong })
                            .ToList()
                    }).ToList();
                return rawData.Select((s, index) => new SVviewModel
                {
                    Stt = ((pageIndex - 1) * pageSize) + index + 1,
                    MSSV = s.MSSV,
                    HoTen = s.HoTen,
                    Khoa = s.KhoaName,
                    VaiTro = string.Join(Environment.NewLine + "- ", s.Activities.Select(a => a.VaiTro)),
                    ThamGia = string.Join(Environment.NewLine + "- ", s.Activities.Select(a => a.TenHoatDong))
                }).ToList();
            }
        }

        //export excel using EPPlus
        public async Task ExportExcelAsync(string filePath, string mssv, string maKhoa, string loai, DateTime? start, DateTime? end)
        {
            //Set License
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var db = new Context())
            {
                //stream everything
                var query = BuildQuery_sv(db, mssv, maKhoa, loai, start, end)
                    .Select(s => new
                    {
                        s.MSSV,
                        s.HoTen,
                        KhoaName = s.KHOA1.TenKhoa,
                        Activities = s.HD_SINHVIEN
                        .Where(h => h.HOAT_DONG.Hide == false &&
                                    (string.IsNullOrEmpty(loai) || h.HOAT_DONG.Loai == loai) &&
                                    (!start.HasValue || h.HOAT_DONG.NgayBatDau >= start) &&
                                    (!end.HasValue || h.HOAT_DONG.NgayKetThuc <= end))
                        .Select(h => new { h.VaiTro, h.HOAT_DONG.TenHoatDong })
                    });

                var dataList = await query.ToListAsync();

                using (var package = new ExcelPackage())
                {
                    var sheet = package.Workbook.Worksheets.Add("ThongKe");
                    //Header
                    sheet.Cells[1, 1].Value = "STT";
                    sheet.Cells[1, 2].Value = "MSSV";
                    sheet.Cells[1, 3].Value = "Họ Tên";
                    sheet.Cells[1, 4].Value = "Khoa";
                    sheet.Cells[1, 5].Value = "Vai Trò";
                    sheet.Cells[1, 6].Value = "Tham Gia";
                    sheet.Cells["A1:F1"].Style.Font.Bold = true;
                    sheet.Cells["A1:F1"].Style.Font.Color.SetColor(Color.White);
                    sheet.Cells["A1:F1"].Style.Fill.Gradient.Color1.SetColor(Color.SlateGray);
                    sheet.Cells["A1:F1"].Style.Fill.Gradient.Color2.SetColor(Color.LightGray);

                    int row = 2;
                    foreach (var item in dataList)
                    {
                        sheet.Cells[row, 1].Value = row - 1;
                        sheet.Cells[row, 2].Value = item.MSSV;
                        sheet.Cells[row, 3].Value = item.HoTen;
                        sheet.Cells[row, 4].Value = item.KhoaName;

                        // Join strings here
                        sheet.Cells[row, 5].Value = string.Join("\n- ", item.Activities.Select(x => x.VaiTro));
                        sheet.Cells[row, 6].Value = string.Join("\n- ", item.Activities.Select(x => x.TenHoatDong));

                        // Enable Wrap Text for the multi-line cells
                        sheet.Cells[row, 5].Style.WrapText = true;
                        sheet.Cells[row, 6].Style.WrapText = true;

                        row++;
                    }
                    sheet.Column(1).Width = 5;
                    sheet.Column(2).Width = 15;
                    sheet.Column(3).Width = 25;
                    sheet.Column(4).Width = 20;
                    sheet.Column(5).Width = 30;
                    sheet.Column(6).Width = 30;

                    await package.SaveAsAsync(new FileInfo(filePath));
                }
            }
        }
    }
}
