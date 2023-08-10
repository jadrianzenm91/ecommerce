/// <summary>
/// Script de Controladora de la Vista 
/// </summary>
/// <remarks>
/// </remarks>
try {
    ns('Ecommerce.Web.AdminProducto.Controller');
    Ecommerce.Web.AdminProducto.Controller = function () {
        var base = this;
        base.Ini = function () {
            'use strict';
           
           

            base.Control.btnNuevoProd().on('click', function () {
                base.Ajax.getViewNuevoProducto(undefined);
            });
           
            base.Control.tblProductos().on('click', '[name=getProducto]', function () {
                base.Event.clickGetProducto($(this));
            });
            base.Control.tblProductos().on('click', '[name=offProducto]', function () {
                base.Event.clickDeleteProducto($(this), false);
            });
            base.Control.tblProductos().on('click', '[name=onProducto]', function () {
                base.Event.clickDeleteProducto($(this), true);
            });

            base.Control.btnSaveProd().on('click', base.Event.clickGuardar);
            $('.number').number(true, 2);
            $('input[name="FileUpload"]').on('change', function () {
                base.Event.changeUpload();
            })
            $('#divImages').on('click', 'button', function () {
                base.Event.clickDeleteImagen($(this));
            })
        };

        base.Parametros = {
            productoEN: null,
            controllerAdminProducto: null,
            listaCarritoCompra: new Array(),
            arrayCaracteristica: null,
            arrayImagenesurl: new Array(),
            arrayImagenesName: new Array(),
            idtienda: $('#hdnTienda').val()
        };

        base.Control = {

            btnNuevoProd: function () { return $('[name=btnNuevoProd]'); },
            divPrincipal: function () { return $('.container-fluid'); },
            hdnIdProductoCaracteristica: function () { return $('#hdnIdProductoCaracteristica'); },
            hdnIdProducto: function() {return $('#hdnIdProducto');},
            txtProducto: function () { return $('#txtProducto'); },
            txtPrecio: function () { return $('#txtPrecio'); },
            txtStock: function () { return $('#txtStock'); },
            ddlCategoria: function () { return $('#ddlCategoria'); },
            txtCodBarra: function () { return $('#txtCodBarra'); },
            txtDescripcion: function () { return $('#txtDescripcion'); },
            btnSaveProd: function () { return $('[name=btnSaveProd]'); },
            tblProductos: function () { return $('[name=tblProductos]'); },
            imgProducto: function () { return $('[name=imgProducto]'); }
        };

        base.Event = {
            clickDeleteProducto: function (obj, active) {
                $('[data-toggle="tooltip"]').tooltip('dispose');
                var id = $(obj).data('id');
                base.Ajax.ajaxDeleteProducto(id, active);
            },
            clickDeleteImagen: function (obj) {
                //var storageRef = firebase.storage().ref();
                var figure = $(obj).closest('figure');
                var id = $(figure).find('button').data('id');
                var fileName = '';
                $.each(base.Parametros.arrayImagenesurl, function (i, item) {
                    
                    fileName = $(figure).find('button').data('fileName');
                    if (item.id == id) {
                        base.Parametros.arrayImagenesurl.splice(i, 1);
                        //var desertRef = storageRef.child('file/' + fileName);

                        //// Delete the file
                        //desertRef.delete().then(function () {
                        //    // File deleted successfully
                        //}).catch(function (error) {
                        //    // Uh-oh, an error occurred!
                        //});
                    }
                    
                });
                $(figure).remove();
               
                
            },
            clickGetProducto: function (obj) {
                var id = $(obj).data('id');
                base.Ajax.ajaxGetProducto(id);
            },
            clickGuardar: function () {
                if (base.Function.validarDatosProducto()) {
                    BootstrapDialog.confirm("Mensaje de Confirmación", "Está seguro de guardar los datos?", function (result) {
                        if (result) {

                            base.Parametros.arrayCaracteristica = new Array();

                            var ProductoCaracteristicaEL = {
                                I_PRODUCTO_CARACTERISTICA: base.Control.hdnIdProductoCaracteristica().val(),
                                V_CARACTERISTICA: base.Control.txtDescripcion().val(),
                                I_CODIGO_CARACTERISTICA: 1 //presentacion
                            };
                            base.Parametros.arrayCaracteristica.push(ProductoCaracteristicaEL);

                            var item = {
                                I_CODIGO_PRODUCTO: base.Control.hdnIdProducto().val(),
                                _listImagenProductoEN: base.Parametros.arrayImagenesurl,
                                _listProductoCaracteristicaEL: base.Parametros.arrayCaracteristica,
                                V_PRODUCTO: base.Control.txtProducto().val(),
                                V_CODIGO_PRODUCTO: base.Control.txtCodBarra().val(),
                                N_PRECIO: base.Control.txtPrecio().val(),
                                I_STOCK: base.Control.txtStock().val(),
                                I_CODIGO_CATEGORIA: base.Control.ddlCategoria().val(),
                                I_CODIGO_TIENDA: $('#hdnTienda').val(),
                                V_CODIGO_BARRA: base.Control.txtCodBarra().val()

                            };

                            base.Ajax.ajaxSetProducto(JSON.stringify(item));

                        } else {
                            return false;
                        }

                    });
                }
            },
            changeUpload: function () {
                
                base.Function.uploadStorageFireBase(document.getElementById("FileUpload"));
            },
        };
        base.Ajax = {
            ajaxGetProducto: function (id) {
                $.ajax({
                    url: '/Producto/getProducto',
                    "type": 'POST',
                    "data": { id: id},
                    success: function (response) {
                        console.log(response);
                        base.Parametros.productoEN = response;
                        base.Ajax.getViewNuevoProducto(base.Function.printDatosProductos);
                        
                    },
                    error: function (xhr) {
                        alerta('danger', "Ocurrió de error en el sistema.");

                    }
                });
            },
            ajaxDeleteProducto: function (id, active) {
                $.ajax({
                    url: '/Producto/deleteProducto',
                    "type": 'POST',
                    "data": { id: id, active: active },
                    success: function (response) {
                        console.log(response);
                        if (response.success) {
                            alerta('success', "Proceso ejecutado con éxito.");
                            window.location.href = Web.Route.Url.home + '/Admin/bandejaProductos';
                        }

                    },
                    error: function (xhr) {
                        alerta('danger', "Ocurrió de error en el sistema.");

                    }
                });
            },
            ajaxSetProducto: function (obj) {
                $.ajax({
                    url: '/Producto/setProducto',
                    "type": 'POST',
                    "data": obj,
                    contentType: 'application/json',
                    success: function (response) {
                        var data = response.data;
                        if (data.success) {
                            window.location.href = Web.Route.Url.home + '/Admin/bandejaProductos';
                            alerta('success', 'Nuevo Producto, ' + data.message);
                            

                        } else {
                            alerta('danger', data.stacktrace);
                        }

                    },
                    error: function (xhr) {
                        alerta('danger', "Ocurrió de error en el sistema.");

                    }
                });
            },
            getViewNuevoProducto: function (callback) {
                $.ajax({
                    url: '/Admin/nuevoProducto',
                    type: 'POST',
                    "data": { idtienda: base.Parametros.idtienda },
                    success: function (data) {
                        base.Control.divPrincipal().html("");
                        base.Control.divPrincipal().html(data);
                        
                        if (callback != undefined) {
                            callback();
                        }
                        base.Ini();
                        
                    },
                    error: function (xhr) {
                        alerta('danger', "Ocurrió de error en el sistema.");

                    }
                });

            },
        };
        base.Function = {
            generarImagenID: function () {
                return (base.Parametros.arrayImagenesurl.length + 1000);
            },
            validarDatosProducto: function () {
                var cad = '';
                if (base.Parametros.arrayImagenesurl.length == 0) {
                    cad = 'Debe cargar al menos una imagen del producto.';
                    
                } else {
                    $.each(base.Parametros.arrayImagenesurl, function (i, item) {
                        if (item.V_URL == undefined || item.V_URL.length == 0) {
                            cad +='Debe cargar al menos una imagen del producto.';
                        }
                    });
                }

                if (cad.length > 0) {
                    alerta('warning', cad);
                    return false;
                }
                return true;
            },
            printDatosProductos: function () {
                var data = base.Parametros.productoEN;
                base.Control.hdnIdProducto().val(data.I_CODIGO_PRODUCTO);
                base.Control.txtProducto().val(data.V_PRODUCTO);
                base.Control.txtCodBarra().val(data.V_CODIGO_BARRA);
                base.Control.txtPrecio().val(data.N_PRECIO);
                base.Control.txtStock().val(data.I_STOCK);
                base.Control.ddlCategoria().val(data.I_CODIGO_CATEGORIA);
                base.Control.txtCodBarra().val(data.V_CODIGO_BARRA);
                $.each(data._listImagenProductoEN, function (index, item) {
                    var id = base.Function.generarImagenID();
                    $('#divImages').append('<figure class="figure"><figcaption class="figure-caption m-1"><button class="btn btn-danger" data-id="' + id + '" name="btnDeleteImg">Eliminar</button></figcaption><img src="' + item.V_URL + '" class="img-thumbnail" alt="' + item.V_URL + '" style="width: 240px;"></figure>');
                    var obj = {
                        id: id,
                        V_URL: item.V_URL
                    };
                    base.Parametros.arrayImagenesurl.push(obj);
                });
                $.each(data._listProductoCaracteristicaEL, function (index, item) {
                    base.Control.hdnIdProductoCaracteristica().val(item.I_PRODUCTO_CARACTERISTICA);
                    base.Control.txtDescripcion().val(item.V_VALOR);
                });
            },
           
            uploadStorageFireBase: function (input) {
                var totalFiles = input.files.length;
                var storageRef = firebase.storage().ref();

                for (var i = 0; i < totalFiles; i++) {
                    var file = input.files[i];
                    var fileName = (+new Date() + '.' + file.name.split('.')[1]);
                    var uploadTask = storageRef.child('/productos/' + Web.Parametro.fileImagesFirebase + '/'+ fileName).put(file);
                    console.log('imagen ' + fileName);
                    uploadTask.on('state_changed',
                       function (snapshot) {
                           var percentage = (snapshot.bytesTransferred / snapshot.totalBytes) * 100;
                           
                           $('.progress-bar').text(fileName + ': Cargado ' + percentage.toFixed(2) + '%');

                           $('.progress-bar').css('width', percentage + '%');
                           
                       },
                       function (error) {
                           $('.progress-bar').text('');
                           $('.progress-bar').css('width', '0%');
                           console.log(error.code);
                       },
                       function () {
                           uploadTask.snapshot.ref.getDownloadURL().then(function (downloadURL) {
                               
                               var id = base.Function.generarImagenID();
                               $('#divImages').append('<figure class="figure"><figcaption class="figure-caption m-1"><button class="btn btn-danger" data-fileName="' + fileName + '" data-id="' + id + '" name="btnDeleteImg">Eliminar</button></figcaption><img src="' + downloadURL + '" class="img-thumbnail" style="width: 240px;"></figure>');
                               var obj = {
                                   id : id,
                                   V_URL: downloadURL
                               };
                               base.Parametros.arrayImagenesurl.push(obj);
                               $('.progress-bar').text('');
                               $('.progress-bar').css('width', '0%');
                               $('input[name="FileUpload"]').val('');
                           });
                           
                       }
                       );
                
                }

            }

        };
    };
} catch (ex) {
    alert(ex.message);
}


