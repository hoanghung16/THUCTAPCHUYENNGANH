document.addEventListener('DOMContentLoaded', function () {
    // 1. Khai báo các biến
    const sortFilter = document.querySelector('.dropdown-menu'); 
    const sortBtnText = document.querySelector('.dropdown-toggle'); 
    const productGrid = document.querySelector('.row.row-cols-2'); 

    if (!productGrid) return; 

    // Lấy tất cả thẻ sản phẩm và chuyển thành mảng để xử lý
    let products = Array.from(productGrid.children);

    // 2. Hàm lấy giá tiền từ HTML (Loại bỏ dấu chấm, chữ đ để tính toán)
    function getPrice(productItem) {
        // Tìm giá sale (ưu tiên) hoặc giá gốc
        const priceEl = productItem.querySelector('.text-danger.fw-bold.fs-5') ||
            productItem.querySelector('.fw-bold');

        if (!priceEl) return 0;

        // Chuyển "27.590.000đ" -> 27590000
        return parseInt(priceEl.innerText.replace(/\./g, '').replace('đ', ''));
    }

    // 3. Hàm lấy % giảm giá
    function getDiscount(productItem) {
        const badge = productItem.querySelector('.bg-danger.text-white');
        if (!badge) return 0;
        
        return parseInt(badge.innerText.replace('-', '').replace('%', ''));
    }

    // 4. Xử lý sự kiện click vào menu sắp xếp
    if (sortFilter) {
        sortFilter.querySelectorAll('.dropdown-item').forEach(item => {
            item.addEventListener('click', function (e) {
                e.preventDefault();
                const type = this.getAttribute('href'); // Lấy loại sắp xếp
                const text = this.innerText;

                // Cập nhật text cho nút bấm
                sortBtnText.innerText = text;

                // Logic sắp xếp
                if (type === '#price-asc') {
                    // Giá thấp đến cao
                    products.sort((a, b) => getPrice(a) - getPrice(b));
                }
                else if (type === '#price-desc') {
                    // Giá cao đến thấp
                    products.sort((a, b) => getPrice(b) - getPrice(a));
                }
                else if (type === '#discount-desc') {
                    // Giảm nhiều nhất
                    products.sort((a, b) => getDiscount(b) - getDiscount(a));
                }

                
                productGrid.innerHTML = '';
                products.forEach(p => productGrid.appendChild(p));
            });
        });
    }
});