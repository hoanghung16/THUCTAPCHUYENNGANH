document.addEventListener('DOMContentLoaded', function () {
    const productGrid = document.querySelector('.grid');
    if (!productGrid) return;

    // Hiệu ứng bay vào "an toàn"
    // (Web không bị trắng xóa nếu script này lỗi)
    function runEntryAnimation() {
        const products = productGrid.querySelectorAll('.product-card');

        if (products.length > 0) {
            anime({
                targets: products,
                // [Quan trọng] JS tự set opacity về 0 để bắt đầu hiệu ứng
                opacity: [0, 1],
                scale: [0.9, 1],
                translateY: [20, 0],
                easing: 'easeOutQuad',
                duration: 800,
                delay: anime.stagger(100)
            });
        }
    }

    runEntryAnimation();
});