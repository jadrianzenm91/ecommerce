/// <summary>
/// Script de Controladora de la Vista 
/// </summary>
/// <remarks>
/// </remarks>
try {
    ns('Ecommerce.Web.Home.Index.Controller');
    Ecommerce.Web.Home.Index.Controller = function () {
        var base = this;
        base.Ini = function () {
            'use strict';
            
            base.Control.btnLogin().click(function () {
                base.Event.IniciarSession($(this).data('social'));
            });

            base.Control.btnLoginUser().on('click', function () {
                base.Event.loginUser();
            });

            base.Control.btnRegisterUser().on('click', function () {
                base.Event.registerUser();
            });
            

            base.Control.linkCerrarSession().click(function () {
                base.Event.CerrarSession();
            });
            
        };


        base.Parametros = {
            urlApiUsuarioProviderAuth: Web.Route.UrlApi.UsuarioProviderAuth
        };

        base.Control = {
            btnLogin: function () { return $('.firebase') },
            btnLoginUser: function () { return $('a[name=btnLoginUser]') },
            btnRegisterUser: function () { return $('a[name=btnRegisterUser]') },
            linkBinvenidaUsuario: function () { return $('#linkBinvenidaUsuario') },
            linkIniciarSession: function () { return $('#linkIniciarSession ') },
            linkCerrarSession: function () { return $('#linkCerrarSession') },
            OpcionesLogin: function () { return $('#linkOpcionesLogin') }
        };

        base.Event = {
            registerUser: function () {
                $("form", "#register").submit();
            },
            loginUser: function () {
                $("form", "#login").submit();
            },
            getAuthProvider: function (social) {
                if (social == 'google') {
                    var provider = new firebase.auth.GoogleAuthProvider();
                    provider.addScope('profile');
                    provider.addScope('email');
                    
                    return provider;
                } else if(social == 'facebook') {
                    var provider = new firebase.auth.FacebookAuthProvider();
                    provider.addScope('email');
                    provider.addScope('name');
                    provider.addScope('picture');
                    return provider;
                } else if (social == 'twitter') {
                    return new firebase.auth.TwitterAuthProvider();
                }
            },
            CerrarSession: function () {
                //cerramos session from FIREBASE
                firebase.auth().signOut();
                Web.Authenticate.User.CodigoUsuario = Web.Authenticate.User.CodigoUsuarioNoAutenticado;
                //cerramos session from .NET
                javascript: document.getElementById('logoutForm').submit();
            },
            IniciarSession: function (social) {
                if (!firebase.auth().currentUser) {

                    var provider = base.Event.getAuthProvider(social);
                    
                    firebase.auth().signInWithPopup(provider).then(function (result) {
                        console.log(result.user);
                        var user = result.user;
                        var UsuarioEN = {
                                        V_UID_PROVIDER: user.uid,
                                        V_NOMBRES: user.displayName,
                                        V_EMAIL: user.email,
                                        V_PROVIDER: user.providerData[0].providerId,
                                        V_PHOTO_URL: user.photoURL
                                    }
                                    //guardar usuario provider
                                    base.Ajax.setUsuarioProviderAuth(UsuarioEN);
                        //if (result.credential) {
                        //    var token = result.credential.accessToken;
                        //    console.log(token);
                            //se dispara el evento firebase.auth().onAuthStateChanged()

                            //firebase.auth().onAuthStateChanged(function (user) {
                            //    console.log('onAuthStateChanged');
                            //    console.log(user);
                            //    if (user) {

                            //        //user = user.providerData[0];
                            //        var UsuarioEN = {
                            //            V_UID_PROVIDER: user.uid,
                            //            V_NOMBRES: user.displayName,
                            //            V_EMAIL: user.email,
                            //            V_PROVIDER: user.providerData[0].providerId,
                            //            V_PHOTO_URL: user.photoURL
                            //        }
                            //        //guardar usuario provider
                            //        base.Ajax.setUsuarioProviderAuth(UsuarioEN);

                            //    }
                            //});

                            
                        //} 

                    }).catch(function (error) {

                        var errorCode = error.code;
                        var errorMessage = error.message;
                        var email = error.email;
                        var credential = error.credential;

                        if (errorCode === 'auth/account-exists-with-different-credential') {
                            alerta('danger','You have already signed up with a different auth provider for that email.');

                        } else {
                            alerta('danger', errorMessage);
                        }
                    });

                }
            }
            
            
        };
        base.Ajax = {
            setUsuarioProviderAuth: function (user) {
                
                $.ajax({
                    url: base.Parametros.urlApiUsuarioProviderAuth,
                    type: 'POST',
                    data: JSON.stringify(user),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        localStorage.setUsuarioProviderAuth = JSON.stringify(data);
                        if (data.success == true) {
                            window.location.reload();
                        } else {
                            alerta('danger', data.data);
                            base.Event.CerrarSession();
                        }
                    }
                });
            }
        };
        base.Function = {
           
        };
    };
} catch (ex) {
    alert(ex.message);
}
