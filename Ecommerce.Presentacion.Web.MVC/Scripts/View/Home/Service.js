app.service('ProductoService', function ($http) {
        
    var base = this;

    base.Parametros = {
        urlAjaxAgregarCarrito: Web.Route.UrlApi.CarritoCompra,
        urlAjaxMisPedidos: Web.Route.UrlApi.MisPedidos
    };

    base.get = function (id) {
        return $http.get('/Producto/getProducto/' + id);
    }
    
    base.GetProductoByID = function (currentProductoID) {
        
        if (currentProductoID != null) {
            return $http.get('/Producto/getProducto', { params: { id: currentProductoID } });
        }
    }

    base.ajax = {

        listProductos: function (idtienda, idcategoria) {
            return $http.get('/Producto/listProductos', { params: { idtienda: idtienda, idcategoria: idcategoria } });
        },

        setCarShopping : function (item) {
            var stringData = JSON.stringify(item);
            console.log(stringData);
            return $http.post(base.Parametros.urlAjaxAgregarCarrito, stringData);
        },

        loadPedidos: function (requestData) {
            return $http.post('/Pedidos/Index', requestData );
        },

        loadCompraCliente: function (requestData) {
            return $http.post(base.Parametros.urlAjaxMisPedidos, requestData);
        }

    }
    

});

















