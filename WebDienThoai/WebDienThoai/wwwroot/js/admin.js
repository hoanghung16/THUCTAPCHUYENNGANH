


document.addEventListener("DOMContentLoaded", function (event) {

    
    const menuToggle = document.getElementById("menu-toggle");

   
    const wrapper = document.getElementById("wrapper");

    
    if (menuToggle) {
        menuToggle.addEventListener("click", function (e) {
            e.preventDefault();

            
            wrapper.classList.toggle("toggled");
        });
    }

    
    if (window.innerWidth >= 768) {
        wrapper.classList.remove("toggled");
    } else {
        wrapper.classList.add("toggled");
    }
});