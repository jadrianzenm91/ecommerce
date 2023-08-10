//Module
var app = angular.module('MyApp', [])
    .controller('HomeController', ['$scope', '$rootScope',
    function ($scope, $rootScope) {
        $scope.name = 'World';
        $rootScope.pedidos = localStorage.carritoCompra == undefined ? new Array() : jQuery.parseJSON(localStorage.carritoCompra);
        $rootScope.getTotal = function () {
            var total = 0;
            for (var i = 0; i < $rootScope.pedidos.length; i++) {
                var product = $rootScope.pedidos[i];
                total += (product.precio * product.cantidad);
            }
            $rootScope.CantPreCompras = $rootScope.pedidos.length;
            return total;
        }
    }]);

//app.run(function ($rootScope) {
//    $rootScope.$on('scope.stored', function (event, data) {
//        console.log("scope.stored", data);
//    });
//});


//factory: Para compartir $scope entre controladores
app.factory('Scopes', function ($rootScope) {
    var mem = {};

    return {
        store: function (key, value) {
            $rootScope.$emit('scope.stored', key);
            mem[key] = value;
        },
        get: function (key) {
            return mem[key];
        }
    };
});

      
//Controller

app.controller('PedidosController', function ($scope, $rootScope) {

});
app.controller('ProductosController', function ($scope, $rootScope, $timeout, Scopes, ProductoService) {
    
    var base = this;
        

        $scope.init = function () {//al cargar la lista de productos
            //base.Parametros.listaCarritoCompra = $rootScope.pedidos;
            
            if (localStorage.carritoCompra != undefined) {

                base.Parametros.listaCarritoCompra = jQuery.parseJSON(localStorage.carritoCompra);
                //se actualiza las cantidades de los productos consultados que se encuentren en carrito de compra.
                angular.forEach($rootScope.productos, function (item, key) {
                    item.cantidad = base.funciones.getCantidadJSON(item);
                });

                $rootScope.CantPreCompras = JSON.parse(localStorage.carritoCompra).length;
            }
            else {
                $rootScope.CantPreCompras = "0";
                angular.forEach($rootScope.productos, function (item, key) {
                    item.cantidad = 0;
                })
            }

            base.funciones.setProductosJSON();
            
           
        }

        //LISTA DE PEDIDOS DEL CLIENTE
        //ProductoService.ajax.loadCompraCliente('texto').then(function (result) {

        //    console.log(result.data);

        //});

        //LISTA DE PRODUCTOS
        ProductoService.ajax.listProductos($('#hdnTienda').val(), $('#hdnCategoria').val()).then(function (result) {
            var productos = result.data;
            var productosArray = new Array();
            
            
            if (localStorage.productos != undefined) {

                //se agrega al array (productosArray) los productos del localstorage.Productos
                var arrayProductosLocalStorage = JSON.parse(localStorage.productos);
                $.each(arrayProductosLocalStorage, function (i, itemA) {
                    productosArray.push(base.funciones.setEntidadProducto(itemA));
                });

                //se agrega al array (productosArray) los productos (traidos de la BD) que no existan en localstorage.Productos
                $.each(productos, function (i, itemP) {
                    var existe = false;
                    $.each(arrayProductosLocalStorage, function (i, itemA) {
                        
                        existe = (itemA.idproducto == itemP.V_CODIGO_PRODUCTO) ? true : false;
                        //si el producto existe en el localStorage, no será tomado en cuenta.
                        if (existe) return false;
                    });
                    //solo se guarda aquellos productos que no están en el LocalStorage
                    if (!existe)
                        productosArray.push(base.funciones.setEntidadProducto(itemP));

                });
               
                localStorage.productos = JSON.stringify(productosArray);
            } else {
                $.each(productos, function (i, item) {
                    productosArray.push(base.funciones.setEntidadProducto(item));
                });
                localStorage.productos = JSON.stringify(productosArray);
            }
            
            
            //$rootScope.productos = productosArray;
            var productosConsultados = new Array();
            $.each(productos, function (i, item) {
                productosConsultados.push(base.funciones.setEntidadProducto(item));
            });
            $rootScope.productos = productosConsultados;
            console.log(productosConsultados);

            $scope.categoria = result.data.length > 0? result.data[0]._CategoriaEN.V_CATEGORIA : 'NO SE ENCONTRARON PRODUCTOS';
            //validamos la autenticación para guardar en la base de datos los pedidos antes realizados
            //if (base.funciones.validarAutenticacion() && localStorage.carritoCompra != undefined) {
            //    base.funciones.setCarShopping();
            //} else {
            //    base.funciones.setCountShopping();
            //}

        });
        
    
        /*-----------------------------------------------------------------------------------------------------------------
        Orientado a objetos
        -----------------------------------------------------------------------------------------------------------------*/
        base.Parametros = {
            listaCarritoCompra: new Array()
        };

        base.Control = {
            mdlPedidos: function () { return $('#mdlPedidos'); },
            mdlFinalizarPedido: function () { return $('#mdlFinalizarPedido'); },
            mdlProducto: function () { return $('#mdlProducto'); },
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
        btnCulqiClick: function () {
            if (base.funciones.validarFinalizarCompra()) {
                Culqi.settings({
                    title: 'SISADRI - TIENDA VIRTUAL',
                    currency: 'PEN',
                    description: 'Pasarela de Pago respaldado por CULQI',
                    amount: ($rootScope.getTotal() * 100).toFixed(0)//los decimales no pueden ir a Culqi, se multiplica por 100
                });
                Culqi.options({

                    style: {
                        logo: 'https://firebasestorage.googleapis.com/v0/b/e-commerce-c6d82.appspot.com/o/logo%2Ficono-logo-220x240.png?alt=media&token=f37896f7-37f1-42e3-86a5-464c7645e46e',
                        maincolor: '#de9e00',
                        buttontext: '#ffffff',
                        maintext: '#4A4A4A',
                        desctext: '#4A4A4A'
                    }
                });
                // Abre el formulario con la configuración en Culqi.settings
                Culqi.open(); //se invoca   culqi()
                //e.preventDefault();
            }
        }
    },
        base.funciones = {
            validarFinalizarCompra: function () {
                var str = "";
                if(base.Control.nombres().val().length == 0){
                    str+= "<br>Debe ingresar su nombre.";
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
                if (base.Control.direccion_entrega().val().length == 0) {
                    str += "<br>Debe ingresar su direccion entrega.";
                }
                if (str.length > 0) {
                    alerta('warning', str);
                    return false;
                }
                
                return true;
            },
            setEntidadProducto: function (item) {
                if (item.V_CODIGO_PRODUCTO != undefined) {
                    var obj = {
                        idproducto: item.V_CODIGO_PRODUCTO,
                        precio: item.N_PRECIO,
                        nombre: item.V_PRODUCTO,
                        v_url: item.V_URL_PRINCIPAL,
                        cantidad: item.cantidad,
                        idcategoria: item._CategoriaEN.I_CODIGO_CATEGORIA,
                        categoria: item._CategoriaEN.V_CATEGORIA
                    }
                } else {
                    
                    obj = item;
                }
                return obj;
            },

            validarAutenticacion: function(){
                if (Web.Authenticate.User.CodigoUsuario != Web.Authenticate.User.CodigoUsuarioNoAutenticado) {
                    return true;
                }
                else {
                    return false;
                }
            },

            getProducto: function (item, index) {
                
                ProductoService.GetProductoByID(item.V_CODIGO_PRODUCTO == undefined ? item.idproducto : item.V_CODIGO_PRODUCTO).then(function (d) {
                    
                    $scope.producto = d.data;
                    $scope.productoCaracteristicas = d.data._listProductoCaracteristicaEL;
                    $scope.productoImagenes = d.data._listImagenProductoEN;
                    //guardamos el indexActual para las acciones del productoModal
                    $scope.indexActual = index;
                    //obtenemos su cantidad de pedidos del cliente
                    var entidadProducto = base.funciones.setEntidadProducto(item);
                    var itemFind = base.funciones.BuscarProductoLocalStoragePedidos(entidadProducto);
                    $scope.producto.cantidad = itemFind != undefined ? itemFind.cantidad : 0;
                    

                });
            },

            setCarShopping: function (token) {
                        $('.modal-preload').removeClass('d-none');
                        base.Parametros.listaCarritoCompra = jQuery.parseJSON(localStorage.carritoCompra);
                        var CarritoCompra = {
                            listCodigoProducto: base.Parametros.listaCarritoCompra,
                            codigoUsuario: Web.Authenticate.User.CodigoUsuario,
                            nombres: base.Control.nombres().val(),
                            email: base.Control.email().val(),
                            dni: base.Control.dni().val(),
                            celular: base.Control.celular().val(),
                            direccion: base.Control.direccion_entrega().val(),
                            total: $rootScope.getTotal(),
                            idtienda: $('#hdnTienda').val(),
                            token_payment: token,
                            codigoFormaPago: $scope.formapago,
                            latitude: base.Control.hdnlatitud().val(),
                            longitude: base.Control.hdnlongitude().val()
                        };
                        
                        ProductoService.ajax.setCarShopping(CarritoCompra).then(function (resp) {
                            console.log(resp);
                            if (resp.data.success) {
                                alerta('success', resp.data.message);
                                base.funciones.resetCountShopping();
                                base.Control.mdlFinalizarPedido().modal('hide');
                                //window.location.reload();
                                setTimeout(function () { location.reload(1); }, 3000);
                            }
                            else {
                                alerta('danger', resp.data.message);
                            }
                            $('.modal-preload').addClass('d-none');
                        });
            },
            BuscarProductoLocalStorageProductos: function (producto) {
                    var ListaProductos = JSON.parse(localStorage.productos);
                    var itemFind = ListaProductos.filter(function (item) {
                        return item.idproducto == (producto.idproducto == undefined ?
                                                    producto.V_CODIGO_PRODUCTO : producto.idproducto);
                    })[0];
                    return itemFind;
            },
            BuscarProductoLocalStoragePedidos: function (detalleProducto) {
                if (base.Parametros.listaCarritoCompra != undefined && base.Parametros.listaCarritoCompra.length > 0) {
                    var itemFind = base.Parametros.listaCarritoCompra.filter(function (item) {
                        return item.idproducto == detalleProducto.idproducto;
                    })[0];
                    return itemFind;
                }
                
            },
            setCantidadScopeProducto: function (idproducto, cantidad, flafijo) {
                $.each($rootScope.productos, function (i, item) {
                    if (item.idproducto == idproducto) {
                        //si se digita una cantidad, se coloca directo el numero.
                        if (flafijo) {
                            item.cantidad = cantidad;
                        }
                        else {
                            item.cantidad = item.cantidad == undefined ? 0 : (item.cantidad + cantidad);
                        }
                        
                    }
                });
                
            },
            setCantidadLocalStorageCarritoCompra: function (producto, cantidad, flafijo) {
                if (localStorage.carritoCompra != undefined) {
                    var existe = false;
                    //se actualiza la cantidad (+ - 1) del producto en el carrito de compra
                    $.each(base.Parametros.listaCarritoCompra, function (i, item) {
                        if (item.idproducto == producto.idproducto) {
                            item.cantidad += cantidad;
                            if (item.cantidad == 0) base.Parametros.listaCarritoCompra.splice(i, 1);
                            existe = true;
                            //si se digita una cantidad, se coloca directo el numero.
                            if (flafijo) {
                                item.cantidad = cantidad;
                                if (item.cantidad == 0) base.Parametros.listaCarritoCompra.splice(i, 1);
                            }
                            return false;
                        }
                    });
                    //se agrega el producto en el carrito de compra con cantidad (+ - 1)
                    if (!existe) {
                        producto.cantidad = producto.cantidad== undefined? 1 : (producto.cantidad+cantidad);
                        base.Parametros.listaCarritoCompra.push(producto);
                    }
                } else {
                    producto.cantidad = cantidad;
                    base.Parametros.listaCarritoCompra.push(producto);
                }

            },
            getIndexProducto: function (idproducto) {
                return objIndex = JSON.parse(localStorage.productos).map(function (item) {
                                    return item.idproducto;
                                  }).indexOf(idproducto);
            },
            getIndexProductoLocalStorage: function (idproducto) {
                return objIndex = base.Parametros.listaCarritoCompra.findIndex(function (item) {
                                        return item.idproducto == idproducto;
                                    });
                
            },
            addProductoCarShopping: function (detalleProducto) {
                detalleProducto = base.funciones.setEntidadProducto(detalleProducto);
                
                base.funciones.setCantidadScopeProducto(detalleProducto.idproducto, 1, false);
                base.funciones.setCantidadLocalStorageCarritoCompra(detalleProducto, 1, false);
                
                base.funciones.setProductosJSON();
                base.funciones.setCountShopping();
            },

            quitProductoCarShopping: function (detalleProducto) {
                detalleProducto = base.funciones.setEntidadProducto(detalleProducto);
                
                base.funciones.setCantidadScopeProducto(detalleProducto.idproducto, -1, false);
                base.funciones.setCantidadLocalStorageCarritoCompra(detalleProducto, -1, false);

                base.funciones.setProductosJSON();
                base.funciones.setCountShopping();
                
                
            },
            ChangeCantidadCarShopping: function (producto, cantidad) {
                base.Parametros.listaCarritoCompra = jQuery.parseJSON(localStorage.carritoCompra);

                if (producto.V_CODIGO_PRODUCTO != undefined) {//entonces proviene de la entidad de BD
                    producto = base.funciones.setEntidadProducto(producto)//convertimos al formato minusculas.
                }
                
                base.funciones.setCantidadScopeProducto(producto.idproducto, cantidad, true);
                base.funciones.setCantidadLocalStorageCarritoCompra(producto, cantidad, true);

                base.funciones.setProductosJSON();
                base.funciones.setCountShopping();
            },
            resetCountShopping: function () {
                
                $rootScope.CantPreCompras = 0;
                $rootScope.pedidos = 0;
                localStorage.removeItem("carritoCompra");
                
            },
            setCountShopping: function () {
                if (localStorage.carritoCompra != undefined) {
                    base.Parametros.listaCarritoCompra = jQuery.parseJSON(localStorage.carritoCompra);
                    $rootScope.CantPreCompras = JSON.parse(localStorage.carritoCompra).length;
                } else {
                    
                    $rootScope.CantPreCompras = 0;
                }
            },

            setProductosJSON: function () {
                if (base.Parametros.listaCarritoCompra != undefined && base.Parametros.listaCarritoCompra.length > 0) {
                    localStorage.carritoCompra = JSON.stringify(base.Parametros.listaCarritoCompra);
                    $rootScope.pedidos = jQuery.parseJSON(localStorage.carritoCompra);
                    base.funciones.setIndexProductoPedidos();
                }
                else {
                    base.funciones.resetCountShopping();
                }
                
                
            },

            getCantidadJSON: function (item) {
                
                if (item != undefined) {
                    var entidadProducto = base.funciones.setEntidadProducto(item);
                    var itemFind = base.funciones.BuscarProductoLocalStoragePedidos(entidadProducto);
                    return itemFind != undefined? itemFind.cantidad: 0;
                }
            },

            setIndexProductoPedidos: function () {
                angular.forEach($rootScope.pedidos, function (item, key) {
                    item.indexScopeProductos = JSON.parse(localStorage.productos).map(function (item) {
                        return item.idproducto;
                    }).indexOf(item.idproducto);
                })
            }
        };

        /*-----------------------------------------------------------------------------------------------------------------
        Acciones desde los botones de la vista
        -----------------------------------------------------------------------------------------------------------------*/
    
        $scope.filterProductos = function (idcategoria) {
            $scope.search = idcategoria;
        }
        $scope.saveFinalizarPedido = function (formapago) {
            $scope.formapago = formapago;
            formapago == Web.Home.Constantes.PagoOnline ? base.Event.btnCulqiClick() : $scope.setCarritoCompra('TOKEN_RESERVA');
        }
        $scope.validarAutenticacion = function () {
            base.funciones.validarAutenticacion();
        }

        $rootScope.loadPedidos = function () {
            if ($rootScope.pedidos.length > 0) {
                base.funciones.setIndexProductoPedidos();
                base.Control.mdlPedidos().modal('show');
            } else {
                alerta('warning','Su carrito de compras no tiene productos seleccionados.');
            }
        };

        
        $rootScope.ChangeCantidadCarShopping = function (producto, cantidad) {
            cantidad  = (cantidad == undefined) ? 0 : cantidad;
            base.funciones.ChangeCantidadCarShopping(producto, cantidad);
                
        };

        $scope.RedirectToView = function (item) {
            $localStorage.idproducto = item.I_CODIGO_PRODUCTO;
            window.location.href = '/Producto/Index/' + item.I_CODIGO_PRODUCTO;
        };

        $scope.getProducto = function (item, index) {
            
            base.funciones.getProducto(item, index);
            base.Control.mdlProducto().modal('show');
        };
        
        $scope.addCarShopping = function (item) {
            var itemfind = base.funciones.BuscarProductoLocalStorageProductos(item);
            //var detalleProducto = base.funciones.setEntidadProducto(itemfind);
            base.funciones.addProductoCarShopping(itemfind);
            base.Control.mdlProducto().modal('hide');
        };

        $rootScope.quitCarShopping = function (item) {
            var detalleProducto = base.funciones.setEntidadProducto(item);
            base.funciones.quitProductoCarShopping(detalleProducto);
        };

        $scope.getCantidadJSON = function (item) {
            
            return base.funciones.getCantidadJSON(item);
        };
        
        $scope.openModalFinalizarPedido = function () {
            //if (base.funciones.validarAutenticacion()) {

                if ($rootScope.pedidos.length > 0) {
                    
                    base.Control.mdlFinalizarPedido().modal('show');
                } else {
                    alerta('warning','Su carrito de compras no tiene productos seleccionados.');
                }

            //} else {
            //    $('#mdlLogin').modal('show');
            //}
            
        };
    
        $scope.setCarritoCompra = function (token) {
            /*
    $scope.setCarritoCompra: Recepciona un token de dos formas, 1ro si realiza un pago online Culqi envía un token generado,    
                             2do cuando se hace una reserva de compra.
                             
    */
            if (base.funciones.validarFinalizarCompra()) {
                base.funciones.setCarShopping(token);
            }
        };

        $scope.submitForm = function (isValid) {

            if (isValid) {
                alert('Formulario enviado');
            }

        };
        
    })


