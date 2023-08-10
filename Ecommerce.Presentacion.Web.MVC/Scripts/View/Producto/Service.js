app.service('ProductoMantenimientoService', function ($http) {
        
    var base = this;

    base.Parametros = {
        urlAjaxSetProducto: Web.Route.UrlApi.setProductoMantenimiento,
        urlAjaxGetProductoById: Web.Route.UrlApi.getProductoById,
        urlAjaxGetCategorias: Web.Route.UrlApi.getCategorias
    };


    base.ajax = {

        setProducto: function (obj) {
            obj.N_PRECIO = parseFloat(obj.N_PRECIO)

            obj.V_USER_CREATE = Web.Authenticate.User.CodigoUsuario;
            var stringData = JSON.stringify(obj);
            console.log('setProducto:' + stringData);
            return $http.post('/Producto/setProducto', stringData);
        },
        getProductoByID: function (id) {
            return $http.post(base.Parametros.urlAjaxGetProductoById, id);
        },
        getCategorias: function () {
            return $http.post(base.Parametros.urlAjaxGetCategorias);
        }

    }
    

});