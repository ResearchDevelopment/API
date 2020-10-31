const persianLanguage = {
    sEmptyTable: "هیچ داده ای در جدول وجود ندارد",
    sInfo: "نمایش _START_ تا _END_ از _TOTAL_ رکورد",
    sInfoEmpty: "نمایش 0 تا 0 از 0 رکورد",
    sInfoFiltered: "(فیلتر شده از _MAX_ رکورد)",
    sInfoPostFix: "",
    sInfoThousands: ",",
    sLengthMenu: "نمایش _MENU_ رکورد",
    sLoadingRecords: "در حال بارگزاری...",
    sProcessing: "در حال پردازش...",
    sSearch: "جستجو:",
    sZeroRecords: "رکوردی با این مشخصات پیدا نشد",
    oPaginate: {
        sFirst: "ابتدا",
        sLast: "انتها",
        sNext: "بعدی",
        sPrevious: "قبلی"
    },
    oAria: {
        sSortAscending: ": فعال سازی نمایش به صورت صعودی",
        sSortDescending: ": فعال سازی نمایش به صورت نزولی"
    }
};

/**
 * 
 * @param {string} title The title of modal
 * @param {string} body  The html content of modal
 */
function showInModal(title, body) {

    if (title === null) {
        title = '';
    }

    var modal = $('<div id="myXModal" class="modal modal-primary modal-fullScreen fade" role="dialog">' +
        '<div class="modal-dialog">' +
        '<div class="modal-content">' +
        '<div class="modal-header">' +
        '<button type="button" class="close pull-left" data-dismiss="modal">&times;</button>' +
        '<h4 class="modal-title">' + title + '</h4>' +
        '</div > ' +
        '<div class="modal-body"></div>' +
        '</div>' +
        '</div>' +
        '</div>');
    $('body').append(modal);
    $('#myXModal .modal-body').append(body);
    $('#myModal *').addClass('font-yekan');
    modal.modal('show');
}