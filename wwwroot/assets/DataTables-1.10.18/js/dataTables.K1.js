'use strict';

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

const editBtnTemplate =
    "<a class='btn btn-sm btn-default btn-showUpdateForm' data-model='~model~'><i class='glyphicon glyphicon-pencil'></i></a>";

const toggleStatusBtnTemplate =
    "<a class='btn btn-sm btn-default btn-toggleStatus' data-ID='~ID~'><i class='glyphicon glyphicon-off'></i></a>";

const statusTemplate = '<span class="glyphicon ~cssClass~"> ~caption~ </span>';

const changePasswordBtnTemplate =
    "<a class='btn btn-sm btn-default btn-showChangePasswordForm' data-model='~model~'><i class='text-danger glyphicon glyphicon-lock'></i></a>";

const userAccountProfileBtnTemplate =
    "<a class='btn btn-sm btn-default btn-showUserAccountProfileForm' data-model='~model~'><i class='text-success glyphicon glyphicon-user'></i></a>";

/**
 * @returns {string} element
 * @param {object} model json object bind to element with data-model attr
 */
function getUserAccountProfileBtnTemplate(model) {
    return userAccountProfileBtnTemplate.replace('~model~', JSON.stringify(model));
}

/**
 * @returns {string} element
 * @param {object} model json object bind to element with data-model attr
 */
function getEditButton(model) {
    return editBtnTemplate.replace('~model~', JSON.stringify(model));
}

/**
 * @returns {string} element
 * @param {object} model json object bind to element with data-model attr
 */
function getChangePasswordButton(model) {
    return changePasswordBtnTemplate.replace('~model~',JSON.stringify(model));
}

/**
 * @returns {string} element
 * @param {number} id id bind to element with data-ID attr
 */
function getToggleStatusButton(id) {
    return toggleStatusBtnTemplate.replace('~ID~',id);
}

function getActiveIcon() {
    return statusTemplate.replace('~cssClass~', 'text-success glyphicon-ok').replace('~caption~','فعال');
}

function getDeActiveIcon() {
    return statusTemplate.replace('~cssClass~', 'text-danger glyphicon-remove').replace('~caption~','غیرفعال');
}

$(function () {


    setTimeout(function () {

        $('.dataTables_filter').parent('div').remove();
        var dataTableFooterBar = $('.dataTables_info').parent('div').parent('div');
        $('.dataTables_length').parent('div').appendTo(dataTableFooterBar);
        dataTableFooterBar.children('.col-sm-6').removeClass('col-sm-6').addClass('col-sm-3');
        dataTableFooterBar.children('.col-sm-5').removeClass('col-sm-5').addClass('col-sm-3');
        dataTableFooterBar.children('.col-sm-7').removeClass('col-sm-7').addClass('col-sm-5');

    }, 100);
    
});

$(document).on('hidden.bs.modal', '#myXModal', function() {

    $(this).remove();

});

function hideModal() {
    $('#myModal').fadeOut(500);
    $('.modal-backdrop').fadeOut(500);
    setTimeout(function() {
        $('#myModal').remove();
        $('.modal-backdrop').remove();
    }, 1000);
}

/**
 * 
 * @param {string} title The title of modal
 * @param {string} body  The html content of modal
 */
