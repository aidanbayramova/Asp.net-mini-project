"use strict"
document.addEventListener('click', async function (e) {
    if (e.target.classList.contains('add-basket')) {
        let button = e.target;
        let productId = button.getAttribute('data-id');
        let parent = button.closest('.product, .single-product-content');
        let qtyInput = parent ? parent.querySelector('[name="qtybox"]') : null;
        let quantity = qtyInput ? parseInt(qtyInput.value) : 1;

        if (quantity < 1) {
            alert('Please select a valid quantity');
            return;
        }
        let response = await fetch(`/Home/AddProductToBasket?id=${productId}&quantity=${quantity}`, {

            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        let result = await response.json();
        document.querySelector('.basket-count').innerText = result;
    }
});
$(document).on('click', '.cart-pro-remove', function (e) {
    e.preventDefault();
    var button = $(this);
    var productId = button.data('id');
    $.ajax({
        url: '/basket/delete',
        type: 'POST',
        data: { id: productId },
        success: function (res) {
            button.closest('tr').remove();
            $('.total-price').text(res.total.toFixed(2) + "$");
            $('.basket-count').text(res.count);
            if (res.count === 0) {
                $('.cart-section').html('<div class="text-center">There are no products in the basket.</div>');
            }
        }
    });
});