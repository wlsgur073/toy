const moreBtn = document.querySelector('.infoAneNext .info .metadata .moreBtn');
const sub_title = document.querySelector('.infoAneNext .info .metadata .sub_title');

moreBtn.addEventListener('click', () => {
    moreBtn.classList.toggle('clicked');
    sub_title.classList.toggle('clamp');
});