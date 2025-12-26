document.addEventListener('DOMContentLoaded', function () {

    /* ==========================================================================
       1. LOGIC SLIDER (GIỮ NGUYÊN CODE CŨ)
       ========================================================================== */
    const sliderWrapper = document.querySelector('.slider-wrapper');
    if (sliderWrapper) {
        const dots = document.querySelectorAll('.dot');
        const slides = document.querySelectorAll('.slide');
        let currentSlide = 0;
        const slideCount = slides.length;

        // ... (Giữ nguyên phần code slider của bạn ở đây) ...

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
        let slideTimer = setInterval(nextSlide, 5000);
        dots.forEach(dot => {
            dot.addEventListener('click', () => {
                const slideIndex = parseInt(dot.dataset.slide);
                goToSlide(slideIndex);
                clearInterval(slideTimer);
                slideTimer = setInterval(nextSlide, 5000);
            });
        });
    }

    /* ==========================================================================
       2. LOGIC ANIME.JS (HIỆU ỨNG MỚI)
       ========================================================================== */

    // --- A. HERO BANNER: Chạy ngay khi tải trang ---
    // Tìm các phần tử cần animate trong slide đầu tiên
    var heroContent = document.querySelectorAll('.slide h1, .slide p, .slide .cta-button');

    if (heroContent.length > 0) {
        anime({
            targets: heroContent,
            translateY: [30, 0], // Trượt từ dưới lên
            opacity: [0, 1],     // Hiện dần
            easing: 'easeOutExpo',
            duration: 1500,
            delay: anime.stagger(200, { start: 300 }) // Cái sau hiện trễ hơn cái trước
        });
    }

    // --- HÀM HỖ TRỢ: Chỉ chạy hiệu ứng khi cuộn tới ---
    const observeAndAnimate = (selector, animationParams) => {
        const elements = document.querySelectorAll(selector);
        if (elements.length === 0) return;

        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    anime({
                        targets: entry.target,
                        ...animationParams
                    });
                    observer.unobserve(entry.target); // Chạy 1 lần rồi thôi
                }
            });
        }, { threshold: 0.15 });

        elements.forEach(el => observer.observe(el));
    };

    // --- B. DANH MỤC (Stagger) ---
    // Vì muốn hiệu ứng "nảy" lần lượt (stagger), ta cần quan sát cha (grid)
    const categoryGrid = document.querySelector('.grid');
    if (categoryGrid && document.querySelector('.category-card')) {
        const catObserver = new IntersectionObserver((entries) => {
            if (entries[0].isIntersecting) {
                anime({
                    targets: '.category-card',
                    scale: [0.8, 1], // Phóng to nhẹ
                    opacity: [0, 1],
                    easing: 'easeOutElastic(1, .6)',
                    duration: 1200,
                    delay: anime.stagger(150)
                });
                catObserver.disconnect();
            }
        }, { threshold: 0.2 });
        catObserver.observe(categoryGrid);
    }

    // --- C. SẢN PHẨM (Lượn sóng) ---
    // Tìm tất cả các lưới sản phẩm
    document.querySelectorAll('.grid').forEach(grid => {
        const products = grid.querySelectorAll('.product-card');
        if (products.length === 0) return;

        const prodObserver = new IntersectionObserver((entries) => {
            if (entries[0].isIntersecting) {
                anime({
                    targets: products,
                    translateY: [50, 0],
                    opacity: [0, 1],
                    easing: 'easeOutExpo',
                    duration: 1000,
                    delay: anime.stagger(100) // Hiệu ứng lượn sóng
                });
                prodObserver.disconnect();
            }
        }, { threshold: 0.1 });
        prodObserver.observe(grid);
    });

    // --- D. BANNER QUẢNG CÁO & NEWSLETTER ---
    observeAndAnimate('.split-banner', {
        translateX: [-50, 0],
        opacity: [0, 1],
        easing: 'easeOutExpo',
        duration: 1500
    });

    observeAndAnimate('.newsletter', {
        translateY: [30, 0],
        opacity: [0, 1],
        easing: 'easeOutQuad',
        duration: 1000
    });
});