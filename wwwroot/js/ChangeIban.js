"use strict"

const apiUrls = {
    getIbans: "/ChangeIban/GetIbansOfMerchant",
    setSettleIban: "/ChangeIban/SetAccountAsDefaultIbanOfAllTerms",
    getTerminals: "/ChangeIban/GetMultiAcountTerminals",
    getDrpTerminals: "/ChangeIban/GetDrpTerminals"

}
var mainGrid;
$(function () {
    $('[data-toggle="tooltip"]').tooltip();
    select2AllDrops();
    chkAllChange();


    $("#btnSubmitIbans").click(function () {
        SetDefaultIban();
    });

    FillDrpParties();
    SelectedChangeParty();
    FillGridTerminal();

    function FillGridTerminal() {

        mainGrid = $("#mainGrid").DataTable({
            language: persianLanguage,
            "ordering": false,
            searching: false,
            ajax: {
                url: apiUrls.getTerminals,
                method: 'GET',
                dataSrc: 'data',
                data: {
                    merchantNumber: function () {
                        let merchantNumber = $("#ddMerchants").val();
                        return merchantNumber;
                    },
                    partyId: function () {
                        let partyId = $("#ddParties").val();
                        return partyId;
                    },
                    terminalNumber: function () {
                        let terminalNumber = $("#ddTerminals").val();
                        return terminalNumber;
                    }
                }
            },
            columns: [
                {
                    data: 'terminalNumber', width: 100, render: function (data, type, row, meta) {
                        let checkBox = '<input type="checkbox" class="chk-terminalNumbers" data-TerminalNumber="' + data + '" />';
                        return checkBox;
                    }
                },
                { data: 'terminalNumber', width: 100 },
                { data: 'terminalName', width: 300 },
                { data: 'merchantPhoneNumber', width: 300 },
                { data: 'iban', width: 300 },
                { data: 'accountNumber', width: 300 },
                { data: 'bankTitle', width: 300 },
                { data: 'merchantName', width: 300 }

            ]
        });
    }

    function FillDrpTerminals() {

        var ddTerminals = $("#ddTerminals");
        ddTerminals.empty().append('<option selected="selected" value="0">در حال دریافت اطلاعات</option>');
        $.ajax({
            type: "GET",
            url: "/ChangeIban/GetDrpTerminals",
            data: {
                merchantNumber: function () {
                    let merchantNumber = $("#ddMerchants").val();
                    return merchantNumber;
                },
                partyId: function () {
                    let partyId = $("#ddParties").val();
                    return partyId;
                }
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                ddTerminals.removeAttr('disabled');

                ddTerminals.empty().append('<option selected="selected" value="0">لطفاً ترمینال را انتخاب نمایید</option>');
                $.each(response, function () {
                    ddTerminals.append($("<option></option>").val(this['value']).html(this['text']));
                });
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });

    }

    function FillDrpParties() {

        var ddlParties = $("#ddParties");
        ddlParties.empty().append('<option selected="selected" value="0" disabled = "disabled">در حال دریافت اطلاعات</option>');
        $.ajax({
            type: "GET",
            url: "/ChangeIban/GetPartiesInfo",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                ddlParties.empty().append('<option selected="selected" value="0">لطفاً مشتری را انتخاب نمایید</option>');
                $.each(response, function () {
                    ddlParties.append($("<option></option>").val(this['value']).html(this['text']));
                });
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });

    }

    function FillDrpMerchants(partyId) {

        var ddlMerchants = $("#ddMerchants");
        ddlMerchants.empty().append('<option selected="selected" value="0" disabled = "disabled">در حال دریافت اطلاعات</option>');
        $.ajax({
            type: "GET",
            url: "/ChangeIban/GetMerchantsOfParty",
            data: { partyId: partyId },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                ddlMerchants.empty().append('<option selected="selected" value="0">لطفاً پذیرنده را انتخاب نمایید</option>');
                $.each(response, function () {
                    ddlMerchants.append($("<option></option>").val(this['value']).html(this['text']));
                });
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });

    }

    function FillDrpIbans(merchantNumber) {
        var ddIbans = $("#ddIbans");

        if (merchantNumber === undefined || merchantNumber === null) {
            ddIbans.attr("disabled", "disabled").empty();
            $('#btnSubmitIbans').attr("disabled", "disabled");
            return;
        }
        ddIbans.empty().append('<option selected="selected" value="0" disabled = "disabled">در حال دریافت اطلاعات</option>');
        $.ajax({
            type: "GET",
            url: apiUrls.getIbans,
            data: {
                merchantNumber: merchantNumber,
                partyId: function () {
                    let partyId = $("#ddParties").val();
                    return partyId;
                }
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                ddIbans.empty().append('<option selected="selected" value="0">لطفاً شبا را انتخاب نمایید</option>');
                ddIbans.removeAttr('disabled');
                $('#btnSubmitIbans').removeAttr('disabled');
                $.each(response, function () {
                    ddIbans.append($("<option></option>").val(this['value']).html(this['text']));
                });
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });

    }

    function SelectedChangeParty() {
        $("#ddParties").change(function () {
            var partyId = $("#ddParties").val();
            FillDrpMerchants(partyId);
            FillDrpTerminals();
            FillDrpIbans(0);
            mainGrid.ajax.reload();


        });
    }

    $("#ddTerminals").change(function () {
        mainGrid.ajax.reload();
    });

    $("#ddMerchants").change(function () {
        //var merchantNumber = $(this).val();
        //FillGridTerminals(merchantNumber);
        mainGrid.ajax.reload();
        document.getElementById("btnSubmitIbans").style.display = "block";
        document.getElementById("ddIbans").style.display = "block";



        FillDrpIbans(merchantNumber);
    });
    function select2AllDrops() {

        $('#ddParties').select2();
        $('#ddMerchants').select2();
        $('#ddIbans').select2();
        $('#ddTerminals').select2();



    }
    function chkAllChange() {
        $(".chk-all-terms").change(function () {
            $(":checkbox[class='chk-terminalNumbers']").prop("checked", this.checked);
        });
    }
    function SetDefaultIban() {

        var x = $(".chk-terminalNumbers:checked")
        let arr = [];
        for (let i = 0; i < x.length; i++) { arr.push($(x[i]).attr("data-TerminalNumber")); }
        $.ajax({
            type: "POST",
            url: apiUrls.setSettleIban,
            data: JSON.stringify(
                {
                    TerminalNumbers: arr,
                    NewDefaultIban: $("#ddIbans").val()
                }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                if (response.success = 'true')
                    mainGrid.ajax.reload();


                showInModal("نتیجه تغییر شبا", response.content);
            },
            failure: function (response) {
                showInModal("بروز خطای ناشناخته", "متاسفانه سیستم با خطا مواجه گردید");

            },
            error: function (response) {
                showInModal("بروز خطای ناشناخته", "متاسفانه سیستم با خطا مواجه گردید");

            }
        });
    }
});