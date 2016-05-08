var $ = jQuery.noConflict();

$(document).ready(function($) {
	"use strict";
	(function() {
		var docElem = document.documentElement,
			didScroll = false,
			changeHeaderOn = 200;
			document.querySelector( 'header' );
		function init() {
			window.addEventListener( 'scroll', function() {
				if( !didScroll ) {
					didScroll = true;
					setTimeout( scrollPage,250 );
				}
			}, false );
		}
		function scrollPage() {
			var sy = scrollY();
			if ( sy >= changeHeaderOn ) {
				$( 'header' ).addClass('active');
			}
			else {
				$( 'header' ).removeClass('active');
			}
			didScroll = false;
		}
		function scrollY() {
			return window.pageYOffset || docElem.scrollTop;
		}
		init();
	})();
});