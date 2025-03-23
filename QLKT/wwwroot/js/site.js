// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Xử lý sự kiện cho form tìm kiếm sinh viên
document.getElementById("masv").addEventListener("change", function () {
    const masv = this.value.trim();
    if (masv === "") return;

    // Hiển thị loading
    document.getElementById("loading").style.display = "block";

    fetch(`/CamDoan/GetStudentInfo?maSV=${encodeURIComponent(masv)}`)
        .then(response => {
            if (!response.ok) throw new Error("Không tìm thấy dữ liệu!");
            return response.json();
        })
        .then(data => {
            // Xử lý dữ liệu
        })
        .catch(error => {
            console.error("Lỗi:", error);
            alert("Không tìm thấy thông tin sinh viên!");
        })
        .finally(() => {
            // Ẩn loading
            document.getElementById("loading").style.display = "none";
        });
});