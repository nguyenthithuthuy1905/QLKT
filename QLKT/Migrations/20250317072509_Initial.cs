using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLKT.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Khoas",
                columns: table => new
                {
                    MaKhoa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhoa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoas", x => x.MaKhoa);
                });

            migrationBuilder.CreateTable(
                name: "LoaiSuCos",
                columns: table => new
                {
                    MaLoaiSuCo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiSuCo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiSuCos", x => x.MaLoaiSuCo);
                });

            migrationBuilder.CreateTable(
                name: "LoaiViPhams",
                columns: table => new
                {
                    MaViPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiViPhamThi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiViPhams", x => x.MaViPham);
                });

            migrationBuilder.CreateTable(
                name: "MonHocs",
                columns: table => new
                {
                    MaMonHoc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMonHoc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHocs", x => x.MaMonHoc);
                });

            migrationBuilder.CreateTable(
                name: "NoiDungCamDoans",
                columns: table => new
                {
                    MaNoiDung = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoiDungCamDoans", x => x.MaNoiDung);
                });

            migrationBuilder.CreateTable(
                name: "PhongThis",
                columns: table => new
                {
                    MaPH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViTri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuongThiSinh = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongThis", x => x.MaPH);
                });

            migrationBuilder.CreateTable(
                name: "GiangViens",
                columns: table => new
                {
                    MaGV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaKhoa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangViens", x => x.MaGV);
                    table.ForeignKey(
                        name: "FK_GiangViens_Khoas_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "Khoas",
                        principalColumn: "MaKhoa");
                });

            migrationBuilder.CreateTable(
                name: "SinhViens",
                columns: table => new
                {
                    MASV = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HoLot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenSV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaLop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NienKhoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiSinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoCMND = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoTenCha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoTenMe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DienThoaiBaoTinGap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaKhoa = table.Column<int>(type: "int", nullable: true),
                    ChuyenNganh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhViens", x => x.MASV);
                    table.ForeignKey(
                        name: "FK_SinhViens_Khoas_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "Khoas",
                        principalColumn: "MaKhoa");
                });

            migrationBuilder.CreateTable(
                name: "KyThis",
                columns: table => new
                {
                    MaKyThi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKyThi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayThi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioThi = table.Column<TimeSpan>(type: "time", nullable: false),
                    ThoiGianLamBai = table.Column<int>(type: "int", nullable: false),
                    MaMonHoc = table.Column<int>(type: "int", nullable: false),
                    MaPH = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KyThis", x => x.MaKyThi);
                    table.ForeignKey(
                        name: "FK_KyThis_MonHocs_MaMonHoc",
                        column: x => x.MaMonHoc,
                        principalTable: "MonHocs",
                        principalColumn: "MaMonHoc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KyThis_PhongThis_MaPH",
                        column: x => x.MaPH,
                        principalTable: "PhongThis",
                        principalColumn: "MaPH");
                });

            migrationBuilder.CreateTable(
                name: "CamDoans",
                columns: table => new
                {
                    MaCamDoan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSV = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NgayThi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioThi = table.Column<TimeSpan>(type: "time", nullable: false),
                    MaPH = table.Column<int>(type: "int", nullable: true),
                    MaLop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaNoiDung = table.Column<int>(type: "int", nullable: false),
                    NoiDungCamDoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDungCamDoanMaNoiDung = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CamDoans", x => x.MaCamDoan);
                    table.ForeignKey(
                        name: "FK_CamDoans_NoiDungCamDoans_NoiDungCamDoanMaNoiDung",
                        column: x => x.NoiDungCamDoanMaNoiDung,
                        principalTable: "NoiDungCamDoans",
                        principalColumn: "MaNoiDung");
                    table.ForeignKey(
                        name: "FK_CamDoans_PhongThis_MaPH",
                        column: x => x.MaPH,
                        principalTable: "PhongThis",
                        principalColumn: "MaPH");
                    table.ForeignKey(
                        name: "FK_CamDoans_SinhViens_MaSV",
                        column: x => x.MaSV,
                        principalTable: "SinhViens",
                        principalColumn: "MASV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichThiSinhViens",
                columns: table => new
                {
                    MaLichThi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSV = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaMonHoc = table.Column<int>(type: "int", nullable: false),
                    NgayThi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioThi = table.Column<TimeSpan>(type: "time", nullable: false),
                    MaPH = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichThiSinhViens", x => x.MaLichThi);
                    table.ForeignKey(
                        name: "FK_LichThiSinhViens_MonHocs_MaMonHoc",
                        column: x => x.MaMonHoc,
                        principalTable: "MonHocs",
                        principalColumn: "MaMonHoc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LichThiSinhViens_PhongThis_MaPH",
                        column: x => x.MaPH,
                        principalTable: "PhongThis",
                        principalColumn: "MaPH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LichThiSinhViens_SinhViens_MaSV",
                        column: x => x.MaSV,
                        principalTable: "SinhViens",
                        principalColumn: "MASV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViPhamQuyChes",
                columns: table => new
                {
                    MaViPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSV = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NgayThi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioThi = table.Column<TimeSpan>(type: "time", nullable: false),
                    MaPH = table.Column<int>(type: "int", nullable: true),
                    MaLop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaLoaiViPham = table.Column<int>(type: "int", nullable: false),
                    NoiDungViPham = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViPhamQuyChes", x => x.MaViPham);
                    table.ForeignKey(
                        name: "FK_ViPhamQuyChes_LoaiViPhams_MaLoaiViPham",
                        column: x => x.MaLoaiViPham,
                        principalTable: "LoaiViPhams",
                        principalColumn: "MaViPham",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ViPhamQuyChes_PhongThis_MaPH",
                        column: x => x.MaPH,
                        principalTable: "PhongThis",
                        principalColumn: "MaPH");
                    table.ForeignKey(
                        name: "FK_ViPhamQuyChes_SinhViens_MaSV",
                        column: x => x.MaSV,
                        principalTable: "SinhViens",
                        principalColumn: "MASV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhanCongCoiThis",
                columns: table => new
                {
                    MaPhanCong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGV = table.Column<int>(type: "int", nullable: false),
                    MaKyThi = table.Column<int>(type: "int", nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanCongCoiThis", x => x.MaPhanCong);
                    table.ForeignKey(
                        name: "FK_PhanCongCoiThis_GiangViens_MaGV",
                        column: x => x.MaGV,
                        principalTable: "GiangViens",
                        principalColumn: "MaGV",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhanCongCoiThis_KyThis_MaKyThi",
                        column: x => x.MaKyThi,
                        principalTable: "KyThis",
                        principalColumn: "MaKyThi",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuCoBatThuongs",
                columns: table => new
                {
                    MaSuCo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKyThi = table.Column<int>(type: "int", nullable: false),
                    MaLoaiSuCo = table.Column<int>(type: "int", nullable: false),
                    NgayThi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioThi = table.Column<TimeSpan>(type: "time", nullable: false),
                    MaPH = table.Column<int>(type: "int", nullable: true),
                    NoiDungSuCo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HuongXuLy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiLapBB1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiLapBB2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiLapBB3 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuCoBatThuongs", x => x.MaSuCo);
                    table.ForeignKey(
                        name: "FK_SuCoBatThuongs_KyThis_MaKyThi",
                        column: x => x.MaKyThi,
                        principalTable: "KyThis",
                        principalColumn: "MaKyThi",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuCoBatThuongs_LoaiSuCos_MaLoaiSuCo",
                        column: x => x.MaLoaiSuCo,
                        principalTable: "LoaiSuCos",
                        principalColumn: "MaLoaiSuCo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuCoBatThuongs_PhongThis_MaPH",
                        column: x => x.MaPH,
                        principalTable: "PhongThis",
                        principalColumn: "MaPH");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CamDoans_MaPH",
                table: "CamDoans",
                column: "MaPH");

            migrationBuilder.CreateIndex(
                name: "IX_CamDoans_MaSV",
                table: "CamDoans",
                column: "MaSV");

            migrationBuilder.CreateIndex(
                name: "IX_CamDoans_NoiDungCamDoanMaNoiDung",
                table: "CamDoans",
                column: "NoiDungCamDoanMaNoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_GiangViens_MaKhoa",
                table: "GiangViens",
                column: "MaKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_KyThis_MaMonHoc",
                table: "KyThis",
                column: "MaMonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_KyThis_MaPH",
                table: "KyThis",
                column: "MaPH");

            migrationBuilder.CreateIndex(
                name: "IX_LichThiSinhViens_MaMonHoc",
                table: "LichThiSinhViens",
                column: "MaMonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_LichThiSinhViens_MaPH",
                table: "LichThiSinhViens",
                column: "MaPH");

            migrationBuilder.CreateIndex(
                name: "IX_LichThiSinhViens_MaSV",
                table: "LichThiSinhViens",
                column: "MaSV");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongCoiThis_MaGV",
                table: "PhanCongCoiThis",
                column: "MaGV");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongCoiThis_MaKyThi",
                table: "PhanCongCoiThis",
                column: "MaKyThi");

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_MaKhoa",
                table: "SinhViens",
                column: "MaKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_SuCoBatThuongs_MaKyThi",
                table: "SuCoBatThuongs",
                column: "MaKyThi");

            migrationBuilder.CreateIndex(
                name: "IX_SuCoBatThuongs_MaLoaiSuCo",
                table: "SuCoBatThuongs",
                column: "MaLoaiSuCo");

            migrationBuilder.CreateIndex(
                name: "IX_SuCoBatThuongs_MaPH",
                table: "SuCoBatThuongs",
                column: "MaPH");

            migrationBuilder.CreateIndex(
                name: "IX_ViPhamQuyChes_MaLoaiViPham",
                table: "ViPhamQuyChes",
                column: "MaLoaiViPham");

            migrationBuilder.CreateIndex(
                name: "IX_ViPhamQuyChes_MaPH",
                table: "ViPhamQuyChes",
                column: "MaPH");

            migrationBuilder.CreateIndex(
                name: "IX_ViPhamQuyChes_MaSV",
                table: "ViPhamQuyChes",
                column: "MaSV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CamDoans");

            migrationBuilder.DropTable(
                name: "LichThiSinhViens");

            migrationBuilder.DropTable(
                name: "PhanCongCoiThis");

            migrationBuilder.DropTable(
                name: "SuCoBatThuongs");

            migrationBuilder.DropTable(
                name: "ViPhamQuyChes");

            migrationBuilder.DropTable(
                name: "NoiDungCamDoans");

            migrationBuilder.DropTable(
                name: "GiangViens");

            migrationBuilder.DropTable(
                name: "KyThis");

            migrationBuilder.DropTable(
                name: "LoaiSuCos");

            migrationBuilder.DropTable(
                name: "LoaiViPhams");

            migrationBuilder.DropTable(
                name: "SinhViens");

            migrationBuilder.DropTable(
                name: "MonHocs");

            migrationBuilder.DropTable(
                name: "PhongThis");

            migrationBuilder.DropTable(
                name: "Khoas");
        }
    }
}
