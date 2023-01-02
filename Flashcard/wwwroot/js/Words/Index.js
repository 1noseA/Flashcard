$(function () {
    // デフォルトで表示する要素を指定
    $('.word, .meaning').show();
    $('.wordForm, .meaningForm').hide();
    $('.btnUpdate').hide();

    // 編集および中止リンクが押下
    $('.edit').click(function () {

        var index = $('.edit').index(this);

        // 要素の表示を切り替える
        $('.word').eq(index).toggle();
        $('.meaning').eq(index).toggle();
        $('.wordForm').eq(index).toggle();
        $('.meaningForm').eq(index).toggle();
        $('.btnUpdate').eq(index).toggle();
        if ($(this).html() === "編集") {
            $(this).html("×");
        } else {
            $(this).html("編集");
        }
    });
});