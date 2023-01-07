$(function () {
    let judgement = document.getElementById('judgement').textContent;
    if (judgement === '〇') {
        $('.myAnswer').addClass('bg-green');
    } else if (judgement === '×') {
        $('.myAnswer').addClass('bg-red');
    }
});