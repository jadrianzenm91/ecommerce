/// <summary>
/// Script de Controladora de la Vista 
/// </summary>
/// <remarks>
/// </remarks>
try {
    ns('Ecommerce.Web.Admin.Controller');
    Ecommerce.Web.Admin.Controller = function () {
        var base = this;
        base.Ini = function () {
            'use strict';
            
            base.Control.btnArmar().on('click', base.Event.clickBtnArmar);
            base.Control.btnEntregaEnCamino().on('click', base.Event.clickBtnEntregaEnCamino);
            base.Control.btnEntregado().on('click', base.Event.clickBtnEntregado);
            base.Control.btnAnular().on('click', base.Event.clickBtnAnular);

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
            clickBtnAnular: function () {
                BootstrapDialog.confirm("Mensaje de Confirmación", "Está seguro de Anular la Orden de Compra?", function (result) {
                    if (result) {
                        base.Ajax.setAnular();
                    }
                });
            },
            clickBtnEntregado: function () {
                BootstrapDialog.confirm("Mensaje de Confirmación", "Está seguro de continuar?", function (result) {
                    if (result) {
                        base.Ajax.setEntregado();
                    }
                });
            },
            clickBtnEntregaEnCamino: function () {
                BootstrapDialog.confirm("Mensaje de Confirmación", "Está seguro de continuar?", function (result) {
                    if (result) {
                        base.Ajax.setEntregaEnCamino();
                    }
                });
            },
            clickBtnArmar: function () {
                BootstrapDialog.confirm("Mensaje de Confirmación", "Está seguro de continuar?", function (result) {
                    if (result) {
                        base.Ajax.setArmar();
                    }
                });
            }
        },
        base.Ajax = {
            setArmar: function () {
                $.ajax({
                    url: ns.admin.url.setArmarOrden,
                    data: { "idcompra": base.Control.hdnCodCompra().val() },
                    type: 'POST',
                    success: function (response) {
                        console.log(response);
                        if (response.success) {
                            alerta('success', response.message);
                            location.reload()
                            
                        } else {
                            alerta('danger', response.message);
                        }
                        
                    },
                    error: function (xhr) {
                        alerta('Danger', 'Error del sistema');
                    }
                });

            },
            setEntregaEnCamino: function () {
                $.ajax({
                    url: ns.admin.url.setEntregaEnCamino,
                    data: { "idcompra": base.Control.hdnCodCompra().val() },
                    type: 'POST',
                    success: function (response) {
                        console.log(response);
                        if (response.success) {
                            alerta('success', response.message);
                            location.reload()

                        } else {
                            alerta('danger', response.message);
                        }

                    },
                    error: function (xhr) {
                        alerta('Danger', 'Error del sistema');
                    }
                });

            },
            setEntregado: function () {
                $.ajax({
                    url: ns.admin.url.setEntregarOrden,
                    data: { "idcompra": base.Control.hdnCodCompra().val() },
                    type: 'POST',
                    success: function (response) {
                        console.log(response);
                        if (response.success) {
                            alerta('success', response.message);
                            location.reload()

                        } else {
                            alerta('danger', response.message);
                        }

                    },
                    error: function (xhr) {
                        alerta('Danger', 'Error del sistema');
                    }
                });

            },
            setAnular: function(){
                $.ajax({
                    url: ns.admin.url.setAnularOrden,
                    data: { "idcompra": base.Control.hdnCodCompra().val() },
                    type: 'POST',
                    success: function (response) {
                        console.log(response);
                        if (response.success) {
                            alerta('success', response.message);
                            location.reload()

                        } else {
                            alerta('danger', response.message);
                        }

                    },
                    error: function (xhr) {
                        alerta('Danger', 'Error del sistema');
                    }
                });
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


