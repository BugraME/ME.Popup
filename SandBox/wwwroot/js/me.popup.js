document.addEventListener("DOMContentLoaded", () => {
	const stopPropagation = e => e.stopPropagation();

	document.addEventListener('click', e => {
		const target = e.target;

		if (target.classList.contains('me-popup')) {
			document.querySelectorAll('.me-popup').forEach(popup => meFadeOut(popup));
			return;
		}

		if (target.classList.contains('me-close-icon') || target.classList.contains('me-cancel')) {
			const popup = target.closest('.me-popup');
			if (popup) meFadeOut(popup);
		}

		if (target.closest('.me-popup > *')) {
			stopPropagation(e);
			return;
		}
	});

	document.addEventListener('change', e => {
		const fileInput = e.target;
		if (fileInput.matches('.me-popup input[type=file]')) {
			const file = fileInput.files[0];
			const imageReviewer = fileInput.parentElement.querySelector('.me-image-reviewer');
			if (file) imageReviewer.src = URL.createObjectURL(file);
			else imageReviewer.removeAttribute('src');
		}
	});
});
function fadeElementIn(triggerSelector, targetSelector) {
	document.addEventListener('click', function (event) {
		if (event.target.matches(triggerSelector)) {
			const element = document.querySelector(targetSelector);
			meFadeIn(element);
		}
	});
}
function meFadeOut(element, duration = 400) {
	element.style.transition = `opacity ${duration}ms`;
	element.style.opacity = 0;

	setTimeout(function () {
		element.style.display = 'none';
	}, duration);
}
function meFadeIn(element, duration = 400, display = 'block') {
	element.style.display = display;
	element.style.opacity = 0;
	element.style.transition = `opacity ${duration}ms`;
	element.getBoundingClientRect();
	element.style.opacity = 1;
	setTimeout(() => {
		element.style.opacity = '';
		element.style.transition = '';
	}, duration);
}