/// <summary>
/// Script de Vista 
/// </summary>
/// <remarks>
/// </remarks>
try {
    ns('Ecommerce.Web.Admin.Index');
    $(document).ready(function () {
        'use strict';
        Ecommerce.Web.Admin.Index.Page = new Ecommerce.Web.Admin.Controller();
        Ecommerce.Web.Admin.Index.Page.Ini();
        
    });
} catch (ex) {
    alert(ex.message);
}

