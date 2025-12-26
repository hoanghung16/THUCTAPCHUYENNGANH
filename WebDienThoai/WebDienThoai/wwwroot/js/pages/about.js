document.addEventListener('DOMContentLoaded', () => {

    // --- 1. HIỆU ỨNG HEADER (Xuất hiện từ trên xuống) ---
    anime({
        targets: '.header-anim',
        translateY: [-20, 0],
        opacity: [0, 1], // JS tự xử lý việc hiện dần
        easing: 'easeOutExpo',
        duration: 1500,
        delay: anime.stagger(200)
    });

    // --- 2. HIỆU ỨNG ẢNH (Zoom nhẹ) ---
    anime({
        targets: '.about-image img',
        scale: [0.9, 1],
        opacity: [0, 1],
        easing: 'easeOutElastic(1, .8)',
        duration: 2000,
        delay: 300
    });

    // --- 3. TIMELINE (Chạy khi cuộn chuột tới) ---
    var timelineObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                anime({
                    targets: '.timeline-item',
                    translateX: [-50, 0], // Trượt từ trái sang
                    opacity: [0, 1],      // [Quan trọng] Set opacity từ 0 lên 1
                    easing: 'easeOutExpo',
                    duration: 1200,
                    delay: anime.stagger(300)
                });
                timelineObserver.unobserve(entry.target);
            }
        });
    }, { threshold: 0.2 });

    var timeline = document.querySelector('.timeline');
    if (timeline) timelineObserver.observe(timeline);

    // --- 4. SỐ NHẢY (Counter) ---
    var statsObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                var counters = document.querySelectorAll('.stats-box strong');

                counters.forEach((el) => {
                    var targetValue = el.getAttribute('data-target');

                    // Logic hiển thị ký tự đặc biệt
                    var suffix = "";
                    if (targetValue == "10") suffix = "K+";
                    if (targetValue == "100") suffix = "%";

                    var obj = { value: 0 };

                    anime({
                        targets: obj,
                        value: targetValue,
                        round: 1,
                        easing: 'easeInOutQuad',
                        duration: 2000,
                        update: function () {
                            el.innerHTML = obj.value + suffix;
                        }
                    });
                });
                statsObserver.unobserve(entry.target);
            }
        });
    }, { threshold: 0.5 });

    var stats = document.querySelector('.stats-box');
    if (stats) statsObserver.observe(stats);
});