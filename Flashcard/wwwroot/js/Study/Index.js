$(function () {
    let judgement = document.getElementById('judgement').textContent;
    if (judgement === '〇') {
        $('.myAnswer').addClass('bg-green');
    } else if (judgement === '×') {
        $('.myAnswer').addClass('bg-red');
    }
});

function studyBreak() {
    if (confirm('学習を終了します。よろしいですか')) {
        location.href = '/Study/Next?btnName=break'
    } else {
        return false;
    }
}