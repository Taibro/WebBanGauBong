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
        var newPrice = $clickedButton.data('price');
        var rawTargetId = $clickedButton.data('target');

        // QUAN TRỌNG: Thêm lại ký tự # vào đầu chuỗi để tạo bộ chọn ID hợp lệ
        var targetPriceElementSelector = '#' + rawTargetId;

        // Cập nhật nội dung text của thẻ giá đó
        $(targetPriceElementSelector).text(newPrice + 'đ');
    });
});
/////////////////////////////////////////
// Popover
const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]');
const popoverList = [...popoverTriggerList].map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl));

$(document).ready(function () {
    $('[data-toggle="popover-user"]').popover({
        trigger: 'hover',
        html: true,
        content: function () {
            return '<img class="img-fluid" src="/Content/Images/desktop-quay-may-man.jpg" />';
        },
        title: 'Toolbox'
    })
});

//////////////////////////////////////////
// Lắng nghe thay đổi khi click checkbox của menu sidebar
$(document).ready(function () {
    $('.checkbox-round').on('change', function () {
        $('#filterForm').submit();
    });
});

$(document).ready(function () {
    $(".button-size.active").on("click", function () {
        $('#filterForm').submit();
    });
});
