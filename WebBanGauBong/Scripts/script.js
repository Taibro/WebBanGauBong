//Button back to top
let mybutton = document.getElementById("btn-back-to-top");


window.onscroll = function () {
    scrollFunction();
};

function scrollFunction() {
    if (
        document.body.scrollTop > 20 ||
        document.documentElement.scrollTop > 20
    ) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}

mybutton.addEventListener("click", backToTop);

function backToTop() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}

/////////////////////////////////////////
// Nút size đổi giá
$(document).ready(function () {

    // Lắng nghe sự kiện click trên bất kỳ nút nào có class .btnSizeCard
    $('.btnSizeCard').on('click', function () {

        // 'this' là nút vừa được click
        var $clickedButton = $(this);

        // --- 1. Xử lý Active Class ---

        // Tìm đến thẻ .card-body chứa nút này
        var $cardBody = $clickedButton.closest('.card-body');

        // Xóa class 'active' khỏi tất cả các nút kích thước CÙNG card-body
        $cardBody.find('.btnSizeCard').removeClass('active');

        // Thêm class 'active' CHỈ cho nút vừa được click
        $clickedButton.addClass('active');

        // --- 2. Xử lý Cập nhật Giá ---

        // Lấy giá từ thuộc tính data-price của nút
        var newPrice = $clickedButton.data('price');

        // Lấy ID của thẻ giá (ví dụ: #price-P123) từ data-target
        var targetPriceElement = $clickedButton.data('target');

        // Cập nhật nội dung text của thẻ giá đó
        $(targetPriceElement).text(newPrice + 'đ');
    });
});
/////////////////////////////////////////