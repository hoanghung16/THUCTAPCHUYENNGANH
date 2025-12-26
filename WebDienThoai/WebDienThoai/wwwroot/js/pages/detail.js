document.addEventListener('DOMContentLoaded', function () {
    /* ==========================================================================
       DỮ LIỆU GIẢ LẬP (Lưu ý: Nên thay bằng lấy từ Database thật qua Controller)
       ========================================================================== */
    const allProducts = {
        1: { id: 1, name: 'Iphone 13', price: 12890000, image: '/img/9.jpg', description: 'Hiệu năng mạnh mẽ với chip A15 Bionic...' },
        2: { id: 2, name: 'Iphone 14 Pro', price: 13790000, image: '/img/19.jpg', description: 'Trải nghiệm Dynamic Island độc đáo...' },
        // ... (các sản phẩm khác) ...
    };

    /* ==========================================================================
       PAGE LOGIC: TRANG CHI TIẾT SẢN PHẨM
       ========================================================================== */
    // Kiểm tra xem có đang ở trang detail không (dựa vào class body hoặc element cụ thể)
    const detailContainer = document.querySelector('.product-detail-container') || document.getElementById('product-image');

    if (detailContainer) {
        const urlParams = new URLSearchParams(window.location.search);
        const productId = parseInt(urlParams.get('id'));
        const product = allProducts[productId];

        if (product) {
            // Render dữ liệu lên giao diện
            const imgEl = document.getElementById('product-image');
            if (imgEl) {
                imgEl.src = product.image;
                imgEl.alt = product.name;
            }
            if (document.getElementById('product-name')) document.getElementById('product-name').textContent = product.name;
            if (document.getElementById('product-price')) document.getElementById('product-price').textContent = `${product.price.toLocaleString('vi-VN')}đ`;
            if (document.getElementById('product-description')) document.getElementById('product-description').textContent = product.description;

            // Gắn sự kiện cho nút "Thêm vào giỏ"
            const addBtn = document.getElementById('add-to-cart-btn');
            if (addBtn) {
                addBtn.addEventListener('click', () => {
                    alert('Chức năng giỏ hàng đang được bảo trì.');
                });
            }
        } else {
            // Xử lý khi không tìm thấy sản phẩm trong JS Data
            // (Thực tế nên để Controller xử lý việc trả về View Not Found)
        }
    }
});