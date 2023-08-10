/// <summary>
/// Script de Vista 
/// </summary>
/// <remarks>
/// </remarks>
try {
    ns('Ecommerce.Web.Checkout.Index');
    $(document).ready(function () {
        'use strict';
        Ecommerce.Web.Checkout.Index.Page = new Ecommerce.Web.Checkout.Controller();
        Ecommerce.Web.Checkout.Index.Page.Ini();
        
    });
} catch (ex) {
    alert(ex.message);
}

