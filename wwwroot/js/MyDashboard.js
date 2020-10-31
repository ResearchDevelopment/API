"use strict"

const apiUrlList = {
    getParties: "/Home/GetPartytiesOfUser",
    getTerminals: "/Home/GetMultiAcountTerminals"

}
$(function () {
    $('[data-toggle="tooltip"]').tooltip();


    var partyGrid = $("#partyGrid").DataTable({
        language: persianLanguage,
        "ordering": false,
        searching: false,
        ajax: {
            url: apiUrlList.getParties,
            method: 'GET',
            dataSrc: 'data'
         
        },
        columns: [
           
            { data: 'partyIdentifier', width: 300 },
            { data: 'persianTitle', width: 300 },
            { data: 'tel', width: 300 },
            { data: 'mobile', width: 300 },
            { data: 'email', width: 300 }
           

        ]
    });

    var terminalGrid = $("#terminalGrid").DataTable({
        language: persianLanguage,
        "ordering": false,
        searching: false,
        ajax: {
            url: apiUrlList.getTerminals,
            method: 'GET',
            dataSrc: 'data'
        },
        columns: [
            
            { data: 'terminalNumber', width: 100 },
            { data: 'terminalName', width: 300 },
            { data: 'merchantPhoneNumber', width: 300 },
            { data: 'iban', width: 300 },
            { data: 'accountNumber', width: 300 },
            { data: 'bankTitle', width: 300 },
            { data: 'merchantName', width: 300 }

        ]
    });



});