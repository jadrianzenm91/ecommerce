/// <summary>
/// Script de Vista 
/// </summary>
/// <remarks>
/// </remarks>
try {
    ns('Ecommerce.Web.AdminProducto.Index');
    $(document).ready(function () {
        'use strict';
        Ecommerce.Web.AdminProducto.Index.Page = new Ecommerce.Web.AdminProducto.Controller();
        Ecommerce.Web.AdminProducto.Index.Page.Ini();

    });
} catch (ex) {
    alert(ex.message);
}

