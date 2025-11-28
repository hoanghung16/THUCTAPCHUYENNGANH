document.addEventListener('DOMContentLoaded', function () {
    const productGrid = document.getElementById('product-grid');
    if (!productGrid) return;

    // Lấy danh sách sản phẩm ban đầu
    let products = Array.from(productGrid.children);

    // Các phần tử điều khiển
    const sortSelect = document.getElementById('sort-select');
    const priceRadios = document.querySelectorAll('input[name="price-filter"]');
    const resultCount = document.getElementById('result-count');

    // Hàm lấy giá tiền từ DOM (để tính toán)
    function getPrice(element) {
        const priceEl = element.querySelector('.product-price-value');
        return parseInt(priceEl.getAttribute('data-value'));
    }

    // --- HÀM XỬ LÝ CHÍNH ---
    function filterAndSort() {
        let filtered = [...products];

        // 1. LỌC THEO GIÁ
        const selectedPrice = document.querySelector('input[name="price-filter"]:checked').value;

        filtered = filtered.filter(p => {
            const price = getPrice(p);
            if (selectedPrice === 'all') return true;
            if (selectedPrice === 'under-5') return price < 5000000;
            if (selectedPrice === '5-15') return price >= 5000000 && price <= 15000000;
            if (selectedPrice === '15-25') return price > 15000000 && price <= 25000000;
            if (selectedPrice === 'over-25') return price > 25000000;
            return true;
        });

        // 2. SẮP XẾP
        const sortValue = sortSelect.value;
        if (sortValue === 'price-asc') {
            filtered.sort((a, b) => getPrice(a) - getPrice(b));
        } else if (sortValue === 'price-desc') {
            filtered.sort((a, b) => getPrice(b) - getPrice(a));
        } else if (sortValue === 'name-asc') {
            filtered.sort((a, b) => a.dataset.name.localeCompare(b.dataset.name));
        }

        // 3. HIỂN THỊ LẠI
        productGrid.innerHTML = '';
        if (filtered.length > 0) {
            filtered.forEach(p => productGrid.appendChild(p));
            resultCount.textContent = `Hiển thị ${filtered.length} sản phẩm`;
        } else {
            productGrid.innerHTML = '<div class="col-12 text-center py-5 text-muted">Không tìm thấy sản phẩm phù hợp</div>';
            resultCount.textContent = '0 sản phẩm';
        }
    }

    // Gắn sự kiện
    sortSelect.addEventListener('change', filterAndSort);
    priceRadios.forEach(radio => radio.addEventListener('change', filterAndSort));
});