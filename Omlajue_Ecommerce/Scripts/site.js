/* =============================================
   Omlajue Ecommerce - Site JavaScript
   ============================================= */

$(document).ready(function() {
    // Initialize tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Smooth scrolling for anchor links
    $('a[href^="#"]').on('click', function(event) {
        var target = $(this.getAttribute('href'));
        if(target.length) {
            event.preventDefault();
            $('html, body').stop().animate({
                scrollTop: target.offset().top - 80
            }, 1000);
        }
    });

    // Auto-hide alerts after 5 seconds
    setTimeout(function() {
        $('.alert').fadeOut('slow');
    }, 5000);

    // Form validation
    (function() {
        'use strict';
        var forms = document.querySelectorAll('.needs-validation');
        Array.prototype.slice.call(forms).forEach(function(form) {
            form.addEventListener('submit', function(event) {
                if (!form.checkValidity()) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    })();

    // Quantity input controls
    $(document).on('click', '.qty-minus', function() {
        var input = $(this).siblings('.qty-input');
        var value = parseInt(input.val());
        if (value > 1) {
            input.val(value - 1);
            input.trigger('change');
        }
    });

    $(document).on('click', '.qty-plus', function() {
        var input = $(this).siblings('.qty-input');
        var value = parseInt(input.val());
        var max = parseInt(input.attr('max'));
        if (value < max) {
            input.val(value + 1);
            input.trigger('change');
        }
    });

    // Price range slider
    if ($('#priceRange').length) {
        var priceRange = document.getElementById('priceRange');
        noUiSlider.create(priceRange, {
            start: [0, 1000000],
            connect: true,
            range: {
                'min': 0,
                'max': 1000000
            },
            format: {
                to: function(value) {
                    return Math.round(value);
                },
                from: function(value) {
                    return Number(value);
                }
            }
        });

        priceRange.noUiSlider.on('update', function(values, handle) {
            $('#minPrice').val(values[0]);
            $('#maxPrice').val(values[1]);
        });
    }

    // Image gallery
    if ($('.product-gallery').length) {
        $('.product-gallery img').on('click', function() {
            var src = $(this).attr('src');
            $('.main-product-image').attr('src', src);
        });
    }

    // Countdown timer for deals
    if ($('.countdown').length) {
        $('.countdown').each(function() {
            var endDate = new Date($(this).data('end-date')).getTime();
            var element = $(this);
            
            var timer = setInterval(function() {
                var now = new Date().getTime();
                var distance = endDate - now;
                
                if (distance < 0) {
                    clearInterval(timer);
                    element.html('Expired');
                    return;
                }
                
                var days = Math.floor(distance / (1000 * 60 * 60 * 24));
                var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                var seconds = Math.floor((distance % (1000 * 60)) / 1000);
                
                element.html(days + 'd ' + hours + 'h ' + minutes + 'm ' + seconds + 's');
            }, 1000);
        });
    }

    // Loading spinner for AJAX requests
    $(document).ajaxStart(function() {
        $('#loadingSpinner').show();
    }).ajaxStop(function() {
        $('#loadingSpinner').hide();
    });

    // Prevent double submission
    $('form').on('submit', function() {
        $(this).find('button[type="submit"]').prop('disabled', true);
    });

    // Confirm delete actions
    $('.btn-delete').on('click', function(e) {
        if (!confirm('Are you sure you want to delete this item?')) {
            e.preventDefault();
        }
    });

    // Format currency
    window.formatCurrency = function(amount) {
        return '?' + parseFloat(amount).toLocaleString('en-PH', {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        });
    };

    // Add to cart with AJAX
    window.addToCart = function(productId, quantity) {
        quantity = quantity || 1;
        
        $.post('/Cart/Add', { 
            productId: productId, 
            quantity: quantity 
        }, function(data) {
            if (data.success) {
                showNotification('success', data.message);
                if (typeof updateCartCount === 'function') {
                    updateCartCount();
                }
            } else {
                showNotification('error', data.message);
            }
        }).fail(function() {
            showNotification('error', 'Please login to add items to cart');
            setTimeout(function() {
                window.location.href = '/Account/Login';
            }, 2000);
        });
    };

    // Add to wishlist with AJAX
    window.addToWishlist = function(productId) {
        $.post('/Wishlist/Add', { productId: productId }, function(data) {
            if (data.success) {
                showNotification('success', data.message);
                if (typeof updateWishlistCount === 'function') {
                    updateWishlistCount();
                }
            } else {
                showNotification('error', data.message);
            }
        }).fail(function() {
            showNotification('error', 'Please login to add items to wishlist');
            setTimeout(function() {
                window.location.href = '/Account/Login';
            }, 2000);
        });
    };

    // Show notification
    window.showNotification = function(type, message) {
        var alertClass = type === 'success' ? 'alert-success' : 'alert-danger';
        var iconClass = type === 'success' ? 'fa-check-circle' : 'fa-exclamation-circle';
        
        var alert = $('<div>', {
            class: 'alert ' + alertClass + ' alert-dismissible fade show',
            role: 'alert',
            html: '<i class="fas ' + iconClass + ' me-2"></i>' + message +
                  '<button type="button" class="btn-close" data-bs-dismiss="alert"></button>'
        });
        
        $('body').prepend(alert);
        
        setTimeout(function() {
            alert.fadeOut('slow', function() {
                $(this).remove();
            });
        }, 5000);
    };

    // Back to top button
    var backToTop = $('<button>', {
        id: 'backToTop',
        class: 'btn btn-primary',
        html: '<i class="fas fa-arrow-up"></i>',
        css: {
            position: 'fixed',
            bottom: '30px',
            right: '30px',
            display: 'none',
            'z-index': '999',
            'border-radius': '50%',
            width: '50px',
            height: '50px'
        }
    });
    
    $('body').append(backToTop);
    
    $(window).scroll(function() {
        if ($(this).scrollTop() > 300) {
            $('#backToTop').fadeIn();
        } else {
            $('#backToTop').fadeOut();
        }
    });
    
    $('#backToTop').click(function() {
        $('html, body').animate({ scrollTop: 0 }, 800);
        return false;
    });
});

// Copy to clipboard
function copyToClipboard(text) {
    var temp = $('<input>');
    $('body').append(temp);
    temp.val(text).select();
    document.execCommand('copy');
    temp.remove();
    showNotification('success', 'Copied to clipboard!');
}

// Format date
function formatDate(date) {
    var d = new Date(date);
    var month = '' + (d.getMonth() + 1);
    var day = '' + d.getDate();
    var year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}

// Debounce function
function debounce(func, wait, immediate) {
    var timeout;
    return function() {
        var context = this, args = arguments;
        var later = function() {
            timeout = null;
            if (!immediate) func.apply(context, args);
        };
        var callNow = immediate && !timeout;
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
        if (callNow) func.apply(context, args);
    };
}
