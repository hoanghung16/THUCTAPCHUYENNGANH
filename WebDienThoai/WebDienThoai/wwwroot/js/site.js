document.addEventListener('DOMContentLoaded', function () {
    /* ==========================================================================
       GLOBAL LOGIC: CÁC CHỨC NĂNG CHẠY TRÊN MỌI TRANG
       ========================================================================== */

    // 1. Chức năng Đăng Xuất (Tách từ index.js sang)
    const logoutLink = document.getElementById('logoutLink');
    if (logoutLink) {
        logoutLink.addEventListener('click', function (event) {
            event.preventDefault();
            const logoutUrl = this.getAttribute('data-url');

            if (typeof Swal !== 'undefined') { // Kiểm tra nếu có thư viện SweetAlert
                Swal.fire({
                    title: 'Đăng xuất?',
                    text: "Bạn có chắc chắn muốn kết thúc phiên làm việc?",
                    icon: 'question',
                    showCancelButton: true,
                    confirmButtonColor: '#d33',
                    cancelButtonColor: '#3085d6',
                    confirmButtonText: 'Vâng, đăng xuất!',
                    cancelButtonText: 'Ở lại',
                    reverseButtons: true
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = logoutUrl;
                    }
                });
            } else {
                if (confirm("Bạn có chắc chắn muốn đăng xuất?")) {
                    window.location.href = logoutUrl;
                }
            }
        });
    }
});