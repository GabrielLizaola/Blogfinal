const navLinks = document.querySelectorAll('.nav-link');
const navBarBrand = document.querySelector('.navbar-brand');

navLinks.forEach(link => {
    link.addEventListener('click', (event) => {
        event.preventDefault();
        const href = link.getAttribute('href');
        window.location.href = href;
    });
});

navBarBrand.addEventListener('click', (event) => {
    event.preventDefault();
    window.location.href = '/';
});