function showInModal(title,body) {

    if (title === null) {
        title = '';
    }

    var modal = $('<div id="myXModal" class="modal modal-primary modal-fullScreen fade" role="dialog">' +
        '<div class="modal-dialog">' +
        '<div class="modal-content">' +
        '<div class="modal-header">' +
        '<button type="button" class="close pull-left" data-dismiss="modal">&times;</button>' +
        '<h4 class="modal-title">' + title + '</h4>'+
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

/**
 * 
 * @param {string} inputName The name of input element
 * @param {string} value The value of input element
 */
function setTextBoxValue(inputName,value){
	$('input[name="'+inputName+'"]').val(value);
}

/**
 * 
 * @param {string} inputName The name of input element
 * @param {string} value The value of input element
 */
function setInputValue(inputName, value) {

    if ($('input[name="' + inputName + '"]').length > 0) {

        $('input[name="' + inputName + '"]').val(value);

    }
    else if ($('select[name="' + inputName + '"]').length > 0) {

        let select = $('select[name="' + inputName + '"]');
        select.val(value);
        select.trigger('change');
    }
}

/**
 * 
 * @param {string} inputName The name of input element
 */
function setInputAsReadOnly(inputName){
    $('input[name="' + inputName + '"]').attr('readonly', 'readonly');
}

$(document).on('keyup','.dataTable-search', function () {

    let value = $(this).val();
    $(this).closest('table').DataTable().search(value).draw();

});

/**
 * 
 * @param {String} message message
 * @param {String} title title
 */
function MessageBox(message,title) {
    this.message = message;
    this.title = title;
}

/**
 * 
 * @param {String} messageType Success | Error
 */
MessageBox.prototype.show = function (messageType) {
    let boxElement = $("<div></div>");
    boxElement.addClass("alert alert-dismissible");
    boxElement.css("display", "none");

    if (messageType === "Success") {
        boxElement.addClass("alert-success");
    }
    else if (messageType === "Error") {
        boxElement.addClass("alert-danger");
    }

    let dismisElement = $("<a></a>");
    dismisElement
        .attr("href", "#")
        .attr("aria-label", "close")
        .data("dismiss", "alert")
        .html("&times;");

    dismisElement.addClass("close");
    dismisElement.on('click', function () {
        $(this).parent('.alert').fadeOut(750);
        setTimeout(function () {
            $(this).parent('.alert').remove();
        }, 100);
    });


    let titleElement = $("<strong></strong>");
    titleElement.text(this.title);

    let messageElement = $("<div></div>");
    messageElement.text(this.message);

    boxElement.append(dismisElement).append(titleElement).append(messageElement);
    
    boxElement.appendTo("body");
    boxElement.fadeIn();
};

const messages = {
    operationSuccessMessage: "عملیات با موفقیت انجام شد"
};

/**
 * 
 * @param {String} label label
 * @param {String} inputName label
 * @param {String} value label
 * @param {Number} colSize bootstrap col size 
 * @returns {JQuery} jquery element
 */
function createCheckMark(label,inputName,value,colSize) {
    let elemFormat = '<div class="col-sm-~colSize~">';

    elemFormat += '<label class="container">';
    elemFormat += '~label~';
    elemFormat += '<input type="checkbox" name="~name~" value="~value~" />';
    elemFormat += '<span class="checkmark"></span>';
    elemFormat += '</label>';
    elemFormat += '</div>';

    var elm = elemFormat.replace("~colSize~", String(colSize))
        .replace("~label~", label)
        .replace("~name~", inputName)
        .replace("~value~", value);

    return $(elm);
}


/**
 * 
 * @param {String} id elementID
 * @param {String} containerId elementID to wrap BsTabs
 */
function BsTabs(id, containerId) {
    this.id = id;
    this.containerID = containerId;
}

BsTabs.prototype.create = function () {

    let elem = $("<ul></ul>");
    elem.addClass("nav nav-tabs");
    elem.attr('id',this.id);

    let contentsContainer = $("<div></div>");
    contentsContainer.addClass("tab-content clearfix");

    elem.appendTo("#" + this.containerID);
    contentsContainer.appendTo("#" + this.containerID);
};

/**
* 
* @param {String} tabTitle tab title
 *@param {String} tabID tabID
 *@param {Boolean} isActive is active tab
*/
BsTabs.prototype.addTab = function (tabTitle, tabID, isActive) {
    
    let tabElem = $("<li></li>");
    if (isActive) {
        tabElem.addClass("active");
    }
    
    let tabLinkElem = $("<a></a>");
    tabLinkElem.attr("href", "#" + tabID);
    tabLinkElem.data("toggle", "tab");
    tabLinkElem.text(tabTitle);

    tabElem.append(tabLinkElem);

    let tabContent = $("<div></dvi>");
    tabContent.attr("id", tabID);
    tabContent.addClass("tab-pane");
    if (isActive) {
        tabContent.add("active");
        tabContent.show();
    }

    tabElem.on('click', function () {
        $('.tab-pane').hide();
        $(this).siblings('li').removeClass('active');
        $(this).addClass('active');
        let tabIdToShow = $(this).children("a").attr("href");
        
        $(tabIdToShow).show();
    });

    $("#" + this.containerID).children(".tab-content").append($(tabContent));

    tabElem.appendTo($("#" + this.id));

};

function mapToMyCustomeResponse(response) {
    let json = JSON.parse(response);
    json.recordsTotal = json.TotalRecords;
    json.data = json.Data;
    json.recordsFiltered = json.TotalFiltered;
    return JSON.stringify(json);
}
