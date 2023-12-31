﻿//preview img
function preview() {
    previewImg.src = URL.createObjectURL(event.target.files[0]);
}


//Get active link
const current = location.href;
const menu = document.querySelectorAll('.none');
const menulength = menu.length;
for (let i = 0; i < menulength; i++) {
    if (menu[i].href === current) {
        menu[i].className = '_active';
    }
}

//const navbar = document.getElementById("Header");
//const sticky = navbar.offsetTop;

//window.addEventListener("scroll", function () {
//    if(window.scrollY > sticky)
//    {
//        navbar.classList.add("sticky_nav");
//    }
//    else{
//        navbar.classList.remove("sticky_nav");
//    }
//})


function compareStrings(str1, str2) {
    return removeDiacritics(str1).toLowerCase().includes(removeDiacritics(str2).toLowerCase());
}

function removeDiacritics(text) {
    return text.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
}