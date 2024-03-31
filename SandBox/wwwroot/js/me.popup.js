$(function () {
	$(document).on('click', '.me-popup', () => $('.me-popup').fadeOut());
	$(document).on('click', '.me-popup > *', (e) => e.stopPropagation());
	$(document).on('click', '.me-popup .me-close-icon', function () { $(this).closest('.me-popup').fadeOut(); });	
})