/// <summary>
/// Script de Controladora de la Vista 
/// </summary>
/// <remarks>
/// </remarks>
try {
    ns('Ecommerce.Web.Pedidos.Controller');
    Ecommerce.Web.Pedidos.Controller = function () {
        var base = this;
        base.Ini = function () {
            'use strict';
            
            base.Control.linkDashboard().on('click', function () {
                base.Ajax.getViewBandejaDashboard($(this));
            });
        };

        base.Parametros = {
           controllerProducto : null
        };

        base.Control = {
            tblProductos: function () { return $('[name=tblProductos]'); },
        };

        base.Event = {
            
        };
        base.Ajax = {
            getViewBandejaDashboard: function (obj) {
                $.ajax({
                    url: ns.Pedidos.url.getViewBandejaDashboard,
                    data: { "id": $('#hdnTienda').val() },
                    type: 'POST',
                    success: function (data) {
                        base.Control.divPrincipal().html("");
                        base.Control.divPrincipal().html(data);
                        
                    },
                    error: function (xhr) {
                        alerta('Danger', 'Error del sistema');
                    }
                });

                base.Function.addActive(obj);
            }
        };
        base.Function = {
            addActive: function (obj) {
                $('li.active').removeClass('active');
                $(obj).addClass('active');
            }
        };
    };
} catch (ex) {
    alert(ex.message);
}


