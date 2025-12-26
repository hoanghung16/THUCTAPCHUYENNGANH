document.addEventListener('DOMContentLoaded', function () {
    /* ==========================================================================
       1. GLOBAL LOGIC: ĐĂNG XUẤT (Giữ nguyên code cũ của bạn)
       ========================================================================== */
    const logoutLink = document.getElementById('logoutLink');
    if (logoutLink) {
        logoutLink.addEventListener('click', function (event) {
            event.preventDefault();
            const logoutUrl = this.getAttribute('data-url');

            if (typeof Swal !== 'undefined') {
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

    /* ==========================================================================
       2. GLOBAL ANIMATION: KHẮC PHỤC LỖI ẨN NỘI DUNG (.anim-item)
       Đây là phần quan trọng giúp các trang Chính sách hiện ra.
       ========================================================================== */
    if (typeof anime !== 'undefined') {
        // Tìm tất cả các phần tử có class .anim-item trên trang hiện tại
        const globalAnimItems = document.querySelectorAll('.anim-item');

        if (globalAnimItems.length > 0) {
            const observer = new IntersectionObserver((entries, observer) => {
                entries.forEach(entry => {
                    // Khi người dùng cuộn tới phần tử
                    if (entry.isIntersecting) {
                        // Kích hoạt hiệu ứng bay lên và hiện rõ (opacity: 1)
                        anime({
                            targets: entry.target,
                            translateY: [30, 0], // Trượt từ dưới lên 30px
                            opacity: [0, 1],     // [QUAN TRỌNG] Chuyển từ ẩn sang hiện
                            easing: 'easeOutQuad',
                            duration: 800,
                            delay: 100 // Trễ nhẹ một chút cho mượt
                        });
                        // Chỉ chạy 1 lần rồi thôi
                        observer.unobserve(entry.target);
                    }
                });
            }, { threshold: 0.1 }); // Kích hoạt khi thấy 10% phần tử

            // Bắt đầu theo dõi
            globalAnimItems.forEach(el => observer.observe(el));
        }
    }
});