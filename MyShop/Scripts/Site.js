var infortisTheme = {}; infortisTheme.responsive = true; infortisTheme.maxBreak = 1280;


optionalZipCountries = ["HK", "IE", "MO", "PA"];
var Translator = new Translate([]);

//////////////////////////////////////////

var SmartHeader = {

    mobileHeaderThreshold: 770
     , rootContainer: jQuery('.header-container')

     , init: function () {
         enquire.register('(max-width: ' + (SmartHeader.mobileHeaderThreshold - 1) + 'px)', {
             match: SmartHeader.moveElementsToMobilePosition,
             unmatch: SmartHeader.moveElementsToRegularPosition
         });
     }

     , activateMobileHeader: function () {
         SmartHeader.rootContainer.addClass('header-mobile').removeClass('header-regular');
     }

     , activateRegularHeader: function () {
         SmartHeader.rootContainer.addClass('header-regular').removeClass('header-mobile');
     }

     , moveElementsToMobilePosition: function () {
         SmartHeader.activateMobileHeader();

         jQuery('#mini-cart-wrapper-mobile').prepend(jQuery('#mini-cart'));
         jQuery('.skip-active').removeClass('skip-active');

         //Disable dropdowns
         jQuery('#mini-cart').removeClass('dropdown');
         jQuery('#mini-compare').removeClass('dropdown');

         //Clean up after dropdowns: reset the "display" property
         jQuery('#header-cart').css('display', '');
         jQuery('#header-compare').css('display', '');

     }

     , moveElementsToRegularPosition: function () {
         SmartHeader.activateRegularHeader();

         jQuery('#mini-cart-wrapper-regular').prepend(jQuery('#mini-cart'));
         jQuery('.skip-active').removeClass('skip-active');

         //Enable dropdowns
         jQuery('#mini-cart').addClass('dropdown');
         jQuery('#mini-compare').addClass('dropdown');
     }

}; //end: SmartHeader

SmartHeader.init();

jQuery(function ($) {

    //Skip Links
    var skipContents = $('.skip-content');
    var skipLinks = $('.skip-link');

    skipLinks.on('click', function (e) {
        e.preventDefault();

        var self = $(this);
        var target = self.attr('href');

        //Get target element
        var elem = $(target);

        //Check if stub is open
        var isSkipContentOpen = elem.hasClass('skip-active') ? 1 : 0;

        //Hide all stubs
        skipLinks.removeClass('skip-active');
        skipContents.removeClass('skip-active');

        //Toggle stubs
        if (isSkipContentOpen) {
            self.removeClass('skip-active');
        } else {
            self.addClass('skip-active');
            elem.addClass('skip-active');
        }
    });

}); //end: on document ready





