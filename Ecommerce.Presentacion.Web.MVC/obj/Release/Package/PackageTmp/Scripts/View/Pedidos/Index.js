/// <summary>
/// Script de Vista 
/// </summary>
/// <remarks>
/// </remarks>
try {
    ns('Ecommerce.Web.Pedidos.Index');
    $(document).ready(function () {
        'use strict';
        Ecommerce.Web.Pedidos.Index.Page = new Ecommerce.Web.Pedidos.Controller();
        Ecommerce.Web.Pedidos.Index.Page.Ini();
        
    });
} catch (ex) {
    alert(ex.message);
}

