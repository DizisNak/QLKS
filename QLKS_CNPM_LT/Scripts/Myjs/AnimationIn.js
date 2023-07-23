const observer = new IntersectionObserver((entries) => {
    entries.forEach((entry) => {
    
        if (entry.isIntersecting) {
            entry.target.classList.add('myshow');
        }
        else {
            entry.target.classList.remove('myshow');
        }
    });
});

const hidden = document.querySelectorAll('.myhidden');
hidden.forEach((el) => observer.observe(el));
