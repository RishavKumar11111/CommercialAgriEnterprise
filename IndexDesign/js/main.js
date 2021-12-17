(function($) {
     "use strict";


     // stickyHeader
     function stickyHeader() {
         if ($('.stricky').length) {
             var strickyScrollPos = '60';
             if ($(window).scrollTop() > strickyScrollPos) {
                 $('.stricky').addClass('stricky-fixed');
             } else if ($(window).scrollTop() <= strickyScrollPos) {
                 $('.stricky').removeClass('stricky-fixed');
             }
         };
     }

     // --------------------------------------------------
     // revolution slider
     // --------------------------------------------------


     function revolutionSliderActiver() {
         if ($('.main-slider .tp-banner').length) {

             jQuery('.main-slider .tp-banner').show().revolution({
                 delay: 4000,
                 startwidth: 1200,
                 startheight: 420,
                 hideThumbs: 600,

                 thumbWidth: 80,
                 thumbHeight: 50,
                 thumbAmount: 5,

                 navigationType: "bullet",
                 navigationArrows: "0",
                 navigationStyle: "preview4",

                 touchenabled: "on",
                 onHoverStop: "off",

                 swipe_velocity: 0.7,
                 swipe_min_touches: 1,
                 swipe_max_touches: 1,
                 drag_block_vertical: false,

                 parallax: "mouse",
                 parallaxBgFreeze: "on",
                 parallaxLevels: [7, 4, 3, 2, 5, 4, 3, 2, 1, 0],

                 keyboardNavigation: "off",

                 navigationHAlign: "center",
                 navigationVAlign: "bottom",
                 navigationHOffset: 0,
                 navigationVOffset: 20,

                 soloArrowLeftHalign: "left",
                 soloArrowLeftValign: "center",
                 soloArrowLeftHOffset: 20,
                 soloArrowLeftVOffset: 0,

                 soloArrowRightHalign: "right",
                 soloArrowRightValign: "center",
                 soloArrowRightHOffset: 20,
                 soloArrowRightVOffset: 0,

                 shadow: 0,
                 fullWidth: "on",
                 fullScreen: "off",

                 spinner: "spinner4",

                 stopLoop: "off",
                 stopAfterLoops: -1,
                 stopAtSlide: -1,

                 shuffle: "off",

                 autoHeight: "off",
                 forceFullWidth: "on",

                 hideThumbsOnMobile: "on",
                 hideNavDelayOnMobile: 1500,
                 hideBulletsOnMobile: "on",
                 hideArrowsOnMobile: "on",
                 hideThumbsUnderResolution: 0,

                 hideSliderAtLimit: 0,
                 hideCaptionAtLimit: 0,
                 hideAllCaptionAtLilmit: 0,
                 startWithSlide: 0,
                 videoJsPath: "",
                 fullScreenOffsetContainer: ""
             });

         }
     }

     //recent product carusal
     $("#gallery_carusal").owlCarousel({

         slideSpeed: 300,
         paginationSpeed: 400,
         autoPlay: true,
         pagination: false, //Set AutoPlay to 3 seconds  
         navigation: true,
         navigationText: [
             "<i class='fa fa-angle-left'></i>",
             "<i class='fa fa-angle-right'></i>"
         ],
         items: 4,
         itemsDesktop: [1199, 3],
         itemsDesktopSmall: [979, 2],
         // itemsTablet: [768, 2],
         itemsTablet: [767, 2],
         widemobiel: [480, 1],
         smallmobiel: [320, 1],
     });

     //recent product carusal
     $("#gallery_Work").owlCarousel({
         navigation: true, // Show next and prev buttons
         // navigationText: ["prev","next"], 
         navigationText: [
             "<i class='fa fa-angle-left'></i>",
             "<i class='fa fa-angle-right'></i>"
         ],
         slideSpeed: 300,
         paginationSpeed: 400,
         autoPlay: true,
         pagination: false,
         items: 3,
         itemsDesktop: [1199, 3],
         itemsDesktopSmall: [979, 2],
         // itemsTablet: [768, 2],
         itemsTablet: [767, 2],
         widemobiel: [480, 1],
         smallmobiel: [320, 1],
         //As above
         goToFirst: true, //Slide to first item if autoPlay reach end
         goToFirstSpeed: 1000
     });


     // Best seller carusal
     $("#best_seller").owlCarousel({
         navigation: false, // Show next and prev buttons 
         slideSpeed: 300,
         paginationSpeed: 400,
         autoPlay: true,
         pagination: true,
         items: 4,
         itemsDesktop: [1199, 3],
         itemsDesktopSmall: [979, 2],
         // itemsTablet: [768, 2],
         itemsTablet: [767, 2],
         widemobiel: [480, 1],
         smallmobiel: [320, 1],
         //As above
         goToFirst: true, //Slide to first item if autoPlay reach end
         goToFirstSpeed: 1000
     });

     //Contact Form Validation
     function contactFormValidation() {
         if ($('.contact-form').length) {
             $('.contact-form').validate({ // initialize the plugin
                 rules: {
                     name: {
                         required: true
                     },
                     email: {
                         required: true,
                         email: true
                     },
                     message: {
                         required: true
                     },
                     subject: {
                         required: true
                     }
                 },
                 submitHandler: function(form) {
                     // sending value with ajax request
                     $.post($(form).attr('action'), $(form).serialize(), function(response) {
                         $(form).parent('div').append(response);
                         $(form).find('input[type="text"]').val('');
                         $(form).find('input[type="email"]').val('');
                         $(form).find('textarea').val('');
                     });
                     return false;
                 }
             });
         }
     }
     // gallery fancybox activator 
     function GalleryFancyboxActivator() {
         var galleryFcb = $('.fancybox');
         if (galleryFcb.length) {
             galleryFcb.fancybox({
                 openEffect: 'elastic',
                 closeEffect: 'elastic',
                 helpers: {
                     media: {}
                 }
             });
         }
     }


     // video player
     function videoplay() {
         if ($('#video').length) {
             $("#video").simplePlayer({

                 autoplay: 1,
                 autohide: 1,
                 border: 0,
                 wmode: 'opaque',
                 enablejsapi: 1,
                 modestbranding: 1,
                 version: 3,
                 hl: 'en_US',
                 rel: 0,
                 showinfo: 0,
                 hd: 1,
                 iv_load_policy: 3 // add origin

             });
         };
     }

     // gallery filter activation 
     function GalleryFilter() {

         if ($('#image-gallery-mix').length) {
             $('.gallery-filter').find('li').each(function() {
                 $(this).addClass('filter');
             });
             $('#image-gallery-mix').mixItUp();
         };
         if ($('.gallery-filter.masonary').length) {
             $('.gallery-filter.masonary span').on('click', function() {
                 var selector = $(this).parent().attr('data-filter');
                 $('.gallery-filter.masonary span').parent().removeClass('active');
                 $(this).parent().addClass('active');
                 $('#image-gallery-isotope').isotope({ filter: selector });
                 return false;
             });
         }
     }

     // Adding hover effect to gallery 
     function galleryHover() {
         // hover effect for masonary gallery
         if ($('.masonary-gallery').length) {
             $('.masonary-gallery .content-wrap').each(function() {
                 $(this).addClass('hvr-shutter-in-vertical');
             });
         };
         // hover effect for normal gallery
         if ($('.normal-gallery').length) {
             $('.normal-gallery .content-wrap').each(function() {
                 $(this).addClass('hvr-rectangle-out');
                 $(this).parent().parent().addClass('mix');
             });
         };

     }




     //Loading Box (Preloader)
     function preloaderbox() {
         if ($('.preloaderbox').length) {
             $('.preloaderbox').fadeOut();
         }
     }

     // Google Map
     function gMap() {
         if ($('.google-map').length) {
             $('.google-map').each(function() {
                 // getting options from html 
                 var mapName = $(this).attr('id');
                 var mapLat = $(this).data('map-lat');
                 var mapLng = $(this).data('map-lng');
                 var iconPath = $(this).data('icon-path');
                 var mapZoom = $(this).data('map-zoom');
                 var mapTitle = $(this).data('map-title');

                 // if zoom not defined the zoom value will be 15;
                 if (!mapZoom) {
                     var mapZoom = 15;
                 };
                 // init map
                 var map;
                 map = new GMaps({
                     div: '#' + mapName,
                     scrollwheel: false,
                     lat: mapLat,
                     lng: mapLng,
                     zoom: mapZoom
                 });
                 // if icon path setted then show marker
                 if (iconPath) {
                     map.addMarker({
                         icon: iconPath,
                         lat: mapLat,
                         lng: mapLng,
                         title: mapTitle
                     });
                 }
             });
         };
     }

     function tabs() {
         $('#myTabs a').click(function(e) {
             e.preventDefault()
             $(this).tab('show')
         })
     }

     $(document).on('ready', function() {
         (function($) {
             // add your functions   
             revolutionSliderActiver();
             GalleryFilter();
             tabs();
             gMap();
             galleryHover();
             GalleryFancyboxActivator();
             videoplay();
             contactFormValidation();
         })(jQuery);
     });

     $(window).on('load', function() {
         (function($) {
             preloaderbox();
         })(jQuery);
     });

     $(window).on('scroll', function() {
         (function($) {
             stickyHeader();
         })(jQuery);
     });

 })(jQuery);