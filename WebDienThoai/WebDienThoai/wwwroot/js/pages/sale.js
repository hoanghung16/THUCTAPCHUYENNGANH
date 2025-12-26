document.addEventListener('DOMContentLoaded', function () {

    // 1. LOGIC ĐẾM NGƯỢC (COUNTDOWN)
    function startCountdown() {
        // Giả lập thời gian kết thúc: 5 tiếng nữa tính từ lúc mở web
        // Thực tế bạn có thể lấy mốc thời gian cố định, ví dụ: "2024-12-31T23:59:59"
        var now = new Date();
        var endTime = new Date(now.getTime() + 5 * 60 * 60 * 1000);

        function updateTimer() {
            var totalSeconds = Date.parse(endTime) - Date.parse(new Date());

            if (totalSeconds <= 0) {
                totalSeconds = 0;
            }

            var seconds = Math.floor((totalSeconds / 1000) % 60);
            var minutes = Math.floor((totalSeconds / 1000 / 60) % 60);
            var hours = Math.floor((totalSeconds / (1000 * 60 * 60)));

            document.getElementById('hours').innerText = hours < 10 ? '0' + hours : hours;
            document.getElementById('minutes').innerText = minutes < 10 ? '0' + minutes : minutes;
            document.getElementById('seconds').innerText = seconds < 10 ? '0' + seconds : seconds;
        }

        updateTimer();
        setInterval(updateTimer, 1000);
    }

    if (document.getElementById('countdown')) {
        startCountdown();
    }

    // 2. LOGIC SẮP XẾP SẢN PHẨM (CLIENT-SIDE)
    const sortSelect = document.getElementById('sale-sort-select');
    const grid = document.getElementById('sale-product-grid');

    if (sortSelect && grid) {
        sortSelect.addEventListener('change', function () {
            const sortType = this.value;
            // Chuyển danh sách thẻ con thành mảng
            let items = Array.from(grid.children);

            items.sort((a, b) => {
                const priceA = parseFloat(a.getAttribute('data-price'));
                const priceB = parseFloat(b.getAttribute('data-price'));
                const discountA = parseFloat(a.getAttribute('data-discount'));
                const discountB = parseFloat(b.getAttribute('data-discount'));

                if (sortType === 'price-asc') return priceA - priceB;
                if (sortType === 'price-desc') return priceB - priceA;
                if (sortType === 'discount-desc') return discountB - discountA;
                return 0; // Mặc định (giữ nguyên thứ tự render)
            });

            // Xóa cũ và thêm lại theo thứ tự mới
            grid.innerHTML = '';
            items.forEach(item => {
                // Reset animation để khi sort có hiệu ứng bay vào lại
                item.style.opacity = '0';
                grid.appendChild(item);
            });

            // Gọi Anime.js để hiện lại (nếu có thư viện)
            if (typeof anime !== 'undefined') {
                anime({
                    targets: grid.children,
                    scale: [0.95, 1],
                    opacity: [0, 1],
                    easing: 'easeOutQuad',
                    duration: 500,
                    delay: anime.stagger(50)
                });
            } else {
                // Fallback nếu không có anime.js
                items.forEach(i => i.style.opacity = '1');
            }
        });

        // Chạy hiệu ứng lúc mới vào trang
        if (typeof anime !== 'undefined') {
            anime({
                targets: grid.children,
                translateY: [20, 0],
                opacity: [0, 1],
                easing: 'easeOutQuad',
                duration: 800,
                delay: anime.stagger(100)
            });
        }
    }
});