jQuery(function ($) {

    var StickyHeader = {

        stickyThreshold: 960
        , isSticky: false
        , isSuspended: false
        , headerContainer: $('.header-container')
        , stickyContainer: $('.sticky-container')	//.nav-container
        , stickyContainerOffsetTop: 55

        , init: function () {
            StickyHeader.stickyContainerOffsetTop =
                StickyHeader.stickyContainer.offset().top + StickyHeader.stickyContainer.outerHeight();

            StickyHeader.applySticky();
            StickyHeader.hookToScroll();

            if (StickyHeader.stickyThreshold > 0) {
                enquire.register('(max-width: ' + (StickyHeader.stickyThreshold - 1) + 'px)', {
                    match: StickyHeader.suspendSticky,
                    unmatch: StickyHeader.unsuspendSticky
                });
            }
        }

        , applySticky: function () {
            if (StickyHeader.isSuspended) return;

            var viewportOffsetTop = $(window).scrollTop();
            if (viewportOffsetTop > StickyHeader.stickyContainerOffsetTop) {
                if (!StickyHeader.isSticky) {
                    StickyHeader.activateSticky();
                }
            }
            else {
                if (StickyHeader.isSticky) {
                    StickyHeader.deactivateSticky();
                }
            }
        }

        , activateSticky: function () {
            var height = StickyHeader.stickyContainer.outerHeight();
            StickyHeader.headerContainer.css('padding-bottom', height); //Fill in the space of the removed container
            //$('.page').css('padding-top', height); //Fill in the space of the removed container

            StickyHeader.headerContainer.addClass('sticky-header');
            StickyHeader.stickyContainer.css('margin-top', '-' + height + 'px').animate({ 'margin-top': '0' }, 200, 'easeOutCubic');
            //StickyHeader.stickyContainer.css('opacity', '0').animate({'opacity': '1'}, 300, 'easeOutCubic');
            StickyHeader.isSticky = true;
        }

        , deactivateSticky: function () {
            StickyHeader.headerContainer.css('padding-bottom', '');
            //$('.page').css('padding-top', '');

            StickyHeader.headerContainer.removeClass('sticky-header');
            StickyHeader.isSticky = false;
        }

        , suspendSticky: function () {
            StickyHeader.isSuspended = true;
            StickyHeader.deactivateSticky();
        }

        , unsuspendSticky: function () {
            StickyHeader.isSuspended = false;
            StickyHeader.applySticky();
        }

        , hookToScroll: function () {
            $(window).on("scroll", StickyHeader.applySticky);
        }

        , hookToScrollDeferred: function () {
            var windowScrollTimeout;
            $(window).on("scroll", function () {
                clearTimeout(windowScrollTimeout);
                windowScrollTimeout = setTimeout(function () {
                    StickyHeader.applySticky();
                }, 50);
            });
        }

    }; //end: StickyHeader

    StickyHeader.init();

}); //end: on document ready
/////////////////////////////////////////////

                   var gridItemsEqualHeightApplied = false;
                    function setGridItemsEqualHeight($) {
                        var $list = $('.category-products-grid');
                        var $listItems = $list.children();

                        var centered = $list.hasClass('centered');
                        var gridItemMaxHeight = 0;
                        $listItems.each(function () {

                            $(this).css("height", "auto"); var $object = $(this).find('.actions');

                            if (centered) {
                                var objectWidth = $object.width();
                                var availableWidth = $(this).width();
                                var space = availableWidth - objectWidth;
                                var leftOffset = space / 2;
                                $object.css("padding-left", leftOffset + "px");
                            }

                            var bottomOffset = parseInt($(this).css("padding-top"));
                            if (centered) bottomOffset += 10;
                            $object.css("bottom", bottomOffset + "px");

                            if ($object.is(":visible")) {
                                var objectHeight = $object.height();
                                $(this).css("padding-bottom", (objectHeight + bottomOffset) + "px");
                            }


                            gridItemMaxHeight = Math.max(gridItemMaxHeight, $(this).height());
                        });

                        //Apply max height
                        $listItems.css("height", gridItemMaxHeight + "px");
                        gridItemsEqualHeightApplied = true;

                    }



                    jQuery(function ($) {

                        $('.collapsible').each(function (index) {
                            $(this).prepend('<span class="opener"></span>');
                            if ($(this).hasClass('active')) {
                                $(this).children('.block-content').css('display', 'block');
                            }
                            else {
                                $(this).children('.block-content').css('display', 'none');
                            }
                        });
                        $('.collapsible .opener').click(function () {

                            var parent = $(this).parent();
                            if (parent.hasClass('active')) {
                                $(this).siblings('.block-content').stop(true).slideUp(300, "easeOutCubic");
                                parent.removeClass('active');
                            }
                            else {
                                $(this).siblings('.block-content').stop(true).slideDown(300, "easeOutCubic");
                                parent.addClass('active');
                            }

                        });


                        var ddOpenTimeout;
                        var dMenuPosTimeout;
                        var DD_DELAY_IN = 200;
                        var DD_DELAY_OUT = 0;
                        var DD_ANIMATION_IN = 0;
                        var DD_ANIMATION_OUT = 0;

                        $('.clickable-dropdown > .dropdown-heading').click(function () {
                            $(this).parent().addClass('open');
                            $(this).parent().trigger('mouseenter');
                        });

                        //$('.dropdown-heading').on('click', function(e) {
                        $(document).on('click', '.dropdown-heading', function (e) {
                            e.preventDefault();
                        });

                        $(document).on('mouseenter', '.dropdown', function () {

                            var ddToggle = $(this).children('.dropdown-heading');
                            var ddMenu = $(this).children('.dropdown-content');
                            var ddWrapper = ddMenu.parent();
                            ddMenu.css("left", "");
                            ddMenu.css("right", "");

                            if ($(this).hasClass('clickable-dropdown')) {
                                if ($(this).hasClass('open')) {
                                    $(this).children('.dropdown-content').stop(true, true).delay(DD_DELAY_IN).fadeIn(DD_ANIMATION_IN, "easeOutCubic");
                                }
                            }
                            else {
                                clearTimeout(ddOpenTimeout);
                                ddOpenTimeout = setTimeout(function () {

                                    ddWrapper.addClass('open');

                                }, DD_DELAY_IN);

                                //$(this).addClass('open');
                                $(this).children('.dropdown-content').stop(true, true).delay(DD_DELAY_IN).fadeIn(DD_ANIMATION_IN, "easeOutCubic");
                            }

                            clearTimeout(dMenuPosTimeout);
                            dMenuPosTimeout = setTimeout(function () {

                                if (ddMenu.offset().left < 0) {
                                    var space = ddWrapper.offset().left; ddMenu.css("left", (-1) * space);
                                    ddMenu.css("right", "auto");
                                }

                            }, DD_DELAY_IN);

                        }).on('mouseleave', '.dropdown', function () {

                            var ddMenu = $(this).children('.dropdown-content');
                            clearTimeout(ddOpenTimeout); ddMenu.stop(true, true).delay(DD_DELAY_OUT).fadeOut(DD_ANIMATION_OUT, "easeInCubic");
                            if (ddMenu.is(":hidden")) {
                                ddMenu.hide();
                            }
                            $(this).removeClass('open');
                        });



                        $(".main").addClass("show-bg");



                        var windowScroll_t;
                        $(window).scroll(function () {

                            clearTimeout(windowScroll_t);
                            windowScroll_t = setTimeout(function () {

                                if ($(this).scrollTop() > 100) {
                                    $('#scroll-to-top').fadeIn();
                                }
                                else {
                                    $('#scroll-to-top').fadeOut();
                                }

                            }, 500);

                        });

                        $('#scroll-to-top').click(function () {
                            $("html, body").animate({ scrollTop: 0 }, 600, "easeOutCubic");
                            return false;
                        });




                        var startHeight;
                        var bpad;
                        $('.category-products-grid').on('mouseenter', '.item', function () {

                            if ($(window).width() >= 320) {

                                if (gridItemsEqualHeightApplied === false) {
                                    return false;
                                }

                                startHeight = $(this).height();
                                $(this).css("height", "auto"); //Release height
                                $(this).find(".display-onhover").fadeIn(400, "easeOutCubic"); //Show elements visible on hover
                                var h2 = $(this).height();

                                ////////////////////////////////////////////////////////////////
                                var addtocartHeight = 0;
                                var addtolinksHeight = 0;


                                var addtolinksEl = $(this).find('.add-to-links');
                                if (addtolinksEl.hasClass("addto-onimage") == false)
                                    addtolinksHeight = addtolinksEl.innerHeight(); //.height();

                                var h3 = h2 + addtocartHeight + addtolinksHeight;
                                var diff = 0;
                                if (h3 < startHeight) {
                                    $(this).height(startHeight);
                                }
                                else {
                                    $(this).height(h3); diff = h3 - startHeight;
                                }
                                ////////////////////////////////////////////////////////////////

                                $(this).css("margin-bottom", "-" + diff + "px");
                            }
                        }).on('mouseleave', '.item', function () {

                            if ($(window).width() >= 320) {

                                //Clean up
                                $(this).find(".display-onhover").stop(true).hide();
                                $(this).css("margin-bottom", "");

                                $(this).height(startHeight);

                            }
                        });




                        $('.products-grid, .products-list').on('mouseenter', '.item', function () {
                            $(this).find(".alt-img").fadeIn(400, "easeOutCubic");
                        }).on('mouseleave', '.item', function () {
                            $(this).find(".alt-img").stop(true).fadeOut(400, "easeOutCubic");
                        });



                        $('.fade-on-hover').on('mouseenter', function () {
                            $(this).animate({ opacity: 0.75 }, 300, 'easeInOutCubic');
                        }).on('mouseleave', function () {
                            $(this).stop(true).animate({ opacity: 1 }, 300, 'easeInOutCubic');
                        });



                        var dResize = {

                            winWidth: 0
                    , winHeight: 0
                    , windowResizeTimeout: null

                    , init: function () {
                        dResize.winWidth = $(window).width();
                        dResize.winHeight = $(window).height();
                        dResize.windowResizeTimeout;

                        $(window).on('resize', function (e) {
                            clearTimeout(dResize.windowResizeTimeout);
                            dResize.windowResizeTimeout = setTimeout(function () {
                                dResize.onEventResize(e);
                            }, 50);
                        });
                    }

                    , onEventResize: function (e) {
                        //Prevent from executing the code in IE when the window wasn't actually resized
                        var winNewWidth = $(window).width();
                        var winNewHeight = $(window).height();

                        //Code in this condition will be executed only if window was actually resized
                        if (dResize.winWidth != winNewWidth || dResize.winHeight != winNewHeight) {
                            //Trigger deferred resize event
                            $(document).trigger("themeResize", e);

                            //Additional code executed on deferred resize
                            dResize.onEventDeferredResize();
                        }

                        //Update window size variables
                        dResize.winWidth = winNewWidth;
                        dResize.winHeight = winNewHeight;
                    }

                    , onEventDeferredResize: function () //Additional code, execute after window was actually resized
                    {
                        //Products grid: equal height of items
                        setGridItemsEqualHeight($);

                    }

                        }; //end: dResize

                        dResize.init();



                    }); //end: on document ready



                    jQuery(window).load(function () {

                        setGridItemsEqualHeight(jQuery);

                    }); //end: jQuery(window).load(){...}