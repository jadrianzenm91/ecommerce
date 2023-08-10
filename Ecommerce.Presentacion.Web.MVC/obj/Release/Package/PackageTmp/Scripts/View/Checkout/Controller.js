/// <summary>
/// Script de Controladora de la Vista 
/// </summary>
/// <remarks>
/// </remarks>
try {
    ns('Ecommerce.Web.Checkout.Controller');
    Ecommerce.Web.Checkout.Controller = function () {
        var base = this;
        base.Ini = function () {
            'use strict';
            base.Function.getGPS();
            base.Control.linkCurrentLocation().on('click', base.Function.getGPS);
            base.Control.btnPagar().on('click', function () {
                base.Event.clickPagar($(this));
            });
            base.Control.cboformapago().on('click', function () {
                if ($(this).val() == 1) {//reserva
                    base.Control.divPagoContraEntrega().removeClass('hidden');
                    base.Control.divPagoOnline().addClass('hidden');
                }
                else if ($(this).val() == 2) {//online
                    base.Control.divPagoOnline().removeClass('hidden');
                    base.Control.divPagoContraEntrega().addClass('hidden');
                }
            });
        };

        base.Parametros = {
            controllerProducto: null,
            latitud: -12.0431805,
            longitude: -77.0282364
        };

        base.Control = {
            btnPagar: function () { return $('[name=btnPagar]'); },
            tblProductos: function () { return $('[name=tblProductos]'); },
            
            
            linkCurrentLocation: function () { return $('[name=linkCurrentLocation]'); },
            divPagoOnline: function () { return $('[name=divPagoOnline]'); },
            divPagoContraEntrega: function () { return $('[name=divPagoContraEntrega]'); },
            cboformapago: function () { return $('[name=cboformapago]'); },
            cboDpto: function () { return $('[name=cboDpto]'); },
            cboProv: function () { return $('[name=cboProv]'); },
            cboDist: function () { return $('[name=cboDist]'); },
            nombres: function () { return $('input[name=nombres]'); },
            dni: function () { return $('input[name=dni]'); },
            email: function () { return $('input[name=email]'); },
            celular: function () { return $('input[name=celular]'); },
            direccion_entrega: function () { return $('[name=direccion_entrega]'); },
            coordenadas: function () { return $('[name=coordenadas]'); },
            hdnlatitud: function () { return $('[name=hdnlatitud]'); },
            hdnlongitude: function () { return $('[name=hdnlongitude]'); },
        };

        base.Event = {
            clickPagar: function (obj) {
                formapago = $(obj).data('op');
                formapago == Web.Home.Constantes.PagoOnline ? base.Event.btnCulqiClick() : base.Event.setCarShopping(Web.Home.Constantes.tokenReserva);
            },
            btnCulqiClick: function () {
                if (base.Function.validarFinalizarCompra()) {
                    base.Ajax.getTotal();
                   
                }
            },
            setCarShopping: function (token) {
                
                var obj = {
                    nombres: base.Control.nombres().val(),
                    email: base.Control.email().val(),
                    dni: base.Control.dni().val(),
                    celular: base.Control.celular().val(),
                    direccion:  base.Control.cboDpto().val() + '/' + base.Control.cboProv().val() + '/' +
                                base.Control.cboDist().val() + '/' + base.Control.direccion_entrega().val(),
                    token: Web.Parameters.token,
                    idtienda: $('#hdnTienda').val(),
                    token_payment: token,
                    latitude: base.Control.hdnlatitud().val(),
                    longitude: base.Control.hdnlongitude().val()
                };
                base.Ajax.setCarShopping(obj);
               
            },
        };
        base.Ajax = {
            getTotal: function () {
                var obj = {
                    token: Web.Home.Constantes.token
                };
                $.ajax({
                    url: Web.Route.UrlApi.totalBolaCompra,
                    "type": 'POST',
                    "data": obj,
                    success: function (response) {
                        console.log(response);
                        Culqi.publicKey = 'pk_test_3BjluxL1NUUdm8xe'; //'pk_live_058TlwOFwkvZ8pS0';// ;
                        Culqi.settings({
                            title: 'CULQI CHECKOUT',
                            currency: 'PEN',
                            description: Web.Parameters.tienda,
                            amount: response.data//los decimales no pueden ir a Culqi, se multiplica por 100
                        });
                        Culqi.options({
                            modal: false,
                            lang: 'auto',
                            style: {
                                /*logo: 'https://firebasestorage.googleapis.com/v0/b/e-commerce-c6d82.appspot.com/o/logo%2Ficono-logo-220x240.png?alt=media&token=f37896f7-37f1-42e3-86a5-464c7645e46e',
                                maincolor: '#de9e00',
                                buttontext: '#ffffff',
                                maintext: '#4A4A4A',
                                desctext: '#4A4A4A'
                                */
                                logo: 'https://static.culqi.com/v2/v2/static/img/logo.png',
                                maincolor: '#0ec1c1',
                                buttontext: '#ffffff',
                                maintext: '#4A4A4A',
                                desctext: '#4A4A4A'
                                
                            }
                        });
                        // Abre el formulario con la configuración en Culqi.settings
                        Culqi.open(); //se invoca   culqi()
                        
                    }
                });
            },
            setCarShopping: function (obj) {
                $.ajax({
                    url: Web.Route.UrlApi.CarritoCompra,
                    data: obj,
                    type: 'POST',
                    success: function (response) {
                        console.log(response);
                        if (response.success) {
                            alerta('success', response.message);
                            debugger;
                            window.location.href = Web.Route.UrlApi.comprafinalizado +'/'+ response.data.codigoCompraEncry;
                        } else {
                            alerta('danger', response.message);
                        }

                    },
                    error: function (xhr) {
                        alerta('Danger', 'Error del sistema');
                    }
                });
            },

        };
        base.Function = {
           
            validarFinalizarCompra: function () {
                var str = "";
                if (base.Control.nombres().val().length == 0) {
                    str += "<br>Debe ingresar su nombre.";
                }
                if (base.Control.dni().val().length == 0) {
                    str += "<br>Debe ingresar su nombre.";
                }
                if (base.Control.email().val().length == 0) {
                    str += "<br>Debe ingresar su email.";
                }
                if (base.Control.celular().val().length == 0) {
                    str += "<br>Debe ingresar su celular.";
                }
                if (base.Control.cboDpto().val().length == 0) {
                    str += "<br>Debe ingresar un Departamento.";
                }
                if (base.Control.cboProv().val().length == 0) {
                    str += "<br>Debe ingresar una Provincia.";
                }
                if (base.Control.cboDist().val().length == 0) {
                    str += "<br>Debe ingresar un Distrito.";
                }
                if (base.Control.direccion_entrega().val().length == 0) {
                    str += "<br>Debe ingresar su direccion entrega.";
                }
                if (str.length > 0) {
                    alerta('warning', str);
                    return false;
                }

                return true;
            },
            markerPosition: function(){
                var map = new google.maps.Map(document.getElementById('map'), {
                    center: { lat: base.Parametros.latitud, lng: base.Parametros.longitude },
                    zoom: 17
                });
                var marker = new google.maps.Marker({
                    position: {
                        lat: base.Parametros.latitud,
                        lng: base.Parametros.longitude
                    },
                    map: map,
                    draggable: true,
                    animation: google.maps.Animation.DROP
                });
                google.maps.event.addListener(marker, 'dragend', function () {
                    base.Function.setCoordenadas(marker.getPosition().lat(), marker.getPosition().lng());
                });
              
                marker.setAnimation(google.maps.Animation.BOUNCE);

            },
            
            getGPS: function () {
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(base.Function.showLoc, base.Function.showErr);
                    
                } else {
                    
                    alerta("warning", "El dispositivo no soporta la geolocalización.");
                }
            },
            showLoc: function (position) {
                
                base.Function.setCoordenadas(position.coords.latitude, position.coords.longitude);
                base.Function.markerPosition();
            },
            setCoordenadas: function (lat, lon) {
                console.log(lat + "," + lon);
                base.Control.hdnlatitud().val(lat);
                base.Control.hdnlongitude().val(lon);
                base.Parametros.latitud = lat;
                base.Parametros.longitude = lon;
                $('[name=coordenadas]').text(lat + "," + lon);
            },
            showErr: function (error) {
                base.Function.markerPosition();
                var msg = '';
                switch (error.code) {
                    case error.PERMISSION_DENIED:
                        msg = "Se recomienda activar Geolocalización del dispositivo.";
                        break;
                    case error.POSITION_UNAVAILABLE:
                        msg = "Location information is unavailable.";
                        break;
                    case error.TIMEOUT:
                        msg = "The request to get user location timed out.";
                        break;
                    case error.UNKNOWN_ERROR:
                        msg = "An unknown error occurred.";
                        break;
                }
                alerta('warning', msg);
            }
            
        };
    };
} catch (ex) {
    alert(ex.message);
}


