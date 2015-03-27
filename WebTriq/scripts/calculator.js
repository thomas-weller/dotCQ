var surplusColor = 'darkgreen';
var baseColor = '#4572A7';

function drawChart() {
    $.ajax({
        type: "GET",
        url: "calc",
        crossDomain: true,
        data: calculationDataToJson(),
        dataType: "jsonp",
        jsonpCallback: "drawChartCallback"
    });
}


function drawChartCallback(response) {
    try {
        var responseData = jQuery.parseJSON(response);
        if (responseData.error == 'false') {
            $('#alert-error').hide();
            refreshData(responseData);
        } else {
            $('#alert-error').show();
        }
    } catch (e) {
        $('#alert-error').show();
    }
}

function refreshData(data) {
    refreshChart1(data);
    refreshChart2(data);
    setTextAndTableData(data);
}

function calculationDataToJson() {

    var staffCosts = $('#inputStaffCosts').val();
    var currency = $('#selectCurrency').val();
    var size = $('#inputSize').val();
    var time = $('#inputTime').val();
    var quality = $('#selectQuality').val();
    var surplus = $('#inputSurplus').val();

    return '{' +
                'sc:' + staffCosts +  ',c:"' + currency +
                '",s:' + size + ',t:' + time +
                ',q:' + quality + ',sp:' + surplus + 
           '}';
}

function setTextAndTableData(data) {
    $('#fp').text(format("#,##0.#", data.fp));
    $('#sloc').text(format("#,##0.#", data.sloc));

    $('#surplus').text(format("#,##0.#", data.surplus));
    $('#currency').text(format("#,##0.#", data.currency));
    $('#breakEvenPoint').text(Math.round(data.breakEvenPoint * 10) / 10);
    $('#totalSaving').text(format("#,##0.#", data.totalDiff * -1));
    $('#savingCurrency').text(format("#,##0.#", data.currency));
    $('#lifetime').text(format("#,##0.#", data.lifetime));
    $('#roi').text(data.roi);
    $('#avgSaving').text(format("#,##0.#", data.avgSaving));
    $('#minSaving').text(format("#,##0.#", data.minSaving));
    $('#maxSaving').text(format("#,##0.#", data.maxSaving));
    $('#savingCurrency2').text(format("#,##0.#", data.currency));
    $('#savingCurrency3').text(format("#,##0.#", data.currency));

    $('#devOrg').text(format("#,##0.#", data.devOrg));
    $('#maintOrg').text(format("#,##0.#", data.maintOrg));
    $('#totalOrg').text(format("#,##0.#", data.totalOrg));
    $('#maintPercentOrg').text(format("#,##0.#", data.maintPercentOrg));
    $('#devMod').text(format("#,##0.#", data.devMod));
    $('#maintMod').text(format("#,##0.#", data.maintMod));
    $('#totalMod').text(format("#,##0.#", data.totalMod));
    $('#maintPercentMod').text(format("#,##0.#", data.maintPercentMod));
    $('#devDiff').text(format("#,##0.#", data.surplus * -1));
    $('#maintDiff').text(format("#,##0.#", data.maintDiff * -1));
    $('#totalDiff').text(format("#,##0.#", data.totalDiff * -1));
}

function refreshChart1(data) {
    var chart = Highcharts.charts[0];
    chart.xAxis[0].setCategories(data.effortX, false);

    chart.yAxis[0].update({
        title: {
            text: data.effortUnit
        }
    }, false);

    chart.series[0].setData(data.effortCurrent, false);
    chart.series[1].setData(data.effortModified, false);

    chart.redraw();
}

function refreshChart2(data) {
    var chart = Highcharts.charts[1];

    chart.xAxis[0].setCategories(data.costsX, false);
    
    chart.xAxis[0].update({
        plotLines: [{
            color: 'grey',
            width: 1,
            value: data.breakEvenPoint,
            label: {
                text: Math.round(data.breakEvenPoint * 10) / 10,
                verticalAlign: 'bottom',
                textAlign: 'right',
                y: -10
            }
        }]
    }, false);
    
    chart.yAxis[0].update({
            plotLines: [{
                color: 'grey',
                width: 1,
                value: data.breakEvenCosts,
                label: {
                    text: format("#,##0.#", data.breakEvenCosts),
                    align: 'left',
                    x: 10,
                    y: -6
                }            
            }]
    }, false);

    chart.tooltip.valueSuffix = ' ' + data.currency;
    
    chart.series[0].setData(data.costsCurrent, false);
    chart.series[1].setData(data.costsModified, false);

    chart.redraw();
}


function createChart1() {
    return new Highcharts.Chart({
        colors: [
            baseColor,
            surplusColor
        ],
        chart: {
            renderTo: 'chart_container',
            type: 'line',
        },
        credits: {
            enabled: false
        },
        title: {
            text: 'Yearly Maintenance Effort'
        },
        xAxis: {
            title: {
                text: 'Year'
            },
            categories: []
        },
        yAxis: {
            title: {
                text: 'Staff-years'
            },
        },
        tooltip: {
            crosshairs: true,
            shared: true,
            useHTML: true,
            headerFormat: '<strong>year: {point.key}</strong><table>',
            pointFormat: '<tr><td style="color: {series.color}">{series.name}: </td>' +
            '<td style="text-align: right"><b>{point.y}</b></td></tr>',
            footerFormat: '</table>',
            valueDecimals: 1
        },
        series: [{
            name: 'Baseline',
            data: []
        },
        {
            name: 'Surplus',
            data: []
        }]
    });
}

function createChart2() {
    return new Highcharts.Chart({
        colors: [
            baseColor,
            surplusColor
        ],
        chart: {
            renderTo: 'chart2_container',
            type: 'line',
        },
        credits: {
            enabled: false
        },
        title: {
            text: 'Accumulated Project Costs',
        },
        xAxis: {
            title: {
                text: 'Year'
            },
            categories: []
        },
        yAxis: {
            title: {
                text: 'Costs'
            }
        },
        tooltip: {
            crosshairs: true,
            shared: true,
            useHTML: true,
            headerFormat: '<strong>year: {point.key}</strong><table>',
            pointFormat: '<tr><td style="color: {series.color}">{series.name}: </td>' +
            '<td style="text-align: right"><b>{point.y}</b></td></tr>',
            footerFormat: '</table>',
        },
        series: [{
            name: 'Baseline',
            data: []
        },
        {
            name: 'Surplus',
            data: []
        }]
    });
}
