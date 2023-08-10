/// <summary>
/// Script de Controladora de la Vista 
/// </summary>
/// <remarks>
/// </remarks>
try {
    ns('Ecommerce.Web.Dashboard.Controller');
    Ecommerce.Web.Dashboard.Controller = function () {
        var base = this;
        base.Ini = function () {
            'use strict';
            
            base.Function.setGraficoTransacciones();
            base.Function.setGraficoFormaPago();
            base.Function.setGraficoVentasDiarias();
        };

        base.Parametros = {
            controllerProducto : null
        };

        base.Control = {
            tblProductos: function () { return $('[name=tblProductos]'); },
            divPrincipal: function () { return $('.container-fluid'); },
            btnArmar: function () { return $('[name=btnArmar]'); },
            btnEntregaEnCamino: function () { return $('[name=btnEntregaEnCamino]'); },
            btnEntregado: function () { return $('[name=btnEntregado]'); },
            btnAnular: function () { return $('[name=btnAnular]'); },
            hdnCodCompra: function () { return $('#hdnCodCompra'); },
        };

        base.Event = {
            
           
        },
        base.Ajax = {
           
        };
        base.Function = {
            setGraficoFormaPago: function () {
                var data = ns.dashboard.data.listRptFormaPago;
                var arrayData = new Array();

                $.each(data, function (i, item) {
                    if (i == 0) {
                        arrayData.push({
                            name: item.formapago, y: item.cant, sliced: true,selected: true
                        });
                    } else {
                        arrayData.push({ name: item.formapago, y: item.cant });
                    }
                    
                });

                Highcharts.chart('container3', {
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        type: 'pie'
                    },
                    title: {
                        text: 'Reporte de uso de Forma de Pago'
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    },
                    accessibility: {
                        point: {
                            valueSuffix: '%'
                        }
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: false
                            },
                            showInLegend: true
                        }
                    },
                    series: [{
                        name: 'Forma de Pago',
                        colorByPoint: true,
                        data: arrayData
                    }]
                });
            },
            setGraficoTransacciones: function () {
                var data = ns.dashboard.data.listRptVentas;
                var arrayCategories = new Array();
                var arrayData = new Array();

                $.each(data, function (i, item) {
                    arrayCategories.push(item.mes);
                    arrayData.push(item.total);
                });

                Highcharts.chart('container2', {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: 'Ventas Totales por Mes'
                    },
                    subtitle: {
                        text: 'Fuente: Mi Tienda Virtual'
                    },
                    xAxis: {
                        categories: arrayCategories,
                        crosshair: true
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: null
                        }
                    },
                    tooltip: {
                        headerFormat: '<span style="font-size:10px">MES: {point.key}</span><table>',
                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                            '<td style="padding:0"><b>S/.{point.y:.2f}</b></td></tr>',
                        footerFormat: '</table>',
                        shared: true,
                        useHTML: true
                    },
                    plotOptions: {
                        column: {
                            pointPadding: 0.2,
                            borderWidth: 0,
                            showInLegend: false,
                            dataLabels: {
                                enabled: true
                            },
                        }

                    },
                    series:
                    [{
                        name: 'Venta',
                        data: arrayData,//[2, 0, 0, 5, 1, 5],
                        //color: '#dc3545'

                    }]
                });
            },
            setGraficoVentasDiarias: function () {
                var data = ns.dashboard.data.listRptVentasDiarias;
                
                var arrayData = new Array();

                $.each(data, function (i, item) {
                    arrayData.push([Date.UTC(item.fecha.split('/')[2], (item.fecha.split('/')[1]-1), item.fecha.split('/')[0]), item.total]);
                });

                Highcharts.chart('container4', {
                    chart: {
                        type: 'spline'
                    },
                    title: {
                        text: 'Transacciones de Ventas Diarias'
                    },
                    subtitle: {
                        text: 'Mi Tienda Virtual'
                    },
                    xAxis: {
                        type: 'datetime',
                        dateTimeLabelFormats: { // don't display the dummy year
                            month: '%e. %b',
                            year: '%b'
                        },
                        title: {
                            text: 'Date'
                        }
                    },
                    yAxis: {
                        title: {
                            text: 'S/'
                        },
                        min: 0
                    },
                    tooltip: {
                        headerFormat: '<b>{series.name}</b><br>',
                        pointFormat: '{point.x:%e. %b}: S/ {point.y:.2f}'
                    },

                    plotOptions: {
                        series: {
                            marker: {
                                enabled: true
                            }
                        }
                    },

                    colors: ['#39F'],

                    // Define the data points. All series have a dummy year
                    // of 1970/71 in order to be compared on the same x axis. Note
                    // that in JavaScript, months start at 0 for January, 1 for February etc.
                    series: [{
                        name: "Transacción",
                        data: arrayData
                    }],

                    responsive: {
                        rules: [{
                            condition: {
                                maxWidth: 500
                            },
                            chartOptions: {
                                plotOptions: {
                                    series: {
                                        marker: {
                                            radius: 2.5
                                        }
                                    }
                                }
                            }
                        }]
                    }
                });
            }
           
        };
    };
} catch (ex) {
    alert(ex.message);
}


