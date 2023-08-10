/// <summary>
/// Script de Vista 
/// </summary>
/// <remarks>
/// </remarks>
try {
    ns('Ecommerce.Web.Home.Index');
    $(document).ready(function () {
        'use strict';
        Ecommerce.Web.Home.Index.Page = new Ecommerce.Web.Home.Index.Controller();
        Ecommerce.Web.Home.Index.Page.Ini();
        
    });
} catch (ex) {
    alert(ex.message);
}
