jQuery(document).ready(function($) {
	
	var navbar 		  		 = $('.navbar-fixed-top');
	var before_navbar 		 = $('.before-navbar');
	var before_navbar_height = before_navbar.height()
	
  // Toggle sticky navbar on scroll
  navbar.css("top", before_navbar.height());
	$(window).scroll(function() {
		if ( $(document).scrollTop() >= before_navbar_height ) {
			navbar.animate({
				top: 0
			}, 50)
		} 

		if( $(document).scrollTop() < before_navbar_height ) {
			navbar.animate({
				top: before_navbar.height()
			}, 50)
		}
	})

  // Add animation to #anchors
  $.localScroll();

  $('#toplink').on('click', function(e) {
    e.preventDefault();
    $.scrollTo('0px', 300);
  })


});


// Custom sorting plugin
(function($) {
  $.fn.sorted = function(customOptions) {
    var options = {
      reversed: false,
      by: function(a) { return a.text(); }
    };
    $.extend(options, customOptions);
    $data = $(this);
    arr = $data.get();
    arr.sort(function(a, b) {
      var valA = options.by($(a));
      var valB = options.by($(b));
      if (options.reversed) {
        return (valA < valB) ? 1 : (valA > valB) ? -1 : 0;				
      } else {		
        return (valA < valB) ? -1 : (valA > valB) ? 1 : 0;	
      }
    });
    return $(arr);
  };
})(jQuery);

// DOMContentLoaded
$(function() {

  // bind radiobuttons in the form
  var $filterType = $('#portfolio-nav');

  // get the first collection
  var $applications = $('#applications');

  // clone applications to get a second collection
  var $data = $applications.clone();


  // attempt to call Quicksand on every form change
  $filterType.find('li').on('click', function(e) {
    e.preventDefault();

    // all/type filtering
    if ( $(this).data('type') == 'all') {
      var $filteredData = $data.find('li');
    } else {
      var $filteredData = $data.find('li[data-type=' + $(this).data('type') + ']');
    }

    // finally, call quicksand
    $applications.quicksand($filteredData, {
      duration: 800,
      easing: 'easeInOutQuad'
    });

  });

});
