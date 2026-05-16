document.addEventListener('DOMContentLoaded', () => {
    
    // 1. Dark Mode Functionality
    const themeToggleBtn = document.getElementById('themeToggle');
    const htmlElement = document.documentElement;

    themeToggleBtn.addEventListener('click', () => {
        const currentTheme = htmlElement.getAttribute('data-theme');
        if (currentTheme === 'light') {
            htmlElement.setAttribute('data-theme', 'dark');
            themeToggleBtn.textContent = '☀️'; 
        } else {
            htmlElement.setAttribute('data-theme', 'light');
            themeToggleBtn.textContent = '🌙'; 
        }
    });

    // 2. Toggle Contact Information
    const contactBtn = document.getElementById('contactRevealBtn');
    const contactInfo = document.getElementById('contactInfo');

    contactBtn.addEventListener('click', () => {
        if (contactInfo.classList.contains('hidden')) {
            contactInfo.classList.remove('hidden');
            contactBtn.textContent = 'Hide Contact Info';
        } else {
            contactInfo.classList.add('hidden');
            contactBtn.textContent = 'View Contact Info';
        }
    });

    // 3. Scroll Reveal Animations (Intersection Observer)
    const revealElements = document.querySelectorAll('.reveal');

    const revealObserver = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('active');
                observer.unobserve(entry.target); // Yalnızca bir kere çalışmasını sağlar
            }
        });
    }, {
        root: null,
        threshold: 0.15, // Elemanın %15'i göründüğünde animasyonu tetikle
        rootMargin: "0px 0px -50px 0px"
    });

    revealElements.forEach(el => {
        revealObserver.observe(el);
    });

});