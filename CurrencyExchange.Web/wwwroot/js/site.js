// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $.get("https://localhost:45455/api/Currency/List", function (data) {
        var selectOptions = data.data.map(function (currency) {
            return `<option value="${currency.id}">${currency.name}</option>`;
        });
        $("#from-currency").html(selectOptions);
        $("#from-currency").val("1");
        $("#to-currency").html(selectOptions);
        fetchCurrencies();
        fetchConversions();
    });
});


$("#submitConversion").click(function (e) {
    $.ajax({
        url: 'https://localhost:45455/api/Conversion/Convert',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            fromCurrencyId: $("#from-currency").val(),
            toCurrencyId: $("#to-currency").val(),
            initialAmount: $("#initial-amount").val(),
            personalNumber: $("#personal-number").val(),
            firstName: $("#first-name").val(),
            lastName: $("#last-name").val(),
            recommendatorPersonalNumber: $("#recommendator-personal-number").val(),
        }),
        success: function (response) {
            fetchConversions()
        },
        error: function (error) {
            console.log(error);
        }
    });
});

$("#from-currency").change(function () {
    var selectedCurrencyId = $(this).val();
    if (selectedCurrencyId != "1") {
        $("#to-currency").val("1");
    }
});

$("#to-currency").change(function () {
    var selectedCurrencyId = $(this).val();
    if (selectedCurrencyId != "1") {
        $("#from-currency").val("1");
    }
});

$("#initial-amount").change(function () {
    var currentAmount = $(this).val();
    if (currentAmount > 3000) {
        $("#extra-person-info").show()
    }
    else {
        $("#extra-person-info").hide()
    }
})

function fetchCurrencies() {
    $.get('https://localhost:45455/api/Currency/List', function (response) {
        const table = $('#currenciesTable tbody');
        table.empty();
        response.data.forEach(currency => {
            table.append(`
                <tr>
                    <td>${currency.name}</td>
                    <td>${currency.code}</td>
                    <td>${currency.buyRate}</td>
                    <td>${currency.sellRate}</td>
                    <td>${currency.rateDate}</td>
                    <td><button class="btn btn-primary changeRateButton" data-currency-id="${currency.id}" data-buy-rate="${currency.buyRate}" data-sell-rate="${currency.sellRate}">კურსის განახლება</button></td>
                </tr>
            `);
        });
    });
}

function fetchConversions(startDate, endDate, personalNumber) {
    var total = 0;
    debugger
    $.get('https://localhost:45455/api/Conversion/Report', { startDate, endDate, personalNumber }, function (response) {
        const table = $('#conversionsTable tbody');
        table.empty();
        total = response.data.length;
        $('#total-conv-amount').val(total);
        response.data.forEach(conversion => {
            table.append(`
                <tr>
                    <td>${conversion.fromExchangeRate}</td>
                    <td>${conversion.toExchangeRate}</td>
                    <td>${conversion.exchangeRate}</td>
                    <td>${conversion.exchangeDate}</td>
                    <td>${conversion.initialAmount}</td>
                    <td>${conversion.convertedAmount}</td>
                    <td>${conversion.fullName}</td>
                    <td>${conversion.personalNumber}</td>
                </tr>
            `);
        });
    });
}

$('#filterButton').click(function () {
    const startDate = $('#startDate').val();
    const endDate = $('#endDate').val();
    const personalNumber = $('#personalNumber').val();

    fetchConversions(startDate, endDate, personalNumber);
});


$(document).on('click', '.changeRateButton', function () {
    const currencyId = $(this).data('currency-id');
    const buyRate = $(this).data('buy-rate');
    const sellRate = $(this).data('sell-rate');

    $('#buyRate').val(buyRate);
    $('#sellRate').val(sellRate);
    $('#currencyId').val(currencyId);

    $('#rateModal').modal('show');
});


$('#saveRateButton').click(function () {
    const currencyId = $('#currencyId').val();
    const buyRate = $('#buyRate').val();
    const sellRate = $('#sellRate').val();

    $.ajax({
        url: 'https://localhost:45455/api/Currency/Rate/Add',
        type: 'POST',
        data: JSON.stringify({ currencyId, buyRate, sellRate }),
        contentType: 'application/json; charset=utf-8',
        success: function () {
            fetchCurrencies();
            $('#rateModal').modal('hide');
        }
    });
});


$('#addCurrencyButton').click(function () {
    $('#addCurrencyModal').modal('show');
});

$('#saveCurrencyButton').click(function () {
    const code = $('#currencyCode').val();
    const nameKa = $('#currencyName').val();

    $.ajax({
        url: 'https://localhost:45455/api/Currency/Add',
        type: 'POST',
        data: JSON.stringify({ code, nameKa }),
        contentType: 'application/json; charset=utf-8',
        success: function () {
            fetchCurrencies();
            $('#addCurrencyModal').modal('hide');
        }
    });
});