document.addEventListener('DOMContentLoaded', function () {

    /* ==========================================================================
       1. DỮ LIỆU GIẢ LẬP (DATABASE)
       ========================================================================== */
    const allProducts = {
        1: { id: 1, name: 'Iphone 13', price: 12890000, image: 'img/9.jpg', description: 'Hiệu năng mạnh mẽ với chip A15 Bionic, màn hình Super Retina XDR sắc nét và hệ thống camera kép tiên tiến.' },
        2: { id: 2, name: 'Iphone 14 Pro', price: 13790000, image: 'img/19.jpg', description: 'Trải nghiệm Dynamic Island độc đáo, camera chính 48MP đột phá và hiệu năng vượt trội cho mọi tác vụ.' },
        3: { id: 3, name: 'Iphone 15', price: 15390000, image: 'img/10.jpg', description: 'Thiết kế bo tròn mềm mại, cổng sạc USB-C tiện lợi và hiệu năng được nâng cấp toàn diện.' },
        4: { id: 4, name: 'Samsung S25 Ultra', price: 12500000, image: 'img/20.jpg', description: 'Vua nhiếp ảnh di động với hệ thống camera zoom quang học ấn tượng, đi kèm bút S Pen đa năng.' },
        5: { id: 5, name: 'Airpods Pro 3', price: 6790000, image: 'img/11.jpg', description: 'Chất âm đỉnh cao, khả năng chống ồn chủ động thông minh và thiết kế vừa vặn hoàn hảo.' },
        6: { id: 6, name: 'AirPods Max USB C', price: 12990000, image: 'img/12.jpg', description: 'Trải nghiệm âm thanh không gian sống động như trong rạp hát với thiết kế sang trọng và cao cấp.' }
    };

    /* ==========================================================================
       2. GLOBAL LOGIC: CÁC CHỨC NĂNG CHẠY TRÊN MỌI TRANG
       ========================================================================== */

    // Chức năng Đăng Xuất
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
       3. PAGE LOGIC: TRANG CHỦ (SLIDER)
       ========================================================================== */
    const sliderWrapper = document.querySelector('.slider-wrapper');
    if (sliderWrapper) {
        const dots = document.querySelectorAll('.dot');
        const slides = document.querySelectorAll('.slide');
        let currentSlide = 0;
        const slideCount = slides.length;
        const slideInterval = 5000;

        function goToSlide(slideIndex) {
            sliderWrapper.style.transform = `translateX(-${slideIndex * (100 / slideCount)}%)`;
            dots.forEach(dot => dot.classList.remove('active'));
            dots[slideIndex].classList.add('active');
            currentSlide = slideIndex;
        }

        function nextSlide() {
            let nextIndex = (currentSlide + 1) % slideCount;
            goToSlide(nextIndex);
        }

        let slideTimer = setInterval(nextSlide, slideInterval);

        dots.forEach(dot => {
            dot.addEventListener('click', () => {
                const slideIndex = parseInt(dot.dataset.slide);
                goToSlide(slideIndex);
                clearInterval(slideTimer);
                slideTimer = setInterval(nextSlide, slideInterval);
            });
        });
    }

    /* ==========================================================================
       4. PAGE LOGIC: TRANG CHI TIẾT SẢN PHẨM
       ========================================================================== */
    if (document.body.classList.contains('product-detail-page')) {
        const urlParams = new URLSearchParams(window.location.search);
        const productId = parseInt(urlParams.get('id'));
        const product = allProducts[productId];

        if (product) {
            // Render dữ liệu lên giao diện
            document.getElementById('product-image').src = product.image;
            document.getElementById('product-image').alt = product.name;
            document.getElementById('product-name').textContent = product.name;
            document.getElementById('product-price').textContent = `${product.price.toLocaleString('vi-VN')}đ`;
            document.getElementById('product-description').textContent = product.description;

            // Gắn sự kiện cho nút "Thêm vào giỏ" (Thông báo bảo trì)
            const addBtn = document.getElementById('add-to-cart-btn');
            if (addBtn) {
                addBtn.addEventListener('click', () => {
                    alert('Chức năng giỏ hàng đang được bảo trì.');
                });
            }
        } else {
            const container = document.querySelector('.product-detail-container');
            if (container) container.innerHTML = '<h1>Sản phẩm không tồn tại hoặc đã bị xóa.</h1>';
        }
    }

});